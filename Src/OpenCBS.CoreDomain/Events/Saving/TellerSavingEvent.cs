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

using System;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.CoreDomain.Contracts.Savings;
using OpenCBS.CoreDomain.Events.Loan;
using OpenCBS.CoreDomain.Products;
using OpenCBS.Shared;
using OpenCBS.Enums;

namespace OpenCBS.CoreDomain.Events.Saving
{
    [Serializable]
    public class TellerSavingPositiveEvent : TellerSavingEvent
    {
        public override sealed OCurrency GetAmountForBalance()
        {
            return Amount;
        }
    }

    public class TellerSavingNegativeEvent : TellerSavingEvent
    {
        public override sealed OCurrency GetAmountForBalance()
        {
            if (!Amount.HasValue) return 0m;
            return (decimal)-1 * Amount;
        }

        public override sealed OCurrency GetFeeForBalance()
        {
            if (!Fee.HasValue) return 0m;
            return (decimal)-1 * Fee;
        }
    }

    public abstract class TellerSavingEvent : SavingEvent
    {
        public string Account { get; set; }
        public string UserName { get; set; }
        public string TellerName { get; set; }
        public string BranchName { get; set; }
    }
}
