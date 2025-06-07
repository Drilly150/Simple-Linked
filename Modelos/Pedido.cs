using ProyectoLES.EstructurasDeDatos;

namespace ProyectoLES.Modelos
{
    public class DetallePedido
    {
        public string IdProducto { get; set; }
        public string NombreProducto { get; set; } // Para facilitar la visualización
        public int Cantidad { get; set; }
        public double PrecioUnitario { get; set; }
        public double SubTotal => Cantidad * PrecioUnitario;
    }

    public class Pedido
    {
        public int IdPedido { get; set; }
        public string IdCliente { get; set; }
        public DateTime Fecha { get; set; }
        public ListaEnlazada<DetallePedido> Detalles { get; set; }

        public Pedido()
        {
            Detalles = new ListaEnlazada<DetallePedido>();
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