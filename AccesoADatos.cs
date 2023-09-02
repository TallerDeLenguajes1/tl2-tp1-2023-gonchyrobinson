using EspacioCadeteria;
using System.IO;
using System.Text;

namespace EspacioArchivos
{
    public abstract class AccesoADatos
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
}