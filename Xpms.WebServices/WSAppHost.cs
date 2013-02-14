using Funq;
using ServiceStack.WebHost.Endpoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xpms.Core;
using Xpms.Core.IDB;
using Xpms.WebServices.Models;

namespace Xpms.WebServices
{
    public class WSAppHost: AppHostBase
    {
        //Tell Service Stack the name of your application and where to find your web services
        public WSAppHost() : base("XMPS Web Services", typeof(WSAppHost).Assembly) { }

        public override void Configure(Container container)
        {
            ServiceStack.Text.JsConfig.EmitCamelCaseNames = true;

            //register user-defined REST-ful urls
            Routes
              .Add<Registration>("/Registration");

            //container.Register<IRepository>(new AzureStorage());
            container.Register(new RegistrationProcess());

        }
    }

}
