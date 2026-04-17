namespace websitepkhoaloi.Services.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Get(int id);
        Task<(int totalpages,IReadOnlyList<T>)> GetAll(int page,int pagesize, string search);
        Task<T> Add(T entity);
        Task<bool> Exists(int id);
        Task Update(T entity);
        Task Delete(int id);
    }
}
