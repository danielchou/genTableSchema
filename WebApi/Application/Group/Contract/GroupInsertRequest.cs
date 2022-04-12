namespace ESUN.AGD.WebApi.Application.Group.Contract
{
    /// <summary>
    /// 電腦電話新增請求
    /// </summary>
    public class GroupInsertRequest : CommonInsertRequest
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