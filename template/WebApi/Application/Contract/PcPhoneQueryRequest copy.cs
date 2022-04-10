

namespace ESUN.AGD.WebApi.Application.PcPhone.Contract
{
    /// <summary>
    /// 進階查詢查詢請求
    /// </summary>
    public class PcPhoneQueryRequest : CommonQuery
    {
        /// <summary>
        /// 分機號碼
        /// </summary>
        public string? extCode { get; set; }
        /// <summary>
        /// 電腦名稱
        /// </summary>
        public string? computerName { get; set; }      
    }
}
