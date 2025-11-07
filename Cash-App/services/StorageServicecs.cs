using Microsoft.JSInterop;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CashApp.services
{
    /// <summary>
    /// This service uses localStorage to save and load data in the browser.
    /// </summary>
    public class StorageServicecs : IStorageService {
        /// <summary>
        /// Provides access to JavaScript runtime and configures JSON serialization options
        /// used for saving and loading data in the browser.
        /// </summary>
        private readonly  IJSRuntime _jsRuntime;
        private JsonSerializerOptions _jsonSerializerOptions = new()
        {
               PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Converters = {new JsonStringEnumConverter() }
        }; 
        /// <summary>
        /// Constructor that receives an IJSRuntime instance and stores it for JavaScript interop.
        /// </summary>
        public StorageServicecs(IJSRuntime jSRuntime) => _jsRuntime = jSRuntime;
        
        /// <summary>
        /// Save data to local storage.
        /// </summary>
        public async Task SetItemAsync <T>(string key, T value)
        { 
            var json = JsonSerializer.Serialize(value, _jsonSerializerOptions);
            await _jsRuntime.InvokeVoidAsync( "localStorage.setItem", key, json);
        }
        
        /// <summary>
        /// Get data from local storage.
        /// </summary>
        public async Task<T> GetItemAsync<T>(string key)
        {
           var json = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", key);
            if (string.IsNullOrWhiteSpace(json)) 
                return default;
            return JsonSerializer.Deserialize<T>(json, _jsonSerializerOptions)!; 
        }
        
        

       
    }
}
