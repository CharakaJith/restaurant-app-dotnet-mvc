namespace restaurant_app_dotnet_mvc.Models
{
    public interface IRepository<T> where T: class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id, QueryOption<T> options);
        Task<IEnumerable<T>> GetAllByIdAsync<TKey>(TKey id, string prop, QueryOption<T> options);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}
