using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace iDuel_EvolutionX.Net
{
    class TaskClient
    {
        private bool isExit = false;
        private TcpClient client;
        private BinaryReader br;
        private BinaryWriter bw;
        private string remoteHost;
        private int remotePort = 51888;

        private static TaskClient tc = null;

        private TaskClient()
        {

        }

        public static TaskClient getInstance()
        {
            if (tc == null)
            {
                tc = new TaskClient();
            }
            return tc;
        }

        public void startConnect(string ip, string socket)
        {
            try
            {
                //此处为方便演示，实际使用时要将Dns.GetHostName()改为服务器域名
                client = new TcpClient(remoteHost, remotePort);
                AddInfo("与服务端连接成功");
                //btnLogin.IsEnabled = false;
            }
            catch
            {
                AddInfo("与服务端连接失败");
                return;
            }
            //获取网络流
            NetworkStream networkStream = client.GetStream();
            //将网络流作为二进制读写对象
            br = new BinaryReader(networkStream);
            bw = new BinaryWriter(networkStream);
            //SendMessage("Login," + textBoxUserName.Text);//格式：Login,用户名
            Task.Run(() => ReceiveData());
        }

        /// <summary>处理接收的数据</summary>
        private void ReceiveData()
        {
            string receiveString = null;
            while (isExit == false)
            {
                try
                {
                    //从网络流中读出字符串
                    //此方法会自动判断字符串长度前缀，并根据长度前缀读出字符串
                    receiveString = br.ReadString();
                }
                catch
                {
                    if (isExit == false)
                    {
                        AddInfo("与服务端失去联系。");
                    }
                    break;
                }
                AddInfo(receiveString);
            }
            // this.Close();
        }

        private void AddInfo(string format, params object[] args)
        {
            //textBlock1.Dispatcher.InvokeAsync(() =>
            //{
            //    textBlock1.Text += string.Format(format, args) + "\n";
            //});
        }

        /// <summary>向服务器端发送信息</summary>
        private void SendMessage(string message)
        {
            try
            {
                //将字符串写入网络流，此方法会自动附加字符串长度前缀
                bw.Write(message);
                bw.Flush();
            }
            catch
            {
                AddInfo("发送失败!");
            }
        }
    }
}
