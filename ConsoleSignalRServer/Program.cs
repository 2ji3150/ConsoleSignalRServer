using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Hosting;
using Owin;
using System;


namespace ConsoleSignalRServer {
    class Program {
        static void Main(string[] args) {
            string url = "http://localhost:8080";
            using (WebApp.Start(url)) {
                Console.WriteLine($"Server running on {url}");
                while (true) {
                  
                    Console.ReadLine();
                    var context = GlobalHost.ConnectionManager.GetHubContext<MyHub>();
                    context.Clients.All.addMessage("server","Hello");
                    context.Clients.All.Notify();
                }


            }
        }
    }

    class Startup {
        public void Configuration(IAppBuilder app) => app.MapSignalR();
    }

    public class MyHub : Hub {

        public void Send(string name, string message) {
            Clients.All.AddMessage(name, message);
        }

        public void NotifyAllClients() {
            Clients.All.Notify();
        }
    }
}
