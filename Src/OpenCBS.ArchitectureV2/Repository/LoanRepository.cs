using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using OpenCBS.ArchitectureV2.Interface.Repository;
using OpenCBS.ArchitectureV2.Model;
using Dapper;
using OpenCBS.ArchitectureV2.Interface;

namespace OpenCBS.ArchitectureV2.Repository
{
    public class LoanRepository : ILoanRepository
    {
        private readonly IConnectionProvider _connectionProvider;

        public LoanRepository(IConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        public List<Alert> GetAlerts(DateTime date, int userId)
        {
            const string query = @"
                select
                    id,
                    kind,
                    status,
                    date,
                    late_days LateDays,
                    amount,
                    contract_code ContractCode,
                    client_name ClientName,
                    loan_officer LoanOfficer,
                    city,
                    address,
                    phone,
                    branch_name BranchName
                from
                    dbo.Alerts(@date, @userId)
            ";
            using (var connection = _connectionProvider.GetConnection())
            {
                return connection.Query<Alert>(query, new { date, userId }).ToList();
            }
        }

        public List<Loan> GetVillageBankLoans(int villageBankId)
        {
            const string query = @"
                select
	                c.id
	                , p.first_name FirstName
	                , p.last_name LastName
	                , c.contract_code ContractCode
                from
	                dbo.Persons p
                left join
	                dbo.Projects j on j.tiers_id = p.id
                left join
	                dbo.Contracts c on c.project_id = j.id
                left join
	                dbo.VillagesPersons vp on vp.person_id = p.id
                where
	                vp.village_id = @villageBankId
	                and c.status = 5
                order by
                    p.id

                select
	                number
                    , contract_id ContractId
                    , start_date StartDate
	                , expected_date ExpectedDate
                    , paid_date PaymentDate
	                , capital_repayment Principal
	                , paid_capital PaidPrincipal
	                , interest_repayment Interest
	                , paid_interest PaidInterest
                    , olb
                from
	                dbo.Installments
                where
	                contract_id in
	                (
		                select
			                c.id
		                from
			                dbo.Persons p
		                left join
			                dbo.Projects j on j.tiers_id = p.id
		                left join
			                dbo.Contracts c on c.project_id = j.id
		                left join
			                dbo.VillagesPersons vp on vp.person_id = p.id
		                where
			                vp.village_id = @villageBankId
			                and c.status = 5
	                )
            ";
            using (var connection = _connectionProvider.GetConnection())
            using (var multi = connection.QueryMultiple(query, new { villageBankId }))
            {
                var loans = multi.Read<Loan>().ToList();
                var installments = multi.Read<Installment>().ToList();
                foreach (var loan in loans)
                {
                    loan.Schedule = installments
                        .Where(x => x.ContractId == loan.Id)
                        .OrderBy(x => x.Number)
                        .ToList();
                }
                return loans;
            }
        }

        public RepaymentEvent SaveRepaymentEvent(RepaymentEvent repaymentEvent, IDbTransaction tx)
        {
            var query = @"
                insert into dbo.ContractEvents (event_type, contract_id, event_date, user_id, is_deleted, entry_date)
                values (@Code, @LoanId, @EventDate, @UserId, 0, getdate())
                select cast(scope_identity() as int)
            ";
            repaymentEvent.Id = tx.Connection.Query<int>(query, repaymentEvent, tx).First();

            query = @"
                insert into dbo.RepaymentEvents (id, past_due_days, principal, interests, installment_number, commissions, penalties, payment_method_id, bounce_fee)
                values (@Id, @LateDays, @Principal, @Interest, @InstallmentNumber, 0, 0, 1, 0)
            ";
            tx.Connection.Execute(query, repaymentEvent, tx);

            // Create a funding line event
            query = @"
                declare @code nvarchar(max)
                declare @funding_line_id int
                select @code = contract_code from dbo.Contracts where id = @LoanId
                select @funding_line_id = fundingLine_id from dbo.Credit where id = @LoanId

                declare @funding_line_code nvarchar(max)
                set @funding_line_code = 'RE_' + @code + '_INS_' + cast(@InstallmentNumber as nvarchar(max))

                insert into dbo.FundingLineEvents
                    (code, amount, direction, fundingline_id, deleted, creation_date, type, user_id, contract_event_id)
                values
                    (@funding_line_code, @Principal, 1, @funding_line_id, 0, @EventDate, 2, @UserId, @Id)
            ";
            tx.Connection.Execute(query, new
            {
                repaymentEvent.LoanId,
                repaymentEvent.InstallmentNumber,
                repaymentEvent.Principal,
                repaymentEvent.EventDate,
                repaymentEvent.UserId,
                repaymentEvent.Id
            }, tx);

            return repaymentEvent;
        }

        public CloseEvent SaveCloseEvent(CloseEvent closeEvent, IDbTransaction tx)
        {
            const string query = @"
                insert into  dbo.ContractEvents (event_type, contract_id, event_date, user_id, is_deleted, entry_date)
                values (@Code, @LoanId, @EventDate, @UserId, 0, getdate())
                select cast(scope_identity() as int)
            ";
            closeEvent.Id = tx.Connection.Query<int>(query, closeEvent, tx).First();
            return closeEvent;
        }

        public void ArchiveSchedule(int loanId, int eventId, List<Installment> oldSchedule, List<Installment> newSchedule, IDbTransaction tx)
        {
            // Archive previous schedule
            var query = @"
                insert into dbo.InstallmentHistory
                (
                    contract_id,
                    event_id, 
                    number,
                    start_date,
                    expected_date, 
                    capital_repayment,
                    interest_repayment,
                    paid_capital,
                    paid_interest,
                    paid_fees,
                    fees_unpaid,
                    paid_date,
                    pending,
                    olb,
                    commission,
                    paid_commission
                )
                values
                (
                    @loanId,
                    @eventId,
                    @Number,
                    @StartDate,
                    @ExpectedDate,
                    @Principal,
                    @Interest,
                    @PaidPrincipal,
                    @PaidInterest,
                    0,
                    0,
                    @PaymentDate,
                    0,
                    @Olb,
                    0,
                    0
                )
            ";
            foreach (var installment in oldSchedule)
            {
                tx.Connection.Execute(query, new
                {
                    loanId,
                    eventId,
                    installment.Number,
                    installment.StartDate,
                    installment.ExpectedDate,
                    installment.Principal,
                    installment.Interest,
                    installment.PaidPrincipal,
                    installment.PaidInterest,
                    installment.PaymentDate,
                    installment.Olb
                },
                tx);
            }

            // Save new schedule
            query = @"
                update dbo.Installments
                set
                    expected_date = @ExpectedDate,
                    interest_repayment = @Interest,
                    capital_repayment = @Principal,
                    paid_interest = @PaidInterest,
                    paid_capital = @PaidPrincipal,
                    paid_date = @PaymentDate,
                    paid_fees = 0,
                    olb = @Olb
                where
                    contract_id = @loanId
                    and number = @Number

            ";
            foreach (var installment in newSchedule)
            {
                tx.Connection.Execute(query, new
                {
                    installment.ExpectedDate,
                    installment.Interest,
                    installment.Principal,
                    installment.PaidInterest,
                    installment.PaidPrincipal,
                    installment.PaymentDate,
                    installment.Olb,
                    loanId,
                    installment.Number
                }, tx);
            }
        }

        public void SetLoanStatusToClosed(int loanId, IDbTransaction tx)
        {
            const string query = @"update dbo.Contracts set closed = 1, status = 6 where id = @loanId";
            tx.Connection.Execute(query, new { loanId }, tx);
        }

        public void UpdateBorrowerStatus(int loanId, IDbTransaction tx)
        {
            const string query = @"
                declare @number_of_active int
                declare @client_id int

                select
	                @client_id = j.tiers_id
                from
	                dbo.Projects j
                inner join
	                dbo.Contracts c on c.project_id = j.id
                where
	                c.id = @loanId

                select 
	                @number_of_active = count(*)
                from
	                dbo.Contracts c
                inner join
	                dbo.Projects j on j.id = c.project_id
                where
	                j.tiers_id = @client_id
	                and c.status = 5

                if @number_of_active = 0
                begin
	                update 
		                dbo.Tiers
	                set
		                active = 0,
		                status = 4
	                where
		                id = @client_id
                end     
                ";
            tx.Connection.Execute(query, new { loanId }, tx);
        }
    }
}
