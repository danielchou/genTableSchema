namespace ESUN.AGD.WebApi.Application.UserRole.Contract
{
    /// <summary>
    /// 使用者角色配對回傳結果
    /// </summary>
    public class  UserRoleResponse : CommonResponse
    {
        /// <summary>
        /// 使用者帳號
        /// </summary>
        public string userID { get; set; }
        /// <summary>
        /// 角色代碼
        /// </summary>
        public string roleID { get; set; }
    }
}
