using System;
using System.IO;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace Common.ConfigUtils
{
    public static class AppSettingsInitialization
    {
        private static IConfigurationRoot configuration;
        
        private static void Init()
        {
            HttpClient httpClient;
            TestServer testServer;

            var settingPath = Path.GetFullPath(Path.Combine(@"../../../../Common/appsettings.json"));
            configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile(settingPath)
                .Build();

            testServer = new TestServer(new WebHostBuilder()
                .UseEnvironment("Development")
                .UseConfiguration(configuration)
                .UseStartup<Startup>()
            );

            httpClient = testServer.CreateClient();            
        }

        public static AutomationVariables GetConfigInstance(string configurationSection = "AutomationVariables")
        {
            var automationConfig = new AutomationVariables();
            Init();

            configuration.GetSection("AutomationVariables").Bind(automationConfig);

            return automationConfig;
        }
    }
}
