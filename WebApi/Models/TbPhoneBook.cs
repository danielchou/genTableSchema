namespace ESUN.AGD.WebApi.Models
{
    public partial class TbPhoneBook : CommonModel
    {
        
        /// <summary>
        /// 流水號
        /// </summary>
        public int SeqNo { get; set; } 
        /// <summary>
        /// 電話簿代碼
        /// </summary>
        public string PhoneBookID { get; set; }  = null!;
        /// <summary>
        /// 電話簿名稱
        /// </summary>
        public string PhoneBookName { get; set; }  = null!;
        /// <summary>
        /// 上層電話簿代碼
        /// </summary>
        public string ParentPhoneBookID { get; set; }  = null!;
        /// <summary>
        /// 電話號碼
        /// </summary>
        public string PhoneBookNumber { get; set; }  = null!;
        /// <summary>
        /// 階層
        /// </summary>
        public int Level { get; set; } 
        /// <summary>
        /// 備註
        /// </summary>
        public string? Memo { get; set; } 
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

