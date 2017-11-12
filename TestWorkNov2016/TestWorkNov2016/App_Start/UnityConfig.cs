using System;
using TestWorkNov2016.Infrastructure;
using TestWorkNov2016.Infrastructure.Interfaces;
using TestWorkNov2016.Infrastructure.Parser;
using TestWorkNov2016.Models.OperationResults;
using TestWorkNov2016.Storage;
using Microsoft.Practices.Unity;
//using Unity.AspNet.WebApi;

namespace TestWorkNov2016
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            var userUploadsPath = System.Configuration.ConfigurationManager.AppSettings["UserDataFolderPath"];
            var testFileName = System.Configuration.ConfigurationManager.AppSettings["TestFileName"];

            container.RegisterType<IFileUploader, FileUploader>();
            container.RegisterType<IDirectoryInfoProvider, DirectoryInfoProvider>(
                new InjectionConstructor(userUploadsPath, testFileName));
            container.RegisterType<ITextParser<StationParsingResult>, MetroStationParser>();
            container.RegisterType<ITextFileParser, TextFileParser>(
                new InjectionConstructor(container.Resolve<ITextParser<StationParsingResult>>()));
            container.RegisterType<IMetroStationStorage, MetroStationStorage>(new ContainerControlledLifetimeManager());
        }
    }
}