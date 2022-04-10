namespace ESUN.AGD.WebApi.Application.Code.Contract
{
    /// <summary>
    /// 系統代碼回傳結果
    /// </summary>
    public class  CodeResponse : CommonResponse
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
