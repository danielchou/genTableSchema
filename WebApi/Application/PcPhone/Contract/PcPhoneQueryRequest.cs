﻿

namespace ESUN.AGD.WebApi.Application.PcPhone.Contract
{
    /// <summary>
    /// 進階查詢查詢請求
    /// </summary>
    public class PcPhoneQueryRequest : CommonQuery
    {
		/// <summary>
        /// 電腦名稱
        /// </summary>
        public string computerName { get; set; } 
		/// <summary>
        /// 電腦IP
        /// </summary>
        public string computerIp { get; set; } 
		/// <summary>
        /// 電話分機
        /// </summary>
        public string extCode { get; set; } 
		/// <summary>
        /// 備註
        /// </summary>
        public string? memo { get; set; } 
		/// <summary>
        /// 是否啟用?
        /// </summary>
        public bool isEnable { get; set; }  = "ALL"
    }
}
