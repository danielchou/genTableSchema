namespace ESUN.AGD.WebApi.Application.RoleFunc.Contract
{
    /// <summary>
    /// 角色功能配對回傳結果
    /// </summary>
    public class  RoleFuncResponse : CommonResponse
    {
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
