using System;
using System.Collections.Generic;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace Yara.IoC
{
    public static class SimpleInjectorContainer
    {
        public static Container RegisterServices()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            RegisterDictionary(ref container, Yara.Data.Entity.IoC.Resolver.GetTypes());
            RegisterDictionary(ref container, Yara.AppService.IoC.Resolver.GetTypes());
            // Register your types, for instance using the scoped lifestyle:


            // This is an extension method from the integration package.
            //container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            container.Verify();

            return container;
        }

        private static void RegisterDictionary(ref Container container, Dictionary<Type, Type> iocs)
        {
            foreach (var ioc in iocs)
            {
                container.Register(ioc.Key, ioc.Value, Lifestyle.Scoped);
            }
        }
    }
}