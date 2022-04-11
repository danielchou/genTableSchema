namespace ESUN.AGD.WebApi.Application.Group.Contract
{
    /// <summary>
    /// 群組回傳結果
    /// </summary>
    public class  GroupResponse : CommonResponse
    {
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
