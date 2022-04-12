namespace ESUN.AGD.WebApi.Application.AuxCode.Contract
{
    /// <summary>
    /// 電腦電話新增請求
    /// </summary>
    public class AuxCodeInsertRequest : CommonInsertRequest
    {
        /// <summary>
        /// 休息碼代碼
        /// </summary>
        public string auxID { get; set; }
        /// <summary>
        /// 休息碼名稱
        /// </summary>
        public string auxName { get; set; }
        /// <summary>
        /// 是否長時間離開?
        /// </summary>
        public bool isLongTimeAux { get; set; }
        /// <summary>
        /// 顯示順序
        /// </summary>
        public int displayOrder { get; set; }
    }
}