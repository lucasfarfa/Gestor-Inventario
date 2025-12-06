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
        public ServicioGestor()
        {
            listadoProductos = new Dictionary<int, Producto>();
            // al crear servicio gestor, levantar datos de .json si existen
            // TODO
        }

        public void AgregarProducto(int id, string nombre, int stock, decimal precio)
        {
            listadoProductos.Add(id, new Producto(id, nombre, stock, precio));
            // grabado log de auditoria sincronico
            // TODO
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

                foreach (var producto in listadoProductos.Values)
                {
                    sb.AppendLine(producto.ToString() + "\n");
                }
                retorno = sb.ToString();
            }

            return retorno;
        }
    }
}
