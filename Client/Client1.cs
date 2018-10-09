using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Client1
    {
        static Socket sck;
        static void Main(string[] args)
        {
            sck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Parse("192.168.1.108"), 1234);

            try
            {
                sck.Connect(localEndPoint);
            }
            catch
            {
                Console.WriteLine("Unable to connect to remote end point! \n");
                Main(args);
            }
            Console.Write("Enter Text");
            String text = Console.ReadLine();
            byte[] data = Encoding.ASCII.GetBytes(text);


            sck.Send(data);
            Console.WriteLine("Data sent! \n");
            Console.Read();
            sck.Close();

        }
    }
}
