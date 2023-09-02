namespace EspacioCadeteria
{
    public enum EstadoPedido
    {
        Entregado,
        Pendiente,
        Rechazado
    }
    public class Pedido
    {
        private int numero;
        private string obs;
        private EstadoPedido estado;
        private Cliente cliente;
        private Cadete cadete;

        public Pedido(int numero, string obs, Cliente cliente, EstadoPedido estado, Cadete cadete)
        {
            this.numero = numero;
            this.obs = obs;
            this.cliente = cliente;
            this.estado = estado;
            this.cadete = cadete;
        }
        public Pedido(int numero, string obs, Cliente cliente, int estadoInt, Cadete cadete)
        {
            EstadoPedido estado = 0;
            EstadoPedido estadoEnum = 0;
            switch (estadoInt)
            {
                case 1:
                    estado = EstadoPedido.Entregado;
                    break;
                case 2:
                    estado = EstadoPedido.Pendiente;
                    break;
                case 3:
                    estado = EstadoPedido.Rechazado;
                    break;
            }
            if (estadoEnum != 0)
            {
                this.estado = estado;
            }
            else
            {
                this.estado = EstadoPedido.Pendiente;
            }
            this.numero = numero;
            this.obs = obs;
            this.cliente = cliente;
            this.estado = estado;
            this.cadete = cadete;
        }
        public Pedido(int numero, string obs, Cliente cliente, Cadete cadete)
        {
            this.numero = numero;
            this.obs = obs;
            this.cliente = cliente;
            this.estado = EstadoPedido.Pendiente;
            this.cadete = cadete;

        }
        public void ActualizarPedido(EstadoPedido estado)
        {
            this.estado = estado;
        }
        public void ActualizarPedido(int estadoInt)
        {
            if (estadoInt == 1 || estadoInt == 2 || estadoInt == 3)
            {
                switch (estadoInt)
                {
                    case 1:
                        estado = EstadoPedido.Entregado;
                        break;
                    case 2:
                        estado = EstadoPedido.Pendiente;
                        break;
                    case 3:
                        estado = EstadoPedido.Rechazado;
                        break;
                }
                this.estado = estado;
            }else
            {
                Console.WriteLine("Error: Estado invalido");
            }
        }
        public string VerDireccionCliente()
        {
            return this.cliente.VerDireccion();
        }
        public string VerDatosCliente()
        {
            return this.cliente.VerDatos();
        }
        public string MostrarPedido()
        {
            return "Numero: " + this.numero + " - Estado: " + this.estado + " - Observaciones: " + this.obs;
        }
        public bool CoincideCadete(int idCadete){
            return this.cadete.Id==idCadete;
        }
        public int Numero { get => numero; set => numero = value; }
        public string Obs { get => obs; set => obs = value; }
        public EstadoPedido Estado { get => estado; set => estado = value; }
        public void AsignarCadeteAPedido(Cadete cadete){
            this.cadete=cadete;
        }
    }
}