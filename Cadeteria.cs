using System;
using System.Collections;
namespace EspacioCadeteria
{
    public class Cadeteria
    {
        private string nombre;
        private string direccion;
        private List<Cadete> cadetes;

        private List<Pedido> pedidos;

        public string Nombre { get => nombre; set => nombre = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public List<Cadete> Cadetes { get => cadetes; set => cadetes = value; }
        public List<Pedido> Pedidos { get => pedidos; set => pedidos = value; }

        public Cadeteria(string nombre, string direccion, List<Cadete> cadetes)
        {
            this.nombre = nombre;
            this.direccion = direccion;
            this.cadetes = cadetes;
            this.pedidos = new List<Pedido>();
        }
        public Cadeteria(){
            
        }
        public Cadeteria(string nombre, string direccion)
        {
            this.nombre = nombre;
            this.direccion = direccion;
            this.cadetes = new List<Cadete>();
            this.pedidos = new List<Pedido>();
        }
        public void CrearPedido(string? nombreCliente, string? direccionCliente, string? telefonoCliente, string? datosReferenciaDireccionCliente, int idPedido, string? obsPedido, int idCadeteElegido)
        {
            Cliente cliente = new Cliente(nombreCliente, direccionCliente, telefonoCliente, datosReferenciaDireccionCliente);
            Cadete asignado = this.cadetes.FirstOrDefault(cad => cad.Id == idCadeteElegido);
            Pedido pedido = new Pedido(idPedido, obsPedido, cliente, asignado);
            this.pedidos.Add(pedido);
        }
        public void CrearPedido(string? nombreCliente, string? direccionCliente, string? telefonoCliente, string? datosReferenciaDireccionCliente, int idPedido, string? obsPedido)
        {
            Cliente cliente = new Cliente(nombreCliente, direccionCliente, telefonoCliente, datosReferenciaDireccionCliente);
            Pedido pedido = new Pedido(idPedido, obsPedido, cliente);
            this.pedidos.Add(pedido);
        }

        public void ReasignarPedido(int pedidoId, int cadeteNuevoId)
        {
            Pedido pedidoEncontrado = this.pedidos.FirstOrDefault(ped => ped.Numero == pedidoId);
            Cadete cadeteNuevo = this.cadetes.FirstOrDefault(cad => cad.Id == cadeteNuevoId);
            if (cadeteNuevo != null && pedidoEncontrado != null)
            {
                pedidoEncontrado.AsignarCadeteAPedido(cadeteNuevo);
            }
            else
            {
                Console.WriteLine("Numero de pedido invalido o id de cadete no encontrado");
            }
        }
        public void EliminarPedido(int pedidoId)
        {
            Pedido eliminar = this.pedidos.FirstOrDefault(ped => ped.Numero == pedidoId);
            if (eliminar != null)
            {
                this.pedidos.Remove(eliminar);
            }
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
        public string Informe()
        {
            List<DatosCadete> datosCadetes = new List<DatosCadete>();
            foreach (var item in this.cadetes)
            {
                DatosCadete datosCad = new DatosCadete(cantPedidosCadeteEntregados(item.Id), JornalACobrar(item.Id), item.Nombre);
                datosCadetes.Add(datosCad);
            }
            var informe = new Informe(datosCadetes);
            return informe.Mostrar();
        }
        public void ActualizarPedido(int idElegido, int nuevoEstadoInt)
        {
            this.pedidos.FirstOrDefault(ped => ped.Numero == idElegido).ActualizarPedido(nuevoEstadoInt);
        }
        public float JornalACobrar(int id)
        {
            float monto = 500 * cantPedidosCadeteEntregados(id);
            return monto;
        }
        public int cantPedidosCadeteEntregados(int id)
        {
            List<Pedido> pedidosCad = this.pedidos.Where(ped => ped.CoincideCadete(id)).ToList();
            return pedidosCad.Count(ped => ped.Estado == EstadoPedido.Entregado);
        }

        public string MostrarPedidos()
        {
            string pedidosMostrar = "";
            foreach (var ped in this.pedidos)
            {
                pedidosMostrar = ped.MostrarPedido();
            }
            return pedidosMostrar;
        }
        public void AsignarCadeteAPedido(int idCadete, int idPedido)
        {
            Cadete cadete = this.cadetes.FirstOrDefault(cad => cad.Id == idCadete);
            if (cadete != null)
            {
                Pedido pedido = this.pedidos.FirstOrDefault(ped => ped.Numero == idPedido);
                if (pedido != null)
                {
                    pedido.AsignarCadeteAPedido(cadete);
                }
                else
                {
                    Console.WriteLine("Pedido inexistente");
                }
            }
            else
            {
                Console.WriteLine("Id de cadete inexistente");
            }
        }

        public bool EncuentraCadete(Cadete cad)
        {
            return this.cadetes.FirstOrDefault(cadet => cadet.Id == cad.Id) != null;
        }
        public List<Pedido> PedidosPendientes()
        {
            return this.pedidos.Where(ped => ped.Estado == EstadoPedido.Pendiente).ToList();
        }
        public List<Pedido> PedidosEntregados()
        {
            return this.pedidos.Where(ped => ped.Estado == EstadoPedido.Entregado).ToList();
        }
        public List<Pedido> PedidosRechazados()
        {
            return this.pedidos.Where(ped => ped.Estado == EstadoPedido.Rechazado).ToList();
        }
        public List<Pedido> PedidosSinCadete()
        {
            return this.pedidos.Where(ped => ped.NoTieneCadeteAsignado()).ToList();
        }
    }
}
