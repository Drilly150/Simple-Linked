namespace ProyectoLES.Modelos
{
    public class Producto
    {
        public string IdProducto { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public int Stock { get; set; }
        [cite_start]// La propiedad "Unidades" mencionada en el documento  parece ser el "Stock".
        // Si se refiriera a otra cosa, se añadiría aquí. Por ahora, se asume que es el stock.

        public void Mostrar()
        {
            Console.WriteLine($"ID: {IdProducto}, Nombre: {Nombre}, Precio: ${Precio:F2}, Stock: {Stock}");
        }
    }
}