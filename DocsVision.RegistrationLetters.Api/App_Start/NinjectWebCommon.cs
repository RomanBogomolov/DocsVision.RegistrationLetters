
using DocsVision.RegistrationLetters.Api.Models;
using DocsVision.RegistrationLetters.Api.Models.Validators;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(DocsVision.RegistrationLetters.Api.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(DocsVision.RegistrationLetters.Api.App_Start.NinjectWebCommon), "Stop")]

namespace DocsVision.RegistrationLetters.Api.App_Start
{
    using System;
    using System.Web;
    using System.Web.Http;
    using System.Configuration;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    using FluentValidation;
    using FluentValidation.WebApi;
    using DocsVision.RegistrationLetters.Api.Services;

    using DocsVision.RegistrationLetters.DataAccess;
    using DocsVision.RegistrationLetters.DataAccess.Sql;
    using DocsVision.RegistrationLetters.Model.Validators;
    using DocsVision.RegistrationLetters.Model;
    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                FluentValidationModelValidatorProvider.
                    Configure(GlobalConfiguration.Configuration, provider => provider.ValidatorFactory =
                        new NinjectValidatorFactory(kernel));

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["MessageDb"].ConnectionString;
            
            /* DAL */
            kernel.Bind<IUserRepository>().To<UserRepository>().WithConstructorArgument(connectionString);
            kernel.Bind<IMessageRepository>().To<MessageRepository>().WithConstructorArgument(connectionString);
            
            /* Validators */
            kernel.Bind<IValidator<Message>>().To<MessageValidator>();
            kernel.Bind<IValidator<CompositeMessageEmails>>().To<CompositeMessageEmailsValidator>();
        }        
    }
}
