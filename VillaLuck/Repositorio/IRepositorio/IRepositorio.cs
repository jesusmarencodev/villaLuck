using System.Linq.Expressions;

namespace VillaLuck.Repositorio.IRepositorio
{
    public interface IRepositorio<T> where T : class
    {
        Task Crear(T entidad);

        //Sino se envia un filtro regresa toda la lista ese el el (?)
        Task<List<T>> ObtenerTodos(Expression<Func<T, bool>>? filtro = null);
        Task<T> Obtener(Expression<Func<T, bool>>? filtro = null, bool traked = true);

        Task Remover(T endidad);
        Task Grabar();

    }
}
