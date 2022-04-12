namespace ESUN.AGD.WebApi.Models
{
    public partial class TbCalendar : CommonModel
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
        /// 排定日期
        /// </summary>
        public DateTime ScheduleDate { get; set; } 
        /// <summary>
        /// 班表內容
        /// </summary>
        public string Content { get; set; }  = null!;
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

