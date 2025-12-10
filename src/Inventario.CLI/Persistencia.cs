using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;

namespace Gestor_Inventario.src.Inventario.CLI
{
    class Persistencia
    {
        private const string RUTA_ARCHIVO = "productos.json";

        // aca usamos JSON Serializer
        // https://learn.microsoft.com/es-es/dotnet/standard/serialization/system-text-json/how-to

        // Serializar
        public async Task GuardarDatosAsync(Dictionary<int, Producto> listadoProductos)
        {
            if (listadoProductos == null || listadoProductos.Count == 0)
            {
                throw new ArgumentException("No se guarda el archivo ya que no hay productos en el sistema.");
            }

            var opciones = new JsonSerializerOptions
            {
                WriteIndented = true // con esta linea es mas facil leer el json
            };
            
            string jsonString = JsonSerializer.Serialize(listadoProductos.Values, opciones); // no olvidar el values, sino inserta solo KEYs...
            
            await File.WriteAllTextAsync(RUTA_ARCHIVO, jsonString);
        }

        // Deserializar, levanta el JSON y convierte a diccionario de productos
        public async Task<Dictionary<int, Producto>> CargarDatos()
        {
            if (!File.Exists(RUTA_ARCHIVO))
            {
                return new Dictionary<int, Producto>();
            }

            string jsonString = await File.ReadAllTextAsync(RUTA_ARCHIVO);

            List<Producto>? listaProductos = JsonSerializer.Deserialize<List<Producto>>(jsonString);

            if (listaProductos == null) return new Dictionary<int, Producto>();

            return listaProductos.ToDictionary(p => p.Id, p => p);
        }

        // hay que convertir porque JSON guarda arrays y yo uso un dictonary
    }
}
