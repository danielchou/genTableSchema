namespace ESUN.AGD.WebApi.Application.Reason.Contract
{
    /// <summary>
    /// 聯繫原因碼更新請求
    /// </summary>
    public class ReasonUpdateRequest : CommonUpdateRequest
    {
		/// <summary>
        /// 流水號
        /// </summary>
        public int seqNo { get; set; }
		/// <summary>
        /// 聯繫原因碼代碼
        /// </summary>
        public string reasonID { get; set; }
		/// <summary>
        /// 聯繫原因碼名稱
        /// </summary>
        public string reasonName { get; set; }
		/// <summary>
        /// 上層聯繫原因碼代碼
        /// </summary>
        public string parentReasonID { get; set; }
		/// <summary>
        /// 階層
        /// </summary>
        public int level { get; set; }
		/// <summary>
        /// 事業處
        /// </summary>
        public string? bussinessUnit { get; set; }
		/// <summary>
        /// B03業務別
        /// </summary>
        public string? bussinessB03Type { get; set; }
		/// <summary>
        /// 覆核類別
        /// </summary>
        public string? reviewType { get; set; }
		/// <summary>
        /// 備註
        /// </summary>
        public string? memo { get; set; }
		/// <summary>
        /// 網頁連結
        /// </summary>
        public string? webUrl { get; set; }
		/// <summary>
        /// KM連結
        /// </summary>
        public string? kMUrl { get; set; }
		/// <summary>
        /// 顯示順序
        /// </summary>
        public int displayOrder { get; set; }
		/// <summary>
        /// 是否常用
        /// </summary>
        public bool isUsually { get; set; }
		/// <summary>
        /// 常用名稱
        /// </summary>
        public string? usuallyReasonName { get; set; }
		/// <summary>
        /// 常用顯示順序
        /// </summary>
        public int usuallyDisplayOrder { get; set; }
		/// <summary>
        /// 是否啟用?
        /// </summary>
        public bool isEnable { get; set; }
    }
}
