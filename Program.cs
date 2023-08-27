using System;
using System.Security.AccessControl;
using EspacioArchivos;
using EspacioCadeteria;
internal partial class Program
{
    private static void Main(string[] args)
    {
        string archivoCadeteria = "Cadeterias.csv";
        string archivoCadete = "Nombres.csv";
        Archivo helper = new Archivo();
        List<Cadeteria> cadeterias = helper.LeerCsvCadeteria(archivoCadeteria, ',');
        List<Cadete> cadetes = helper.LeerCsvCadete(archivoCadete, ',');
        AgregaCadetesLista(cadeterias, cadetes);
        Console.WriteLine("Seleccione en que cadeteria desea manejar: ");
        MostrarCadeteriasDisponilbes(cadeterias);
        Console.WriteLine("Cadeteria elegida: ");
        Cadeteria elegida = cadeterias[Convert.ToInt32(Console.ReadLine()) - 1];
        Console.WriteLine("\n===============================MENU=========================\n");
        Console.WriteLine("Ingrese una opcion:\n\ta)Dar de alta pedidos\n\tb)Asiganrlos a cadetes\n\tc)Cambiarlos de estado\n\td)Reasignar el pedido a otro cadete\n\te) Ver informe provisiorio");
        string? opcion = Console.ReadLine();
        List<Pedido> pedidos = new List<Pedido>();
        while (opcion == "a" || opcion == "b" || opcion == "c" || opcion == "d" || opcion == "e")
        {
            char opcionChar = opcion[0];
            switch (opcionChar)
            {
                case 'a':
                    Pedido pedidoCargar = CargarPedido(elegida, pedidos);
                    pedidos.Add(pedidoCargar);
                    break;
                case 'b':
                    AsignarPedidoACadete(elegida, pedidos);
                    break;
                case 'c':
                    CambiarDeEstadoPedido(elegida,pedidos);
                    break;
                case 'd':
                    ReasignarPedidoAOtroCadete(elegida, pedidos);
                    break;
                case 'e':
                    VerInforme(elegida);
                break;
            }
            Console.WriteLine("Ingrese una opcion:\n\ta)Dar de alta pedidos\n\tb)Asiganrlos a cadetes\n\tc)Cambiarlos de estado\n\td)Reasignar el pedido a otro cadete\n\te) Ver informe provisiorio");
            opcion = Console.ReadLine();
        }
        VerInforme(elegida);

    }
    public static void VerInforme(Cadeteria elegida)
    {
        Console.WriteLine("\n\n===================================INFORME " + elegida.Nombre + "=====================================");
        Console.WriteLine(elegida.Informe());
    }
    public static void AgregaCadetes(Cadeteria cadeteria, List<Cadete> cadetes)
    {
        Random rand = new Random();
        int cant = rand.Next() % cadetes.Count();
        List<Cadete> cadetesAgregar = new List<Cadete>();
        for (int i = 0; i < cant; i++)
        {
            Cadete cadAgregar = cadetes[rand.Next() % cadetes.Count()];
            while (cadeteria.EncuentraCadete(cadAgregar))
            {
                cadAgregar = cadetes[rand.Next() % cadetes.Count()];
            }
            cadeteria.AgregarCadete(cadAgregar);
        }
    }
    public static void AgregaCadetesLista(List<Cadeteria> cadeteria, List<Cadete> cadetes)
    {
        for (int i = 0; i < cadeteria.Count(); i++)
        {
            AgregaCadetes(cadeteria[i], cadetes);
        }
    }
    public static Pedido CargarPedido(Cadeteria cadeteria, List<Pedido> pedidos)
    {
        Console.WriteLine("Primero debe registrarse, para lo cual debe ingresar nombre - dni - telefono - datos de referencia de su direccion: ");
        Cliente cliente = new Cliente(Console.ReadLine(), Console.ReadLine(), Console.ReadLine(), Console.ReadLine());
        Console.WriteLine("Agregue las observaciones para su pedido: ");
        Pedido pedidoCargar = new Pedido(pedidos.Count(), Console.ReadLine(), cliente);
        return pedidoCargar;
    }
    public static void MostrarCadeteriasDisponilbes(List<Cadeteria> cadeterias)
    {
        int i = 1;
        foreach (var item in cadeterias)
        {
            Console.WriteLine(i + ")" + item.Nombre);
            i++;
        }
    }
    public static void AsignarPedidoACadete(Cadeteria cadeteria, List<Pedido> pedidos)
    {
        Console.WriteLine("Pedidos pendientes: ");
        MostrarPedidosPendientes(pedidos);
        Console.WriteLine("Elegido:  ");
        int numElegido = Convert.ToInt32(Console.ReadLine());
        Pedido pedidoElegido = pedidos.FirstOrDefault(ped => ped.Numero == numElegido, null);
        if (pedidoElegido != null)
        {
            Console.WriteLine("Seleccione el cadete al que le asignara el pedido: ");
            Console.WriteLine(cadeteria.MuestraCadetes());
            Console.WriteLine("Elegido:  ");
            int numCadeteElegido = Convert.ToInt32(Console.ReadLine());
            Cadete cadeteElegido = cadeteria.DevuelveCadete(numCadeteElegido);
            if (cadeteElegido != null)
            {
                cadeteElegido.AsignarPedido(pedidoElegido);
            }
            else
            {
                Console.WriteLine("Error: Id de cadete inexistente");
            }
        }
        else
        {
            Console.WriteLine("Error: Numero de pedido inexistente");
        }
    }
    public static void MostrarPedidosPendientes(List<Pedido> pedidos)
    {
        foreach (var item in pedidos)
        {
            if (item.Estado == EstadoPedido.Pendiente)
            {
                Console.WriteLine("\t" + item.MostrarPedido());
            }
        }
    }
    public static void MostrarPedidosAceptados(List<Pedido> pedidos)
    {
        foreach (var item in pedidos)
        {
            if (item.Estado == EstadoPedido.Aceptado)
            {
                Console.WriteLine("\t" + item.MostrarPedido());
            }
        }
    }
    public static void MostrarPedidos(List<Pedido> pedidos)
    {
        foreach (var item in pedidos)
        {
            Console.WriteLine(item.MostrarPedido());
        }
    }
    public static void CambiarDeEstadoPedido(Cadeteria elegida, List<Pedido> pedidos)
    {
        Console.WriteLine("Pedidos Realizados:  ");
        MostrarPedidos(pedidos);
        int numElegido = Convert.ToInt32(Console.ReadLine());
        Pedido pedidoElegido = pedidos.FirstOrDefault(ped => ped.Numero == numElegido, null);
        if (pedidoElegido != null)
        {
            Console.WriteLine("Nuevo estado: 1) Aceptado - 2) Pendiente - 3) Rechazado");
            int estado = Convert.ToInt32(Console.ReadLine());
            elegida.ActualizarPedido(pedidoElegido,pedidos,estado);
        }
        else
        {
            Console.WriteLine("Pedido inexistente");
        }
    }
    public static void ReasignarPedidoAOtroCadete(Cadeteria cadeteria, List<Pedido> pedidos)
    {
        Console.WriteLine("Seleccione el pedido que desea reasignar: ");
        MostrarPedidosAceptados(pedidos);
        Console.WriteLine("Elegido: ");
        int elegido = Convert.ToInt32(Console.ReadLine());
        Pedido pedidoElegido = pedidos.FirstOrDefault(ped => ped.Numero == elegido, null);
        if (pedidoElegido != null)
        {
            Console.WriteLine("Ingrese a que cadete desea reasignar el pedido: ");
            Console.WriteLine(cadeteria.MuestraCadetes());
            Console.WriteLine("Cadete elegido: ");
            int numCadElegido = Convert.ToInt32(Console.ReadLine());
            Cadete cadeteNuevo = cadeteria.DevuelveCadete(numCadElegido);
            if (cadeteNuevo != null)
            {
                cadeteria.ReasignarPedido(pedidoElegido, cadeteNuevo);
            }
            else
            {
                Console.WriteLine("Error: No existe el cadete al que intenta reasignar");
            }
        }
        else
        {
            Console.WriteLine("Pedido inexistente");
        }
    }
}