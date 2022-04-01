﻿namespace ESUN.AGD.WebApi.Application.$pt_TableName.Contract
{
    /// <summary>
    /// 電腦電話新增請求
    /// </summary>
    public class $pt_TableNam$insertRequest:CommonInsertRequest
    {
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
    }
}
