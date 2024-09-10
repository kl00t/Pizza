namespace Pizza.Api.Repository;

public interface IRepository<T> where T : class
{
    IEnumerable<T> GetAll();
    T GetById(Guid id);
    void Create(T entity);
    void Update(T entity);
    void Delete(Guid id);
}
