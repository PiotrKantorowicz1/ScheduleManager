using System.Net.Http;
using System.Text;
using Manager.Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;

namespace Manager.Tests.e2e.Controllers
{
    public abstract class BaseTestsController
    {
        protected readonly TestServer Server;
        protected readonly HttpClient Client;

        protected BaseTestsController()
        {
            Server = new TestServer(new WebHostBuilder()
                            .UseStartup<Startup>());
            Client = Server.CreateClient();
        }

        protected static StringContent GetPayload(object data)
        {
            var json = JsonConvert.SerializeObject(data);

            return new StringContent(json, Encoding.UTF8, "application/json");
        }

    }
}