namespace ProyectoLES.Modelos
{
    public class Cliente
    {
        public string IdCliente { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }

        public void Mostrar()
        {
            Console.WriteLine($"ID: {IdCliente}, Nombre: {Nombre} {Apellido}, Teléfono: {Telefono}, Email: {Email}");
        }
    }
}namespace ProyectoLES.Modelos
{
    public class Cliente
    {
        public string IdCliente { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }

        public void Mostrar()
        {
            Console.WriteLine($"ID: {IdCliente}, Nombre: {Nombre} {Apellido}, Teléfono: {Telefono}, Email: {Email}");
        }
    }
}