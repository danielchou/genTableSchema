namespace ESUN.AGD.WebApi.Models
{
    public partial class TbPcPhone : CommonModel
    {
        /// <summary>
        /// 電腦電話序號
        /// </summary>
        public int SeqNo { get; set; }
        /// <summary>
        /// 分機號碼
        /// </summary>
        public string ExtCode { get; set; } = null!;
        /// <summary>
        /// 電腦名稱
        /// </summary>
        public string ComputerName { get; set; } = null!;
        /// <summary>
        /// 電腦IP
        /// </summary>
        public string ComputerIp { get; set; } = null!;
        /// <summary>
        /// 備註
        /// </summary>
        public string? Memo { get; set; }
        /// <summary>
        /// 是否啟用
        /// </summary>
        public bool IsEnable { get; set; }
        /// <summary>
        /// 建立日期
        /// </summary>
        public DateTime CreateDt { get; set; }
        /// <summary>
        /// 建立者
        /// </summary>
        public string Creator { get; set; } = null!;
        /// <summary>
        /// 更新日期
        /// </summary>
        public DateTime UpdateDt { get; set; }
        /// <summary>
        /// 更新者
        /// </summary>
        public string Updator { get; set; } = null!;
    }
}

