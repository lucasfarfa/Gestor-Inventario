using System.Text;

namespace Gestor_Inventario.src.Inventario.CLI.Domain
{
    class ServicioGestor
    {
        private Dictionary<int, Producto> _listadoProductos;
        private readonly IRepositorio _repo;
        private readonly ILogger _logger;
        public ServicioGestor(IRepositorio repo, ILogger logger)
        {
            _repo = repo;
            _logger = logger;
            _listadoProductos = new Dictionary<int, Producto>();
        }

        public async Task InicializarAsync()
        {
            _listadoProductos = await _repo.CargarDatos();
        }

        public async Task AgregarProductoAsync(int id, string nombre, int stock, decimal precio)
        {
            if (_listadoProductos.ContainsKey(id))
            {
                throw new ArgumentException($"El producto con ID {id} ya existe.");
            }

            if (precio < 0)
            {
                throw new ArgumentException("El precio no puede ser negativo.");
            }

            _listadoProductos.Add(id, new Producto(id, nombre, stock, precio));

            await _logger.GrabarLogAsync($"Se agregó producto ID:{id}, Nombre:{nombre}, Stock:{stock}, Precio:{precio}");
        }

        public string ListadoProductos()
        {
            string retorno;

            if (_listadoProductos == null || _listadoProductos.Count == 0) // chequeo null primero asi no revienta el compilador
            {
                throw new InvalidOperationException("No hay productos cargados.");
            }
            else
            {
                const int ESTIMADO_POR_PRODUCTO = 100;
                int capacidadInicial = _listadoProductos.Count * ESTIMADO_POR_PRODUCTO; // optimizacion de memoria para StringBuilder
                StringBuilder sb = new StringBuilder(capacidadInicial);

                foreach (Producto producto in _listadoProductos.Values)
                {
                    sb.AppendLine(producto.ToString() + "\n");
                }
                retorno = sb.ToString();
            }

            return retorno;
        }

        public async Task ModificarStockAsync(int id, int nuevoStock)
        {
            if (_listadoProductos == null || _listadoProductos.Count == 0)
            {
                throw new InvalidOperationException("No hay productos cargados.");
            }

            if (!_listadoProductos.ContainsKey(id))
            {
                throw new KeyNotFoundException($"El producto con ID {id} no existe.");
            }

            _listadoProductos[id].Stock = nuevoStock ;
            await _logger.GrabarLogAsync($"Se modificó stock del producto ID:{id}, Nuevo Stock:{nuevoStock}");
        }

        public async Task GuardaProductosAsync()
        {
            try
            {
                await _repo.GuardarDatosAsync(_listadoProductos);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException("Error al guardar los productos: " + ex.Message);
            }
        }
    }

}
