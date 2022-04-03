using ESUN.AGD.WebApi.Application.Role.Contract;

namespace ESUN.AGD.WebApi.Application.Role
{
    public interface IRoleService
    {
        /// <summary>
        /// 依序號取得角色設定
        /// </summary>
        /// <param>
        /// seqNo - 電腦電話序號
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// roleId           - string     - 角色代碼
		/// roleName         - string     - 角色名稱
		/// isEnable         - bool       - 是否啟用?
		/// creator          - string     - 建立者
		/// updator          - string     - 異動者
		/// createDt         - DateTime   - 建立時間
		/// updateDt         - DateTime   - 異動時間
        /// updatorName - 更新者名稱
        /// </returns>
        ValueTask<BasicResponse<RoleResponse>> GetRole(int seqNo);

        /// <summary>
        /// 搜尋角色設定 
        /// </summary>
        /// <param name="request">
		/// roleId           - string     - 角色代碼
		/// roleName         - string     - 角色名稱
		/// isEnable         - bool       - 是否啟用?
        /// page             - int        - 分頁
        /// rowsPerPage      - int        - 每頁筆數
        /// sortColumn       - string     - 排序欄位
        /// sortOrder        - bool       - 排序順序
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// roleId           - string     - 角色代碼
		/// roleName         - string     - 角色名稱
		/// isEnable         - bool       - 是否啟用?
		/// creator          - string     - 建立者
		/// updator          - string     - 異動者
		/// createDt         - DateTime   - 建立時間
		/// updateDt         - DateTime   - 異動時間
        /// updatorName      - string     - 更新者名稱
        /// </returns> 
        ValueTask<BasicResponse<List<RoleResponse>>> QueryRole(RoleQueryRequest request);

        /// <summary>
        /// 新增角色設定 
        /// </summary>
        /// <param name="request">
		/// roleId           - string     - 角色代碼
		/// roleName         - string     - 角色名稱
		/// isEnable         - bool       - 是否啟用?
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> InsertRole(RoleInsertRequest request);

        /// <summary>
        /// 更新角色設定
        /// </summary>
        /// <param name="request">
		/// seqNo            - int        - 流水號
		/// roleId           - string     - 角色代碼
		/// roleName         - string     - 角色名稱
		/// isEnable         - bool       - 是否啟用?
        /// </param>
        /// <returns></returns>
        ValueTask<BasicResponse<bool>> UpdateRole(RoleUpdateRequest request);

        /// <summary>
        /// 刪除角色設定
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
        /// </param>
        /// <returns></returns>
        ValueTask<BasicResponse<bool>> DeleteRole(int seqNo);

        /// <summary>
        /// 檢查角色是否存在
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
		/// roleId           - string     - 角色代碼
		/// roleName         - string     - 角色名
        /// </param>
        /// <returns></returns>
        ValueTask<BasicResponse<bool>> Exists(int seqNo,string roleId,string roleName);

    }
}
