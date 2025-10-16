
namespace CashApp.Interface
{
    public interface IStorageService
    {
        //spara
        Task SetItemAsync<T>(string key, T item);
        //hemta
        // Task<List<IBankAccount>> GetItemAsync<T>(string storageKey);

        Task <T> GetItemAsync<T>(string storageKey);
    }
}
