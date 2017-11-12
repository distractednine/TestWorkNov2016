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
        public static void RegisterTypes()
        {
            var container = new UnityContainer();
            var userUploadsPath = System.Configuration.ConfigurationManager.AppSettings["UserDataFolderPath"];
            var testFileName = System.Configuration.ConfigurationManager.AppSettings["TestFileName"];

            container.RegisterType<IFileUploader, FileUploader>();
            container.RegisterType<IDirectoryInfoProvider, DirectoryInfoProvider>(
                new InjectionConstructor(userUploadsPath, testFileName));
            container.RegisterType<ITextParser<StationParsingResult>, MetroStationParser>();
            container.RegisterType<ITextFileParser, TextFileParser>(
                new InjectionConstructor(container.Resolve<ITextParser<StationParsingResult>>()));
            container.RegisterType<IMetroStationStorage, MetroStationStorage>(new ContainerControlledLifetimeManager());

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}