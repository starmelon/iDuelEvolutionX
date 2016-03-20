using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows;

namespace iDuel_EvolutionX.Net
{
    class ServerSocket:IDisposable
    {
        Thread mythread;
        Socket socket;

        //public static IPAddress GetServerIP()
        //{

        //    IPHostEntry ieh = Dns.GetHostByName(Dns.GetHostName());

        //    return ieh.AddressList[0];

        //}

        private void startlisten(object sender, System.EventArgs e)
        {

            try
            {

                mythread = new Thread(new ThreadStart(BeginListen));

                mythread.Start();

            }

            catch (System.Exception er)
            {

                MessageBox.Show(er.Message, "完成", MessageBoxButton.OK, MessageBoxImage.Stop);

            }

        }


        private void BeginListen()
        {

            IPAddress ServerIp = IPAddress.Parse("127.0.0.1");

            IPEndPoint iep = new IPEndPoint(ServerIp, 3600);

            socket = new

                     Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            byte[] byteMessage = new byte[100];

            Console.WriteLine( iep.ToString());

            socket.Bind(iep);

            //            do

            while (true)
            {

                try
                {

                    socket.Listen(5);

                    Socket newSocket = socket.Accept();

                    newSocket.Receive(byteMessage);

                    string sTime = DateTime.Now.ToShortTimeString();

                    string msg = sTime + ":" + "Message from:";

                    msg += newSocket.RemoteEndPoint.ToString() + Encoding.Default.GetString(byteMessage);

                    Console.WriteLine(msg);

                }

                catch (SocketException ex)
                {

                    Console.WriteLine( ex.ToString());

                }

            }

            //            while(byteMessage!=null);

        }

        public void Dispose()
        {
            try
            {

                socket.Close();//释放资源

                mythread.Abort();//中止线程

            }

            catch { }

            //if (disposing)
            //{

            //    if (components != null)
            //    {

            //        components.Dispose();

            //    }

            //}

            //base.Dispose(disposing);
        }

        
    }
}
