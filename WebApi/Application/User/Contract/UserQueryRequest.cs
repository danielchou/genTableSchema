

namespace ESUN.AGD.WebApi.Application.User.Contract
{
    /// <summary>
    /// 進階查詢查詢請求
    /// </summary>
    public class UserQueryRequest : CommonQuery
    {
		/// <summary>
        /// 使用者帳號
        /// </summary>
        public string? userID { get; set; }
		/// <summary>
        /// 使用者名稱
        /// </summary>
        public string? userName { get; set; }
		/// <summary>
        /// CTI登入帳號
        /// </summary>
        public string? agentLoginID { get; set; }
		/// <summary>
        /// 群組代碼
        /// </summary>
        public string? groupID { get; set; }
		/// <summary>
        /// 是否啟用?
        /// </summary>
        public string? isEnable { get; set; }

    }
}
