using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Clinet
{
    class Program
    {
        class SyncSocketClient
        {
            public static void StartClient()
            {
                byte[] bytes = new byte[1024];
                try
                {
                    var hostName = Dns.GetHostName();
                    IPHostEntry ipHost = Dns.GetHostEntry(hostName);
                    Console.WriteLine($"Host:{hostName}");
                    IPAddress ip = ipHost.AddressList[0];
                    IPEndPoint remoteEp = new IPEndPoint(ip, 8080);
                    Socket sender = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                    try
                    {
                        sender.Connect(remoteEp);
                        Console.WriteLine("socket connected......");
                        Console.WriteLine();
                        sender.RemoteEndPoint.ToString();

                        byte[] msg = Encoding.ASCII.GetBytes("It is just a test");

                        int byteSet = sender.Send(msg);
                        int byteRec = sender.Receive(bytes);

                        Console.WriteLine($"Echod test{Encoding.ASCII.GetString(bytes, 0, byteRec)}");
                        sender.Shutdown(SocketShutdown.Both);
                        sender.Close();
                    }
                    catch (ArgumentNullException ex)
                    {
                        Console.WriteLine(ex.Message);
                        throw;
                    }
                    catch (SocketException ex2)
                    {
                        Console.WriteLine(ex2.Message);
                    }
                }
                catch (Exception ex3)
                {
                    Console.WriteLine(ex3.Message);
                }
            }
            static void Main(string[] args)
            {
                Console.WriteLine("Press Any key to connect.......");
                Console.WriteLine();
                SyncSocketClient.StartClient();
                Console.ReadLine();
            }
        }
    }
}
