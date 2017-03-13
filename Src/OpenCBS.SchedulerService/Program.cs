using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace OpenCBS.SchedulerService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void MainBackUp()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new Scheduler() 
            };

            if (Environment.UserInteractive)
            {
                var type = typeof(ServiceBase);
                const BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
                var method = type.GetMethod("OnStart", flags);

                foreach (var service in ServicesToRun)
                {
                    method.Invoke(service, new object[] { null });
                }

                Console.WriteLine("Service Started!");
                Console.ReadLine();

                method = type.GetMethod("OnStop", flags);

                foreach (var service in ServicesToRun)
                {
                    method.Invoke(service, null);
                }
                Environment.Exit(0);
            }
            else
            {
                ServiceBase.Run(ServicesToRun);
            }
        }

        public static int Main(string[] args)
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new Scheduler()
            };

            if (Environment.UserInteractive)
            {
                // we only care about the first two characters
                if (args.Length > 0)
                {
                    string arg = args[0].ToLowerInvariant().Substring(0, 2);

                    switch (arg)
                    {
                        case "/i":  // install
                            return InstallService();

                        case "/u":  // uninstall
                            return UninstallService();

                        default:  // unknown option
                            Console.WriteLine("Argument not recognized: {0}", args[0]);
                            Console.WriteLine(string.Empty);
                            DisplayUsage();
                            return 1;
                    }
                }
                else
                {
                    Console.WriteLine("/i to install");
                    Console.WriteLine("/u to uninstall");

                    var command = Console.ReadLine();
                    switch (command)
                    {
                        case "/i":  // install
                            return InstallService();                            
                        case "/u":  // uninstall
                            return UninstallService();                            
                        default:  // unknown option
                            Console.WriteLine("Argument not recognized: {0}", command);
                            break;
                    }
                }                
            }
            else
            {
                // run as a standard service as we weren't started by a user
                ServiceBase.Run(ServicesToRun);
            }

            return 0;
        }

        private static void DisplayUsage()
        {
            
        }

        private static int InstallService()
        {
            var service = new Scheduler();

            try
            {
                // perform specific install steps for our queue service.
                service.InstallService();

                // install the service with the Windows Service Control Manager (SCM)
                ManagedInstallerClass.InstallHelper(new string[] { Assembly.GetExecutingAssembly().Location });
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException.GetType() == typeof(Win32Exception))
                {
                    Win32Exception wex = (Win32Exception)ex.InnerException;
                    Console.WriteLine("Error(0x{0:X}): Service already installed!", wex.ErrorCode);
                    return wex.ErrorCode;
                }
                else
                {
                    Console.WriteLine(ex.ToString());
                    return -1;
                }
            }
            /*finally
            {
                Console.WriteLine("Press any key to continue...");
                Console.ReadLine();
            }*/

            return 0;
        }

        private static int UninstallService()
        {
            var service = new Scheduler();

            try
            {
                // perform specific uninstall steps for our queue service
                service.UninstallService();

                // uninstall the service from the Windows Service Control Manager (SCM)
                ManagedInstallerClass.InstallHelper(new string[] { "/u", Assembly.GetExecutingAssembly().Location });
            }
            catch (Exception ex)
            {
                if (ex.InnerException.GetType() == typeof(Win32Exception))
                {
                    Win32Exception wex = (Win32Exception)ex.InnerException;
                    Console.WriteLine("Error(0x{0:X}): Service not installed!", wex.ErrorCode);
                    return wex.ErrorCode;
                }
                else
                {
                    Console.WriteLine(ex.ToString());
                    return -1;
                }
            }

            return 0;
        }
    }
}
