namespace ESUN.AGD.WebApi.Application.RoleFunc.Contract
{
    /// <summary>
    /// 電腦電話新增請求
    /// </summary>
    public class RoleFuncInsertRequest : CommonInsertRequest
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