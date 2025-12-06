using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor_Inventario.src.Inventario.CLI
{
    class Producto
    {
        // JSON Serializer me pide si o si getters y setters publicos
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public Producto(int id, string nombre, int stock, decimal precio)
        {
            this.Id = id;
            this.Nombre = nombre;
            this.Stock = stock;
            this.Precio = precio;
        }

        public override bool Equals(object? obj)
        {
            bool equals = false;
            if (obj != null && obj is Producto)
            {
                Producto otroProducto = (Producto)obj;
                equals = this.Id == otroProducto.Id;
            }
            return equals;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"ID: {Id}, Nombre: {Nombre}, Stock: {Stock}, Precio: {Precio}";
        }

    }
}
