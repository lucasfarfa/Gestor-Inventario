
/*
 * Objetivo: Crear una aplicación de consola que permita agregar productos y listarlos, guardando todo en un archivo JSON local. 
 */


namespace Gestor_Inventario.src.Inventario.CLI
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await new AppInventario().Run();
        }
    }
}
