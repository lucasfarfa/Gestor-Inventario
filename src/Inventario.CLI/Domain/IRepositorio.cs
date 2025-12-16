namespace Gestor_Inventario.src.Inventario.CLI.Domain
{
    interface IRepositorio
    {
        Task GuardarDatosAsync(Dictionary<int, Producto> listadoProductos);
        Task<Dictionary<int, Producto>> CargarDatos();
    }
}
