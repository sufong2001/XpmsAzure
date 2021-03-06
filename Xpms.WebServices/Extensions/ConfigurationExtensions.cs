﻿using Funq;
using J.F.Libs.Extensions;
using ServiceStack.Authentication.OpenId;
using ServiceStack.Configuration;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.WebHost.Endpoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xpms.Core.IDB;
using Xpms.Core.Models.Requests;
using Xpms.WebServices.Auth;
using Xpms.WebServices.IoC;

namespace Xpms.WebServices.Extensions
{
    public static class ConfigurationExtensions
    {
        public static void ConfigAuth(this List<IPlugin> plugins, Container container)
        {
            var appSettings = new AppSettings();
            plugins.Add(new AuthFeature(() => new AuthUserSession(),
                new IAuthProvider[] {
                    container.Resolve<XpmsAuthProvider>() ,
                    new TwitterAuthProvider(appSettings),       //Sign-in with Twitter
                    new FacebookAuthProvider(appSettings),      //Sign-in with Facebook
                    new DigestAuthProvider(appSettings),        //Sign-in with Digest Auth
                    new BasicAuthProvider(),                    //Sign-in with Basic Auth

                    //Register new OpenId providers you want to allow authentication with
                    new GoogleOpenIdOAuthExProvider(appSettings) { Repository = container.Resolve<IRepository>() } , //Sign-in with Goolge OpenId
                    new YahooOpenIdOAuthExProvider(appSettings) { Repository = container.Resolve<IRepository>() },  //Sign-in with Yahoo OpenId
                    new OpenIdOAuthProvider(appSettings),       //Sign-in with any Custom OpenId Provider
                }));
        }

        public static void ConfigRoutes(this IServiceRoutes routes)
        {
            //Configure User Defined REST Paths
            routes.RegisterRequestModels<IRequest>();
        }

        public static void RegisterRequestModels<T>(this IServiceRoutes routes, Assembly assemble = null
            , string removePrefix = " ", string removePostfix = "Request", bool fullName = false)
        {
            var tType = typeof(T);

            var requestModels = (assemble ?? tType.Assembly).GetTypes()
                .Where(type => (tType.IsAssignableFrom(type) && type.IsClass && !type.IsAbstract)
                    || type.IsSubclassOf(tType)).ToArray();

            foreach (var requestModel in requestModels)
            {
                var name = fullName ? requestModel.FullName : requestModel.Name;
                var restPath = "/" + name.Replace(removePrefix, "")
                                        .Replace(removePostfix, "")
                                        .SeperateCamelCase("-").ToLower();

                routes.Add(requestModel, restPath, null);
            }
        }

        public static void RegisterProcesses<T>(this Container container, Assembly assemble = null, ReuseScope reuse = ReuseScope.Hierarchy)
        {
            var tType = typeof(T);
            var processTypes = (assemble ?? tType.Assembly).GetTypes()
                .Where(type => (tType.IsAssignableFrom(type) && type.IsClass && !type.IsAbstract)
                    || type.IsSubclassOf(tType)).ToArray();

            var iocAdapter = container.Adapter ?? new IocContainerAdapter(container);

            container.Register(iocAdapter);
            container.RegisterAutoWiredTypes(processTypes, reuse);
        }

        public static void RegisterDataRecords<T>(this Container container, Assembly assImplementation, Assembly assInterface = null)
        {
            var tType = typeof(T);
            var interfaceTypes = (assInterface ?? tType.Assembly).GetTypes()
                .Where(type => (type.IsInterface
                    && type.GetInterfaces().Any(i => i == tType)
                    )).ToArray();

            var implementationTypes = assImplementation.GetTypes()
                .Where(type => (tType.IsAssignableFrom(type)
                    && type.IsClass && !type.IsAbstract
                    )).ToArray();

            foreach (Type t in implementationTypes)
            {
                container.RegisterAutoWiredType(t
                    , interfaceTypes.FirstOrDefault(i => i.IsAssignableFrom(t))
                   );
            }
        }
    }
}