using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BackendTests.Brokers
{
    internal class BackendApiWebApplicationFactory: WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);
        }
    }

    internal class WebAppBroker
    {
        private static BackendApiWebApplicationFactory backendApiWebApplicationFactory;
        private static HttpClient httpClient;
        private static readonly object padlock = new object();
        private WebAppBroker() { }

        public static BackendApiWebApplicationFactory GetWebAppInstance()
        {
            lock (padlock)
            {
                if (backendApiWebApplicationFactory == null)
                {
                    backendApiWebApplicationFactory = new BackendApiWebApplicationFactory();
                }
                return backendApiWebApplicationFactory;
            }
        }

        public static HttpClient GetHttpClient()
        {
            lock (padlock)
            {
                if (httpClient == null)
                {
                    var factory = GetWebAppInstance();
                    httpClient = factory.CreateClient();
                }
                return httpClient;
            }
        }
    }
}
