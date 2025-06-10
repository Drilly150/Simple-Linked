using ProyectoLES.EstructurasDeDatos;

namespace ProyectoLES.Modelos
{
    public class DetallePedido
    {
        public required string IdProducto { get; set; }
        public required string NombreProducto { get; set; } // Para facilitar la visualización
        public int Cantidad { get; set; }
        public double PrecioUnitario { get; set; }
        public double SubTotal => Cantidad * PrecioUnitario;
    }

    public class Pedido
    {
        public required int IdPedido { get; set; } // Sigue siendo required
        public required string IdCliente { get; set; } // Sigue siendo required
        public required DateTime Fecha { get; set; } // Sigue siendo required
        public ListaEnlazada<DetallePedido> Detalles { get; set; } // ¡QUITAMOS 'required' aquí!

        // Constructor para inicializar Detalles. Las propiedades 'required' se inicializan con el inicializador de objeto.
        public Pedido()
        {
            Detalles = new ListaEnlazada<DetallePedido>(); // Ya se inicializa aquí
        }

        public double Total
        {
            get
            {
                double total = 0;
                var actual = Detalles.Cabeza;
                while (actual != null)
                {
                    total += actual.Dato.SubTotal;
                    actual = actual.Siguiente;
                }
                return total;
            }
        }
    }
}