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
    /// <summary>
    /// 学员考勤管理模块
    /// </summary>
    public class StudentAttendanceServer
    {

        /// <summary>
        /// 查询所有考勤
        /// </summary>
        /// <returns></returns>
        public List<Attendance> GetAttendance()
        {
            string sql = "SELECT * FROM Attendance";
            SqlDataReader reader = DBHelper.SQLHelper.GetReader(sql);
            List<Attendance> list = new List<Attendance>();
            while (reader.Read())
            {
                list.Add(new Attendance()
                {
                    CardNo = (reader["CardNo"]).ToString(),
                    DTime = (DateTime)reader["ClassName"]
                });
            }
            reader.Close();
            return list;
        }

        public List<StudentExt> GetStudents1(int cid1)
        {
            string sql = "SELECT StudentID,StudentName,StudentSex,Birthday,StudentidNo,Attendance.CardNo,StuImage,Age,PhoneNumber,StudentAddress,Attendance.DTime FROM Students , StudentClass , Attendance WHERE StudentClass.ClassID=Students.ClassID  AND Attendance.CardNo=Students.CardNo and StudentClass.ClassID=" + cid1;
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
                    //ClassName = reader["ClassName"].ToString(),
                    StudentSex = reader["StudentSex"].ToString(),
                    PhoneNumber = reader["PhoneNumber"].ToString(),
                    StudentAddress = reader["StudentAddress"].ToString(),
                    StudentidNo = reader["StudentidNo"].ToString(),
                    StudentName = reader["StudentName"].ToString(),
                    StuImage = reader["StuImage"].ToString(),
                    DTime=(DateTime)reader["DTime"]
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
            string sql = string.Format("SELECT StudentID, StudentName, StudentSex, Birthday, StudentidNo, Attendance.CardNo, StuImage, Age, PhoneNumber, StudentAddress,Attendance.DTime FROM Students INNER JOIN Attendance ON Attendance.CardNo=Students.CardNo WHERE StudentID LIKE '%{0}%'OR StudentName LIKE  '%{0}%'", target);
            SqlDataReader reader = DBHelper.SQLHelper.GetReader(sql);
            List<StudentExt> list = DataReturnObj1(reader);
            reader.Close();
            return list;
        }
        private static List<StudentExt> DataReturnObj1(SqlDataReader reader)
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
                    //ClassName = reader["ClassName"].ToString(),
                    StudentSex = reader["StudentSex"].ToString(),
                    PhoneNumber = reader["PhoneNumber"].ToString(),
                    StudentAddress = reader["StudentAddress"].ToString(),
                    StudentidNo = reader["StudentidNo"].ToString(),
                    StudentName = reader["StudentName"].ToString(),
                    StuImage = reader["StuImage"].ToString(),
                    DTime = (DateTime)reader["DTime"]
                });
            }
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
            string sql = string.Format("SELECT StudentID, StudentName,StudentSex,Birthday, StudentidNo, CardNo, StuImage, Age, PhoneNumber, StudentAddress, StudentClass.ClassName,Attendance.DTime FROM Students INNER JOIN StudentClass ON Attendance.CardNo=Students.CardNo WHERE StudentID = {0}", sid);
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
                    StuImage = reader["StuImage"].ToString(),
                      DTime = (DateTime)reader["DTime"]
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
        /// 打印到Excel
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public DataTable GetDataTable(int cid)
        {
            string sql = "SELECT StudentID,StudentName,StudentSex,Birthday,StudentidNo,Attendance.CardNo,Age,PhoneNumber,StudentAddress,StudentClass.ClassName,Attendance.DTime FROM Students , StudentClass , Attendance WHERE StudentClass.ClassID=Students.ClassID  AND Attendance.CardNo=Students.CardNo and StudentClass.ClassID=" + cid;
            DataTable table = DBHelper.SQLHelper.GetDataSet(sql).Tables[0];
            return table;
        }

    }
}
