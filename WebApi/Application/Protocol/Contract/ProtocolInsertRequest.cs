namespace ESUN.AGD.WebApi.Application.Protocol.Contract
{
    /// <summary>
    /// 電腦電話新增請求
    /// </summary>
    public class ProtocolInsertRequest : CommonInsertRequest
    {
        /// <summary>
        /// 通路碼代碼
        /// </summary>
        public string protocol { get; set; }
        /// <summary>
        /// 通路碼名稱
        /// </summary>
        public string protocolName { get; set; }
        /// <summary>
        /// IN/OUT方向
        /// </summary>
        public string direction { get; set; }
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