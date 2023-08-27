namespace EspacioCadeteria
{
    public partial class Cadete
    {
        private int id;
        private string nombre;
        private string direccion;
        private string telefono;
        private List<Pedido> pedidos;

        public Cadete(int id, string nombre, string direccion, string telefono, List<Pedido> pedidos)
        {
            this.id = id;
            this.nombre = nombre;
            this.direccion = direccion;
            this.telefono = telefono;
            this.pedidos = pedidos;
        }
        public Cadete(int id, string nombre, string direccion, string telefono)
        {
            this.id = id;
            this.nombre = nombre;
            this.direccion = direccion;
            this.telefono = telefono;
            this.pedidos = new List<Pedido>();
        }
        public Cadete(){
            this.pedidos=new List<Pedido>();
        }

        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        

        public void AsignarPedido(Pedido pedido){
            pedido.ActualizarPedido(EstadoPedido.Aceptado);
            pedidos.Add(pedido);
        }
        public void ActualizarPedido(Pedido pedido,EstadoPedido estado){
            Pedido pedidoAct = this.pedidos.Find(el =>el.Numero==pedido.Numero);
            if (pedidoAct!=null)
            {
                pedidoAct.ActualizarPedido(estado);
            }
        }
        public void EliminarPedido(Pedido pedido){
            this.pedidos.RemoveAll(el => el.Numero==pedido.Numero);
        }
        public float JornalACobrar(){
            float monto=500*this.pedidos.Count();
            return monto;
            
        }
        public bool CoincideId(Cadete cadete2){
            return this.Id==cadete2.Id;
        }
        public string Mostrar(){
            return "ID: "+this.id+" - Nombre: "+this.nombre+" - Telefono: "+this.telefono+" - Direccion: "+this.direccion;
        }
        public bool TienePedido(Pedido pedido){
            return (this.pedidos.FirstOrDefault(ped => ped.Numero==pedido.Numero, null)!=null);
        }
        public int CantidadPedidos(){
            return this.pedidos.Count();
        }
    }   
}