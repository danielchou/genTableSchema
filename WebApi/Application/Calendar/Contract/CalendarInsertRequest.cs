namespace ESUN.AGD.WebApi.Application.Calendar.Contract
{
    /// <summary>
    /// 電腦電話新增請求
    /// </summary>
    public class CalendarInsertRequest : CommonInsertRequest
    {
        /// <summary>
        /// 使用者帳號
        /// </summary>
        public string userID { get; set; }
        /// <summary>
        /// 排定日期
        /// </summary>
        public DateTime scheduleDate { get; set; }
        /// <summary>
        /// 班表內容
        /// </summary>
        public string content { get; set; }
    }
}