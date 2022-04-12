namespace ESUN.AGD.WebApi.Application.AuxCode.Contract
{
    /// <summary>
    /// 休息碼更新請求
    /// </summary>
    public class AuxCodeUpdateRequest : CommonUpdateRequest
    {
		/// <summary>
        /// 流水號
        /// </summary>
        public int seqNo { get; set; }
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
