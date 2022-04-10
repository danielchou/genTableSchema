﻿namespace ESUN.AGD.WebApi.Application.PcPhone.Contract
{
    /// <summary>
    /// 電腦電話更新請求
    /// </summary>
    public class PcPhoneUpdateRequest:CommonUpdateRequest
    {
        /// <summary>
        /// 序號
        /// </summary>
        public int seqNo { get; set; }
        /// <summary>
        /// 分機號碼
        /// </summary>
        public string? extCode { get; set; }
        /// <summary>
        /// 電腦名稱
        /// </summary>
        public string? computerName { get; set; }
        /// <summary>
        /// 電腦IP
        /// </summary>
        public string? computerIp { get; set; }
        /// <summary>
        /// 備註
        /// </summary>
        public string? memo { get; set; }       
        /// <summary>
        /// 是否啟用
        /// </summary>
        public bool isEnable { get; set; }
       
    }
}