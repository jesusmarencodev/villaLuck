using VillaLuck.Modelos;

namespace VillaLuck.Repositorio.IRepositorio
{
    public interface IVillaRepositorio: IRepositorio<Villa>
    {
        Task<Villa> Actualizar(Villa entidad);
    }
}
