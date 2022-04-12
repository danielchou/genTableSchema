namespace ESUN.AGD.WebApi.Models
{
    public partial class TbCode : CommonModel
    {
        
        /// <summary>
        /// 流水號
        /// </summary>
        public int SeqNo { get; set; } 
        /// <summary>
        /// 共用碼類別
        /// </summary>
        public string CodeType { get; set; }  = null!;
        /// <summary>
        /// 共用碼代碼
        /// </summary>
        public string CodeID { get; set; }  = null!;
        /// <summary>
        /// 共用碼名稱
        /// </summary>
        public string CodeName { get; set; }  = null!;
        /// <summary>
        /// 共用碼內容
        /// </summary>
        public string? Content { get; set; } 
        /// <summary>
        /// 備註
        /// </summary>
        public string? Memo { get; set; } 
        /// <summary>
        /// 顯示順序
        /// </summary>
        public int DisplayOrder { get; set; } 
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

