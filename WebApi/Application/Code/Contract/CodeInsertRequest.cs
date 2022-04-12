namespace ESUN.AGD.WebApi.Application.Code.Contract
{
    /// <summary>
    /// 電腦電話新增請求
    /// </summary>
    public class CodeInsertRequest : CommonInsertRequest
    {
        /// <summary>
        /// 共用碼類別
        /// </summary>
        public string codeType { get; set; }
        /// <summary>
        /// 共用碼代碼
        /// </summary>
        public string codeID { get; set; }
        /// <summary>
        /// 共用碼名稱
        /// </summary>
        public string codeName { get; set; }
        /// <summary>
        /// 共用碼內容
        /// </summary>
        public string? content { get; set; }
        /// <summary>
        /// 備註
        /// </summary>
        public string? memo { get; set; }
        /// <summary>
        /// 顯示順序
        /// </summary>
        public int displayOrder { get; set; }
        /// <summary>
        /// 是否啟用?
        /// </summary>
        public bool isEnable { get; set; }
    }
}