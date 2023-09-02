using System;
using System.Security.AccessControl;
using System.Security.Cryptography;
using EspacioArchivos;
using EspacioCadeteria;
internal partial class Program
{
    private static void Main(string[] args)
    {
        AccesoADatos helper;
        Console.WriteLine("Ingrese desde que tipo de archivo desea leer los datos: (1) CSV  -   2) JSON)");
        int tipoArchivo = Convert.ToInt32(Console.ReadLine());
        List<Cadeteria> cadeterias;
        List<Cadete> cadetes;
        if (tipoArchivo == 1)
        {
            string archivoCadeteria = "Cadeterias.csv";
            string archivoCadete = "Nombres.csv";
            helper = new AccesoCSV();
            cadeterias = helper.AccesoCSVCadeterias(archivoCadeteria);
            cadetes = helper.AccesoCSVCadetes(archivoCadete);
            AgregaCadetesLista(cadeterias, cadetes);

        }
        else
        {
            string rutaJsonCadeterias = "Cadeterias.json";
            string rutaJsonCadetes = "Cadetes.json";
            helper = new AccesoJson();
            cadeterias = helper.AccesoJSONCadeterias(rutaJsonCadeterias);
            cadetes = helper.AccesoJSONCadetes(rutaJsonCadetes);
        }
        Console.WriteLine("Seleccione en que cadeteria desea manejar: ");
        MostrarCadeteriasDisponilbes(cadeterias);
        Console.WriteLine("Cadeteria elegida: ");
        Cadeteria elegida = cadeterias[Convert.ToInt32(Console.ReadLine()) - 1];
        Console.WriteLine("\n===============================MENU=========================\n");
        Console.WriteLine("Ingrese una opcion:\n\ta)Dar de alta pedidos\n\tb)Cambiarlos de estado\n\tc)Reasignar el pedido a otro cadete\n\td)Asignar pedido (sin cadete) a cadete\n\te)Ver Informe provisorio\n\tOtra letra: salir");
        string? opcion = Console.ReadLine();
        int contador = 1;
        while (opcion == "a" || opcion == "b" || opcion == "c" || opcion == "d" || opcion == "e")
        {
            char opcionChar = opcion[0];
            switch (opcionChar)
            {
                case 'a':
                    CargarPedido(elegida, contador);
                    contador++;
                    break;
                case 'b':
                    CambiarDeEstadoPedido(elegida);
                    break;
                case 'c':
                    ReasignarPedidoAOtroCadete(elegida);
                    break;
                case 'd':
                    AsignarCadeteAPedido(elegida);
                    break;
                case 'e':
                    VerInforme(elegida);
                    break;
            }
            Console.WriteLine("Ingrese una opcion:\n\ta)Dar de alta pedidos\n\tb)Cambiarlos de estado\n\tc)Reasignar el pedido a otro cadete\n\td)Asignar pedido (sin cadete) a cadete\n\te)Ver Informe provisorio\n\tOtra letra: salir");
            opcion = Console.ReadLine();
        }
        VerInforme(elegida);

    }

    public static void AsignarCadeteAPedido(Cadeteria elegida)
    {
        Console.WriteLine("Seleccione a que pedido desea asignarle un cadete: ");
        MostrarLista(elegida.PedidosSinCadete());
        Console.WriteLine("Elegido:  ");
        int pedidoEl = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Seleccione el cadete al que le asignara el pedido: ");
        Console.WriteLine(elegida.MuestraCadetes());
        Console.WriteLine("Elegido:  ");
        int numCadeteElegido = Convert.ToInt32(Console.ReadLine());
        elegida.AsignarCadeteAPedido(numCadeteElegido, pedidoEl);
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
    public static void CargarPedido(Cadeteria cadeteria, int contador)
    {
        Console.WriteLine("Primero debe registrarse, para lo cual debe ingresar nombre - direccion - telefono - datos de referencia de su direccion: ");
        string? nombreCliente = Console.ReadLine();
        string? direccionCliente = Console.ReadLine();
        string? telefonoCliente = Console.ReadLine();
        string? datosRefCliente = Console.ReadLine();
        Console.WriteLine("Agregue las observaciones para su pedido: ");
        string? observacionesPedido = Console.ReadLine();
        cadeteria.CrearPedido(nombreCliente, direccionCliente, telefonoCliente, datosRefCliente, contador, observacionesPedido);
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

    public static void MostrarLista(List<Pedido> pedidos)
    {
        foreach (var item in pedidos)
        {
            Console.WriteLine("\t" + item.MostrarPedido());
        }
    }


    public static void CambiarDeEstadoPedido(Cadeteria elegida)
    {
        Console.WriteLine("Pedidos Realizados:  ");
        Console.WriteLine(elegida.MostrarPedidos());
        int numElegido = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Nuevo estado: 1) Entregado - 2) Pendiente - 3) Rechazado");
        int estado = Convert.ToInt32(Console.ReadLine());
        elegida.ActualizarPedido(numElegido, estado);
    }
    public static void ReasignarPedidoAOtroCadete(Cadeteria cadeteria)
    {
        Console.WriteLine("Seleccione el pedido que desea reasignar: ");
        Console.WriteLine(cadeteria.MostrarPedidos());
        Console.WriteLine("Elegido: ");
        int elegido = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Ingrese a que cadete desea reasignar el pedido: ");
        Console.WriteLine(cadeteria.MuestraCadetes());
        Console.WriteLine("Cadete elegido: ");
        int numCadElegido = Convert.ToInt32(Console.ReadLine());
        cadeteria.ReasignarPedido(elegido, numCadElegido);
    }
}