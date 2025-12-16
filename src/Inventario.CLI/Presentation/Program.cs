namespace Gestor_Inventario.src.Inventario.CLI.Presentation
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await new AppInventario().Run();
        }
    }
}
