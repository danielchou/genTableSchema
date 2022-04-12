namespace ESUN.AGD.WebApi.Models
{
    public partial class TbAuxCode : CommonModel
    {
        
        /// <summary>
        /// 流水號
        /// </summary>
        public int SeqNo { get; set; } 
        /// <summary>
        /// 休息碼代碼
        /// </summary>
        public string AuxID { get; set; }  = null!;
        /// <summary>
        /// 休息碼名稱
        /// </summary>
        public string AuxName { get; set; }  = null!;
        /// <summary>
        /// 是否長時間離開?
        /// </summary>
        public bool IsLongTimeAux { get; set; } 
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

