using System;
using System.Collections;
namespace EspacioCadeteria
{
    public class Cadeteria
    {
        private string nombre;
        private string direccion;
        private List<Cadete> cadetes;


        public string Nombre { get => nombre; set => nombre = value; }
        public string Direccion { get => direccion; set => direccion = value; }

        public Cadeteria(string nombre, string direccion, List<Cadete> cadetes)
        {
            this.nombre = nombre;
            this.direccion = direccion;
            this.cadetes = cadetes;
        }
        public Cadeteria(string nombre, string direccion)
        {
            this.nombre = nombre;
            this.direccion = direccion;
            this.cadetes = new List<Cadete>();
        }
        public void CrearPedido(Pedido pedido, Cadete cadete)
        {
            pedido.ActualizarPedido(EstadoPedido.Aceptado);
            cadete.AsignarPedido(pedido);
        }

        public void ReasignarPedido(Pedido pedido, Cadete cadeteNuevo)
        {
            Cadete cadeteAnt = this.cadetes.FirstOrDefault(cad => cad.TienePedido(pedido), null);
            if (cadeteAnt != null)
            {
                cadeteAnt.EliminarPedido(pedido);
                cadeteNuevo.AsignarPedido(pedido);
            }else
            {
                Console.WriteLine("Error: pedido no asignado a ningun cliente");
            }
        }
        public void EliminarPedido(Pedido pedido, Cadete cadete)
        {
            cadete.EliminarPedido(pedido);
        }
        public void AgregarCadete(Cadete cadete)
        {
            this.cadetes.Add(cadete);
        }
        public void BorrarCadete(Cadete cadete)
        {
            this.cadetes.RemoveAll(el => el.Id == cadete.Id);
        }
        public void CargarCadetes(List<Cadete> cadetes)
        {
            this.cadetes = cadetes;
        }
        public bool EncuentraCadete(Cadete cadete)
        {
            if (this.cadetes.Find(el => el.CoincideId(cadete)) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Cadete? DevuelveCadete(Cadete cadete)
        {
            return this.cadetes.FirstOrDefault(cad => cad.Id == cadete.Id, null);
        }
        public Cadete? DevuelveCadete(int numCadete)
        {
            return this.cadetes.FirstOrDefault(cad => cad.Id == numCadete, null);
        }
        public string MuestraCadetes()
        {
            string lista = "";
            foreach (var item in this.cadetes)
            {
                lista += "\t - " + item.Mostrar() + "\n";
            }
            return lista;
        }
        public string Informe(){
            var informe = new Informe();
            foreach (var item in cadetes)
            {
                DatosCadete datosCadete=new DatosCadete(item);
                informe.AgregarCadete(datosCadete);
            }
            return informe.Mostrar();
        }
        public void ActualizarPedido(Pedido elegido, List<Pedido> listaPedidos, EstadoPedido nuevoEstado){
            if(nuevoEstado!=EstadoPedido.Aceptado){
                Cadete cadetePedido = this.cadetes.FirstOrDefault(cad => cad.TienePedido(elegido),null);
                if (cadetePedido!=null)
                {
                    if (nuevoEstado==EstadoPedido.Pendiente)
                    {
                        elegido.ActualizarPedido(EstadoPedido.Pendiente);
                        cadetePedido.EliminarPedido(elegido);
                    }else{
                        cadetePedido.EliminarPedido(elegido);
                        listaPedidos.Remove(elegido);
                    }
                }
            }else
            {
                elegido.ActualizarPedido(EstadoPedido.Aceptado);
            }
        }
        public void ActualizarPedido(Pedido elegido, List<Pedido> listaPedidos, int nuevoEstadoInt){
            EstadoPedido nuevoEstado = 0;
            switch (nuevoEstadoInt)
            {
                case 1:
                    nuevoEstado = EstadoPedido.Aceptado;
                    break;
                case 2:
                    nuevoEstado = EstadoPedido.Pendiente;
                    break;
                case 3:
                    nuevoEstado = EstadoPedido.Rechazado;
                    break;
            }
            if(nuevoEstado!=EstadoPedido.Aceptado){
                Cadete cadetePedido = this.cadetes.FirstOrDefault(cad => cad.TienePedido(elegido),null);
                if (cadetePedido!=null)
                {
                    if (nuevoEstado==EstadoPedido.Pendiente)
                    {
                        elegido.ActualizarPedido(EstadoPedido.Pendiente);
                        cadetePedido.EliminarPedido(elegido);
                    }else{
                        cadetePedido.EliminarPedido(elegido);
                        listaPedidos.Remove(elegido);
                    }
                }
            }else
            {
                elegido.ActualizarPedido(EstadoPedido.Aceptado);
            }
        }
    }
}
