using ESUN.AGD.WebApi.Application.User.Contract;

namespace ESUN.AGD.WebApi.Application.User
{
    public interface IUserService
    {
        /// <summary>
        /// 依序號取得使用者設定
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// userID           - string     - 使用者帳號
		/// userName         - string     - 使用者名稱
		/// userCode         - string     - 使用者代碼
		/// agentLoginID     - string     - CTI登入帳號
		/// agentLoginCode   - string     - CTI登入代碼
		/// employeeNo       - string     - 員工編號
		/// nickName         - string     - 使用者暱稱
		/// empDept          - string     - 所屬單位
		/// groupID          - string     - 群組代碼
		/// officeEmail      - string     - 公司Email
		/// employedStatusCode - string     - 在職狀態代碼
		/// isSupervisor     - bool       - 是否為主管?
		/// b08Code1         - string     - B08_code1
		/// b08Code2         - string     - B08_code2
		/// b08Code3         - string     - B08_code3
		/// b08Code4         - string     - B08_code4
		/// b08Code5         - string     - B08_code5
		/// isEnable         - bool       - 是否啟用?
		/// createDT         - DateTime   - 建立時間
		/// creator          - string     - 建立者
		/// updateDT         - DateTime   - 更新時間
		/// updator          - string     - 更新者
        /// updatorName      - string     - 更新者名稱
        /// </returns>
        ValueTask<BasicResponse<UserResponse>> GetUser(int seqNo);

        /// <summary>
        /// 搜尋使用者設定 
        /// </summary>
        /// <param name="request">
		/// userID           - string     - 使用者帳號
		/// userName         - string     - 使用者名稱
		/// agentLoginID     - string     - CTI登入帳號
		/// groupID          - string     - 群組代碼
		/// isEnable         - string     - 是否啟用?
        /// page             - int        - 分頁
        /// rowsPerPage      - int        - 每頁筆數
        /// sortColumn       - string     - 排序欄位
        /// sortOrder        - string     - 排序順序
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// userID           - string     - 使用者帳號
		/// userName         - string     - 使用者名稱
		/// userCode         - string     - 使用者代碼
		/// agentLoginID     - string     - CTI登入帳號
		/// agentLoginCode   - string     - CTI登入代碼
		/// employeeNo       - string     - 員工編號
		/// nickName         - string     - 使用者暱稱
		/// empDept          - string     - 所屬單位
		/// groupID          - string     - 群組代碼
		/// officeEmail      - string     - 公司Email
		/// employedStatusCode - string     - 在職狀態代碼
		/// isSupervisor     - bool       - 是否為主管?
		/// b08Code1         - string     - B08_code1
		/// b08Code2         - string     - B08_code2
		/// b08Code3         - string     - B08_code3
		/// b08Code4         - string     - B08_code4
		/// b08Code5         - string     - B08_code5
		/// isEnable         - bool       - 是否啟用?
		/// createDT         - DateTime   - 建立時間
		/// creator          - string     - 建立者
		/// updateDT         - DateTime   - 更新時間
		/// updator          - string     - 更新者
        /// updatorName      - string     - 更新者名稱
        /// </returns> 
        ValueTask<BasicResponse<List<UserResponse>>> QueryUser(UserQueryRequest request);

        /// <summary>
        /// 新增使用者設定 
        /// </summary>
        /// <param name="request">
		/// userID           - string     - 使用者帳號
		/// userName         - string     - 使用者名稱
		/// userCode         - string     - 使用者代碼
		/// agentLoginID     - string     - CTI登入帳號
		/// agentLoginCode   - string     - CTI登入代碼
		/// employeeNo       - string     - 員工編號
		/// nickName         - string     - 使用者暱稱
		/// empDept          - string     - 所屬單位
		/// groupID          - string     - 群組代碼
		/// officeEmail      - string     - 公司Email
		/// employedStatusCode - string     - 在職狀態代碼
		/// isSupervisor     - bool       - 是否為主管?
		/// b08Code1         - string     - B08_code1
		/// b08Code2         - string     - B08_code2
		/// b08Code3         - string     - B08_code3
		/// b08Code4         - string     - B08_code4
		/// b08Code5         - string     - B08_code5
		/// isEnable         - bool       - 是否啟用?
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> InsertUser(UserInsertRequest request);

        /// <summary>
        /// 更新使用者設定
        /// </summary>
        /// <param name="request">
		/// seqNo            - int        - 流水號
		/// userID           - string     - 使用者帳號
		/// userName         - string     - 使用者名稱
		/// userCode         - string     - 使用者代碼
		/// agentLoginID     - string     - CTI登入帳號
		/// agentLoginCode   - string     - CTI登入代碼
		/// employeeNo       - string     - 員工編號
		/// nickName         - string     - 使用者暱稱
		/// empDept          - string     - 所屬單位
		/// groupID          - string     - 群組代碼
		/// officeEmail      - string     - 公司Email
		/// employedStatusCode - string     - 在職狀態代碼
		/// isSupervisor     - bool       - 是否為主管?
		/// b08Code1         - string     - B08_code1
		/// b08Code2         - string     - B08_code2
		/// b08Code3         - string     - B08_code3
		/// b08Code4         - string     - B08_code4
		/// b08Code5         - string     - B08_code5
		/// isEnable         - bool       - 是否啟用?
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> UpdateUser(UserUpdateRequest request);

        /// <summary>
        /// 刪除使用者設定
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> DeleteUser(int seqNo);

        /// <summary>
        /// 檢查使用者是否存在
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
		/// userID           - string     - 使用者帳號
		/// agentLoginID     - string     - CTI登入帳號
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> Exists(int seqNo,string userID,string agentLoginID);

    }
}
