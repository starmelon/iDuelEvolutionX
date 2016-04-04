using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using iDuel_EvolutionX.Service;
using System.Windows;
using NBX3.Service;
using System.IO;
using System.Windows.Media.Imaging;
using iDuel_EvolutionX.Tools;
using iDuel_EvolutionX.UI;
using System.Windows.Controls;
using System.Windows.Media.Effects;
using iDuel_EvolutionX.EventJson;
using Newtonsoft.Json;

namespace iDuel_EvolutionX.Net
{
    class Server:IDisposable
    {


        //定义处理action传送调用的回调
        private delegate void HandleMsgCallBack(string text); //定义接收消息的回调
        private HandleMsgCallBack handleMsgCallBack;          //声明回调

        private delegate void HandleConnectedCallBack(string text);
        private HandleConnectedCallBack handleConnectedCallBack;      //声明处理连接成功的回调

        private delegate void HandleCardbackCallBack(byte[] img); //定义接收图片的
        private HandleCardbackCallBack handleCardbackCallBack;

        private TcpListener myTcpListener;    //TCP监听器
        private Thread acceptTcpClientThread; //接受连接线程
        private TcpClient tcpClient;    //连接的客户端
        private NetworkStream ns;
        private static Server sr = null;

        private static MainWindow mainwindow;

        private Server()
        {
            handleMsgCallBack = new HandleMsgCallBack(handleMsg);
            handleConnectedCallBack = new HandleConnectedCallBack(handleConnected);
            handleCardbackCallBack = new HandleCardbackCallBack(handleCardback);
        }

        

        public static Server getInstance()
        {
            if (sr == null)
            {
                sr = new Server();
            }
            return sr;
        }

        #region <-- 初始化服务端 -->

        public static Server getInstance(MainWindow mw)
        {
            if (sr==null)
            {
                sr = new Server();
                mainwindow = mw;
            }
            return sr;
        }

        #endregion

        #region <-- 判断是否开启了服务端 -->

        public static bool check()
        {
            if (sr == null)
            {
                return false;
            }
            return true;
 
        }

        #endregion

        #region <-- 开启监听 -->

        //开启监听
        public void startlisten(string ip,string socket)
        {
            //DuelOperate.getInstance().Myself.userindex = "2";
            //mainwindow.img_head_op.Effect.SetValue(BlurEffect.RadiusProperty,3.0);
            
            //mainwindow.img_head_op.SetValue(Image.EffectProperty.)

            //System.Windows.Media.Animation.Storyboard test = mainwindow.Resources["sb_waittingConnect"] as System.Windows.Media.Animation.Storyboard;
            //test.Begin();
            //UIAnimation.stateshow().Begin(mainwindow.tb_opOperation);
            
            UIAnimation.getInstance().opacity21.Begin(mainwindow.img_serchP2);
            UIAnimation.getInstance().rotateAnimation.Begin(mainwindow.img_serchP2);


            IPEndPoint iPendPoint = new IPEndPoint(IPAddress.Parse(ip), Convert.ToInt32(socket));
            myTcpListener = new TcpListener(iPendPoint);
            
            myTcpListener.Start();

            //Console.WriteLine(mainwindow.duelistname.Text);
            //acceptTcpClientThread = new Thread(new ParameterizedThreadStart(acceptTcpClient));         
            //acceptTcpClientThread.IsBackground = true;
            //acceptTcpClientThread.Start("2,"+mainwindow.tb_setDuelistName.Text);

            acceptTcpClientThread = new Thread(acceptTcpClient);
            acceptTcpClientThread.IsBackground = true;
            acceptTcpClientThread.Start();
        }

        #endregion

        #region <-- 停止监听 -->

        public void stoptlisten()
        {
            myTcpListener.Server.Close();
            myTcpListener.Stop();
            acceptTcpClientThread.Abort();

            GC.Collect();
        }

        #endregion

        #region <-- 接受链接&信息接收 -->

        //接受链接
        private void acceptTcpClient(object duelistname)
        {
            try
            {            
                //接收客户端
                tcpClient = myTcpListener.AcceptTcpClient();

                //绑定客户端的网络流
                ns = tcpClient.GetStream();

                
                //sendMsg(duelistname as string + "," + "-1");
                BinaryReader br = new BinaryReader(ns);

                mainwindow.Dispatcher.BeginInvoke(handleConnectedCallBack, "连接成功");

                //循环接收消息
                while (true)
                {
                    
                    int readlen = tcpClient.Available;
                    if (readlen > 0)
                    {
                        #region 废弃的代码

                        ////声明并初始化负责接收的字节数组
                        //byte[] getData = new byte[1024];

                        ////从网络流中读取数据包
                        //ns.Read(getData, 0, getData.Length);
                        
                        ////转化获得的数据包为字符串
                        
                        ////string msg = Encoding.UTF8.GetString(getData);
                        //string msg = Encoding.UTF8.GetString(getData).Replace("\0", "").TrimEnd();
                        //Console.WriteLine("收到的字节数是："+msg.Length);

                        #endregion

                        string msg = br.ReadString();

                        //if (msg.Contains("getcardback="))
                        //{
                        //    int piclen;
                        //    string[] msgs = msg.Split('=');
                        //    if (msgs != null && msgs.Length == 2)
                        // {
                        //        if(int.TryParse(msgs[1],out piclen))
                        //        {
                        //            while (true)
                        //            {
                        //                byte[] cardback = br.ReadBytes(piclen);
                        //                if (cardback != null)
                        //                {
                        //                    mainwindow.Dispatcher.Invoke(handleCardbackCallBack, cardback);
                        //                    break; 
                        //                }
                        //            }
                        //            continue;
                        //        }

                        // }

                        //}
                        //mainwindow.Dispatcher.Invoke(handleMsgCallBack, msg);
                        Application.Current.Dispatcher.Invoke(handleMsgCallBack, msg);
                        //OpponentOperate.ActionAnalyze(getMsg, false);
                        //返回收到的确认信息给客户端
                        //string sendback = "服务器已经接收到[" + getMsg.TrimEnd() + "]这条消息";
                        //byte[] sendDataBack = Encoding.UTF8.GetBytes(sendback);
                        //ns.Write(sendDataBack, 0, sendDataBack.Length);
                    }
                    Thread.Sleep(100);
                }
            }
            catch (ThreadAbortException)
            {

            }
            catch (Exception )
            {
                //Console.WriteLine(ex);
                myTcpListener.Stop();
            }

        }

        #endregion

        #region <-- 发送消息 -->

        public void sendMsg(string msg)
        {
            //msg += mainwindow.duelistname.Text;
            if (ns != null)
            {
                BinaryWriter bw = new BinaryWriter(ns); 
                //byte[] sendDataBack = Encoding.UTF8.GetBytes(msg);
                try
                {
                    bw.Flush();
                    bw.Write(msg);
                    //ns.Write(sendDataBack, 0, sendDataBack.Length);

                }
                catch (System.IO.IOException)
                {
                    MessageBox.Show("与对手失去连接！");
                    myTcpListener.Stop();
                    acceptTcpClientThread.Abort();
                    mainwindow.mi_listen.IsEnabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("出错：" + ex);
                    //throw;
                }
            }
                      
        }

        #endregion

        #region <-- 图像传送 -->

        public void sendMsg(byte[] pic)
        {

            if (ns != null)
            {
                BinaryWriter bw = new BinaryWriter(ns);
                try
                {
                    ////新建字节数组并赋值
                    //byte[] sendData = Encoding.UTF8.GetBytes(msg);
                    ////写入网络数据流        
                    //ns.Write(sendData, 0, sendData.Length);
                    sendMsg("getcardback=" + pic.Length);
                    bw.Flush();
                    bw.Write(pic);
                }
                catch (System.IO.IOException)
                {
                    MessageBox.Show("与对手失去连接！");
                    mainwindow.mi_connect.IsEnabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }

        }

        #endregion

        #region <-- 接收消息后调用的委托指向的处理方法 -->
        //添加处理action传送调用的被委托的方法
        private void handleMsg(string msg)
        {
            DuelOperate.getInstance().receiveMsg(msg);
        }

        #endregion

        #region <-- 连接成功后调用的委托指向的传送玩家信息的方法 -->
        //添加处理
        private void handleConnected(string text)
        {
            DuelistInfo duelistInfo = new DuelistInfo();
            duelistInfo.name = "星瓜";
            //duelistInfo.cardBack = BitmapImagehandle.BitmapImageToByteArray(DuelOperate.getInstance().myself.cardback);
            String contentJson = JsonConvert.SerializeObject(duelistInfo);

            BaseJson bj = new BaseJson();
            bj.guid = Guid.NewGuid();
            bj.cid = "";
            bj.action = ActionCommand.GAME_SET_DUELST_INFO;
            bj.json = contentJson;

            String json = JsonConvert.SerializeObject(bj);
            sendMsg(json);
            //conteneJson



            ////传送己方设定的玩家名字
            //sendMsg(DuelOperate.getInstance().sendmyself());
            ////sendMsg("2," + mainwindow.tb_Duelist.Text + "," + "-1");

            ////如果存在卡背则传送卡背
            //string path = AppConfigOperate.getInstance().Custom_path+"\\cardback0.jpg";
            ////BitmapImage cardback = DuelOperate.getInstance().myself.cardback.CloneCurrentValue();
            //if (System.IO.File.Exists(path))
            //{
            //    try
            //    {
            //        BitmapImage cardback = BitmapImagehandle.GetBitmapImage(path);
            //        sendMsg(BitmapImagehandle.BitmapImageToByteArray(cardback));
            //    }
            //    catch (ArgumentNullException ex)
            //    {
            //        throw ex;
            //    }
            //    finally
            //    {

            //    }
            //}
        }

        #endregion

        #region <-- 处理图像接收调用的委托指向的方法 -->

        private void handleCardback(byte[] img)
        {
            BitmapImage opCardback = BitmapImagehandle.ByteArrayToBitmapImage(img);
            DuelOperate.getInstance().opponent.cardback = opCardback;
        }

        #endregion




        public void Dispose()
        {
            
        }
    }
}
