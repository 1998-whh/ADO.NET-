using StudentManagerDAL;
using StudentManagerModel.ObjExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentManagerModel;
using System.Data;

namespace StudentManagerBLL
{
    /// <summary>
    /// 学生管理业务逻辑
    /// </summary>
    public class StudentManager
    {
        StudentServer server = new StudentServer();
        /// <summary>
        /// 班级查询
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public List<StudentExt> GetStudents(int cid)
        {
            return server.GetStudents(cid);
        }
        /// <summary>
        /// 学号姓名查询
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public List<StudentExt> GetStudentsByIdOrname(string target)
        {
           return server.GetStudentsByIdOrname(target);
        }

        /// <summary>
        /// 查看信息
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public StudentExt GetStudentId(int sid)
        {
            return server.extstudent(sid);
        }


        /// <summary>
        /// 修改信息
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public bool XiugaiStudentManage(StudentExt student)
        {
            if (server.XiugaiStudentManage(student) <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 添加信息
        /// </summary>
        /// <param name="student1"></param>
        /// <returns></returns>
        public bool AddstudentManager(StudentExt student1)
        {
            if (server.AddstudentManager(student1) <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 删除学员信息
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public bool DeleteStudentById(int sid)
        {
            if (server.DeleteStudentById(sid) <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public StudentExt GetStudentById(int sid)
        {
            return server.extstudent(sid);
        }

        /// <summary>
        /// 打印到excel
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public DataTable GetDataTable(int cid)
        {
            return server.GetDataTable(cid);
        }

        /// <summary>
        /// 导入Excel
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public List<StudentExt> GetStudentByExcel(string path)
        {
            return server.GetStudentByExcel(path);
        }

        public int InsertStudent(StudentExt stu)
        {
            if (server.CheckStuId(stu.StudentidNo) > 0)
            {
                return -1;
            }
            return server.InsertStudent(stu);
        }
    }
}
