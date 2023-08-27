using System;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;
using System.Text;
using EspacioCadeteria;

namespace EspacioArchivos
{
    public class Archivo
    {
        // Esta funcion retorna un string con el archivo JSON en formato string. Por lo tanto debo deserializarlo al string que me devuelve con  JsonSerializer.Deserialize<List<nombreClase>>(stringQueMeDevuelveLaFuncionAbrirArchivo). El Deserialize, me devolverá una lista de objetos del tipo alumno. Para usarlo ponbo por ejemplo:
        // var listadoAlumnosRecuperado = JsonSerializer.Deserialize<List<Alumno>>(jsonDocument);
        public string AbrirArchivoTexto(string nombreArchivo)
        {
            string documento;
            using (var archivoOpen = new FileStream(nombreArchivo, FileMode.Open))
            {
                using (var strReader = new StreamReader(archivoOpen))
                {
                    documento = strReader.ReadToEnd();
                    archivoOpen.Close();
                }
            }


            return documento;
        }
        //Debo antes Deserializar el archivo JsonSerializer.Serialize(nombreListaDeObjetos). Por ejemplo: string alumnosJson = JsonSerializer.Serialize(listadoAlumnos);
        //NOmbre de archivo debe ser extencino Json
        public void GuardarCadeteria(string nombreArchivo, Cadeteria datos)
        {
            using (var archivo = new FileStream(nombreArchivo, FileMode.Create))
            {
                using (var strWriter = new StreamWriter(archivo))
                {
                    strWriter.WriteLine("Nombre,Direccion,Cadetes");

                    strWriter.Close();
                }
            }
        }
        //Dada una lista de objetos las carga en un archivo. NO OLVIDARSE DE CAMBIAR EL NOMBRE DEL OBJETO
        public void CargarArchivo(string nombreArchivo)
        {
            using (var archivo = new FileStream(nombreArchivo, FileMode.Create))
            {
                using (var strWriter = new StreamWriter(archivo))
                {
                    // strWriter.WriteLine("{0}", );
                    strWriter.Close();
                }
            }
        }
        // Vos ingresas la ruta de un archivo csv que termine en \, el nombre del archivo (incluyendo la extencion) y el caracter delimitador del csv (generalmente es ,) y te devuelve un arreglo de strings separados por el delimitador
        public List<Cadeteria> LeerCsvCadeteria(string rutaDeArchivo, char caracter)
        {
            FileStream MiArchivo = new FileStream(rutaDeArchivo, FileMode.Open);
            StreamReader StrReader = new StreamReader(MiArchivo);
            string Linea = "";
            List<Cadeteria> cadeterias = new List<Cadeteria>();

            while ((Linea = StrReader.ReadLine()) != null)
            {
                string[] fila = Linea.Split(caracter);
                cadeterias.Add(new Cadeteria(fila[0],fila[1]));
            }

            return cadeterias;
        }
        public List<Cadete> LeerCsvCadete(string rutaDeArchivo, char caracter)
        {
            FileStream MiArchivo = new FileStream(rutaDeArchivo, FileMode.Open);
            StreamReader StrReader = new StreamReader(MiArchivo);
            string Linea = "";
            List<Cadete> cadetes = new List<Cadete>();
            int contador=1;
            while ((Linea = StrReader.ReadLine()) != null)
            {
                string[] fila = Linea.Split(caracter);
                cadetes.Add(new Cadete(contador,fila[0],fila[1],fila[2]));
                contador++;
            }

            return cadetes;
        }
        //Devuelve una fecha aleatoria
       
        //Retorna true si un archivo existe y no está vacio, false si o no existe o esta vacio
        public bool ExisteArchivoYNoEsVacio(string path)
        {
            if (File.Exists(path))
            {
                if (File.ReadAllText(path) != "")
                {
                    return (true);
                }
                else
                {
                    return (false);
                }
            }
            else
            {
                return (false);
            }
        }
  
    }
}