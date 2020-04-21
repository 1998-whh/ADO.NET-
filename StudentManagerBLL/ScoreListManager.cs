using StudentManagerDAL;
using StudentManagerModel;
using StudentManagerModel.ObjExt;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagerBLL
{
    public class ScoreListManager
    {
        ScoreListServer server = new ScoreListServer();

        /// <summary>
        /// 添加成绩
        /// </summary>
        /// <param name="score"></param>
        /// <returns></returns>
        public bool AddScoreManager(ScoreList score)
        {
            if (server.AddScoreManager(score) <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        /// <summary>
        /// 全部查询
        /// </summary>
       
        public List<ScoreExt> GetScoreList(int cid)
        {
            return server.GetScoreList(cid);
        }
        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public List<ScoreExt> GetStudentByldOrName(string target)
        {
            return server.GetStudentByldOrName(target);
        }
        /// <summary>
        /// 删除成绩
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
        /// <summary>
        /// 查看个人成绩
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ScoreExt GetStudentById(int id)
        {
            return server.GetStudentById(id);
        }
        /// <summary>
        /// 修改成绩
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public bool UpdateStudentInfor(ScoreExt student)
        {
            if (server.UpdateStudentInfor(student) <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
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
    }
}
