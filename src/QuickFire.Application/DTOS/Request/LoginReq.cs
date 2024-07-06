namespace QuickFire.Application.DTOS.Request
{
    public class LoginReq
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public required string userAccount { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public required string password { get; set; }
    }
}
