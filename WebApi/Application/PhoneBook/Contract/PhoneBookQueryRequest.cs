

namespace ESUN.AGD.WebApi.Application.PhoneBook.Contract
{
    /// <summary>
    /// 進階查詢查詢請求
    /// </summary>
    public class PhoneBookQueryRequest : CommonQuery
    {
		/// <summary>
        /// 電話簿代碼
        /// </summary>
        public string? phoneBookID { get; set; }
		/// <summary>
        /// 電話簿名稱
        /// </summary>
        public string? phoneBookName { get; set; }
		/// <summary>
        /// 上層電話簿代碼
        /// </summary>
        public string? parentPhoneBookID { get; set; }
		/// <summary>
        /// 電話號碼
        /// </summary>
        public string? phoneBookNumber { get; set; }

    }
}
