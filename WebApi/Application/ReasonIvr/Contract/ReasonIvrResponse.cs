namespace ESUN.AGD.WebApi.Application.ReasonIvr.Contract
{
    /// <summary>
    /// 聯繫原因Ivr配對回傳結果
    /// </summary>
    public class  ReasonIvrResponse : CommonResponse
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
