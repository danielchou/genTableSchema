namespace ESUN.AGD.WebApi.Application.PcPhone.Contract
{
    /// <summary>
    /// 電腦電話配對回傳結果
    /// </summary>
    public class  PcPhoneResponse : CommonResponse
    {
        /// <summary>
        /// 電腦IP
        /// </summary>
        public string computerIP { get; set; }
        /// <summary>
        /// 電腦名稱
        /// </summary>
        public string computerName { get; set; }
        /// <summary>
        /// 分機號碼
        /// </summary>
        public string extCode { get; set; }
        /// <summary>
        /// 備註
        /// </summary>
        public string? memo { get; set; }
    }
}
