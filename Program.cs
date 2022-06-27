using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ConsoleApp3
{
    public class SynchronousSocketClient
    {
        static void Main(string[] args)
        {
            byte[] bytes = new byte[1024];

            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAdress = ipHostInfo.AddressList[0];
            IPEndPoint remoteEP = new IPEndPoint(ipAdress,11000);

            Socket sender = new Socket(ipAdress.AddressFamily, SocketType.Stream ,ProtocolType.Tcp);

            sender.Connect(remoteEP);

            Console.WriteLine("Socket connected to {0}", sender.RemoteEndPoint.ToString());

            byte[] msg = Encoding.ASCII.GetBytes("This is a test <EOF>");

            int bytesSent = sender.Send(msg);

            int bytesRec = sender.Receive(bytes);
            Console.WriteLine("Echoed test = {0}", Encoding.ASCII.GetString(bytes, 0, bytesRec));

            sender.Shutdown(SocketShutdown.Both);
            sender.Close();

            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
        }
    }
}
