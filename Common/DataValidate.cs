using System.Text.RegularExpressions;
namespace Common
{
    public class DataValidate
    {
        /// <summary>
        /// 验证正整数
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsInteger(string str)
        {
            Regex regex = new Regex(@"^[1-9]\d*$");
            return regex.IsMatch(str);
        }

        /// <summary>
        /// 手机号验证
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsPhone(string str)
        {
            Regex regex = new Regex(@"^1[3578]\d{8}$");
            return regex.IsMatch(str);
        }

        /// <summary>
        /// 身份证验证
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsSCard(string str)
        {
            Regex reg = new Regex(@"(^\d{ 18 }$)| (^\d{ 17} (\d | X | x)$)");
            return reg.IsMatch(str);
        }
    }
}
