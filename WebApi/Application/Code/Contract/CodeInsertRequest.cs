namespace ESUN.AGD.WebApi.Application.Code.Contract
{
    /// <summary>
    /// 電腦電話新增請求
    /// </summary>
    public class CodeInsertRequest:CommonInsertRequest
    {
        
        /// <summary>
        /// 代碼分類
        /// </summary>
        public string codeType { get; set; }
        /// <summary>
        /// 系統代碼檔代碼
        /// </summary>
        public string codeId { get; set; }
        /// <summary>
        /// 系統代碼檔名稱
        /// </summary>
        public string codeName { get; set; }
        /// <summary>
        /// 是否啟用?
        /// </summary>
        public bool isEnable { get; set; }
    }
}
