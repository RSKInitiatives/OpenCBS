using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace OpenCBS.Shared.Settings
{
    public class ApplicationRegistry
    {
        public string DatabaseName;
        public string DatabaseServerName;


        public static ApplicationRegistry Find(bool dump = false)
        {
            string path1 = TechnicalSettings.GetRegistryPath();
            string[] paths = { path1, "SOFTWARE\\Wow6432Node\\Open Octopus Ltd\\OpenCBS\\15.8.0.0" };

            //RegistryKey rKey1 = Registry.CurrentUser;
            //RegistryKey subkey = rKey1.CreateSubKey(@"Wow6432Node\Supervision");
            //subkey.SetValue("time000", "desired value you wish to have");

            RegistryHive[] keys = new RegistryHive[] { RegistryHive.Users, RegistryHive.LocalMachine };
            RegistryView[] views = new RegistryView[] { RegistryView.Registry32, RegistryView.Registry64 };

            foreach (string path in paths)
                foreach (var hive in keys)
                {
                    foreach (var view in views)
                    {
                        RegistryKey rk = null,
                            basekey = null;

                        try
                        {
                            basekey = RegistryKey.OpenBaseKey(hive, view);
                            rk = basekey.OpenSubKey(path, RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryRights.FullControl);
                        }
                        catch (Exception ex) { continue; }

                        if (basekey == null || rk == null)
                            continue;

                        if (rk == null)
                        {
                            if (dump) Console.WriteLine("ERROR: failed to open subkey '{0}'", path);
                            return null;
                        }

                        if (dump) Console.WriteLine("Reading registry at {0}", rk.ToString());

                        foreach (string skName in rk.GetSubKeyNames())
                        {
                            try
                            {
                                RegistryKey sk = rk.OpenSubKey(skName);
                                if (sk == null) continue;

                                object dbName = sk.GetValue("DATABASE_NAME");
                                object dbServerName = sk.GetValue("DATABASE_SERVER_NAME");

                                if (dbName == null || dbServerName == null)
                                    continue;

                                string databaseName = dbName.ToString();
                                string databaseServerName = dbServerName.ToString();

                                if (dump) Console.WriteLine("{0}: {1}", databaseName, databaseServerName);

                                ApplicationRegistry inf = new ApplicationRegistry();
                                inf.DatabaseName = databaseName;
                                inf.DatabaseServerName = databaseServerName;

                                return inf;
                            }
                            catch (Exception ex)
                            {
                                // todo
                            }
                        }
                    } // view
                } // hive

            return null;
        }
    }
}
