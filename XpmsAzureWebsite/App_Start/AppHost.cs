using Funq;
using ServiceStack.CacheAccess;
using ServiceStack.CacheAccess.Providers;
using ServiceStack.Logging;
using ServiceStack.Logging.Log4Net;
using ServiceStack.Razor;
using ServiceStack.ServiceInterface.Validation;
using ServiceStack.WebHost.Endpoints;
using System.Configuration;
using Xpms.AzureRepository;
using Xpms.Core.IDB;
using Xpms.Core.ISecurity;
using Xpms.Core.Message;
using Xpms.Core.Processes.Base;
using Xpms.WebServices;
using Xpms.WebServices.Auth;
using Xpms.WebServices.Extensions;
using Xpms.WebServices.Mail;
using Xpms.WebServices.Validators;
using XpmsAzureWebsite.App_Start;

[assembly: WebActivator.PreApplicationStartMethod(typeof(AppHost), "Start")]

namespace XpmsAzureWebsite.App_Start
{
    [System.Runtime.InteropServices.GuidAttribute("95AC3A68-E9FD-4349-8D03-B47129F4F8F2")]
    public class AppHost : AppHostBase
    {
        public AppHost() //Tell ServiceStack the name and where to find your web services
            : base("XMPS Web Services", typeof(SignupService).Assembly) { }

        public static void Start()
        {
            new AppHost().Init();
        }

        public override void Configure(Container container)
        {
            //Set JSON web services to return idiomatic JSON camelCase properties
            ServiceStack.Text.JsConfig.EmitCamelCaseNames = true;

            SetConfig(new EndpointHostConfig
            {
                //DebugMode = true,
                //CustomHttpHandlers = {
                //    { HttpStatusCode.NotFound, new RazorHandler("/notfound") },
                //    { HttpStatusCode.Unauthorized, new RazorHandler("/login") }
                //}
            });

            LogManager.LogFactory = new Log4NetFactory(true);

            Routes.ConfigRoutes();

            RegisterDependencies(container);

            Plugins.Add(new ValidationFeature());
            Plugins.Add(RazorFormat.Instance);
            Plugins.ConfigAuth(container);
        }

        private static void RegisterDependencies(Container container)
        {
            //Register all your dependencies
            container.Register<ICacheClient>(new MemoryCacheClient());
            container.Register<IAuth>(new PasswordAuth());
            container.Register<IRepository>(AzureStorage.CreateSingleton(
                ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString
                ));

            container.RegisterAutoWired<XpmsAuthProvider>().ReusedWithin(ReuseScope.Hierarchy);
            container.RegisterAutoWiredAs<Mailer, IMailer>().ReusedWithin(ReuseScope.Hierarchy);
            
            container.RegisterProcesses<AbstractProcess>();
            container.RegisterValidators(typeof(SignupRequestValidator).Assembly);
            container.RegisterDataRecords<IRepoData>(typeof(AzureStorage).Assembly);
        }
    }
}