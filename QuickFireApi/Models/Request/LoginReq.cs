namespace QuickFireApi.Models.Request
{
    public class LoginReq
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public required string username { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public required string password { get; set; }
    }
}
