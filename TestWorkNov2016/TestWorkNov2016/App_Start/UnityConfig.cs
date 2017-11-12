using System;
using Microsoft.Practices.Unity;
using System.Web.Http;
using TestWorkNov2016.Infrastructure;
using TestWorkNov2016.Infrastructure.Interfaces;
using TestWorkNov2016.Infrastructure.Parser;
using TestWorkNov2016.Models.OperationResults;
using TestWorkNov2016.Storage;
using Unity.WebApi;

namespace TestWorkNov2016
{
    public static class UnityConfig
    {
        private static readonly Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }

        public static void RegisterTypes(UnityContainer container)
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