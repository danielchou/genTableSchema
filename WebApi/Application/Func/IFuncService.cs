using ESUN.AGD.WebApi.Application.Func.Contract;

namespace ESUN.AGD.WebApi.Application.Func
{
    public interface IFuncService
    {
        /// <summary>
        /// 依序號取得功能設定
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// funcID           - string     - 功能代碼
		/// funcName         - string     - 功能名稱
		/// parentFuncID     - string     - 上層功能代碼
		/// level            - int        - 階層
		/// systemType       - string     - 系統類別
		/// iconName         - string     - Icon名稱
		/// routeName        - string     - 路由名稱
		/// displayOrder     - int        - 顯示順序
		/// createDT         - DateTime   - 建立時間
		/// creator          - string     - 建立者
		/// updateDT         - DateTime   - 更新時間
		/// updator          - string     - 更新者
        /// updatorName      - string     - 更新者名稱
        /// </returns>
        ValueTask<BasicResponse<FuncResponse>> GetFunc(int seqNo);

        /// <summary>
        /// 搜尋功能設定 
        /// </summary>
        /// <param name="request">
		/// funcID           - string     - 功能代碼
		/// funcName         - string     - 功能名稱
        /// page             - int        - 分頁
        /// rowsPerPage      - int        - 每頁筆數
        /// sortColumn       - string     - 排序欄位
        /// sortOrder        - string     - 排序順序
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// funcID           - string     - 功能代碼
		/// funcName         - string     - 功能名稱
		/// parentFuncID     - string     - 上層功能代碼
		/// level            - int        - 階層
		/// systemType       - string     - 系統類別
		/// iconName         - string     - Icon名稱
		/// routeName        - string     - 路由名稱
		/// displayOrder     - int        - 顯示順序
		/// createDT         - DateTime   - 建立時間
		/// creator          - string     - 建立者
		/// updateDT         - DateTime   - 更新時間
		/// updator          - string     - 更新者
        /// updatorName      - string     - 更新者名稱
        /// </returns> 
        ValueTask<BasicResponse<List<FuncResponse>>> QueryFunc(FuncQueryRequest request);

        /// <summary>
        /// 新增功能設定 
        /// </summary>
        /// <param name="request">
		/// funcID           - string     - 功能代碼
		/// funcName         - string     - 功能名稱
		/// parentFuncID     - string     - 上層功能代碼
		/// level            - int        - 階層
		/// systemType       - string     - 系統類別
		/// iconName         - string     - Icon名稱
		/// routeName        - string     - 路由名稱
		/// displayOrder     - int        - 顯示順序
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> InsertFunc(FuncInsertRequest request);

        /// <summary>
        /// 更新功能設定
        /// </summary>
        /// <param name="request">
		/// seqNo            - int        - 流水號
		/// funcID           - string     - 功能代碼
		/// funcName         - string     - 功能名稱
		/// parentFuncID     - string     - 上層功能代碼
		/// level            - int        - 階層
		/// systemType       - string     - 系統類別
		/// iconName         - string     - Icon名稱
		/// routeName        - string     - 路由名稱
		/// displayOrder     - int        - 顯示順序
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> UpdateFunc(FuncUpdateRequest request);

        /// <summary>
        /// 刪除功能設定
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> DeleteFunc(int seqNo);

        /// <summary>
        /// 檢查功能是否存在
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
		/// funcID           - string     - 功能代碼
		/// funcName         - string     - 功能名稱
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> Exists(int seqNo,string funcID,string funcName);

    }
}
