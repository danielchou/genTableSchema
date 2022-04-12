namespace ESUN.AGD.WebApi.Models
{
    public partial class TbProtocol : CommonModel
    {
        
        /// <summary>
        /// 流水號
        /// </summary>
        public int SeqNo { get; set; } 
        /// <summary>
        /// 通路碼代碼
        /// </summary>
        public string Protocol { get; set; }  = null!;
        /// <summary>
        /// 通路碼名稱
        /// </summary>
        public string ProtocolName { get; set; }  = null!;
        /// <summary>
        /// IN/OUT方向
        /// </summary>
        public string Direction { get; set; }  = null!;
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

