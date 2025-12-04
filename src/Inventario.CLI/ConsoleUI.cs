using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor_Inventario.src.Inventario.CLI
{
    class ConsoleUI
    {
        public string PedirStringNoVacio(string mensaje)
        {
            string? retorno;
            do
            {
                Console.Write(mensaje + " ");
                retorno = Console.ReadLine();
                if (string.IsNullOrEmpty(retorno))
                {
                    MostrarError("Error! Ingrese cadena de texto, no puede estar vacío.");
                }
            } while (string.IsNullOrEmpty(retorno));
            return (retorno);
        }

        public decimal PedirDecimal(string mensaje)
        {
            decimal retorno = 0;
            decimal convert = 0;
            do
            {
                Console.Write(mensaje + " ");
                if (decimal.TryParse(Console.ReadLine(), out convert) && convert > 0)
                {
                    retorno = convert;
                }
                else
                {
                    MostrarError("Error! Ingrese un numero valido y mayor a 0.");
                }
            } while (retorno <= 0);
            return retorno;
        }

        public void MostrarTitulo(string titulo)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("*************************************");
            Console.WriteLine($" {titulo.ToUpper()} ");
            Console.WriteLine("*************************************");
            Console.ResetColor();
            Console.WriteLine();
        }

        public void MostrarError(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n{mensaje}");
            Console.ResetColor();
        }

        public void MostrarExito(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n{mensaje}");
            Console.ResetColor();
        }

        public void PausarYLimpiar()
        {
            Console.WriteLine("\nPresione Enter para continuar...");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
