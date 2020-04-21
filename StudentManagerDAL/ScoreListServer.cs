using StudentManagerModel;
using StudentManagerModel.ObjExt;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagerDAL
{
    public  class ScoreListServer
    {
        /// <summary>
        /// 添加成绩
        /// </summary>
        /// <param name="score"></param>
        /// <returns></returns>
        public int AddScoreManager(ScoreList score)
        {
            string sql = string.Format("INSERT INTO dbo.ScoreList([StudentID],CSharp,SQLServerDB) values('{0}','{1}','{2}')", score.StudentlD, score.CSharp, score.SQLServerDB);
            return DBHelper.SQLHelper.ExecuteNonQuery(sql);


        }
        /// <summary>
        /// 根据班级查询
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public List<ScoreExt> GetScoreList(int cid)
        {
            string sql = "SELECT ScoreList.StudentID, Students.StudentName ,CSharp,SQLServerDB,UpdateTime,StudentClass.ClassName FROM ScoreList INNER JOIN Students ON Students.Studentid=ScoreList.StudentID INNER JOIN StudentClass ON StudentClass.Classid=Students.Classid WHERE StudentClass.ClassID=" + cid;
            SqlDataReader reader = DBHelper.SQLHelper.GetReader(sql);
            List<ScoreExt> list = DataReturnObj(reader);
            reader.Close();
            return list;
        }
        private static List<ScoreExt> DataReturnObj(SqlDataReader reader)
        {
            List<ScoreExt> list = new List<ScoreExt>();
            while (reader.Read())
            {
                list.Add(new ScoreExt()
                {
                    StudentlD = Convert.ToInt32(reader["StudentID"]),
                    StudentName = reader["StudentName"].ToString(),
                    CSharp = Convert.ToInt32(reader["CSharp"]),
                    ClassName = reader["ClassName"].ToString(),
                    SQLServerDB = Convert.ToInt32(reader["SQLServerDB"]),
                    UPdateTime = Convert.ToDateTime(reader["UpdateTime"])
                });
            }

            return list;
        }
        /// <summary>
        /// 模糊查询
        /// </summary>
        public List<ScoreExt> GetStudentByldOrName(string target)
        {
           
            string sql = string.Format("SELECT ScoreList.StudentID,Students.StudentName ,CSharp,SQLServerDB,UpdateTime,StudentClass.ClassName FROM ScoreList INNER JOIN Students ON Students.Studentid=ScoreList.StudentID INNER JOIN StudentClass ON StudentClass.Classid=Students.Classid WHERE ScoreList.StudentID LIKE '%{0}%' OR StudentName LIKE '%{0}%'", target);
            SqlDataReader reader = DBHelper.SQLHelper.GetReader(sql);
            List<ScoreExt> list = DataReturnObj(reader);
            reader.Close();
            return list;
        }
        /// <summary>
        /// 删除学员
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public int DeleteStudentById(int sid)
        {
            string sql = "DELETE FROM ScoreList WHERE StudentID=" + sid;
            return DBHelper.SQLHelper.ExecuteNonQuery(sql);
        }
        public ScoreExt GetStudentById(int id)
        {
            string sql = "SELECT ScoreList.StudentID, Students.StudentName ,CSharp,SQLServerDB,UpdateTime,StudentClass.ClassName FROM ScoreList INNER JOIN Students ON Students.Studentid=ScoreList.StudentID INNER JOIN StudentClass ON StudentClass.Classid=Students.Classid WHERE ScoreList.Studentid=" + id;
            SqlDataReader reader = DBHelper.SQLHelper.GetReader(sql);
            ScoreExt student = null;
            while (reader.Read())
            {
                student = (new ScoreExt()
                {
                    StudentlD = Convert.ToInt32(reader["StudentID"]),
                    StudentName = reader["StudentName"].ToString(),
                    CSharp = Convert.ToInt32(reader["CSharp"]),
                    ClassName = reader["ClassName"].ToString(),
                    SQLServerDB = Convert.ToInt32(reader["SQLServer"]),
                    UPdateTime = Convert.ToDateTime(reader["UpdateTime"])
                });
            }
            return student;
        }
        /// <summary>
        /// 修改学员信息
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public int UpdateStudentInfor(ScoreExt student)
        {
            string sql = string.Format("UPDATE ScoreList SET CSharp={0},SQLServerDB={1} WHERE StudentID={2}", student.CSharp, student.SQLServerDB, student.StudentlD);
            return DBHelper.SQLHelper.ExecuteNonQuery(sql);
        }


        /// <summary>
        /// 打印到Excel
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public DataTable GetDataTable(int cid)
        {
            string sql = "SELECT ScoreList.StudentID, Students.StudentName ,CSharp,SQLServerDB,UpdateTime,StudentClass.ClassName FROM ScoreList INNER JOIN Students ON Students.Studentid=ScoreList.StudentID INNER JOIN StudentClass ON StudentClass.Classid=Students.Classid WHERE StudentClass.ClassID=" + cid;
            DataTable table = DBHelper.SQLHelper.GetDataSet(sql).Tables[0];
            return table;

            
        }

    }
}
