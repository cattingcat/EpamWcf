using System;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Routing;

using Ninject;
using Ninject.Modules;
using NLog;

using DataAccessors.Accessors;
using DataAccessors.Entity;
using BusinessLogic;
using MvcClient.App_Code;


namespace MvcClient
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        IKernel _ninjectKernel;
        

        public NinjectControllerFactory()
        {
            _ninjectKernel = new StandardKernel(new[]{new RegisterDependencies()});
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
                return null;
            return _ninjectKernel.Get(controllerType) as IController;
        }



        private class RegisterDependencies : NinjectModule
        {
            public override void Load()
            {
                Kernel.Bind<IPersonBll>().To<PersonBusinessLogicServiceAdapter>().InSingletonScope();
                Kernel.Bind<IPhoneBll>().To<PhoneBusinessLogicServiceAdapter>().InSingletonScope();
            }
        }
    }
}