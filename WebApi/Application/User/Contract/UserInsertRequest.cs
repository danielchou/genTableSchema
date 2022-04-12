namespace ESUN.AGD.WebApi.Application.User.Contract
{
    /// <summary>
    /// 電腦電話新增請求
    /// </summary>
    public class UserInsertRequest : CommonInsertRequest
    {
        /// <summary>
        /// 使用者帳號
        /// </summary>
        public string userID { get; set; }
        /// <summary>
        /// 使用者名稱
        /// </summary>
        public string userName { get; set; }
        /// <summary>
        /// 使用者代碼
        /// </summary>
        public string userCode { get; set; }
        /// <summary>
        /// CTI登入帳號
        /// </summary>
        public string agentLoginID { get; set; }
        /// <summary>
        /// CTI登入代碼
        /// </summary>
        public string agentLoginCode { get; set; }
        /// <summary>
        /// 員工編號
        /// </summary>
        public string employeeNo { get; set; }
        /// <summary>
        /// 使用者暱稱
        /// </summary>
        public string nickName { get; set; }
        /// <summary>
        /// 所屬單位
        /// </summary>
        public string empDept { get; set; }
        /// <summary>
        /// 群組代碼
        /// </summary>
        public string? groupID { get; set; }
        /// <summary>
        /// 公司Email
        /// </summary>
        public string officeEmail { get; set; }
        /// <summary>
        /// 在職狀態代碼
        /// </summary>
        public string employedStatusCode { get; set; }
        /// <summary>
        /// 是否為主管?
        /// </summary>
        public bool isSupervisor { get; set; }
        /// <summary>
        /// B08_code1
        /// </summary>
        public string? b08Code1 { get; set; }
        /// <summary>
        /// B08_code2
        /// </summary>
        public string? b08Code2 { get; set; }
        /// <summary>
        /// B08_code3
        /// </summary>
        public string? b08Code3 { get; set; }
        /// <summary>
        /// B08_code4
        /// </summary>
        public string? b08Code4 { get; set; }
        /// <summary>
        /// B08_code5
        /// </summary>
        public string? b08Code5 { get; set; }
        /// <summary>
        /// 是否啟用?
        /// </summary>
        public bool isEnable { get; set; }
    }
}