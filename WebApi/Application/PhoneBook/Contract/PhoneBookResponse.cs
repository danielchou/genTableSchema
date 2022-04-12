namespace ESUN.AGD.WebApi.Application.PhoneBook.Contract
{
    /// <summary>
    /// 電話簿回傳結果
    /// </summary>
    public class  PhoneBookResponse : CommonResponse
    {
        /// <summary>
        /// 電話簿代碼
        /// </summary>
        public string phoneBookID { get; set; }
        /// <summary>
        /// 電話簿名稱
        /// </summary>
        public string phoneBookName { get; set; }
        /// <summary>
        /// 上層電話簿代碼
        /// </summary>
        public string parentPhoneBookID { get; set; }
        /// <summary>
        /// 電話號碼
        /// </summary>
        public string phoneBookNumber { get; set; }
        /// <summary>
        /// 階層
        /// </summary>
        public int level { get; set; }
        /// <summary>
        /// 備註
        /// </summary>
        public string? memo { get; set; }
        /// <summary>
        /// 顯示順序
        /// </summary>
        public int displayOrder { get; set; }
    }
}
