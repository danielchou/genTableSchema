﻿namespace ESUN.AGD.WebApi.Application.MessageSheet.Contract
{
    /// <summary>
    /// 電腦電話新增請求
    /// </summary>
    public class MessageSheetInsertRequest : CommonInsertRequest
    {
        /// <summary>
        /// 訊息傳送頁籤類別
        /// </summary>
        public string messageSheetType { get; set; }
        /// <summary>
        /// 訊息傳送頁籤代碼
        /// </summary>
        public string messageSheetID { get; set; }
        /// <summary>
        /// 訊息傳送頁籤名稱
        /// </summary>
        public string messageSheetName { get; set; }
        /// <summary>
        /// 顯示順序
        /// </summary>
        public int displayOrder { get; set; }
        /// <summary>
        /// 是否啟用?
        /// </summary>
        public bool isEnable { get; set; }
    }
}