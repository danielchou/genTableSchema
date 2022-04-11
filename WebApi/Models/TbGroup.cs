namespace ESUN.AGD.WebApi.Models
{
    public partial class TbGroup : CommonModel
    {
        
        /// <summary>
        /// 流水號
        /// </summary>
        public int SeqNo { get; set; } 
        /// <summary>
        /// 群組代碼
        /// </summary>
        public string GroupID { get; set; }  = null!;
        /// <summary>
        /// 群組名稱
        /// </summary>
        public string GroupName { get; set; }  = null!;
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

