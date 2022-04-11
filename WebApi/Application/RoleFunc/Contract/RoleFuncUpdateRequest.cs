namespace ESUN.AGD.WebApi.Application.RoleFunc.Contract
{
    /// <summary>
    /// 角色功能配對更新請求
    /// </summary>
    public class RoleFuncUpdateRequest : CommonUpdateRequest
    {
		/// <summary>
        /// 流水號
        /// </summary>
        public int seqNo { get; set; }
		/// <summary>
        /// 角色代碼
        /// </summary>
        public string roleID { get; set; }
		/// <summary>
        /// 功能代碼
        /// </summary>
        public string funcID { get; set; }
    }
}
