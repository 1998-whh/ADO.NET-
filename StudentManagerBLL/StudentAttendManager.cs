using StudentManagerDAL;
using StudentManagerModel.ObjExt;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagerBLL
{
    public class StudentAttendManager
    {
        StudentServer Server = new StudentServer();
        StudentAttendanceServer attendanceServer = new StudentAttendanceServer();
        /// <summary>
        /// 班级查询
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public List<StudentExt> GetStudents(int cid)
        {
            return attendanceServer.GetStudents1(cid);
        }
        /// <summary>
        /// 学号姓名查询
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public List<StudentExt> GetStudentsByIdOrname(string target)
        {
            return attendanceServer.GetStudentsByIdOrname(target);
        }

        /// <summary>
        /// 打印到excel
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public DataTable GetDataTable(int cid)
        {
            return attendanceServer.GetDataTable(cid);
        }

    }
}
