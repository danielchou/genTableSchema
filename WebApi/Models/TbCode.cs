namespace ESUN.AGD.WebApi.Models
{
    public partial class TbCode : CommonModel
    {
        
        /// <summary>
        /// 流水號
        /// </summary>
        public int seqNo { get; set; } 
        /// <summary>
        /// 代碼分類
        /// </summary>
        public string CodeType { get; set; }  = null!;
        /// <summary>
        /// 系統代碼檔代碼
        /// </summary>
        public string CodeId { get; set; }  = null!;
        /// <summary>
        /// 系統代碼檔名稱
        /// </summary>
        public string CodeName { get; set; }  = null!;
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

