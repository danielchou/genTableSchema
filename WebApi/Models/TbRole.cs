namespace ESUN.AGD.WebApi.Models
{
    public partial class TbRole : CommonModel
    {
        
        /// <summary>
        /// 流水號
        /// </summary>
        public int SeqNo { get; set; } 
        /// <summary>
        /// 角色代碼
        /// </summary>
        public string RoleId { get; set; }  = null!;
        /// <summary>
        /// 角色名稱
        /// </summary>
        public string RoleName { get; set; }  = null!;
        /// <summary>
        /// 是否啟用?
        /// </summary>
        public bool IsEnable { get; set; } 
        /// <summary>
        /// 建立者
        /// </summary>
        public string Creator { get; set; }  = null!;
        /// <summary>
        /// 異動者
        /// </summary>
        public string Updator { get; set; }  = null!;
        /// <summary>
        /// 建立時間
        /// </summary>
        public DateTime CreateDt { get; set; } 
        /// <summary>
        /// 異動時間
        /// </summary>
        public DateTime UpdateDt { get; set; } 
    }
}

