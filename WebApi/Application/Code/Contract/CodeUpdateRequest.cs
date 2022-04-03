namespace ESUN.AGD.WebApi.Application.Code.Contract
{
    /// <summary>
    /// 系統代碼更新請求
    /// </summary>
    public class CodeUpdateRequest : CommonUpdateRequest
    {
		/// <summary>
        /// 流水號
        /// </summary>
        public int seqNo { get; set; }
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
		/// <summary>
        /// 異動者
        /// </summary>
        public string updator { get; set; }
    }
}
