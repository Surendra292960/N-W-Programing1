using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Client
{
    class Client2
    {
        static void Main(string[] args)
        {
            Socket sck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("192.168.1.108"), 139);
            sck.Connect(endPoint);
            Console.WriteLine(endPoint);

            Console.WriteLine("Enter Message");
            string msg = Console.ReadLine();
            byte[] msgBuffer = Encoding.Default.GetBytes(msg);
            sck.Send(msgBuffer, 0, msgBuffer.Length, 0);

            byte[] buffer = new byte[255];
            int rec = sck.Send(buffer, 0, buffer.Length, 0);

            Array.Resize(ref buffer, rec);

            Console.WriteLine("Sended:{0}", Encoding.Default.GetString(buffer));
            
            Console.Read();

        }

    }
}
