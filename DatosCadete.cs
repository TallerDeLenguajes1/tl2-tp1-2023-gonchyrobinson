namespace EspacioCadeteria
{
    public class DatosCadete{
        private float monto;
        private int cantEnvios;
        private string nombre;

        public float Monto { get => monto; set => monto = value; }
        public int CantEnvios { get => cantEnvios; set => cantEnvios = value; }

        public DatosCadete(Cadete cadete){
        this.CantEnvios=cadete.CantidadPedidos();
        this.Monto = this.CantEnvios*500;
        this.nombre=cadete.Nombre;
    }
    public DatosCadete(){
        this.CantEnvios=0;
        this.Monto = 0;
    }
    public string Mostrar(){
        return "Nombre: "+this.nombre+"\n\tTotal de envios realizados: "+this.cantEnvios+"\t-\tMonto a cobrar: "+this.monto;
    }
    }
    
}