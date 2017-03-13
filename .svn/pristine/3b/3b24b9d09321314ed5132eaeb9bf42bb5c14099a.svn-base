// Octopus MFS is an integrated suite for managing a Micro Finance Institution: 
// clients, contracts, accounting, reporting and risk
// Copyright © 2006,2007 OCTO Technology & OXUS Development Network
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License along
// with this program; if not, write to the Free Software Foundation, Inc.,
// 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//
// Website: http://www.opencbs.com
// Contact: contact@opencbs.com

using OpenCBS.CoreDomain.Contracts.Savings.CalculateInterests.Interfaces;
using OpenCBS.Shared;
using System;
using OpenCBS.CoreDomain.Events.Saving;
using System.Collections.Generic;
using OpenCBS.Enums;

namespace OpenCBS.CoreDomain.Contracts.Savings.CalculateInterests.Accrual.MinimalAmount
{
    public class Monthly : ICalculateInterests
    {
        private SavingBookContract _saving;
        private User _user;
        private bool _checkUsage = false;

        public Monthly(SavingBookContract pSaving, User pUser)
        {
            _saving = pSaving;
            _user = pUser;
        }

        public Monthly(SavingBookContract pSaving, User pUser, bool pCheckUsage)
        {
            _saving = pSaving;
            _user = pUser;
            _checkUsage = pCheckUsage;
        }

        public List<SavingInterestsAccrualEvent> CalculateInterest(DateTime pClosureDate)
        {
            List<SavingInterestsAccrualEvent> listInterestsAccrualEvent = new List<SavingInterestsAccrualEvent>();

            DateTime lastClosureDate = _saving.GetLastAccrualDate();

            while (DateCalculationStrategy.DateCalculationMonthly(lastClosureDate, pClosureDate))
            {
                DateTime accrualDate = new DateTime(lastClosureDate.AddMonths(1).Year, lastClosureDate.AddMonths(1).Month, 01,
                    DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

                int count = _saving.Events.FindAll(se =>
                                    (se.Code == OSavingEvents.Withdraw ||
                                    se.Code == OSavingEvents.DebitTransfer/* ||
                                    se.Code == OSavingEvents.SpecialOperationDebit*/) &&
                                    (se.Date > lastClosureDate && se.Date < accrualDate)).Count;

                if (count < 3)
                    if (!_checkUsage)
                        listInterestsAccrualEvent.Add(GetInterests(lastClosureDate, accrualDate));
                    else
                    {
                        //slycode
                        SavingInitialDepositEvent firstDeposit = null;

                        foreach (var evnt in _saving.Events)
                        {
                            if (evnt is SavingInitialDepositEvent)
                            {
                                firstDeposit = (SavingInitialDepositEvent)evnt;

                                if (DateCalculationStrategy.DateCalculationMonthly(firstDeposit.Date, DateTime.Now, -3))
                                {
                                    listInterestsAccrualEvent.Add(GetInterests(lastClosureDate, accrualDate));
                                }

                                break;
                            }
                        }
                    }
                lastClosureDate = lastClosureDate.AddMonths(1);
            }

            return listInterestsAccrualEvent;
        }

        private SavingInterestsAccrualEvent GetInterests(DateTime pLastClosureDate, DateTime pClosureDate)
        {
            double interestRate = _saving.InterestRate;

            OCurrency minimalAmount = _saving.GetBalanceMin(pLastClosureDate);
            DateTime currentDate = pLastClosureDate.AddDays(1);

            while (currentDate < pClosureDate)
            {
                OCurrency amountAtCurrentDate = _saving.GetBalanceMin(currentDate);

                if (minimalAmount > amountAtCurrentDate)
                    minimalAmount = amountAtCurrentDate;

                currentDate = currentDate.AddDays(1);
            }

            OCurrency interests = interestRate * minimalAmount;

            return new SavingInterestsAccrualEvent
            {
                Amount = interests,
                Date = pClosureDate,
                Fee = 0,
                User = _user,
                Cancelable = true,
                ProductType = _saving.Product.GetType(),
                Description = _saving.Code,
                Branch = _saving.Branch,
                Currency = _saving.Product.Currency,
                ContracId = _saving.Id
            };
        }
    }
}
