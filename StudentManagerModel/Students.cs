using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagerModel
{
    /// <summary>
    /// 学生实体对象
    /// </summary>
    [Serializable]
    public  class Students
    {
        /// <summary>
        /// 学号
        /// </summary>
        public int StudentID { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string StudentName { get; set; }

       /// <summary>
       /// 性别
       /// </summary>
        public string StudentSex { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public string Birthday { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string StudentidNo { get; set; }

        /// <summary>
        /// 打卡号
        /// </summary>
        public string CardNo { get; set; }

        /// <summary>
        /// 学生图片
        /// </summary>
        public string StuImage { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// 电话号
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string StudentAddress { get; set; }

        /// <summary>
        /// 班级编号
        /// </summary>
        public int ClasslD{ get; set; }

        public int ClassID { get; set; }

    }
}
