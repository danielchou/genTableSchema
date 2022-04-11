namespace ESUN.AGD.WebApi.Models
{
    public partial class TbUser : CommonModel
    {
        
        /// <summary>
        /// 流水號
        /// </summary>
        public int SeqNo { get; set; } 
        /// <summary>
        /// 使用者帳號
        /// </summary>
        public string UserID { get; set; }  = null!;
        /// <summary>
        /// 使用者名稱
        /// </summary>
        public string UserName { get; set; }  = null!;
        /// <summary>
        /// 使用者代碼
        /// </summary>
        public string UserCode { get; set; }  = null!;
        /// <summary>
        /// CTI登入帳號
        /// </summary>
        public string AgentLoginID { get; set; }  = null!;
        /// <summary>
        /// CTI登入代碼
        /// </summary>
        public string AgentLoginCode { get; set; } 
        /// <summary>
        /// 員工編號
        /// </summary>
        public string EmployeeNo { get; set; } 
        /// <summary>
        /// 使用者暱稱
        /// </summary>
        public string NickName { get; set; } 
        /// <summary>
        /// 所屬單位
        /// </summary>
        public string EmpDept { get; set; } 
        /// <summary>
        /// 群組代碼
        /// </summary>
        public string? GroupID { get; set; } 
        /// <summary>
        /// 公司Email
        /// </summary>
        public string OfficeEmail { get; set; } 
        /// <summary>
        /// 在職狀態代碼
        /// </summary>
        public string EmployedStatusCode { get; set; } 
        /// <summary>
        /// 是否為主管?
        /// </summary>
        public bool IsSupervisor { get; set; } 
        /// <summary>
        /// B08_code1
        /// </summary>
        public string? B08Code1 { get; set; } 
        /// <summary>
        /// B08_code2
        /// </summary>
        public string? B08Code2 { get; set; } 
        /// <summary>
        /// B08_code3
        /// </summary>
        public string? B08Code3 { get; set; } 
        /// <summary>
        /// B08_code4
        /// </summary>
        public string? B08Code4 { get; set; } 
        /// <summary>
        /// B08_code5
        /// </summary>
        public string? B08Code5 { get; set; } 
        /// <summary>
        /// 是否啟用?
        /// </summary>
        public bool IsEnable { get; set; } 
        /// <summary>
        /// 建立時間
        /// </summary>
        public DateTime CreateDT { get; set; } 
        /// <summary>
        /// 建立者
        /// </summary>
        public string Creator { get; set; }  = null!;
        /// <summary>
        /// 更新時間
        /// </summary>
        public DateTime UpdateDT { get; set; } 
        /// <summary>
        /// 更新者
        /// </summary>
        public string Updator { get; set; }  = null!;
    }
}

