using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppAtusPiesPr.Entidades
{
    public class ClProductoE
    {
        public int idProducto {  get; set; }

        public string Codigo { get; set; }

        public string Nombre { get; set; }

        public int CantidadStock { get; set; }

        public int Precio { get; set; }

        public string imagen { get; set; }

        public string Estado { get; set; }

        public string Talla { get; set; }

        public string DescripcionProducto { get; set; }

        public string Presentacion { get; set; }
        public int descuento { get; set; }

        public  string descripcionCategoria { get; set; }
        public string referencia { get; set; }
        public int idVendedor { get; set; }
        public string Descripcion { get; set; }
        public int CantidadVendida {  get; set; }
        public decimal TotalVentas { get; set; }
        public string Marca { get; set; }
        public string Categoria { get; set; }
        public string NombreVendedor { get; set; }
        public string apellidoVendedor { get; set; }
        public int? valoracion { get; set; }


    }

    public class ClProductoEmpresaE
    {
        public int idProdctoEmpresa { get; set; }
        public int CantidadVendida {get; set; }
        public decimal totalVentas { get; set; }
        public string marca {  get; set; }
        public int idVendedor { get; set; }
        public int idProducto { get; set; }
        public string nombreProducto { get; set; }
        public string nombreVendedor { get; set; }
        public int? cantidadStock { get; set; } //puede aceptar valores nulos
        public int? precioVenta { get; set; }
        public string descripcionProducto { get; set; }

        public string descripcion { get; set; }
        public string referencia { get; set; }


        public string imagen { get; set; }
        public string ImagenUrl
        {
            get
            {
                return !string.IsNullOrEmpty(imagen) ? imagen : "~/Images/no-image-found.png";
            }
        }

        public string Estado { get; set; }
        public string descripcionCategoria { get; set; }
        public int? descuento { get; set; }
        public string nombres { get; set; }
        public string apellidoVendedor { get; set; }
        public string nombreMarca { get; set; }
        public string NombreVendedor { get; set; }

        // Nueva propiedad para las tallas asociadas
        public List<ClTallaE> TallasDisponibles { get; set; }
    }
    public class ClTallaE
    {
        public int idTalla { get; set; }
        public string descripcionTalla { get; set; }

        public string Valor { get; set; }
    }

    public class ClproductoEmpresaTalla
    {
        public int idProductoEmpresaTalla { get; set; }
        public int idProductoEmpresa { get; set; }
        public int idTalla { get; set; }

    }
    public class ClCategoriaE
    {
        public int idCategoria { get; set; }
        public string descripcion { get; set; }

        public int TotalProductos {  get; set; }
    }

    public class ClComentarioE
    {
        public int idComentario { get; set; }
        public int idProducto { get; set; } // Relación con la tabla Productos
        public int idCliente { get; set; }  // Relación con la tabla Usuarios
        public string comentario { get; set; }   // Texto del comentario
        public DateTime FechaComentario { get; set; } // Fecha del comentario
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public int? valoracion { get; set; }
    }

    public class ClMarcasE
    {
        public int idMarca { get; set; }
        public string nombreMarca { get; set; }
        public string descripcion { get; set; }
    }

}