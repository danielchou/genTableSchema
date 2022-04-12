namespace ESUN.AGD.WebApi.Models
{
    public partial class TbReason : CommonModel
    {
        
        /// <summary>
        /// 流水號
        /// </summary>
        public int SeqNo { get; set; } 
        /// <summary>
        /// 聯繫原因碼代碼
        /// </summary>
        public string ReasonID { get; set; }  = null!;
        /// <summary>
        /// 聯繫原因碼名稱
        /// </summary>
        public string ReasonName { get; set; }  = null!;
        /// <summary>
        /// 上層聯繫原因碼代碼
        /// </summary>
        public string ParentReasonID { get; set; }  = null!;
        /// <summary>
        /// 階層
        /// </summary>
        public int Level { get; set; } 
        /// <summary>
        /// 事業處
        /// </summary>
        public string? BussinessUnit { get; set; } 
        /// <summary>
        /// B03業務別
        /// </summary>
        public string? BussinessB03Type { get; set; } 
        /// <summary>
        /// 覆核類別
        /// </summary>
        public string? ReviewType { get; set; } 
        /// <summary>
        /// 備註
        /// </summary>
        public string? Memo { get; set; } 
        /// <summary>
        /// 網頁連結
        /// </summary>
        public string? WebUrl { get; set; } 
        /// <summary>
        /// KM連結
        /// </summary>
        public string? KMUrl { get; set; } 
        /// <summary>
        /// 顯示順序
        /// </summary>
        public int DisplayOrder { get; set; } 
        /// <summary>
        /// 是否常用
        /// </summary>
        public bool IsUsually { get; set; } 
        /// <summary>
        /// 常用名稱
        /// </summary>
        public string? UsuallyReasonName { get; set; } 
        /// <summary>
        /// 常用顯示順序
        /// </summary>
        public int UsuallyDisplayOrder { get; set; } 
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

