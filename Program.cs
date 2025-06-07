using ProyectoLES.Modelos;
using ProyectoLES.EstructurasDeDatos;
using System.Text.RegularExpressions;

public class Program
{
    // Declaración de las listas enlazadas para toda la aplicación
    private static readonly ListaEnlazada<Cliente> listaClientes = new ListaEnlazada<Cliente>();
    private static readonly ListaEnlazada<Producto> listaProductos = new ListaEnlazada<Producto>();
    private static readonly ListaEnlazada<Pedido> listaPedidos = new ListaEnlazada<Pedido>();

    static void Main(string[] args)
    {
        if (!Login())
        {
            Console.WriteLine("Acceso denegado. El programa se cerrará.");
            return;
        }

        PreCargarDatos();
        Console.Clear();
        Console.WriteLine("¡Bienvenido al Sistema de Gestión de Ventas al Mayor!");

        bool salir = false;
        while (!salir)
        {
            Console.WriteLine("\n--- MENÚ PRINCIPAL ---");
            Console.WriteLine("1. Gestionar Clientes");
            Console.WriteLine("2. Gestionar Productos");
            Console.WriteLine("3. Registrar Nueva Compra");
            Console.WriteLine("4. Ver Pedidos");
            Console.WriteLine("5. Salir");
            Console.Write("Seleccione una opción: ");

            switch (Console.ReadLine())
            {
                case "1":
                    MenuClientes();
                    break;
                case "2":
                    MenuProductos();
                    break;
                case "3":
                    RegistrarCompra();
                    break;
                case "4":
                    VerPedidos();
                    break;
                case "5":
                    salir = true;
                    break;
                default:
                    Console.WriteLine("Opción no válida. Intente de nuevo.");
                    break;
            }
        }
    }

    private static bool Login()
    {
        Console.WriteLine("--- INICIO DE SESIÓN ---");
        Console.Write("Usuario: ");
        string usuario = Console.ReadLine();
        Console.Write("Clave: ");
        string clave = Console.ReadLine();

        if (usuario == "Estudiante" && clave == "123456")
        {
            return true;
        }
        return false;
    }

    private static void PreCargarDatos()
    {
        [cite_start]// Precarga de productos 
        listaProductos.Agregar(new Producto { IdProducto = "10000000001", Nombre = "Laptop Ultrabook X1", Precio = 1200, Stock = 15 });
        listaProductos.Agregar(new Producto { IdProducto = "20000000002", Nombre = "Smartphone Galaxy S25", Precio = 950, Stock = 25 });
        listaProductos.Agregar(new Producto { IdProducto = "30000000003", Nombre = "Auriculares Inalámbricos", Precio = 150, Stock = 50 });
        listaProductos.Agregar(new Producto { IdProducto = "40000000004", Nombre = "Smartwatch FitPro", Precio = 280, Stock = 20 });
        listaProductos.Agregar(new Producto { IdProducto = "50000000005", Nombre = "Monitor Curvo 27\"", Precio = 350, Stock = 10 });
        listaProductos.Agregar(new Producto { IdProducto = "60000000006", Nombre = "Teclado Mecánico RGB", Precio = 90, Stock = 40 });
        listaProductos.Agregar(new Producto { IdProducto = "70000000007", Nombre = "Mouse Gaming HyperX", Precio = 60, Stock = 30 });
        listaProductos.Agregar(new Producto { IdProducto = "80000000008", Nombre = "Impresora Multifuncional", Precio = 220, Stock = 18 });
        listaProductos.Agregar(new Producto { IdProducto = "90000000009", Nombre = "Disco Duro Externo 2TB", Precio = 80, Stock = 35 });
        listaProductos.Agregar(new Producto { IdProducto = "11000000010", Nombre = "Webcam Full HD", Precio = 45, Stock = 60 });

        [cite_start]// Precarga de clientes 
        listaClientes.Agregar(new Cliente { IdCliente = "AB123456", Nombre = "Ana", Apellido = "García", Telefono = "4123456789", Email = "ana.garcia@email.com" });
        listaClientes.Agregar(new Cliente { IdCliente = "CD789012", Nombre = "Luis", Apellido = "Pérez", Telefono = "4249876543", Email = "luis.perez@email.com" });
        listaClientes.Agregar(new Cliente { IdCliente = "EF345678", Nombre = "María", Apellido = "Rodríguez", Telefono = "4161122334", Email = "maria.r@email.com" });
        listaClientes.Agregar(new Cliente { IdCliente = "GH901234", Nombre = "Pedro", Apellido = "González", Telefono = "4145566778", Email = "pedro.g@email.com" });
        listaClientes.Agregar(new Cliente { IdCliente = "IJ567890", Nombre = "Sofía", Apellido = "Hernández", Telefono = "4269988776", Email = "sofia.h@email.com" });
        listaClientes.Agregar(new Cliente { IdCliente = "KL123456", Nombre = "Diego", Apellido = "Sánchez", Telefono = "4128877665", Email = "diego.s@email.com" });
        listaClientes.Agregar(new Cliente { IdCliente = "MN789012", Nombre = "Valeria", Apellido = "Torres", Telefono = "4243344556", Email = "valeria.t@email.com" });
        listaClientes.Agregar(new Cliente { IdCliente = "OP345678", Nombre = "Ricardo", Apellido = "Castro", Telefono = "4167788990", Email = "ricardo.c@email.com" });
        
        [cite_start]// La carga de pedidos es más compleja debido a su estructura de dos tablas 
        // Se asumirá una lógica para agrupar los detalles por ID de pedido.
        var pedido1 = new Pedido { IdPedido = 10000001, IdCliente = "AB123456", Fecha = DateTime.Parse("2025-05-28") };
        pedido1.Detalles.Agregar(new DetallePedido { IdProducto = "10000000001", Cantidad = 1, PrecioUnitario = 1200 });
        pedido1.Detalles.Agregar(new DetallePedido { IdProducto = "30000000003", Cantidad = 2, PrecioUnitario = 150 });
        listaPedidos.Agregar(pedido1);

        var pedido2 = new Pedido { IdPedido = 10000002, IdCliente = "CD789012", Fecha = DateTime.Parse("2025-05-28") };
        pedido2.Detalles.Agregar(new DetallePedido { IdProducto = "20000000002", Cantidad = 1, PrecioUnitario = 950 });
        pedido2.Detalles.Agregar(new DetallePedido { IdProducto = "60000000006", Cantidad = 1, PrecioUnitario = 90 });
        listaPedidos.Agregar(pedido2);
        
        // ... Se agregarían los demás pedidos de la misma forma ...
    }

    private static void MenuClientes()
    {
        [cite_start]// Lógica para el menú de clientes (Agregar , Modificar , Eliminar, Ver)
        Console.WriteLine("\n--- Gestión de Clientes ---");
        Console.WriteLine("1. Ver todos los clientes");
        Console.WriteLine("2. Agregar nuevo cliente");
        Console.Write("Seleccione una opción: ");
        // Implementación de la lógica completa sería extensa pero seguiría este patrón.
        switch(Console.ReadLine())
        {
            case "1":
                Console.WriteLine("\n--- Listado de Clientes ---");
                foreach(var cliente in listaClientes)
                {
                    cliente.Mostrar();
                }
                break;
            case "2":
                [cite_start]// Pedir datos, validar  y agregar
                Console.WriteLine("\n--- Agregar Cliente ---");
                var nuevoCliente = new Cliente();
                Console.Write("ID Cliente (ej. AA123456): ");
                nuevoCliente.IdCliente = Console.ReadLine(); // Aquí iría la validación con Regex
                Console.Write("Nombre: ");
                nuevoCliente.Nombre = Console.ReadLine();
                // ... pedir resto de datos ...
                listaClientes.Agregar(nuevoCliente);
                Console.WriteLine("Cliente agregado con éxito.");
                break;
        }
    }
    
    private static void MenuProductos()
    {
        [cite_start]// Lógica para el menú de productos (Agregar , Modificar , Eliminar, Ver)
        Console.WriteLine("\n--- Gestión de Productos ---");
        Console.WriteLine("1. Ver inventario");
        // ... más opciones
        Console.Write("Seleccione una opción: ");
        switch(Console.ReadLine())
        {
            case "1":
                Console.WriteLine("\n--- Inventario de Productos ---");
                foreach(var producto in listaProductos)
                {
                    producto.Mostrar();
                }
                break;
        }
    }

    private static void RegistrarCompra()
    {
        Console.WriteLine("\n--- Registrar Nueva Compra ---");
        
        [cite_start]// 1. Seleccionar Cliente 
        Console.Write("Ingrese el ID del Cliente: ");
        string idCliente = Console.ReadLine();
        var cliente = listaClientes.Buscar(c => c.IdCliente == idCliente);
        if (cliente == null)
        {
            Console.WriteLine("Cliente no encontrado.");
            return;
        }

        var nuevoPedido = new Pedido
        {
            IdPedido = new Random().Next(10000010, 99999999), // Generar un ID de pedido aleatorio
            IdCliente = cliente.IdCliente,
            Fecha = DateTime.Now
        };

        bool agregandoProductos = true;
        while (agregandoProductos)
        {
            [cite_start]// 2. Seleccionar Productos 
            Console.Write("Ingrese el ID del producto a agregar (o 'fin' para terminar): ");
            string idProducto = Console.ReadLine();
            if (idProducto.ToLower() == "fin")
            {
                agregandoProductos = false;
                continue;
            }

            var producto = listaProductos.Buscar(p => p.IdProducto == idProducto);
            if (producto == null)
            {
                Console.WriteLine("Producto no encontrado.");
                continue;
            }

            Console.Write($"Cantidad (Stock disponible: {producto.Stock}): ");
            if (int.TryParse(Console.ReadLine(), out int cantidad) && cantidad > 0 && cantidad <= producto.Stock)
            {
                var detalle = new DetallePedido
                {
                    IdProducto = producto.IdProducto,
                    NombreProducto = producto.Nombre,
                    Cantidad = cantidad,
                    PrecioUnitario = producto.Precio
                };
                nuevoPedido.Detalles.Agregar(detalle);

                [cite_start]// 3. Actualizar Inventario 
                producto.Stock -= cantidad; 
                Console.WriteLine("Producto añadido al pedido.");
            }
            else
            {
                Console.WriteLine("Cantidad no válida o sin stock suficiente.");
            }
        }

        if (nuevoPedido.Detalles.Cabeza != null)
        {
            listaPedidos.Agregar(nuevoPedido);
            [cite_start]// 4. Mostrar detalles del pedido 
            Console.WriteLine("\n--- Pedido Registrado Exitosamente ---");
            MostrarDetalleDeUnPedido(nuevoPedido);
        }
        else
        {
            Console.WriteLine("No se agregaron productos. Pedido cancelado.");
        }
    }

    private static void VerPedidos()
    {
        Console.WriteLine("\n--- Historial de Pedidos ---");
        foreach (var pedido in listaPedidos)
            {
            var cliente = listaClientes.Buscar(c => c.IdCliente == pedido.IdCliente);
                Console.WriteLine("------------------------------------------");
                Console.WriteLine($"ID Pedido: {pedido.IdPedido} | Fecha: {pedido.Fecha:dd-MM-yyyy}");
                Console.WriteLine($"Cliente: {cliente?.Nombre} {cliente?.Apellido} ({pedido.IdCliente})");
                Console.WriteLine($"Total del Pedido: ${pedido.Total:F2}");
                Console.WriteLine("------------------------------------------");
            }
    }
    
    private static void MostrarDetalleDeUnPedido(Pedido pedido)
    {
        var cliente = listaClientes.Buscar(c => c.IdCliente == pedido.IdCliente);
        Console.WriteLine($"ID Pedido: {pedido.IdPedido}\t\tFecha: {pedido.Fecha:dd-MM-yyyy}");
        Console.WriteLine($"ID Cliente: {pedido.IdCliente}\nNombre: {cliente?.Nombre}\tApellido: {cliente?.Apellido}");
        Console.WriteLine("\n{0,-15} {1,-25} {2,10} {3,10} {4,12}", "ID Producto", "Nombre", "Precio", "Cantidad", "SubTotal");
        Console.WriteLine(new string('-', 75));
        
        foreach(var detalle in pedido.Detalles)
        {
            Console.WriteLine("{0,-15} {1,-25} {2,10:C2} {3,10} {4,12:C2}", detalle.IdProducto, detalle.NombreProducto, detalle.PrecioUnitario, detalle.Cantidad, detalle.SubTotal);
        }
        Console.WriteLine(new string('-', 75));
        Console.WriteLine($"TOTAL: {pedido.Total:C2}");
    }
}