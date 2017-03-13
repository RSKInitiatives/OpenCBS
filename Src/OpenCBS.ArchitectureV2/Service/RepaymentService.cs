using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using OpenCBS.ArchitectureV2.Interface.Service;
using OpenCBS.CoreDomain.Contracts.Loans;
using OpenCBS.CoreDomain.Contracts.Loans.Installments;
using OpenCBS.Enums;
using OpenCBS.Services;

namespace OpenCBS.ArchitectureV2.Service
{
    public class RepaymentService : IRepaymentService
    {
        public RepaymentSettings Settings { get; set; }

        public RepaymentService()
        {
            Settings = new RepaymentSettings();
        }

        public Loan Repay()
        {
            var newSettings = (RepaymentSettings) Settings.Clone();
            var script = RunScript();
            script.Main(newSettings);
            Settings = newSettings;
            return Settings.Loan;
        }

        public decimal GetRepaymentAmount(DateTime date)
        {
            var newSettings = (RepaymentSettings) Settings.Clone();
            newSettings.Date = date;
            newSettings.DateChanged = true;
            var script = RunScript();
            script.Main(newSettings);
            return
                Math.Round(newSettings.Penalty + newSettings.Interest + newSettings.Principal + newSettings.BounceFee, 2);
        }

        private static dynamic RunScript()
        {
            var options = new Dictionary<string, object>();
#if DEBUG
            options["Debug"] = true;
#endif
            ScriptEngine engine = Python.CreateEngine(options);
            var file = Directory
                .EnumerateFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Scripts\\Repayment\\"), "*.py",
                                SearchOption.TopDirectoryOnly).FirstOrDefault();
            var assemby = typeof (ServicesProvider).Assembly;
            engine.Runtime.LoadAssembly(assemby);
            assemby = typeof (Installment).Assembly;
            engine.Runtime.LoadAssembly(assemby);
            assemby = typeof (OPaymentType).Assembly;
            engine.Runtime.LoadAssembly(assemby);

            return engine.ExecuteFile(file);
        }
    }
}
