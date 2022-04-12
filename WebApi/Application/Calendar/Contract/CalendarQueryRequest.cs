

namespace ESUN.AGD.WebApi.Application.Calendar.Contract
{
    /// <summary>
    /// 進階查詢查詢請求
    /// </summary>
    public class CalendarQueryRequest : CommonQuery
    {
		/// <summary>
        /// 使用者帳號
        /// </summary>
        public string? userID { get; set; }
		/// <summary>
        /// 排定日期
        /// </summary>
        public DateTime? scheduleDate { get; set; }

    }
}
