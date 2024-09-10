namespace Pizza.Api.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly List<T> _items = new();

    public virtual IEnumerable<T> GetAll()
    {
        return _items;
    }

    public virtual T GetById(Guid id)
    {
        // Assuming all entities have a "Id" property of type Guid
        var item = _items.FirstOrDefault(i => (Guid)i.GetType().GetProperty("Id")?.GetValue(i) == id);
        return item;
    }

    public virtual void Create(T entity)
    {
        _items.Add(entity);
    }

    public virtual void Update(T entity)
    {
        // This logic can be customized based on specific entity update needs.
        var id = (Guid)entity.GetType().GetProperty("Id")?.GetValue(entity);
        var existingItem = GetById(id);

        if (existingItem != null)
        {
            _items.Remove(existingItem);
            _items.Add(entity);
        }
    }

    public virtual void Delete(Guid id)
    {
        var existingItem = GetById(id);
        if (existingItem != null)
        {
            _items.Remove(existingItem);
        }
    }
}
