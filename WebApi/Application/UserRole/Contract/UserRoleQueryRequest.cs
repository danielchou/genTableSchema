

namespace ESUN.AGD.WebApi.Application.UserRole.Contract
{
    /// <summary>
    /// 進階查詢查詢請求
    /// </summary>
    public class UserRoleQueryRequest : CommonQuery
    {
		/// <summary>
        /// 使用者帳號
        /// </summary>
        public string? userID { get; set; }
		/// <summary>
        /// 角色代碼
        /// </summary>
        public string? roleID { get; set; }

    }
}
