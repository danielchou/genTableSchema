namespace ESUN.AGD.WebApi.Application.ReasonIvr.Contract
{
    /// <summary>
    /// 聯繫原因Ivr配對更新請求
    /// </summary>
    public class ReasonIvrUpdateRequest : CommonUpdateRequest
    {
		/// <summary>
        /// 流水號
        /// </summary>
        public int seqNo { get; set; }
		/// <summary>
        /// Ivr代碼
        /// </summary>
        public string ivrID { get; set; }
		/// <summary>
        /// 原因代碼
        /// </summary>
        public string reasonID { get; set; }
    }
}
