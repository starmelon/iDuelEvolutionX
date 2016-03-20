using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data;

namespace iDuel_EvolutionX.ADO
{
    class SqliteHelper
    {
        /// <summary>
        /// 在ADO.net中，null为不提供参数，因此必须对参数进行判断和转化成DBnNull
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static object FromDbValue(object value)
        {
            if (value == DBNull.Value)
            {
                return null;
            }
            else
            {
                return value;
            }
        }

        public static object ToDbValue(object value)
        {
            if (value == null)
            {
                return DBNull.Value;
            }
            else
            {
                return value;
            }
        }

        //设置连接字符串
        private static string connStr = "Data Source=" + System.IO.Directory.GetCurrentDirectory() + @"\Data\exp_1403445880.sqlite;Pooling=true;FailIfMissing=false";
         //private static string password = "i-star@ocgsoft.cn";

        //更换Sqlite版本报错时跟密码有关
       private static string password = "";

        public static int ExecuteNonQuery(string sql, params SQLiteParameter[] parameters)
        {

            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                //连接数据库
                conn.SetPassword(password);
                conn.Open();
                using (SQLiteCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;

                    //foreach (SqlParameter param in parameters)
                    //{
                    //    cmd.Parameters.Add(param);
                    //}
                    //更简洁的写法如下
                    cmd.Parameters.AddRange(parameters);

                    return cmd.ExecuteNonQuery();
                }
            }

        }

        public static object ExecuteScalar(string sql, params SQLiteParameter[] parameters)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.SetPassword(password);
                conn.Open();
                using (SQLiteCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteScalar();
                }

            }
        }

        //只用来执行查询结果比较少的sql
        //public static DataSet ExecuteDataSet(string sql)
        //一般只返回一个表
        /// <summary>
        /// 调用范例：
        /// 在UI层的调用范例
        /// DataTable table = SqlHelper.ExecuteDataTable("sql语句（参数化范例形式@Name）",
        ///     new SqlParameter("@Name","小明")
        ///     new SqlParameter("@Age",20)
        ///     ）;
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(string sql, params SQLiteParameter[] parameters)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.SetPassword(password);
                conn.Open();
                using (SQLiteCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(parameters);
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
                    DataSet dataset = new DataSet();
                    adapter.Fill(dataset);
                    return dataset.Tables[0];
                }
            }
        }

        public static DataTable ExecuteDataTable(string sql)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.SetPassword(password);
                conn.Open();
                conn.ChangePassword("");
                using (SQLiteCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    //cmd.Parameters.AddRange(parameters);
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    //DataSet dataset = new DataSet();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }

    }
}
