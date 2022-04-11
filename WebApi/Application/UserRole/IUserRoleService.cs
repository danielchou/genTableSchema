using ESUN.AGD.WebApi.Application.UserRole.Contract;

namespace ESUN.AGD.WebApi.Application.UserRole
{
    public interface IUserRoleService
    {
        /// <summary>
        /// 依序號取得使用者角色配對設定
        /// </summary>
        /// <param>
        /// seqNo - 電腦電話序號
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// userID           - string     - 使用者帳號
		/// roleID           - string     - 角色代碼
		/// createDT         - DateTime   - 建立時間
		/// creator          - string     - 建立者
		/// updateDT         - DateTime   - 更新時間
		/// updator          - string     - 更新者
        /// updatorName      - string     - 更新者名稱
        /// </returns>
        ValueTask<BasicResponse<UserRoleResponse>> GetUserRole(int seqNo);

        /// <summary>
        /// 搜尋使用者角色配對設定 
        /// </summary>
        /// <param name="request">
		/// userID           - string     - 使用者帳號
		/// roleID           - string     - 角色代碼
        /// page             - int        - 分頁
        /// rowsPerPage      - int        - 每頁筆數
        /// sortColumn       - string     - 排序欄位
        /// sortOrder        - string     - 排序順序
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// userID           - string     - 使用者帳號
		/// roleID           - string     - 角色代碼
		/// createDT         - DateTime   - 建立時間
		/// creator          - string     - 建立者
		/// updateDT         - DateTime   - 更新時間
		/// updator          - string     - 更新者
        /// updatorName      - string     - 更新者名稱
        /// </returns> 
        ValueTask<BasicResponse<List<UserRoleResponse>>> QueryUserRole(UserRoleQueryRequest request);

        /// <summary>
        /// 新增使用者角色配對設定 
        /// </summary>
        /// <param name="request">
		/// userID           - string     - 使用者帳號
		/// roleID           - string     - 角色代碼
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> InsertUserRole(UserRoleInsertRequest request);

        /// <summary>
        /// 更新使用者角色配對設定
        /// </summary>
        /// <param name="request">
		/// seqNo            - int        - 流水號
		/// userID           - string     - 使用者帳號
		/// roleID           - string     - 角色代碼
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> UpdateUserRole(UserRoleUpdateRequest request);

        /// <summary>
        /// 刪除使用者角色配對設定
        /// </summary>
        /// <param>
		/// userID           - string     - 使用者帳號
		/// roleID           - string     - 角色代碼
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> DeleteUserRole(string userID,string roleID);

        /// <summary>
        /// 檢查使用者角色配對是否存在
        /// </summary>
        /// <param>
		/// userID           - string     - 使用者帳號
		/// roleID           - string     - 角色代碼
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> Exists(string userID,string roleID);

    }
}
