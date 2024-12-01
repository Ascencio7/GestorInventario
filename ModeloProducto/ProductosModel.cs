using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorInventario.ModeloProducto
{
    public class ProductosModel
    {
        public int ProductoID { get; set; }
        public string  Nombre_Producto { get; set; }
        public decimal Precio_Producto { get; set; }
        public int Cantidad_Producto { get; set; }
        public decimal Total_Producto { get; set; }
        public string Proveedor {  get; set; }

        public ProductosModel() { }
    }
}