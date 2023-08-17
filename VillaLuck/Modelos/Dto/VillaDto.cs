using System.ComponentModel.DataAnnotations;

namespace VillaLuck.Modelos.Dto
{
    public class VillaDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Nombre { get; set; }
        public string Detalle { get; set; }
        public double Tarifa { get; set; }
        public int Ocupantes { get; set; }
        public int MetrosCuadrados { get; set; }
        public int ImagenUrl { get; set; }
        public string Amenidad { get; set; }
    }
}
