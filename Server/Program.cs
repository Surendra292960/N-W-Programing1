using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


namespace Server
{
    class Program
    {
        public class SyncServerSocket
        {
            public static string data;

            public static void StartListener()
            {
                byte[] bytes = new byte[1024];
                IPHostEntry iphost = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddress = iphost.AddressList[0];
                IPEndPoint localEndpoint = new IPEndPoint(ipAddress, 8080);
                Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    while (true)
                    {
                        listener.Bind(localEndpoint);
                        listener.Listen(10);
                        Console.WriteLine($"Waiting for a Connection.....");
                        Socket handler = listener.Accept();
                        data = null;
                        while (true)
                        {
                            int byteRec = handler.Receive(bytes);
                            data += Encoding.ASCII.GetString(bytes, 0, byteRec);
                            if (data.IndexOf("<EOF>") > -1)
                                break;
                        }
                        Console.WriteLine($"Test recieved:{data}");
                        byte[] msg = Encoding.ASCII.GetBytes(data);
                        handler.Send(msg);
                        handler.Shutdown(SocketShutdown.Both);
                        handler.Close();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                Console.WriteLine("Press Any key to Exit Program");
                Console.ReadLine();
            }
            static void Main(string[] args)
            {
                Console.WriteLine("Server connected....");
                Console.WriteLine();
                Console.WriteLine("Press enter to continue....");
                Console.ReadLine();
                SyncServerSocket.StartListener();
                Console.ReadLine();
            }
        }
    }
}
