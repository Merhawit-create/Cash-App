using Microsoft.JSInterop;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CashApp.services
{
    public class StorageServicecs : IStorageService {
        private readonly  IJSRuntime _jsRuntime;
            
       private JsonSerializerOptions _jsonSerializerOptions = new()
        {
               PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Converters = {new JsonStringEnumConverter() }
        };
       
        public StorageServicecs(IJSRuntime jSRuntime) => _jsRuntime = jSRuntime;
        

        public async Task SetItemAsync <T>(string key, T value)
        {
            var json = JsonSerializer.Serialize(value, _jsonSerializerOptions);
            await _jsRuntime.InvokeVoidAsync( "localStorage.setItem", key, json);
        }



        public async Task<T> GetItemAsync<T>(string key)
        {
           var json = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", key);
            //  if (json is null)
            if (string.IsNullOrWhiteSpace(json))
                return default;


            return JsonSerializer.Deserialize<T>(json, _jsonSerializerOptions)!; 
        }

       
    }
}
