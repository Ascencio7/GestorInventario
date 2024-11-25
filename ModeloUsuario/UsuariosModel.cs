using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorInventario.ModeloUsuario
{
    public class UsuariosModel
    {
        public int UserID { get; set; }
        public string NombreUsuario { get; set; }
        public string Correo {  get; set; }
        public byte[] Contra { get; set; }
        public int RolID { get; set; }
        public int EstadoID { get; set; }

        public UsuariosModel() { }
    }
}
