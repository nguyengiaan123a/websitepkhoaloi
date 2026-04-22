using websitepkhoaloi.Helpper;

namespace websitepkhoaloi.Services.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        Task<(int totalpages,IReadOnlyList<T>)> GetAll(int page,int pagesize, string search);
        Task<status> Add(T entity);
        Task<status> Update(T entity ,int id);
        Task<status> Delete(int id);
    }
}
