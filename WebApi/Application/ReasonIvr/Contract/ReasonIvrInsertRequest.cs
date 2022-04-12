namespace ESUN.AGD.WebApi.Application.ReasonIvr.Contract
{
    /// <summary>
    /// 電腦電話新增請求
    /// </summary>
    public class ReasonIvrInsertRequest : CommonInsertRequest
    {
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