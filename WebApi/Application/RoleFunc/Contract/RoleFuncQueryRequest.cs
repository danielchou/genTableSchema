

namespace ESUN.AGD.WebApi.Application.RoleFunc.Contract
{
    /// <summary>
    /// 進階查詢查詢請求
    /// </summary>
    public class RoleFuncQueryRequest : CommonQuery
    {
		/// <summary>
        /// 角色代碼
        /// </summary>
        public string? roleID { get; set; }
		/// <summary>
        /// 功能代碼
        /// </summary>
        public string? funcID { get; set; }

    }
}
