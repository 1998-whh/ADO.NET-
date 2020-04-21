using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagerDAL.DBHelper
{
    class SQLHelper
    {
        static string con = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        //static string con = "Server=.;Initial Catalog=InLett1;Trusted_Connection=True;";
        /// <summary>
        /// 该方法可以对SQL语句执行完成之后返回结果为受影响行数的SQL语句进行命令执行(增加、删除、修改数据操作);
        /// </summary>
        /// <param name="sql">要执行的SQL语句</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string sql)
        {
            SqlConnection Con = new SqlConnection(con);
            SqlCommand cmd = new SqlCommand(sql, Con);
            try
            {
                Con.Open();
                return cmd.ExecuteNonQuery();
  
            }
            catch (Exception ex)
            {
                //记录日志
                throw ex;
            }
            finally
            {
                Con.Close();
            }
        }

        /// <summary>
        ///  该方法可以对SQL语句执行完成之后返回结果中的首行首列数据，适用于SQL语句查询单一结果值
        /// </summary>
        /// <param name="sql">查询的SQL语句</param>
        /// <returns></returns>
        public static object ExecuteScalar(string sql)
        {
            SqlConnection Con = new SqlConnection(con);
            SqlCommand cmd = new SqlCommand(sql, Con);
            try
            {
                Con.Open();
                return cmd.ExecuteScalar();

            }
            catch (Exception ex)
            {
                //记录日志
                throw ex;
            }
            finally
            {
                Con.Close();
            }
        }
        /// <summary>
        /// 查询结果返回DataSet
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataSet GetDataSet(string sql)
        {
            SqlConnection Con = new SqlConnection(con);
            SqlCommand cmd = new SqlCommand(sql, Con);
            DataSet set = new DataSet();
            try
            {
                Con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(set);
                return set;
            }
            catch (Exception ex)
            {
                //记录日志
                throw ex;
            }
            finally
            {
                Con.Close();
            } 
        }
        /// <summary>
        /// 查询结果用GetReader
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static SqlDataReader GetReader(string sql)
        {
            SqlConnection Con = new SqlConnection(con);
            SqlCommand cmd = new SqlCommand(sql, Con);
            try
            {
                Con.Open();
                //不需要手动关闭con
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                //记录日志
                throw ex;
            }
        }
    }
}
