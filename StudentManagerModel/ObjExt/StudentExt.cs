using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagerModel.ObjExt
{
    public class StudentExt : Students
    {
        public string ClassName { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public string ImgPath { get; set; }

        /// <summary>
        /// 打卡时间
        /// </summary>
        public DateTime DTime { get; set; }

    }
}
