namespace ESUN.AGD.WebApi.Application.Group.Contract
{
    /// <summary>
    /// 群組更新請求
    /// </summary>
    public class GroupUpdateRequest : CommonUpdateRequest
    {
		/// <summary>
        /// 流水號
        /// </summary>
        public int seqNo { get; set; }
		/// <summary>
        /// 群組代碼
        /// </summary>
        public string groupID { get; set; }
		/// <summary>
        /// 群組名稱
        /// </summary>
        public string groupName { get; set; }
    }
}
