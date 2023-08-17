using VillaLuck.Modelos.Dto;

namespace VillaLuck.datos
{
    public static class VillaStore
    {
        public static List<VillaDto> villaList = new List<VillaDto>
        {
             new VillaDto{ Id=1, Nombre="Vista a la piscina", Ocupantes=4, MetrosCuadrados= 120},
             new VillaDto{ Id=2, Nombre="Vista al jardin", Ocupantes=3, MetrosCuadrados=190}
        };
    }
}
