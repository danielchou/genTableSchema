namespace ESUN.AGD.WebApi.Models
{
    public partial class TbFunc : CommonModel
    {
        
        /// <summary>
        /// 流水號
        /// </summary>
        public int SeqNo { get; set; } 
        /// <summary>
        /// 功能代碼
        /// </summary>
        public string FuncID { get; set; }  = null!;
        /// <summary>
        /// 功能名稱
        /// </summary>
        public string FuncName { get; set; }  = null!;
        /// <summary>
        /// 上層功能代碼
        /// </summary>
        public string ParentFuncID { get; set; }  = null!;
        /// <summary>
        /// 階層
        /// </summary>
        public int Level { get; set; } 
        /// <summary>
        /// 系統類別
        /// </summary>
        public string SystemType { get; set; }  = null!;
        /// <summary>
        /// Icon名稱
        /// </summary>
        public string? IconName { get; set; } 
        /// <summary>
        /// 路由名稱
        /// </summary>
        public string? RouteName { get; set; } 
        /// <summary>
        /// 顯示順序
        /// </summary>
        public int DisplayOrder { get; set; } 
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

