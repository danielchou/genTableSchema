

namespace ESUN.AGD.WebApi.Application.Role.Contract
{
    /// <summary>
    /// 進階查詢查詢請求
    /// </summary>
    public class RoleQueryRequest : CommonQuery
    {
		/// <summary>
        /// 角色代碼
        /// </summary>
        public string roleID { get; set; } 
		/// <summary>
        /// 角色名稱
        /// </summary>
        public string roleName { get; set; } 
    }
}
