namespace ESUN.AGD.WebApi.Application.Role.Contract
{
    /// <summary>
    /// 角色回傳結果
    /// </summary>
    public class  RoleResponse : CommonResponse
    {
        
        /// <summary>
        /// 角色代碼
        /// </summary>
        public string roleId { get; set; }
        /// <summary>
        /// 角色名稱
        /// </summary>
        public string roleName { get; set; }
        /// <summary>
        /// 是否啟用?
        /// </summary>
        public bool isEnable { get; set; }
    }
}
