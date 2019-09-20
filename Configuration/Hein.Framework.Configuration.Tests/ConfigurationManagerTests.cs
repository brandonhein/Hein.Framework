using Hein.Framework.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using Xunit;

namespace Hein.Framework.Configuration.Tests
{
    public class ConfigurationManagerTests
    {
        public ConfigurationManagerTests()
        {
            //build your config
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.unittests.json", false)
                .Build();

            var services = new ServiceCollection();
            //use our extension to add your config you rendered from config builder
            services.AddConfiguration(config);
            //dont forget to call BuildServiceLocator
            services.BuildServiceLocator();
        }

        [Fact]
        public void Should_get_the_app_id_from_app_settings()
        {
            var appId = ConfigurationManager.AppSettings["ApplicationId"];
            Assert.Equal("747474", appId);
        }

        [Fact]
        public void Should_get_the_connection_string_from_configuration_manager()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DbConn"];
            var expectedConnString = "Data Source=localhost;Initial Catalog=MyDatabase;Persist Security Info=True";
            Assert.Equal(expectedConnString, connectionString);
        }

        [Fact]
        public void Should_get_my_own_custom_section_from_the_app_settings_json()
        {
            var section = ConfigurationManager.GetSection("MyCustomSection");
            var result = section["HowAboutThem"];
            Assert.Equal("Apples", result);
        }
    }
}
