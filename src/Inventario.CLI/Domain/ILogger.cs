namespace Gestor_Inventario.src.Inventario.CLI.Domain
{
    interface ILogger
    {
        Task GrabarLogAsync(string message);
    }
}
