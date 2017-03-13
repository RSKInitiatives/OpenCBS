using System.Data;
using System.Linq;
using OpenCBS.ArchitectureV2.Interface;
using OpenCBS.ArchitectureV2.Interface.Repository;
using OpenCBS.ArchitectureV2.Model;
using Dapper;

namespace OpenCBS.ArchitectureV2.Repository
{
    public class VillageBankRepository : IVillageBankRepository
    {
        private readonly IConnectionProvider _connectionProvider;

        public VillageBankRepository(IConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        public VillageBank Get(int id)
        {
            const string query = @"
                select id, name from dbo.Villages where id = @id

                select
	                p.id
	                , p.first_name FirstName
	                , p.last_name LastName
	                , p.identification_data Passport
                    , t.loan_cycle LoanCycle
                    , t.active
                from
	                dbo.Persons p
                left join
                    dbo.Tiers t on t.id = p.id
                left join
	                dbo.VillagesPersons vp on vp.person_id = p.id
                where
	                vp.left_date is null
	                and vp.village_id = @id
                order by
                    p.id

                select
	                c.id
	                , p.first_name FirstName
	                , p.last_name LastName
	                , c.contract_code ContractCode
	                , st.status_name Status
	                , pack.name LoanProductName
	                , it.name InstallmentTypeName
	                , cr.nb_of_installment Duration
	                , cr.amount
	                , i.olb
                from
	                dbo.Contracts c
                left join
	                dbo.Credit cr on cr.id = c.id
                left join
	                dbo.Projects j on j.id = c.project_id
                left join
	                dbo.Persons p on p.id = j.tiers_id
                left join
	                dbo.VillagesPersons vp on vp.person_id = p.id
                left join
	                dbo.Statuses st on st.id = c.status
                left join
	                dbo.Packages pack on pack.id = cr.package_id
                left join
	                dbo.InstallmentTypes it on it.id = cr.installment_type
                left join
                (
	                select
		                contract_id
		                , sum(capital_repayment - paid_capital) olb
	                from
		                dbo.Installments
	                group by
		                contract_id
                ) i on i.contract_id = c.id
                where
	                vp.village_id = @id
	                and c.status in (1, 2, 5)
                order by
                    p.id
            ";
            using (var connection = _connectionProvider.GetConnection())
            using (var multi = connection.QueryMultiple(query, new {id}))
            {
                var result = multi.Read<VillageBank>().Single();
                result.Members = multi.Read<VillageBankMember>().ToList();
                result.Loans = multi.Read<VillageBankLoan>().ToList();
                return result;
            }
        }

        public void SyncVillageBankStatus(int villageBankId, IDbTransaction tx)
        {
            const string query = @"
                update dbo.Tiers
                set active = 
	                case 
		                when (select count(*) from dbo.Contracts where status = 5 and nsg_id = @villageBankId) > 0 then 1 
		                else 0 
	                end
                where id = @villageBankId
            ";
            tx.Connection.Execute(query, new { villageBankId }, tx);
        }
    }
}
