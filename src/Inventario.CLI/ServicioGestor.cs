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

        public void AgregarProducto(int id, string nombre, int stock, decimal precio)
        {
            listadoProductos.Add(id, new Producto(id, nombre, stock, precio));

            // grabado log de auditoria sincronico
            Logger.GrabarLog($"Se agregó producto ID:{id}, Nombre:{nombre}, Stock:{stock}, Precio:{precio}");
        }

        public bool ExisteProducto(int id)
        {
            return listadoProductos.ContainsKey(id);
        }

        public string ListadoProductos()
        {
            string retorno;

            if (listadoProductos == null || listadoProductos.Count == 0) // chequeo null primero asi no revienta el compilador
            {
                retorno = string.Empty;
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

        public void ModificarStock(int id, int nuevoStock)
        {
            Producto producto = listadoProductos[id];
            producto.Stock = nuevoStock;
            Logger.GrabarLog($"Se modificó stock del producto ID:{id}, Nuevo Stock:{nuevoStock}");
        }

        public void GuardaProductos()
        {
            persistencia.GuardarDatos(listadoProductos);
        }
    }
}
