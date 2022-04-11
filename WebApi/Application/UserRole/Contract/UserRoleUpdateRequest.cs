namespace ESUN.AGD.WebApi.Application.UserRole.Contract
{
    /// <summary>
    /// 使用者角色配對更新請求
    /// </summary>
    public class UserRoleUpdateRequest : CommonUpdateRequest
    {
		/// <summary>
        /// 流水號
        /// </summary>
        public int seqNo { get; set; }
		/// <summary>
        /// 使用者帳號
        /// </summary>
        public string userID { get; set; }
		/// <summary>
        /// 角色代碼
        /// </summary>
        public string roleID { get; set; }
    }
}
