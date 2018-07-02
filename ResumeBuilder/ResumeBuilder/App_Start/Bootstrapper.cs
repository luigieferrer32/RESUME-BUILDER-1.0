using Microsoft.Practices.Unity;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;
using ResumeBuilder.Controllers;
using Unity.Injection;
namespace ResumeBuilder.App_Start
{
    public class Bootstrapper
    {

        public static IUnityContainer Initialize()
        {
            var container = BuildUnityContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            return container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            //regitser all your components with the container here
            container.RegisterType<IResumeRepo, ResumeRepository>();
            container.RegisterType<AccountController>(new InjectionConstructor());
            RegisterTypes(container);
            return container;

        }

        public static void RegisterTypes(IUnityContainer container)
        {

        }
    }
}