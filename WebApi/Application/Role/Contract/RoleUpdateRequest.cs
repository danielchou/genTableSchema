namespace ESUN.AGD.WebApi.Application.Role.Contract
{
    /// <summary>
    /// 角色更新請求
    /// </summary>
    public class RoleUpdateRequest : CommonUpdateRequest
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
        /// 角色名稱
        /// </summary>
        public string roleName { get; set; }
    }
}
