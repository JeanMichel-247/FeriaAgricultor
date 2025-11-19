using FeriaAgricultor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeriaAgricultor.Data
{
    public static class InicializadorDatos
    {
        // Método estático para cargar datos falsos si el archivo está vacío
        public static void Inicializar(IRepositorio<Usuario> repoUsuarios, IRepositorio<Producto> repoProductos)
        {
            // 1. Cargar Usuarios 
            if (!repoUsuarios.ObtenerTodos().Any())
            {
                // Crear 50 usuarios compradores
                for (int i = 1; i <= 50; i++)
                {
                    repoUsuarios.Agregar(new Usuario
                    {
                        NombreCompleto = $"Comprador {i}",
                        Correo = $"usuario{i}@prueba.com",
                        Clave = "123456",
                        EsProductor = false
                    });
                }

                // --- 5 PRODUCTORES (Del ID 51 al 55) ---
                string[] nombresFincas = { "Finca Los Pinos", "Huerta Doña Maria", "Granja El Sol", "Organicos del Valle", "Frutas Don Pedro" };

                for (int i = 0; i < 5; i++)
                {
                    repoUsuarios.Agregar(new Usuario
                    {
                        Id = 51 + i, // Forzamos IDs 51, 52, 53, 54, 55
                        NombreCompleto = nombresFincas[i],
                        Correo = $"ventas{i + 1}@feria.com",
                        Clave = "admin123",
                        EsProductor = true
                    });
                }
            }

            // 2. Cargar Productos 
            if (!repoProductos.ObtenerTodos().Any())
            {
                // Vamos a crear 3 productos para cada uno de los 5 productores
                var random = new System.Random();
                string[] listaProductos = { "Papas", "Cebollas", "Tomates", "Lechuga", "Zanahoria", "Fresas", "Moras", "Sandía", "Piña", "Yuca", "Ñampí", "Plátano" };

                for (int i = 0; i < 15; i++) // Crear 15 productos en total
                {
                    // Asigna aleatoriamente a uno de los 5 productores (IDs 51-55)
                    int idProductorRandom = random.Next(51, 56);

                    repoProductos.Agregar(new Producto
                    {
                        Nombre = listaProductos[random.Next(listaProductos.Length)],
                        Precio = random.Next(5, 50) * 100, // Precios entre 500 y 5000
                        Unidad = (UnidadMedida)random.Next(0, 4),
                        CantidadStock = random.Next(10, 100),
                        IdProductor = idProductorRandom
                    });
                }
            }
        }
    }
}
