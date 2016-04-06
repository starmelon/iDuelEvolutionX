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
using iDuel_EvolutionX.EventJson;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace iDuel_EvolutionX.Net
{
    class Client
    {
        //定义处理action传送调用的回调
        private delegate void HandleMsgCallBack(string text); //定义处理消息接收的回调      
        private HandleMsgCallBack handleMsgCallBack;          //声明处理消息接收的回调

        private delegate void HandleConnectedCallBack(string text);
        private HandleConnectedCallBack handleConnectedCallBack;      //声明处理连接成功的回调

        private delegate void HandleCardbackCallBack(byte[] img); //定义接收图片的回调
        private HandleCardbackCallBack handleCardbackCallBack;

        private TcpClient myTcpClient;                       //Tcp客户端
        private NetworkStream ns;                            //网络数据流
        private BinaryReader br;
        private BinaryWriter bw;

        private Thread receiveMsgThread;                     //接收消息的线程 
        private static Client cl = null;

        TcpCommon tcpCommon = new TcpCommon();

        private static MainWindow mainwindow;
      
        //窗体启动加载时间
        private Client()
        {
            //实例化回调
            handleMsgCallBack = new HandleMsgCallBack(handleMsg);
            handleConnectedCallBack = new HandleConnectedCallBack(handleConnected);
            handleCardbackCallBack = new HandleCardbackCallBack(handleCardback);
        }

       

        public static Client getInstance()
        {
            if (cl == null)
            {
                cl = new Client();
            }
            return cl;
        }

        public static Client getInstance(MainWindow mw)
        {
            if (cl == null)
            {
                cl = new Client();
                mainwindow = mw;
            }
            return cl;
        }

        #region <-- 判断是否开启了客户端 -->

        public static bool check()
        {
            if (cl == null)
            {
                return false;
            }
            return true;

        }

        #endregion

        #region <-- 开始连接 -->

        //建立连接的按钮单机事件
        public void startConnect(string ip, string socket)
        {
            AsyncCallback connectAsyncCallBack = new AsyncCallback(Connect2Server);

            //创建并实例化IP终结点
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse(ip), Convert.ToInt32(socket));

            //实例化TCP客户端 
            myTcpClient = new TcpClient();

            IAsyncResult result = myTcpClient.BeginConnect(
                IPAddress.Parse(ip), Convert.ToInt32(socket), connectAsyncCallBack, myTcpClient);

            #region 废弃的代码

            //开启等待动画

            //try
            //{
                
            //    //发起TCP连接
            //    //myTcpClient.BeginConnect(ipEndPoint);
            //    myTcpClient.Connect(ipEndPoint);
                
                

            //    //获取绑定的网络数据流
            //    ns = myTcpClient.GetStream();
            //    receiveMsgThread = new Thread(ReceiveMsg);
            //    receiveMsgThread.IsBackground = true;
            //    receiveMsgThread.Start();

            //    //mainwindow.report.AppendText("system：成功建立连接" + Environment.NewLine);
            //    sendMsg("2,"+mainwindow.tb_setDuelistName.Text + "," + "-1");
            //    //DuelOperate.sendMsg("Connect=" + );
            //}
            //catch (ThreadAbortException)
            //{

            //}
            //catch (Exception )
            //{
                
            //    //MessageBox.Show(ex.ToString());
            //    mainwindow.report.AppendText("system：建立连接失败"+Environment.NewLine);
            //    mainwindow.mi_connect.IsEnabled = true;
            //    //tb_isrecieve.Text = ex.ToString();
            //    //throw;
            //}

            #endregion

        }

        #endregion

        #region <-- 异步连接到服务器 -->
        private void Connect2Server(IAsyncResult iar)
        {
            try
            {
                //获取异步操作中返回的TCP客户端
                myTcpClient = (TcpClient)iar.AsyncState;
                if (myTcpClient != null)
                {
                    myTcpClient.EndConnect(iar);
                    
                    //获取绑定的网络数据流
                    ns = myTcpClient.GetStream();
                    //将网络流作为二进制读写对象
                    br = new BinaryReader(ns);
                    bw = new BinaryWriter(ns);

                    //启动消息接收线程
                    //receiveMsgThread = new Thread(ReceiveMsg);
                    //receiveMsgThread.IsBackground = true;
                    //receiveMsgThread.Start();

                    //mainwindow.report.AppendText("system：成功建立连接" + Environment.NewLine);
                    
                    //使用Task来接收消息
                    Task.Run(() => ReceiveMsg());

                }             
                
                //DuelOperate.sendMsg("Connect=" + );
            }
            catch (ThreadAbortException)
            {

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());

                //mainwindow.report.AppendText("system：建立连接失败" + Environment.NewLine);
                //mainwindow.mi_connect.IsEnabled = true;
                //tb_isrecieve.Text = ex.ToString();
                //throw;
            }
        }

        #endregion

        #region <-- 断开连接 -->

        public void disConnect()
        {
            //关闭客户端连接
            myTcpClient.Close();
            //销毁数据流
            if(ns!=null)ns.Dispose();
            //终止接收线程
            if (receiveMsgThread != null)
            {
                receiveMsgThread.Abort();
            }
            
        }

        #endregion

        #region <-- 信息接收 -->
        private void ReceiveMsg()
        {
            //sendMsg("2," + mainwindow.tb_setDuelistName.Text + "," + "-1");
            //byte[] sendData = Encoding.UTF8.GetBytes(msg);
            ////写入网络数据流        
            //ns.Write(sendData, 0, sendData.Length);
            //BinaryReader br = new BinaryReader(ns);
            mainwindow.Dispatcher.BeginInvoke(handleConnectedCallBack, "连接成功");
            string msg = null;
            while (true)
            {

                try
                {
                    #region 废弃的代码

                    ////获得数据
                    //byte[] getData = new byte[1024];
                    //ns.Read(getData, 0, getData.Length);
                    ////转换为字符串形式
                    //string msg = Encoding.UTF8.GetString(getData).Replace("\0","").TrimEnd();
                    ////msg.Replace("\0", "");
                    ////MessageBox.Show(msg);

                    //if (msg.ToLower() == "filebakok")
                    //{

                    //}

                    //mainwindow.Dispatcher.Invoke(handleMsgCallBack, msg);
                    ////OpponentOperate.ActionAnalyze(msg,false);
                    ////将接收到的消息添加到消息控件中
                    ////mainwindow.report.Dispatcher.Invoke(showReceiveMsgCallBack, msg);

                    #endregion

                    msg = br.ReadString();
                    
                    
                    //if (msg.Contains("getcardback="))
                    //{
                    //    int piclen;
                    //    string[] msgs = msg.Split('=');
                    //    if (msgs != null && msgs.Length == 2)
                    //    {
                    //        if (int.TryParse(msgs[1], out piclen))
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

                    //    }

                    //}
                    mainwindow.Dispatcher.Invoke(handleMsgCallBack, msg);

                }
                catch (ThreadAbortException)
                {
                    //mainwindow.report.Dispatcher.Invoke(showReceiveMsgCallBack, "断开连接");
                }
                catch (Exception)
                {


                    if (ns != null)
                    {
                        //释放相关系统资源
                        ns.Dispose();
                        break;
                    }
                }
            }

        }

        #endregion

        #region <-- 信息发送 -->

        public void sendMsg(string message)
        {
            try
            {
                //将字符串写入网络流，此方法会自动附加字符串长度前缀
                bw.Flush();
                bw.Write(message);
                
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

            //if (ns!=null)
            //{
            //    BinaryWriter bw = new BinaryWriter(ns); 
            //    try
            //    {
            //        ////新建字节数组并赋值
            //        //byte[] sendData = Encoding.UTF8.GetBytes(msg);
            //        ////写入网络数据流        
            //        //ns.Write(sendData, 0, sendData.Length);

            //        bw.Flush();
            //        bw.Write(msg);
            //    }
            //    catch (System.IO.IOException)
            //    {
            //        MessageBox.Show("与对手失去连接！");
            //        mainwindow.mi_connect.IsEnabled = true;
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.ToString());
            //    }
                
            //}
            
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
        //添加处理连接成功时调用的被委托的方法
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

            //[userindex][Connect=DuelistName,]

            //sendMsg(DuelOperate.getInstance().sendmyself());
            //string path = AppConfigOperate.getInstance().Custom_path + "\\cardback0.jpg";
            //if (System.IO.File.Exists(path))
            //{

            //    try
            //    {
            //        BitmapImage cardback = BitmapImagehandle.GetBitmapImage(path);
            //        sendMsg( BitmapImagehandle.BitmapImageToByteArray(cardback));
            //    }
            //    catch (ArgumentNullException ex)
            //    {
            //        throw ex;
            //    }
            //    finally
            //    {

            //    }
            //}

            #region 废弃的代码

            //    tcpCommon.SendFile(cardbackpath, ns);
            //    sendMsg("FileBak");

            //    if (ns.Read().ToLower() == "filebakok")
            //    {
            //        client.SendMessage(dt.Rows[i]["RelativePath"].ToString());
            //        client.SendFile(dt.Rows[i]["FullPath"].ToString());
            //        client.SendMessage(client.CalcFileHash(dt.Rows[i]["FullPath"].ToString()));

            //        if (client.ReadMessage().ToLower() == "ok")
            //        {
            //            LOGClass.WriteLog("备份文件【" + dt.Rows[i]["FullPath"].ToString() + "】成功");
            //        }
            //        else
            //        {
            //            LOGClass.WriteLog("备份文件【" + dt.Rows[i]["FullPath"].ToString() + "】失败。");
            //        }
            //    }
            //}

            #endregion
        }

        #endregion

        #region <-- 处理图像接收调用的委托指向的方法 -->

        private void handleCardback(byte[] img)
        {
            BitmapImage opCardback = BitmapImagehandle.ByteArrayToBitmapImage(img);
            DuelOperate.getInstance().opponent.cardback = opCardback;
        }

        #endregion

        //#region <-- 获得卡片引用 -->

        //public static BitmapImage GetBitmapImage(string path)
        //{
        //    BitmapImage bitmap = new BitmapImage();
        //    bitmap.BeginInit();

        //    bitmap.CacheOption = BitmapCacheOption.OnLoad;
        //    bitmap.StreamSource = new MemoryStream(File.ReadAllBytes(path));
        //    bitmap.EndInit();
        //    bitmap.Freeze();
        //    return bitmap;
        //}

        //#endregion

        //#region <-- 2进制数组转换为图像 -->

        //public static BitmapImage ByteArrayToBitmapImage(byte[] byteArray)
        //{
        //    BitmapImage bmp = null;

        //    try
        //    {
        //        bmp = new BitmapImage();
        //        bmp.BeginInit();
        //        bmp.StreamSource = new MemoryStream(byteArray);
        //        bmp.EndInit();
        //    }
        //    catch
        //    {
        //        bmp = null;
        //    }

        //    return bmp;
        //}

        //#endregion

        //#region <-- 图片转换为2进制数组 -->

        //public static byte[] BitmapImageToByteArray(BitmapImage bmp)
        //{
        //    byte[] byteArray = null;

        //    try
        //    {
        //        Stream sMarket = bmp.StreamSource;

        //        if (sMarket != null && sMarket.Length > 0)
        //        {
        //            //很重要，因为Position经常位于Stream的末尾，导致下面读取到的长度为0。
        //            sMarket.Position = 0;

        //            using (BinaryReader br = new BinaryReader(sMarket))
        //            {
        //                byteArray = br.ReadBytes((int)sMarket.Length);
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        //other exception handling
        //    }

        //    return byteArray;
        //}

        //#endregion
    }
}
