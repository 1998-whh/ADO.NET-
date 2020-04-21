using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentManagerDAL.DBHelper;
using StudentManagerModel.ObjExt;
using StudentManagerModel;
using StudentManagerDAL;
using System.Data.SqlClient;
using System.Data;
using Common;

namespace StudentManagerDAL
{
    /// <summary>
    /// 学生管理数据访问
    /// </summary>
    public class StudentServer
    {
        public List<StudentExt> GetStudents(int cid)
        {
            string sql = "SELECT StudentID,StudentName,StudentSex,Birthday,StudentidNo,CardNo,StuImage,Age,PhoneNumber,StudentAddress,StudentClass.ClassName FROM Students INNER JOIN StudentClass ON StudentClass.ClassID=Students.ClassID WHERE StudentClass.ClassID=" + cid;
            SqlDataReader reader = DBHelper.SQLHelper.GetReader(sql);
            List<StudentExt> list = DataReturnObj(reader);
            return list;
        }

        private static List<StudentExt> DataReturnObj(SqlDataReader reader)
        {
            List<StudentExt> list = new List<StudentExt>();
            while (reader.Read())
            {
                list.Add(new StudentExt()
                {
                    StudentID = Convert.ToInt32(reader["StudentID"]),
                    Age = Convert.ToInt32(reader["Age"]),
                    Birthday = Convert.ToString(reader["Birthday"]),
                    CardNo = reader["CardNo"].ToString(),
                    ClassName = reader["ClassName"].ToString(),
                    StudentSex = reader["StudentSex"].ToString(),
                    PhoneNumber = reader["PhoneNumber"].ToString(),
                    StudentAddress = reader["StudentAddress"].ToString(),
                    StudentidNo = reader["StudentidNo"].ToString(),
                    StudentName = reader["StudentName"].ToString(),
                    StuImage = reader["StuImage"].ToString()
                });
            }
            reader.Close();
            return list;
        }

        /// <summary>
        /// 根据ID姓名查询
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public List<StudentExt> GetStudentsByIdOrname(string target)
        {
            string sql = string.Format("SELECT StudentID, StudentName, StudentSex, Birthday, StudentidNo, CardNo, StuImage, Age, PhoneNumber, StudentAddress, StudentClass.ClassName FROM Students INNER JOIN StudentClass ON StudentClass.ClassID = Students.ClassID WHERE StudentID LIKE '%{0}%'OR StudentName LIKE  '%{0}%'",target);
            SqlDataReader reader = DBHelper.SQLHelper.GetReader(sql);
            List<StudentExt> list = DataReturnObj(reader);
            reader.Close();
            return list;
        }
        /// <summary>
        /// 查看信息
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public StudentExt extstudent(int sid)
        {
            string sql = string.Format("SELECT StudentID, StudentName, StudentSex, Birthday, StudentidNo, CardNo, StuImage, Age, PhoneNumber, StudentAddress, StudentClass.ClassName FROM Students INNER JOIN StudentClass ON StudentClass.ClassID = Students.ClassID WHERE StudentID = {0}", sid);
            SqlDataReader reader = DBHelper.SQLHelper.GetReader(sql);
            StudentExt student = null;
            while (reader.Read())
            {
                student = new StudentExt()
                {
                    StudentID = Convert.ToInt32(reader["StudentID"]),
                    Age = Convert.ToInt32(reader["Age"]),
                    Birthday = reader["Birthday"].ToString(),
                    CardNo = reader["CardNo"].ToString(),
                    ClassName = reader["ClassName"].ToString(),
                    StudentSex = reader["StudentSex"].ToString(),
                    PhoneNumber = reader["PhoneNumber"].ToString(),
                    StudentAddress = reader["StudentAddress"].ToString(),
                    StudentidNo = reader["StudentidNo"].ToString(),
                    StudentName = reader["StudentName"].ToString(),
                    StuImage = reader["StuImage"].ToString()
                };               
            }            
            return student;
        }

        /// <summary>
        /// 修改信息
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public int XiugaiStudentManage(StudentExt student)
        {
            string sql = string.Format("UPDATE Students SET StudentName='{0}',StudentSex='{1}',Birthday='{2}',StudentidNo='{3}',CardNo='{4}',StuImage='{5}',Age={6},PhoneNumber='{7}',StudentAddress='{8}',ClassID={9} WHERE StudentID={10}", student.StudentName, student.StudentSex, student.Birthday, student.StudentidNo, student.CardNo, student.StuImage, student.Age, student.PhoneNumber, student.StudentAddress, student.ClasslD, student.StudentID);
            return DBHelper.SQLHelper.ExecuteNonQuery(sql);
        }

       


        /// <summary>
        /// 添加信息
        /// </summary>
        /// <param name="student1"></param>
        /// <returns></returns>
        public int AddstudentManager(StudentExt student1)
        {
            string sql = string.Format("INSERT INTO dbo.Students values('{0}','{1}','{2}','{3}','{4}','{5}',{6},'{7}','{8}','{9}')", student1.StudentName, student1.StudentSex, student1.Birthday, student1.StudentidNo, student1.CardNo, student1.StuImage, student1.Age, student1.PhoneNumber, student1.StudentAddress, student1.ClasslD);
            return DBHelper.SQLHelper.ExecuteNonQuery(sql);
           

        }
      

        /// <summary>
        /// 删除学员信息
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public int DeleteStudentById(int sid)
        {
            string sql = "DELETE FROM Students WHERE StudentID=" + sid;
            return DBHelper.SQLHelper.ExecuteNonQuery(sql);
        }



        /// <summary>
        /// 打印到Excel
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public DataTable GetDataTable(int cid)
        {
            string sql = "SELECT StudentID ,StudentName,StudentSex,Birthday,StudentidNo,CardNo,Age,PhoneNumber,StudentAddress,StudentClass.ClassName FROM Students INNER JOIN StudentClass ON StudentClass.ClassID=Students.ClassID WHERE StudentClass.ClassID=" + cid;
            DataTable table = DBHelper.SQLHelper.GetDataSet(sql).Tables[0];
            return table;
        }


        StudentClassServer classServer = new StudentClassServer();
        /// <summary>
        /// 从Excel文件中读取数据
        /// </summary>
        /// <returns></returns>
        public List<StudentExt> GetStudentByExcel(string path)
        {
            List<StudentExt> list = new List<StudentExt>();
            string sql = string.Format("select * from [{0}$] ", Common.ImportOrExportData.SheetName(path));
            DataSet ds = Common.OleDbHelper.GetDataSet(sql, path);
            DataTable dt = ds.Tables[0];
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new StudentExt()
                {
                    StudentName = row["姓名"].ToString(),
                    StudentSex = row["性别"].ToString(),
                    Birthday = Convert.ToString(row["出生日期"]),
                    Age = DateTime.Now.Year - Convert.ToDateTime(row["出生日期"]).Year,
                    CardNo = row["考勤卡号"].ToString(),
                    StudentidNo = row["身份证号"].ToString(),
                    PhoneNumber = row["电话号码"].ToString(),
                    StudentAddress = row["家庭住址"].ToString(),
                    ClassName = row["班级名称"].ToString(),
                    ClasslD = classServer.GetClassIdByName(row["班级名称"].ToString())
                   
                });
            }
            return list;
        }

        /// <summary>
        /// 查询班级姓名
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int CheckStuId(string id)
        {
            string sql = "SELECT COUNT(*) FROM Students WHERE StudentidNo='" + id + "'";
            object res = DBHelper.SQLHelper.ExecuteScalar(sql);
            return (int)res;
        }

        /// <summary>
        /// 添加学员信息
        /// </summary>
        /// <param name="stu"></param>
        /// <returns></returns>
        public int InsertStudent(StudentExt stu)
        {
            string sql = string.Format("INSERT INTO dbo.Students values('{0}','{1}','{2}','{3}','{4}','{5}',{6},'{7}','{8}','{9}')", stu.StudentName, stu.StudentSex, stu.Birthday, stu.StudentidNo, stu.CardNo, stu.StuImage, stu.Age, stu.PhoneNumber, stu.StudentAddress, stu.ClasslD);
            return DBHelper.SQLHelper.ExecuteNonQuery(sql);
        }

        
    }
}
