
namespace CashApp.Interface
{
    /// <summary>
    /// Used to save and load data from local storage.
    /// </summary>
    public interface IStorageService
    {
        //spara
        Task SetItemAsync<T>(string key, T item);
        //hemta
        Task <T> GetItemAsync<T>(string storageKey);
    }
}
