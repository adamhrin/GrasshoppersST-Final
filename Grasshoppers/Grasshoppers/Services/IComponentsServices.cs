using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Grasshoppers.Services
{
    public interface IComponentsServices<T>
    {
        Task<ObservableCollection<T>> GetAllComponentsAsync();
        Task<bool> DeleteComponentAsync(int id);
        Task<bool> PutComponentAsync(int id, T selectedComponent);
        Task<bool> PostComponentAsync(T newComponent);
    }
}
