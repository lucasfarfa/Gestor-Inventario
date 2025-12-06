using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor_Inventario.src.Inventario.CLI
{
    class Producto
    {
        private int id;
        private string nombre;
        private int stock;
        private decimal precio;

        public Producto(int id, string nombre, int stock, decimal precio)
        {
            this.id = id;
            this.nombre = nombre;
            this.stock = stock;
            this.precio = precio;
        }

        public override bool Equals(object? obj)
        {
            bool equals = false;
            if (obj != null && obj is Producto)
            {
                Producto otroProducto = (Producto)obj;
                equals = this.id == otroProducto.id;
            }
            return equals;
        }

        public override int GetHashCode()
        {
            return id.GetHashCode();
        }

        public override string ToString()
        {
            return $"ID: {id}, Nombre: {nombre}, Stock: {stock}, Precio: {precio}";
        }

    }
}
