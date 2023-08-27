namespace EspacioCadeteria
{
    public class Informe{
        public List<DatosCadete> datosCadetes;
        public float enviosPromedioPorCadete;
        public int totalEnvios;
        public float totalMontoGanado;
        public Informe(List<DatosCadete> datosCadetes){
            this.datosCadetes=datosCadetes;
            this.totalEnvios=datosCadetes.Sum(cliente => cliente.CantEnvios);
            this.totalMontoGanado=datosCadetes.Sum(cliente => cliente.Monto);
            this.enviosPromedioPorCadete=(float)this.totalEnvios/(float)datosCadetes.Count();
        }
        public Informe(){
            this.datosCadetes=new List<DatosCadete>();
            this.totalEnvios=0;
            this.totalMontoGanado=0;
            this.enviosPromedioPorCadete=0;
        }
        public void AgregarCadete(DatosCadete datosCadete){
            this.datosCadetes.Add(datosCadete);
            this.totalEnvios=this.datosCadetes.Sum(cliente => cliente.CantEnvios);
            this.totalMontoGanado=this.datosCadetes.Sum(cliente => cliente.Monto);
            this.enviosPromedioPorCadete=(float)this.totalEnvios/(float)this.datosCadetes.Count();
        }
        public string Mostrar(){
            string datos ="-------------------DATOS DE CADA CADETE-----------------------\n";
            foreach (var item in this.datosCadetes)
            {
                datos+="\t"+item.Mostrar()+"\n";
            }
            datos+="------------------------DATOS DE LA CADETERIA-------------------------\n\tTotal de envios: "+this.totalEnvios+"\n\tTotal a ganado: "+this.totalMontoGanado+"\n\tEnvios Promedio por cadete: "+this.enviosPromedioPorCadete+"\n\n";
            return datos;
        }
    }


    
}