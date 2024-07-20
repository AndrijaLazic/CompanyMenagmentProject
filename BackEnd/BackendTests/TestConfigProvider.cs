using Docker.DotNet.Models;
using DOMAIN.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BackendTests
{
    public sealed class TestConfigProvider
    {
        private static TestConfig testConfigInstance;
        private static AppConfigClass appConfigInstance;
        private static readonly object padlock = new object();

        TestConfigProvider()
        {
        }

        public static TestConfig GetTestConfig()
        {
            
            lock (padlock)
            {
                if(testConfigInstance == null)
                {
                    var jsonString = File.ReadAllText("./TestConfig.json");
                    testConfigInstance = JsonSerializer.Deserialize<TestConfig>(jsonString)!;
                }
                if(appConfigInstance == null)
                {
                    appConfigInstance = GetAppConfig();
                }
                return testConfigInstance!;
            }
            
        }

        private static AppConfigClass GetAppConfig()
        {

            
            if (appConfigInstance == null)
            {
                var jsonString = File.ReadAllText(testConfigInstance.AppSettingsRoot);
                using (JsonDocument document = JsonDocument.Parse(jsonString))
                {
                    JsonElement root = document.RootElement;
                    JsonElement addressElement = root.GetProperty("MyAppSettings");
                    string addressJson = addressElement.GetRawText();

                    appConfigInstance = JsonSerializer.Deserialize<AppConfigClass>(addressJson)!;
                }
            }
            return appConfigInstance;
            
        }
    }

    public class TestConfig
    {
        public string AppSettingsRoot { get; set; }
        public string BaseApiURL { get; set; }
    }
}
