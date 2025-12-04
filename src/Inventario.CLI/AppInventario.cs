using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor_Inventario.src.Inventario.CLI
{
    class AppInventario
    {
        private ServicioGestor servicioGestor;
        private ConsoleUI ui;
        public AppInventario()
        {
            servicioGestor = new ServicioGestor();
            ui = new ConsoleUI();
        }

        public void Run()
        {
            string opcion;

            do
            {
                ui.MostrarTitulo("Gestor de Inventario");
                opcion = ui.PedirStringNoVacio("Seleccione una opción:\n1. Agregar Producto\n2. Listar Productos\n3. Salir\nOpción:");

                switch (opcion)
                {
                    case "1":
                        //AgregarProducto();
                        break;
                    case "2":
                        //ListarProductos();
                        break;
                    case "3":
                        ui.MostrarExito("Saliendo del programa...");
                        break;
                    default:
                        ui.MostrarError("Opción no válida. Intente de nuevo.");
                        ui.PausarYLimpiar();
                        break;
                }
            } while (opcion != "3");
        }


    }
}
