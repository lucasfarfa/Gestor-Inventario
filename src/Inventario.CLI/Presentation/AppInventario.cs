using Gestor_Inventario.src.Inventario.CLI.Domain;

namespace Gestor_Inventario.src.Inventario.CLI.Presentation
{
    class AppInventario
    {

        private readonly ServicioGestor service;
        private readonly ConsoleUI ui;
                
        public AppInventario()
        {
            /* modificar aca si se cambia el repositorio ejemplo a SQL, despues no hace falta tocar nada mas */
            IRepositorio repo = new Persistence.RepositorioJSON();
            ILogger logger = new Persistence.LogTXT();

            service = new ServicioGestor(repo, logger);
            ui = new ConsoleUI();
        } 

        public async Task Run()
        {
            await service.InicializarAsync(); // levanto datos del JSON al iniciar app
            string opcion;

            do
            {
                ui.MostrarTitulo("Gestor de Inventario");
                opcion = ui.PedirStringNoVacio("Seleccione una opción:\n1. Agregar Producto\n2. Modificar Stock\n3. Listar Productos\n4. Salir\nOpción:");

                switch (opcion)
                {
                    case "1":
                        await ValidaAltaProducto();
                        ui.PausarYLimpiar();
                        break;
                    case "2":
                        await ValidaModificarStock();
                        ui.PausarYLimpiar();
                        break;
                    case "3":
                        ListarProductos();
                        ui.PausarYLimpiar();
                        break;
                    case "4":
                        await GuardarPersistencia();
                        ui.MostrarExito("Saliendo del programa...");
                        break;
                    default:
                        ui.MostrarError("Opción no válida. Intente de nuevo.");
                        ui.PausarYLimpiar();
                        break;
                }
            } while (opcion != "4");
        }

        private async Task GuardarPersistencia()
        {
            try
            {
                await service.GuardaProductosAsync();
            } catch (ArgumentException ex)
            {
                ui.MostrarError(ex.Message);
            } catch (Exception ex)
            {
                ui.MostrarError("Error inesperado al guardar productos: " + ex.Message);
            }   
        }

        private async Task ValidaAltaProducto()
        {

            ui.MostrarTitulo("alta de producto");
            int id = ui.PedirEnteroPositivo("Ingrese el ID del producto:");
            string nombre = ui.PedirStringNoVacio("Ingrese el nombre del producto:");
            int stock = ui.PedirEnteroPositivo("Ingrese el stock del producto:");
            decimal precio = ui.PedirDecimal("Ingrese el precio del producto:");
                
            try
            {
                await service.AgregarProductoAsync(id, nombre, stock, precio);
                // en el try mantengo lo que quiero hacer si todo sale bien
                ui.MostrarExito("Producto agregado exitosamente!");
            }
            catch (ArgumentException ex)
            {
                ui.MostrarError(ex.Message);
            }
            catch (Exception ex)
            {
                ui.MostrarError("Error inesperado al agregar producto: " + ex.Message);
            }   
        }
        private async Task ValidaModificarStock()
        {            
            ui.MostrarTitulo("Modificar Stock");
            int id = ui.PedirEnteroPositivo("Ingrese el ID del producto:");
            int nuevoStock = ui.PedirEnteroPositivo("Ingrese el nuevo stock del producto:");

            try
            {
                await service.ModificarStockAsync(id, nuevoStock);
                ui.MostrarExito("Stock modificado con exito!");
            }
            catch (InvalidOperationException e)
            {
                ui.MostrarError(e.Message);
            }
            catch (KeyNotFoundException e)
            {
                ui.MostrarError(e.Message);
            }          
        }
        private void ListarProductos()
        {
            try
            {
                ui.MostrarTitulo("Listado de Productos");
                ui.MostrarExito(service.ListadoProductos());
            }
            catch (InvalidOperationException ex)
            {
                ui.MostrarError(ex.Message);

            }
            catch (Exception ex)
            {
                ui.MostrarError("Error inesperado al listar productos: " + ex.Message);
            }
        }
    }
}
