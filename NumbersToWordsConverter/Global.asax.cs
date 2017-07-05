using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Mvc;
using NumbersToWordsConverter.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NumbersToWordsConverter
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //DI unity container
            var container = new UnityContainer();

            //Registering th required interfaces and corresponding implementation.
            container.RegisterType<NumberToWordService.IService>(
                new ContainerControlledLifetimeManager(),
                new InjectionFactory(
                    (c) => new ChannelFactory<NumberToWordService.IService>("BasicHttpBinding_IService").CreateChannel()));
            container.RegisterType<IController, HomeController>("Home");
                   
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
   

    }
