namespace Gestor_Inventario.src.Inventario.CLI.Domain
{
    class Producto
    {
        // JSON Serializer me pide si o si getters publicos
        public int Id { get; }
        public string Nombre { get; }
        public decimal Precio { get; }
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
