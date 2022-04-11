namespace ESUN.AGD.WebApi.Models
{
    public partial class TbPcPhone : CommonModel
    {
        
        /// <summary>
        /// 流水號
        /// </summary>
        public int SeqNo { get; set; } 
        /// <summary>
        /// 電腦IP
        /// </summary>
        public string ComputerIP { get; set; }  = null!;
        /// <summary>
        /// 電腦名稱
        /// </summary>
        public string ComputerName { get; set; }  = null!;
        /// <summary>
        /// 分機號碼
        /// </summary>
        public string ExtCode { get; set; }  = null!;
        /// <summary>
        /// 備註
        /// </summary>
        public string? Memo { get; set; } 
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

