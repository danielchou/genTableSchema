using ESUN.AGD.WebApi.Application.Group.Contract;

namespace ESUN.AGD.WebApi.Application.Group
{
    public interface IGroupService
    {
        /// <summary>
        /// 依序號取得群組設定
        /// </summary>
        /// <param>
        /// seqNo - 電腦電話序號
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// groupID          - string     - 群組代碼
		/// groupName        - string     - 群組名稱
		/// createDT         - DateTime   - 建立時間
		/// creator          - string     - 建立者
		/// updateDT         - DateTime   - 更新時間
		/// updator          - string     - 更新者
        /// updatorName      - string     - 更新者名稱
        /// </returns>
        ValueTask<BasicResponse<GroupResponse>> GetGroup(int seqNo);

        /// <summary>
        /// 搜尋群組設定 
        /// </summary>
        /// <param name="request">
		/// groupID          - string     - 群組代碼
		/// groupName        - string     - 群組名稱
        /// page             - int        - 分頁
        /// rowsPerPage      - int        - 每頁筆數
        /// sortColumn       - string     - 排序欄位
        /// sortOrder        - string     - 排序順序
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// groupID          - string     - 群組代碼
		/// groupName        - string     - 群組名稱
		/// createDT         - DateTime   - 建立時間
		/// creator          - string     - 建立者
		/// updateDT         - DateTime   - 更新時間
		/// updator          - string     - 更新者
        /// updatorName      - string     - 更新者名稱
        /// </returns> 
        ValueTask<BasicResponse<List<GroupResponse>>> QueryGroup(GroupQueryRequest request);

        /// <summary>
        /// 新增群組設定 
        /// </summary>
        /// <param name="request">
		/// groupID          - string     - 群組代碼
		/// groupName        - string     - 群組名稱
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> InsertGroup(GroupInsertRequest request);

        /// <summary>
        /// 更新群組設定
        /// </summary>
        /// <param name="request">
		/// seqNo            - int        - 流水號
		/// groupID          - string     - 群組代碼
		/// groupName        - string     - 群組名稱
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> UpdateGroup(GroupUpdateRequest request);

        /// <summary>
        /// 刪除群組設定
        /// </summary>
        /// <param>
		/// groupID          - string     - 群組代碼
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> DeleteGroup(string groupID);

        /// <summary>
        /// 檢查群組是否存在
        /// </summary>
        /// <param>
		/// groupID          - string     - 群組代碼
		/// groupName        - string     - 群組名稱
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> Exists(string groupID,string groupName);

    }
}
