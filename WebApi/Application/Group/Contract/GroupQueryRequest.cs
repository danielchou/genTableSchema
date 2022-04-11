

namespace ESUN.AGD.WebApi.Application.Group.Contract
{
    /// <summary>
    /// 進階查詢查詢請求
    /// </summary>
    public class GroupQueryRequest : CommonQuery
    {
		/// <summary>
        /// 群組代碼
        /// </summary>
        public string groupID { get; set; } 
		/// <summary>
        /// 群組名稱
        /// </summary>
        public string groupName { get; set; } 
    }
}
