using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Xml;
using System.Xml.Linq;

namespace iDuel_EvolutionX.Service
{
    class AppConfigOperate
    {
        private static AppConfigOperate iduelconfig;
        private string configUrl;
        private static XmlDocument config;
        private static Dictionary<string, string> deckManergerDocument; //保存卡组管理的相关配置信息
        private string deck_first;      //第1个卡组文件夹名字
        private string deck_first_path; //第1个卡组文件夹路径
        private string deck_second;     //第2个卡组文件夹名字
        private string deck_second_path;//第2个卡组文件夹路径
        private string deck_third;      //第3个卡组文件夹名字
        private string deck_third_path; //第3个卡组文件夹路径
        private string image_path;      //卡图文件夹路径
        private string token_path;      //Token图文件夹路径
        private string custom_path;     //自定义图文件夹路径
        private IPAddress ip;  //ip地址
        private int socket;    //端口号          
        private string key;    //密码
        private string duelist;//己方昵称

        #region <-- 字段的封装 -->

        public string Deck_first
        {
            get { return deck_first; }
            set { deck_first = value; }
        }
        
        public string Deck_first_path
        {
            get { return deck_first_path; }
            set { deck_first_path = value; }
        }

        
        public string Deck_second
        {
            get { return deck_second; }
            set { deck_second = value; }
        }
        
        public string Deck_second_path
        {
            get { return deck_second_path; }
            set { deck_second_path = value; }
        }
        
        public string Deck_third
        {
            get { return deck_third; }
            set { deck_third = value; }
        }
        
        public string Deck_third_path
        {
            get { return deck_third_path; }
            set { deck_third_path = value; }
        }
        
        public string Image_path
        {
            get { return image_path; }
            set { image_path = value; }
        }
        
        public string Token_path
        {
            get { return token_path; }
            set { token_path = value; }
        }
        
        public string Custom_path
        {
            get { return custom_path; }
            set { custom_path = value; }
        }
        
        public IPAddress Ip
        {
            get { return ip; }
            set 
            { 
                ip = value;
                saveSetting("ip", value.ToString());
            }
        }
             
        public int Socket
        {
            get { return socket; }
            set 
            { 
                socket = value;
                saveSetting("socket", value.ToString());
            }
        }
        
        public string Key
        {
            get { return key; }
            set 
            { 
                key = value;
                saveSetting("key", value.ToString());
            }

        }
        
        public string Duelist
        {
            get { return duelist; }
            set 
            { 
                duelist = value;
                saveSetting("duelist", value.ToString());
            }
        }

        #endregion


        //private static string deckManergerDocument1;
        //private static string 

        public static Dictionary<string, string> DeckManergerDocument
        {
            get { return AppConfigOperate.deckManergerDocument; }
            set { AppConfigOperate.deckManergerDocument = value; }
        }

        public AppConfigOperate()
        {

        }

        public static AppConfigOperate getInstance()
        {
            if (iduelconfig == null)
            {
                iduelconfig = new AppConfigOperate();
                config = new XmlDocument();
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.IgnoreComments = true;//忽略文档里面的注释
                iduelconfig.configUrl = System.IO.Directory.GetCurrentDirectory() + @"\Data\Setting.config";
                if (System.IO.File.Exists(iduelconfig.configUrl))
                {
                    XmlReader reader = XmlReader.Create(iduelconfig.configUrl, settings);
                    config.Load(reader);
                    XmlNodeList xnl = config.ChildNodes;
                    getAllConfig(xnl);
                    reader.Close();
                }
                else
                {
                    return null;
                }
                
            }
            return iduelconfig;
        }

        /// <summary>
        /// 获取所有设置
        /// </summary>
        /// <param name="xnl"></param>
        private static void getAllConfig(XmlNodeList xnl)
        {
            DeckManergerDocument = new Dictionary<string, string>();
            string appPath = System.IO.Directory.GetCurrentDirectory();
            //System.IO.Path.Combine()

            foreach (XmlNode xn in xnl)
            {
                if (xn.Name.Equals("Settings"))
                {                  
                    //XmlElement xe = (XmlElement)xn;               
                    //DeckManergerDocument.Add("Path", xe.GetAttribute("Path").ToString());                 
                    foreach (XmlNode xn2 in xn.ChildNodes)
                    {
                        XmlElement setting = (XmlElement)xn2;
                        switch (setting.GetAttribute("name"))
                        {
                            #region <-- 设定卡组文件夹1路径&名字 -->
                            case "deck_first":
                                {
                                    iduelconfig.Deck_first = setting.GetAttribute("value");
                                    iduelconfig.Deck_first_path = appPath + setting.GetAttribute("path")+ "\\"+iduelconfig.deck_first;
                                }
                                break;
                            #endregion
                            #region <-- 设定卡组文件夹2路径&名字 -->
                            case "deck_second":
                                {
                                    iduelconfig.Deck_second = setting.GetAttribute("value");
                                    iduelconfig.Deck_second_path = appPath + setting.GetAttribute("path") + "\\" + iduelconfig.deck_second;
 
                                }
                                break;
                            #endregion
                            #region <-- 设定卡组文件夹3路径&名字 -->
                            case "deck_third":
                                {
                                    iduelconfig.Deck_third = setting.GetAttribute("value");
                                    iduelconfig.Deck_third_path = appPath + setting.GetAttribute("path") + "\\" + iduelconfig.deck_third;
                                }
                                break;
                            #endregion
                            #region <-- 设定卡图文件夹路径 -->
                            case "image_path":
                                {
                                    iduelconfig.Image_path = appPath + setting.GetAttribute("path");
                                }
                                break;
                            #endregion
                            #region <-- 设定Token图文件夹路径 -->
                            case "token_path":
                                {
                                    iduelconfig.Token_path = appPath + setting.GetAttribute("path");
                                }
                                break;
                            #endregion
                            #region <-- 设定自定义文件夹路径 -->
                            case "custom_path":
                                {
                                    iduelconfig.Custom_path = appPath + setting.GetAttribute("path");
                                }
                                break;
                            #endregion
                            #region <-- 设定IP -->
                            case "ip":
                                {
                                    if (!IPAddress.TryParse(setting.GetAttribute("value"), out iduelconfig.ip))
                                    {
                                        MessageBox.Show("配置文件中设定的IP不符合规范");
                                    }
                                }
                                break;
                            #endregion
                            #region <-- 设定端口号 -->
                            case "socket":
                                {
                                    if (!int.TryParse(setting.GetAttribute("value"),out iduelconfig.socket))
                                    {
                                        MessageBox.Show("配置文件中设定的端口号不符合规范");
                                    }
                                }
                                break;
                            #endregion
                            #region <-- 设定密码 -->
                            case "key":
                                {
                                    iduelconfig.Key = setting.GetAttribute("value");
                                }
                                break;
                            #endregion
                            #region <-- 设定玩家名字 -->
                            case "duelist":
                                {
                                    iduelconfig.Duelist = setting.GetAttribute("value");
                                }
                                break;
                            #endregion
                            
                        }
                        //DeckManergerDocument.Add(index.GetAttribute("index"), index.InnerText);
                    }
                }
            }

        }

        /// <summary>
        /// 保存设置
        /// </summary>
        /// <param name="setting">name</param>
        /// <param name="value">value</param>
        private void saveSetting(string setting,string value)
        {
            XmlNodeList xnl = config.ChildNodes;
            XElement xe = XElement.Load(iduelconfig.configUrl); //使用linq2Xml
            IEnumerable<XElement> element = from _setting in xe.Elements("setting")
                                            where _setting.Attribute("name").Value == setting
                                            select _setting;
            if ( element.Count() > 0)
            {
                XElement first = element.First();
                first.SetAttributeValue("value", value);
            }
            try
            {
                xe.Save(iduelconfig.configUrl);
            }
            catch (Exception)
            {             
                
            }
            
        }

    }

    
}
