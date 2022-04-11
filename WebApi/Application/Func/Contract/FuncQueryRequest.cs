

namespace ESUN.AGD.WebApi.Application.Func.Contract
{
    /// <summary>
    /// 進階查詢查詢請求
    /// </summary>
    public class FuncQueryRequest : CommonQuery
    {
		/// <summary>
        /// 功能代碼
        /// </summary>
        public string funcID { get; set; } 
		/// <summary>
        /// 功能名稱
        /// </summary>
        public string funcName { get; set; } 
    }
}
