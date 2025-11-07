using CashApp;
using CashApp.services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;

namespace CashApp
{
  
    public class Program
    {
        /// <summary>
        /// The start point for the app.
        /// </summary>
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");
            // Register the Accountservices class as the implementation for IAccountServices
            builder.Services.AddScoped<IAccountServices, Accountservices>();
            // Register the StorageServicecs class as the implementation for IStorageService
            builder.Services.AddScoped<IStorageService, StorageServicecs>();
            // Register an HttpClient for making web requests, using the app's base address
            builder.Services.AddScoped(sp => new HttpClient
                { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            
            
            
 

           
            // (Duplicate registration) Adds IAccountServices again using the full namespace — likely not needed if already registered above
           // builder.Services.AddScoped<CashApp.Interface.IAccountServices, CashApp.services.Accountservices>();
            await builder.Build().RunAsync();
        }
    }
}
