using FeriaAgricultor.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeriaAgricultor.Core.Data
{
    public static class InicializadorDatos
    {
        // Método estático para cargar datos falsos si el archivo está vacío
        public static void Inicializar(IRepositorio<Usuario> repoUsuarios, IRepositorio<Producto> repoProductos, IRepositorio<Orden> repoOrdenes)
        {
            // 1. Cargar Usuarios 
            if (repoUsuarios.ObtenerTodos().Any()) return;
            var random = new Random();

            {
                // Crear 50 usuarios compradores
                for (int i = 1; i <= 50; i++)
                {
                    repoUsuarios.Agregar(new Usuario
                    {
                        Id = i,
                        NombreCompleto = $"Cliente {i}",
                        Correo = $"cliente{i}@prueba.com",
                        Clave = "123", // Clave genérica para pruebas
                        EsProductor = false,
                        DireccionesGuardadas = new List<string> { "San José, Centro", "Heredia, Centro" }
                    });
                }

                // Crear administrador 
                repoUsuarios.Agregar(new Usuario
                {
                    Id = 999, // Un ID especial
                    NombreCompleto = "Administrador General",
                    Correo = "admin@feria.com",
                    Clave = "admin",
                    EsProductor = true // Esto le da permiso de ver todos los reportes
                });

                // Crear 30 Productores en 5 Ferias distintas 
                string[] ferias = { "Feria del Agricultor Zapote", "Feria del Agricultor Heredia", "Feria del Agricultor Alajuela", "Feria Verde Aranjuez", "Feria del Agricultor Santa Ana" };

                List<int> idsProductores = new List<int>();

                for (int i = 0; i < 30; i++)
                {
                    int idProductor = 100 + i;
                    idsProductores.Add(idProductor);

                    // Asignamos una feria aleatoria a este productor
                    string feriaAsignada = ferias[random.Next(ferias.Length)];

                    repoUsuarios.Agregar(new Usuario
                    {
                        Id = idProductor,
                        NombreCompleto = $"Productor {i + 1} - {feriaAsignada}", // Aquí se visualiza el punto de feria
                        Correo = $"productor{i + 1}@feria.com",
                        Clave = "admin123",
                        EsProductor = true
                    });
                }



                string[] verduras = { "Papas", "Cebollas", "Zanahorias", "Vainicas", "Camote", "Yuca", "Ñampí", "Ayote", "Chayote", "Elote" };
                string[] frutas = { "Fresas", "Moras", "Piña", "Sandía", "Papaya", "Banano", "Mango", "Cas", "Guanábana", "Limón" };
                string[] otros = { "Huevos de Pastoreo", "Queso Turrialba", "Natilla Casera", "Miel de Abeja", "Pan Artesanal" };

                var todosLosProductos = new List<string>();
                todosLosProductos.AddRange(verduras);
                todosLosProductos.AddRange(frutas);
                todosLosProductos.AddRange(otros);

                // Generamos inventario para cada uno de los 30 productores
                foreach (int idProd in idsProductores)
                {
                    // Cada productor tendrá entre 5 y 10 productos diferentes
                    int cantidadProductos = random.Next(5, 11);

                    for (int j = 0; j < cantidadProductos; j++)
                    {
                        string nombreProducto = todosLosProductos[random.Next(todosLosProductos.Count)];

                        repoProductos.Agregar(new Producto
                        {
                            Nombre = nombreProducto,
                            Precio = random.Next(5, 80) * 100, // Precios entre 500 y 8000 colones
                            Unidad = (UnidadMedida)random.Next(0, 4),
                            CantidadStock = random.Next(50, 500), // Bastante stock inicial
                            IdProductor = idProd
                        });
                    }
                }

                // Obtenemos lo que acabamos de crear para relacionarlo
                var listaProductos = repoProductos.ObtenerTodos();
                var listaClientes = repoUsuarios.ObtenerTodos().Where(u => !u.EsProductor).ToList();

                // Solo creamos órdenes si logramos crear productos y usuarios antes
                if (listaProductos.Count > 0 && listaClientes.Count > 0)
                {
                    for (int i = 1; i <= 100; i++)
                    {
                        // 1. Cliente al azar
                        var cliente = listaClientes[random.Next(listaClientes.Count)];

                        // 2. Fecha al azar (últimos 6 meses)
                        DateTime fechaOrden = DateTime.Now.AddDays(-random.Next(0, 180));

                        // 3. Crear detalles (Items de la factura)
                        var detalles = new List<DetalleOrden>();
                        int numeroItems = random.Next(1, 6); // Entre 1 y 5 productos por orden
                        decimal totalOrden = 0;

                        for (int k = 0; k < numeroItems; k++)
                        {
                            var prod = listaProductos[random.Next(listaProductos.Count)];
                            int cantidadComprada = random.Next(1, 5);

                            detalles.Add(new DetalleOrden
                            {
                                IdProducto = prod.Id,
                                NombreProducto = prod.Nombre,
                                PrecioUnitario = prod.Precio,
                                Cantidad = cantidadComprada
                            });
                            totalOrden += (prod.Precio * cantidadComprada);
                        }

                        // 4. Guardar la orden
                        repoOrdenes.Agregar(new Orden
                        {
                            Id = 1000 + i,
                            IdUsuario = cliente.Id,
                            Fecha = fechaOrden,
                            Detalles = detalles,
                            Total = totalOrden,
                            DireccionEntrega = "Dirección Generada Automáticamente"
                        });
                    }
                }

            }
        }
    }
}
