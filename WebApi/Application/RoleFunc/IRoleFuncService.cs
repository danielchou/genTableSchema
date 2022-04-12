using ESUN.AGD.WebApi.Application.RoleFunc.Contract;

namespace ESUN.AGD.WebApi.Application.RoleFunc
{
    public interface IRoleFuncService
    {
        /// <summary>
        /// 依序號取得角色功能配對設定
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// roleID           - string     - 角色代碼
		/// funcID           - string     - 功能代碼
		/// createDT         - DateTime   - 建立時間
		/// creator          - string     - 建立者
		/// updateDT         - DateTime   - 更新時間
		/// updator          - string     - 更新者
        /// updatorName      - string     - 更新者名稱
        /// </returns>
        ValueTask<BasicResponse<RoleFuncResponse>> GetRoleFunc(int seqNo);

        /// <summary>
        /// 搜尋角色功能配對設定 
        /// </summary>
        /// <param name="request">
		/// roleID           - string     - 角色代碼
		/// funcID           - string     - 功能代碼
        /// page             - int        - 分頁
        /// rowsPerPage      - int        - 每頁筆數
        /// sortColumn       - string     - 排序欄位
        /// sortOrder        - string     - 排序順序
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// roleID           - string     - 角色代碼
		/// funcID           - string     - 功能代碼
		/// createDT         - DateTime   - 建立時間
		/// creator          - string     - 建立者
		/// updateDT         - DateTime   - 更新時間
		/// updator          - string     - 更新者
        /// updatorName      - string     - 更新者名稱
        /// </returns> 
        ValueTask<BasicResponse<List<RoleFuncResponse>>> QueryRoleFunc(RoleFuncQueryRequest request);

        /// <summary>
        /// 新增角色功能配對設定 
        /// </summary>
        /// <param name="request">
		/// roleID           - string     - 角色代碼
		/// funcID           - string     - 功能代碼
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> InsertRoleFunc(RoleFuncInsertRequest request);

        /// <summary>
        /// 更新角色功能配對設定
        /// </summary>
        /// <param name="request">
		/// seqNo            - int        - 流水號
		/// roleID           - string     - 角色代碼
		/// funcID           - string     - 功能代碼
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> UpdateRoleFunc(RoleFuncUpdateRequest request);

        /// <summary>
        /// 刪除角色功能配對設定
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> DeleteRoleFunc(int seqNo);

        /// <summary>
        /// 檢查角色功能配對是否存在
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
		/// roleID           - string     - 角色代碼
		/// funcID           - string     - 功能代碼
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> Exists(int seqNo,string roleID,string funcID);

    }
}
