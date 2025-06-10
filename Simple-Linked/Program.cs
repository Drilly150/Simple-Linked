using ProyectoLES.Modelos;
using ProyectoLES.EstructurasDeDatos;
using System.Text.RegularExpressions;
using System.Collections.Generic; // Asegúrate de tener esta directiva para las listas

public class Program
{
    // Declaración de las listas enlazadas para toda la aplicación
    private static readonly ListaEnlazada<Cliente> listaClientes = new ListaEnlazada<Cliente>();
    private static readonly ListaEnlazada<Producto> listaProductos = new ListaEnlazada<Producto>();
    private static readonly ListaEnlazada<Pedido> listaPedidos = new ListaEnlazada<Pedido>();

    static void Main(string[] args)
    {
        // Se puede habilitar la comprobación de nulidad para el proyecto entero en el .csproj con <Nullable>enable</Nullable>
        // Si no se hace, se puede deshabilitar localmente con #nullable disable o usando operadores ? y !

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
                    RegistrarNuevaCompra();
                    break;
                case "4":
                    MenuPedidos();
                    break;
                case "5":
                    Console.WriteLine("Saliendo del programa. ¡Hasta pronto!");
                    salir = true;
                    break;
                default:
                    Console.WriteLine("Opción no válida. Intente de nuevo.");
                    break;
            }
        }
    }

    static bool Login()
    {
        Console.WriteLine("--- INICIO DE SESIÓN ---");
        int intentos = 3;
        while (intentos > 0)
        {
            Console.Write("Usuario: ");
            string? usuario = Console.ReadLine(); // Puede ser null

            Console.Write("Contraseña: ");
            string? password = Console.ReadLine(); // Puede ser null

            // Ejemplo de credenciales simples
            if (usuario == "Estudiante" && password == "123456")
            {
                return true;
            }
            else
            {
                Console.WriteLine("Credenciales incorrectas. Intentos restantes: " + --intentos);
            }
        }
        return false;
    }

    static void PreCargarDatos()
    {
        // Clientes de ejemplo (asegúrate de que las propiedades 'required' estén inicializadas)
        listaClientes.Agregar(new Cliente { IdCliente = "CLI001", Nombre = "Juan", Apellido = "Pérez", Telefono = "123456789", Email = "juan@example.com" });
        listaClientes.Agregar(new Cliente { IdCliente = "CLI002", Nombre = "María", Apellido = "Gómez", Telefono = "987654321", Email = "maria@example.com" });
        listaClientes.Agregar(new Cliente { IdCliente = "CLI003", Nombre = "Carlos", Apellido = "Rodríguez", Telefono = "555112233", Email = "carlos@example.com" });

        // Productos de ejemplo (asegúrate de que las propiedades 'required' estén inicializadas)
        listaProductos.Agregar(new Producto { IdProducto = "PROD001", Nombre = "Laptop Dell XPS", Precio = 1200.00, Stock = 50 });
        listaProductos.Agregar(new Producto { IdProducto = "PROD002", Nombre = "Teclado Mecánico RGB", Precio = 85.50, Stock = 120 });
        listaProductos.Agregar(new Producto { IdProducto = "PROD003", Nombre = "Monitor Curvo 27 pulgadas", Precio = 350.00, Stock = 75 });
        listaProductos.Agregar(new Producto { IdProducto = "PROD004", Nombre = "Mouse Inalámbrico", Precio = 25.00, Stock = 200 });

        // Pedidos de ejemplo
        var pedido1 = new Pedido { IdPedido = 1, IdCliente = "CLI001", Fecha = DateTime.Now };
        pedido1.Detalles.Agregar(new DetallePedido { IdProducto = "PROD001", NombreProducto = "Laptop Dell XPS", Cantidad = 1, PrecioUnitario = 1200.00 });
        pedido1.Detalles.Agregar(new DetallePedido { IdProducto = "PROD002", NombreProducto = "Teclado Mecánico RGB", Cantidad = 2, PrecioUnitario = 85.50 });
        listaPedidos.Agregar(pedido1);

        var pedido2 = new Pedido { IdPedido = 2, IdCliente = "CLI002", Fecha = DateTime.Now.AddDays(-5) };
        pedido2.Detalles.Agregar(new DetallePedido { IdProducto = "PROD003", NombreProducto = "Monitor Curvo 27 pulgadas", Cantidad = 1, PrecioUnitario = 350.00 });
        pedido2.Detalles.Agregar(new DetallePedido { IdProducto = "PROD004", NombreProducto = "Mouse Inalámbrico", Cantidad = 3, PrecioUnitario = 25.00 });
        listaPedidos.Agregar(pedido2);
    }

    static void MenuClientes()
    {
        bool volver = false;
        while (!volver)
        {
            Console.WriteLine("\n--- GESTIÓN DE CLIENTES ---");
            Console.WriteLine("1. Ver todos los clientes");
            Console.WriteLine("2. Añadir nuevo cliente");
            Console.WriteLine("3. Buscar cliente por ID");
            Console.WriteLine("4. Eliminar cliente por ID");
            Console.WriteLine("5. Volver al menú principal");
            Console.Write("Seleccione una opción: ");

            switch (Console.ReadLine())
            {
                case "1":
                    MostrarTodosLosClientes();
                    break;
                case "2":
                    AnadirNuevoCliente();
                    break;
                case "3":
                    BuscarClientePorId();
                    break;
                case "4":
                    EliminarClientePorId();
                    break;
                case "5":
                    volver = true;
                    break;
                default:
                    Console.WriteLine("Opción no válida. Intente de nuevo.");
                    break;
            }
        }
    }

    static void MostrarTodosLosClientes()
    {
        Console.WriteLine("\n--- LISTA DE CLIENTES ---");
        if (listaClientes.Cabeza == null)
        {
            Console.WriteLine("No hay clientes registrados.");
            return;
        }

        foreach (var cliente in listaClientes)
        {
            cliente.Mostrar();
        }
    }

    static void AnadirNuevoCliente()
    {
        Console.WriteLine("\n--- AÑADIR NUEVO CLIENTE ---");
        Console.Write("ID del Cliente: ");
        string? idCliente = Console.ReadLine(); // Puede ser null

        if (string.IsNullOrWhiteSpace(idCliente))
        {
            Console.WriteLine("El ID del cliente no puede estar vacío.");
            return;
        }

        if (listaClientes.Buscar(c => c.IdCliente == idCliente) != null) // Comprobar si existe antes
        {
            Console.WriteLine($"Ya existe un cliente con el ID: {idCliente}");
            return;
        }

        Console.Write("Nombre: ");
        string? nombre = Console.ReadLine();

        Console.Write("Apellido: ");
        string? apellido = Console.ReadLine();

        Console.Write("Teléfono: ");
        string? telefono = Console.ReadLine();

        Console.Write("Email: ");
        string? email = Console.ReadLine();

        // Asegúrate de que las propiedades 'required' se inicialicen
        listaClientes.Agregar(new Cliente
        {
            IdCliente = idCliente,
            Nombre = nombre ?? string.Empty, // Coalesce null a string.Empty
            Apellido = apellido ?? string.Empty,
            Telefono = telefono ?? string.Empty,
            Email = email ?? string.Empty
        });
        Console.WriteLine("Cliente añadido exitosamente.");
    }

    static void BuscarClientePorId()
    {
        Console.WriteLine("\n--- BUSCAR CLIENTE ---");
        Console.Write("Ingrese el ID del cliente a buscar: ");
        string? idBuscar = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(idBuscar))
        {
            Console.WriteLine("El ID no puede estar vacío.");
            return;
        }

        Cliente? clienteEncontrado = listaClientes.Buscar(c => c.IdCliente == idBuscar); // Puede retornar null
        if (clienteEncontrado != null)
        {
            Console.WriteLine("Cliente encontrado:");
            clienteEncontrado.Mostrar();
        }
        else
        {
            Console.WriteLine($"Cliente con ID '{idBuscar}' no encontrado.");
        }
    }

    static void EliminarClientePorId()
    {
        Console.WriteLine("\n--- ELIMINAR CLIENTE ---");
        Console.Write("Ingrese el ID del cliente a eliminar: ");
        string? idEliminar = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(idEliminar))
        {
            Console.WriteLine("El ID no puede estar vacío.");
            return;
        }

        // Primero verifica si el cliente existe para dar un mejor mensaje
        if (listaClientes.Buscar(c => c.IdCliente == idEliminar) == null)
        {
            Console.WriteLine($"No existe un cliente con el ID: {idEliminar}");
            return;
        }

        listaClientes.Eliminar(c => c.IdCliente == idEliminar);
        Console.WriteLine($"Cliente con ID '{idEliminar}' eliminado si existía.");
    }

    static void MenuProductos()
    {
        bool volver = false;
        while (!volver)
        {
            Console.WriteLine("\n--- GESTIÓN DE PRODUCTOS ---");
            Console.WriteLine("1. Ver todos los productos");
            Console.WriteLine("2. Añadir nuevo producto");
            Console.WriteLine("3. Buscar producto por ID");
            Console.WriteLine("4. Eliminar producto por ID");
            Console.WriteLine("5. Volver al menú principal");
            Console.Write("Seleccione una opción: ");

            switch (Console.ReadLine())
            {
                case "1":
                    MostrarTodosLosProductos();
                    break;
                case "2":
                    AnadirNuevoProducto();
                    break;
                case "3":
                    BuscarProductoPorId();
                    break;
                case "4":
                    EliminarProductoPorId();
                    break;
                case "5":
                    volver = true;
                    break;
                default:
                    Console.WriteLine("Opción no válida. Intente de nuevo.");
                    break;
            }
        }
    }

    static void MostrarTodosLosProductos()
    {
        Console.WriteLine("\n--- LISTA DE PRODUCTOS ---");
        if (listaProductos.Cabeza == null)
        {
            Console.WriteLine("No hay productos registrados.");
            return;
        }

        foreach (var producto in listaProductos)
        {
            producto.Mostrar();
        }
    }

    static void AnadirNuevoProducto()
    {
        Console.WriteLine("\n--- AÑADIR NUEVO PRODUCTO ---");
        Console.Write("ID del Producto: ");
        string? idProducto = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(idProducto))
        {
            Console.WriteLine("El ID del producto no puede estar vacío.");
            return;
        }

        if (listaProductos.Buscar(p => p.IdProducto == idProducto) != null)
        {
            Console.WriteLine($"Ya existe un producto con el ID: {idProducto}");
            return;
        }

        Console.Write("Nombre: ");
        string? nombre = Console.ReadLine();

        Console.Write("Precio: ");
        double precio;
        while (!double.TryParse(Console.ReadLine(), out precio) || precio < 0)
        {
            Console.WriteLine("Precio inválido. Por favor, ingrese un número positivo:");
        }

        Console.Write("Stock: ");
        int stock;
        while (!int.TryParse(Console.ReadLine(), out stock) || stock < 0)
        {
            Console.WriteLine("Stock inválido. Por favor, ingrese un número entero positivo:");
        }

        listaProductos.Agregar(new Producto
        {
            IdProducto = idProducto,
            Nombre = nombre ?? string.Empty, // Coalesce null
            Precio = precio,
            Stock = stock
        });
        Console.WriteLine("Producto añadido exitosamente.");
    }

    static void BuscarProductoPorId()
    {
        Console.WriteLine("\n--- BUSCAR PRODUCTO ---");
        Console.Write("Ingrese el ID del producto a buscar: ");
        string? idBuscar = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(idBuscar))
        {
            Console.WriteLine("El ID no puede estar vacío.");
            return;
        }

        Producto? productoEncontrado = listaProductos.Buscar(p => p.IdProducto == idBuscar);
        if (productoEncontrado != null)
        {
            Console.WriteLine("Producto encontrado:");
            productoEncontrado.Mostrar();
        }
        else
        {
            Console.WriteLine($"Producto con ID '{idBuscar}' no encontrado.");
        }
    }

    static void EliminarProductoPorId()
    {
        Console.WriteLine("\n--- ELIMINAR PRODUCTO ---");
        Console.Write("Ingrese el ID del producto a eliminar: ");
        string? idEliminar = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(idEliminar))
        {
            Console.WriteLine("El ID no puede estar vacío.");
            return;
        }

        if (listaProductos.Buscar(p => p.IdProducto == idEliminar) == null)
        {
            Console.WriteLine($"No existe un producto con el ID: {idEliminar}");
            return;
        }

        listaProductos.Eliminar(p => p.IdProducto == idEliminar);
        Console.WriteLine($"Producto con ID '{idEliminar}' eliminado si existía.");
    }

    static void RegistrarNuevaCompra()
    {
        Console.WriteLine("\n--- REGISTRAR NUEVA COMPRA ---");

        Console.Write("ID del Cliente para el pedido: ");
        string? idCliente = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(idCliente))
        {
            Console.WriteLine("El ID del cliente no puede estar vacío.");
            return;
        }

        Cliente? clienteExistente = listaClientes.Buscar(c => c.IdCliente == idCliente);
        if (clienteExistente == null)
        {
            Console.WriteLine("Cliente no encontrado. Por favor, registre el cliente primero.");
            return;
        }

        // Generar un ID de pedido único (simple, puedes mejorarlo)
        int nuevoIdPedido = listaPedidos.Cabeza == null ? 1 : listaPedidos.Max(p => p.IdPedido) + 1;

        Pedido nuevoPedido = new Pedido
        {
            IdPedido = nuevoIdPedido,
            IdCliente = idCliente,
            Fecha = DateTime.Now,
            Detalles = new ListaEnlazada<DetallePedido>() // Se inicializa en el constructor de Pedido, pero es bueno ser explícito
        };

        bool agregarMasProductos = true;
        while (agregarMasProductos)
        {
            Console.Write("Ingrese el ID del producto a añadir (o 'fin' para terminar): ");
            string? idProducto = Console.ReadLine();

            if (idProducto?.ToLower() == "fin")
            {
                agregarMasProductos = false;
                continue;
            }

            if (string.IsNullOrWhiteSpace(idProducto))
            {
                Console.WriteLine("El ID del producto no puede estar vacío.");
                continue;
            }

            Producto? productoExistente = listaProductos.Buscar(p => p.IdProducto == idProducto);
            if (productoExistente == null)
            {
                Console.WriteLine("Producto no encontrado.");
                continue;
            }

            Console.Write($"Cantidad de '{productoExistente.Nombre}' a añadir: ");
            int cantidad;
            while (!int.TryParse(Console.ReadLine(), out cantidad) || cantidad <= 0 || cantidad > productoExistente.Stock)
            {
                Console.WriteLine($"Cantidad inválida. Stock disponible: {productoExistente.Stock}. Ingrese un número positivo y menor o igual al stock:");
            }

            // Crear el DetallePedido con todas las propiedades required
            DetallePedido nuevoDetalle = new DetallePedido
            {
                IdProducto = productoExistente.IdProducto,
                NombreProducto = productoExistente.Nombre, // Se añade la propiedad requerida
                Cantidad = cantidad,
                PrecioUnitario = productoExistente.Precio
            };
            nuevoPedido.Detalles.Agregar(nuevoDetalle);

            // Actualizar stock del producto
            productoExistente.Stock -= cantidad;

            Console.WriteLine($"'{productoExistente.Nombre}' (x{cantidad}) añadido al pedido.");
        }

        if (nuevoPedido.Detalles.Cabeza != null)
        {
            listaPedidos.Agregar(nuevoPedido);
            Console.WriteLine($"Pedido {nuevoPedido.IdPedido} registrado exitosamente para el cliente {clienteExistente.Nombre} {clienteExistente.Apellido}. Total: ${nuevoPedido.Total:F2}");
        }
        else
        {
            Console.WriteLine("No se añadieron productos al pedido. El pedido no se registró.");
        }
    }

    static void MenuPedidos()
    {
        bool volver = false;
        while (!volver)
        {
            Console.WriteLine("\n--- GESTIÓN DE PEDIDOS ---");
            Console.WriteLine("1. Ver todos los pedidos");
            Console.WriteLine("2. Ver detalle de un pedido por ID");
            Console.WriteLine("3. Volver al menú principal");
            Console.Write("Seleccione una opción: ");

            switch (Console.ReadLine())
            {
                case "1":
                    MostrarTodosLosPedidos();
                    break;
                case "2":
                    VerDetalleDeUnPedidoPorId();
                    break;
                case "3":
                    volver = true;
                    break;
                default:
                    Console.WriteLine("Opción no válida. Intente de nuevo.");
                    break;
            }
        }
    }

    static void MostrarTodosLosPedidos()
    {
        Console.WriteLine("\n--- LISTA DE PEDIDOS ---");
        if (listaPedidos.Cabeza == null)
        {
            Console.WriteLine("No hay pedidos registrados.");
            return;
        }

        foreach (var pedido in listaPedidos)
        {
            // Se asume que MostrarResumenPedido es un método auxiliar
            MostrarResumenPedido(pedido);
        }
    }

    private static void MostrarResumenPedido(Pedido pedido)
    {
        // Se añade el operador ? a cliente para evitar CS8602 si cliente fuera null
        var cliente = listaClientes.Buscar(c => c.IdCliente == pedido.IdCliente);
        Console.WriteLine("------------------------------------------");
        Console.WriteLine($"ID Pedido: {pedido.IdPedido} | Fecha: {pedido.Fecha:dd-MM-yyyy}");
        Console.WriteLine($"Cliente: {cliente?.Nombre} {cliente?.Apellido} ({pedido.IdCliente})");
        Console.WriteLine($"Total del Pedido: ${pedido.Total:F2}");
        Console.WriteLine("------------------------------------------");
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
        Console.WriteLine($"Total del Pedido: ${pedido.Total:F2}");
    }

    static void VerDetalleDeUnPedidoPorId()
    {
        Console.WriteLine("\n--- VER DETALLE DE PEDIDO ---");
        Console.Write("Ingrese el ID del pedido a ver: ");
        string? idStr = Console.ReadLine();

        if (!int.TryParse(idStr, out int idPedido))
        {
            Console.WriteLine("ID de pedido inválido.");
            return;
        }

        Pedido? pedidoEncontrado = listaPedidos.Buscar(p => p.IdPedido == idPedido);
        if (pedidoEncontrado != null)
        {
            Console.WriteLine("Detalle del pedido:");
            MostrarDetalleDeUnPedido(pedidoEncontrado);
        }
        else
        {
            Console.WriteLine($"Pedido con ID '{idPedido}' no encontrado.");
        }
    }
}