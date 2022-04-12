namespace ESUN.AGD.WebApi.Models
{
    public partial class TbSysConfig : CommonModel
    {
        
        /// <summary>
        /// 流水號
        /// </summary>
        public int SeqNo { get; set; } 
        /// <summary>
        /// 系統參數類別
        /// </summary>
        public string SysConfigType { get; set; }  = null!;
        /// <summary>
        /// 系統參數代碼
        /// </summary>
        public string SysConfigID { get; set; }  = null!;
        /// <summary>
        /// 系統參數名稱
        /// </summary>
        public string SysConfigName { get; set; }  = null!;
        /// <summary>
        /// 系統參數內容
        /// </summary>
        public string Content { get; set; }  = null!;
        /// <summary>
        /// 是否顯示?
        /// </summary>
        public bool IsVisible { get; set; } 
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

