using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor_Inventario.src.Inventario.CLI
{
    class AppInventario
    {
        private ServicioGestor service;
        private ConsoleUI ui;
        public AppInventario()
        {
            service = new ServicioGestor();
            ui = new ConsoleUI();
        }

        public void Run()
        {
            string opcion;

            do
            {
                ui.MostrarTitulo("Gestor de Inventario");
                opcion = ui.PedirStringNoVacio("Seleccione una opción:\n1. Agregar Producto\n2. Modificar Stock\n3. Listar Productos\n4. Salir\nOpción:");

                switch (opcion)
                {
                    case "1":
                        ValidaAltaProducto();
                        ui.PausarYLimpiar();
                        break;
                    case "2":
                        ValidaModificarStock();
                        ui.PausarYLimpiar();
                        break;
                    case "3":
                        ListarProductos();
                        ui.PausarYLimpiar();
                        break;
                    case "4":
                        GuardarPersistencia();
                        ui.MostrarExito("Saliendo del programa...");
                        break;
                    default:
                        ui.MostrarError("Opción no válida. Intente de nuevo.");
                        ui.PausarYLimpiar();
                        break;
                }
            } while (opcion != "4");
        }

        private void GuardarPersistencia()
        {
            if (string.IsNullOrEmpty(service.ListadoProductos()))
            {
                ui.MostrarError("No se guardan productos porque no se cargó nada.");
            }
            else
            {
                service.GuardaProductos();
            }
        }

        private void ValidaAltaProducto()
        {
            ui.MostrarTitulo("alta de producto");

            int id = 0; ;
            do
            {
                id = ui.PedirEnteroPositivo("Ingrese el ID del producto:");
                if (service.ExisteProducto(id))
                {
                    ui.MostrarError("Error! El ID ya existe. Ingrese un ID diferente.");
                    id = 0;
                }
            } while (id == 0);
            
            string nombre = ui.PedirStringNoVacio("Ingrese el nombre del producto:");
            int stock = ui.PedirEnteroPositivo("Ingrese el stock del producto:");
            decimal precio = ui.PedirDecimal("Ingrese el precio del producto:");

            service.AgregarProducto(id, nombre, stock, precio);
            ui.MostrarExito("Producto agregado exitosamente!");
        }
        private void ValidaModificarStock()
        {
            if (string.IsNullOrEmpty(service.ListadoProductos()))
            {
                ui.MostrarError("No hay productos registrados.");
                ui.PausarYLimpiar();
            }
            else
            {
                ui.MostrarTitulo("Modificar Stock");

                int id = 0; ;
                do
                {
                    id = ui.PedirEnteroPositivo("Ingrese el ID del producto:");
                    if (!service.ExisteProducto(id))
                    {
                        ui.MostrarError("Error! El producto ingresado no existe.");
                        id = 0;
                    }
                } while (id == 0);

                int nuevoStock = ui.PedirEnteroPositivo("Ingrese el nuevo stock del producto:");
                service.ModificarStock(id, nuevoStock);
                ui.MostrarExito("Stock modificado con exito!");
            }

        }
        private void ListarProductos()
        {
            if (string.IsNullOrEmpty(service.ListadoProductos()))
            {
                ui.MostrarError("No hay productos registrados.");
                
            }
            else
            {
                ui.MostrarTitulo("Listado de Productos");
                ui.MostrarExito(service.ListadoProductos());
            }
        }

    }
}
