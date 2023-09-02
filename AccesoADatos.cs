using EspacioCadeteria;
using System.Text.Json;
using System.IO;
using System.Text;

namespace EspacioArchivos
{
    public class AccesoADatos
    {
        public virtual List<Cadeteria> AccesoCSVCadeterias(string rutaDeArchivo){
            return new List<Cadeteria>();
        }
        public virtual List<Cadete> AccesoCSVCadetes(string rutaDeArchivo){
            return new List<Cadete>();
        }
        public virtual List<Cadeteria> AccesoJSONCadeterias(string rutaDeArchivo){
            return new List<Cadeteria>();
        }
        public virtual List<Cadete> AccesoJSONCadetes(string rutaDeArchivo){
            return new List<Cadete>();
        }
        public virtual void EscribirJSONCadeteria(string rutaDeArchivo, List<Cadeteria> cadeteria)
        {
        }
        public virtual void EscribirJSONCadetes(string rutaDeArchivo, List<Cadete> cadetes)
        {
        }
        
    }
    public class AccesoCSV:AccesoADatos{
        public override List<Cadeteria> AccesoCSVCadeterias(string rutaDeArchivo)
        {
             FileStream MiArchivo = new FileStream(rutaDeArchivo, FileMode.Open);
            StreamReader StrReader = new StreamReader(MiArchivo);
            string Linea = "";
            List<Cadeteria> cadeterias = new List<Cadeteria>();
            char caracter =',';
            while ((Linea = StrReader.ReadLine()) != null)
            {
                string[] fila = Linea.Split(caracter);
                cadeterias.Add(new Cadeteria(fila[0],fila[1]));
            }

            return cadeterias;
        }
        public override List<Cadete> AccesoCSVCadetes(string rutaDeArchivo){
            FileStream MiArchivo = new FileStream(rutaDeArchivo, FileMode.Open);
            StreamReader StrReader = new StreamReader(MiArchivo);
            string Linea = "";
            List<Cadete> cadetes = new List<Cadete>();
            int contador=1;
            char caracter =',';
            while ((Linea = StrReader.ReadLine()) != null)
            {
                string[] fila = Linea.Split(caracter);
                cadetes.Add(new Cadete(contador,fila[0],fila[1],fila[2]));
                contador++;
            }

            return cadetes;
        }
    }
    public class AccesoJson: AccesoADatos{
        public override List<Cadeteria> AccesoJSONCadeterias(string rutaDeArchivo){
            List<Cadeteria> listaProductos;
            string documento;
            using (var archivoOpen = new FileStream(rutaDeArchivo, FileMode.Open))
            {
                using (var strReader = new StreamReader(archivoOpen))
                {
                    documento = strReader.ReadToEnd();
                    archivoOpen.Close();
                }
                listaProductos = JsonSerializer.Deserialize<List<Cadeteria>>(documento);
            }
            return (listaProductos);
        }
        public override List<Cadete> AccesoJSONCadetes(string rutaDeArchivo){
            List<Cadete> listaCadetes;
            string documento;
            using (var archivoOpen = new FileStream(rutaDeArchivo, FileMode.Open))
            {
                using (var strReader = new StreamReader(archivoOpen))
                {
                    documento = strReader.ReadToEnd();
                    archivoOpen.Close();
                }
                listaCadetes = JsonSerializer.Deserialize<List<Cadete>>(documento);
            }
            return (listaCadetes);
        }
        public override void EscribirJSONCadeteria(string rutaDeArchivo, List<Cadeteria> cadeteria)
        {
            string datos = JsonSerializer.Serialize(cadeteria);
            using (var archivo = new FileStream(rutaDeArchivo, FileMode.Create))
            {
                using (var strWriter = new StreamWriter(archivo))
                {
                    strWriter.WriteLine("{0}", datos);
                    strWriter.Close();
                }
            }
        }
        public override void EscribirJSONCadetes(string rutaDeArchivo, List<Cadete> cadetes)
        {
            string datos = JsonSerializer.Serialize(cadetes);
            using (var archivo = new FileStream(rutaDeArchivo, FileMode.Create))
            {
                using (var strWriter = new StreamWriter(archivo))
                {
                    strWriter.WriteLine("{0}", datos);
                    strWriter.Close();
                }
            }
        }
    }
}