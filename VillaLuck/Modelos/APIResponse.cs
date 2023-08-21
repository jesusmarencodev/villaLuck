using System.Net;

namespace VillaLuck.Modelos
{
    public class APIResponse
    {
        public APIResponse()
        {
            ErrorMensajes = new List<string> ();
        }

        public HttpStatusCode StatusCode { get; set; }
        public bool IsExitoso { get; set; } = true;
        public List<string> ErrorMensajes { get; set; }
        public object Resultado { get; set; }

    }
}
