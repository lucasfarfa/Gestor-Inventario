using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor_Inventario.src.Inventario.CLI
{
    class ServicioGestor
    {
        private Dictionary<int, Producto> listadoProductos;
        private Persistencia persistencia;
        public ServicioGestor()
        {
            // al crear servicio gestor, levantar datos de .json si existen
            persistencia = new Persistencia();
            listadoProductos = persistencia.CargarDatos();
        }

        public async Task AgregarProductoAsync(int id, string nombre, int stock, decimal precio)
        {
            if ( listadoProductos.ContainsKey(id))
            {
                throw new ArgumentException($"El producto con ID {id} ya existe.");
            }

            if (precio < 0)
            {
                throw new ArgumentException("El precio no puede ser negativo.");
            }

            listadoProductos.Add(id, new Producto(id, nombre, stock, precio));

            await Logger.GrabarLogAsync($"Se agregó producto ID:{id}, Nombre:{nombre}, Stock:{stock}, Precio:{precio}");
        }

        public string ListadoProductos()
        {
            string retorno;

            if (listadoProductos == null || listadoProductos.Count == 0) // chequeo null primero asi no revienta el compilador
            {
                throw new InvalidOperationException("No hay productos cargados.");
            }
            else
            {
                const int ESTIMADO_POR_PRODUCTO = 100;
                int capacidadInicial = listadoProductos.Count * ESTIMADO_POR_PRODUCTO; // optimizacion de memoria para StringBuilder
                StringBuilder sb = new StringBuilder(capacidadInicial);

                foreach (Producto producto in listadoProductos.Values)
                {
                    sb.AppendLine(producto.ToString() + "\n");
                }
                retorno = sb.ToString();
            }

            return retorno;
        }

        public async Task ModificarStockAsync(int id, int nuevoStock)
        {
            if (listadoProductos == null || listadoProductos.Count == 0)
            {
                throw new InvalidOperationException("No hay productos cargados.");
            }

            if (!listadoProductos.ContainsKey(id))
            {
                throw new KeyNotFoundException($"El producto con ID {id} no existe.");
            }

            Producto producto = listadoProductos[id];
            producto.Stock = nuevoStock;
            await Logger.GrabarLogAsync($"Se modificó stock del producto ID:{id}, Nuevo Stock:{nuevoStock}");
        }

        public async Task GuardaProductosAsync()
        {
            try
            {
                await persistencia.GuardarDatosAsync(listadoProductos);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException("Error al guardar los productos: " + ex.Message);
            }
        }
    }

}
