using VillaLuck.Modelos;

namespace VillaLuck.datos
{
    public static class VillaStore
    {
        public static List<VillaUpdateDto> villaList = new List<VillaUpdateDto>
        {
             new VillaUpdateDto{ Id=1, Nombre="Vista a la piscina", Ocupantes=4, MetrosCuadrados= 120},
             new VillaUpdateDto{ Id=2, Nombre="Vista al jardin", Ocupantes=3, MetrosCuadrados=190}
        };
    }
}
