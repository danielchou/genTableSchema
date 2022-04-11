namespace ESUN.AGD.WebApi.Application.UserRole.Contract
{
    /// <summary>
    /// 電腦電話新增請求
    /// </summary>
    public class UserRoleInsertRequest : CommonInsertRequest
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