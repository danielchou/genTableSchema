namespace ESUN.AGD.WebApi.Application.Func.Contract
{
    /// <summary>
    /// 功能更新請求
    /// </summary>
    public class FuncUpdateRequest : CommonUpdateRequest
    {
		/// <summary>
        /// 流水號
        /// </summary>
        public int seqNo { get; set; }
		/// <summary>
        /// 功能代碼
        /// </summary>
        public string funcID { get; set; }
		/// <summary>
        /// 功能名稱
        /// </summary>
        public string funcName { get; set; }
		/// <summary>
        /// 上層功能代碼
        /// </summary>
        public string parentFuncID { get; set; }
		/// <summary>
        /// 階層
        /// </summary>
        public  level { get; set; }
		/// <summary>
        /// 系統類別
        /// </summary>
        public string systemType { get; set; }
		/// <summary>
        /// 路由名稱
        /// </summary>
        public string routeName { get; set; }
		/// <summary>
        /// 顯示順序
        /// </summary>
        public int displayOrder { get; set; }
    }
}
