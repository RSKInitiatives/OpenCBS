using Free.iso8583;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Iso8583WebClient
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            String rootDir = Server.MapPath("~/");

            Logger.GetInstance().ReplaceOutput(new FreeIso8583Logger(rootDir));
            Logger.GetInstance().Level = LogLevel.Notice;

            String configPath = rootDir + "/../../src/messagemap-config.xml";
            Stream fileConfig = null;
            try
            {
                fileConfig = File.Open(configPath, FileMode.Open);
            }
            catch (FileNotFoundException ex)
            {
                Logger.GetInstance().Write(ex);
            }
            if (fileConfig != null)
            {
                MessageProcessor.GetInstance().Load(fileConfig);
                Logger.GetInstance().WriteLine("Config loaded...");
            }
        }
    }
}
