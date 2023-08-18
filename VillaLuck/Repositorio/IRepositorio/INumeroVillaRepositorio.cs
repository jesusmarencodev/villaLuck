using VillaLuck.Modelos;

namespace VillaLuck.Repositorio.IRepositorio
{
    public interface IVillaNumeroRepositorio: IRepositorio<NumeroVilla>
    {
        Task<NumeroVilla> Actualizar(NumeroVilla entidad);
    }
}
