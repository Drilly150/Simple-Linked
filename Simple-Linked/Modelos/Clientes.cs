namespace ProyectoLES.Modelos
{
    public class Cliente
    {
        public required string IdCliente { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required string Telefono { get; set; }
        public required string Email { get; set; }

        public void Mostrar()
        {
            Console.WriteLine($"ID: {IdCliente}, Nombre: {Nombre} {Apellido}, Teléfono: {Telefono}, Email: {Email}");
        }
    }
}