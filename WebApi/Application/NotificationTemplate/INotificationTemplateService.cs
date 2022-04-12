using ESUN.AGD.WebApi.Application.NotificationTemplate.Contract;

namespace ESUN.AGD.WebApi.Application.NotificationTemplate
{
    public interface INotificationTemplateService
    {
        /// <summary>
        /// 依序號取得通知公告範本設定
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// notificationType - string     - 通知公告類別
		/// notificationID   - string     - 通知公告代碼
		/// notificationName - string     - 通知公告名稱
		/// content          - string     - 通知公告範本
		/// displayOrder     - int        - 顯示順序
		/// isEnable         - bool       - 是否啟用?
		/// createDT         - DateTime   - 建立時間
		/// creator          - string     - 建立者
		/// updateDT         - DateTime   - 更新時間
		/// updator          - string     - 更新者
        /// updatorName      - string     - 更新者名稱
        /// </returns>
        ValueTask<BasicResponse<NotificationTemplateResponse>> GetNotificationTemplate(int seqNo);

        /// <summary>
        /// 搜尋通知公告範本設定 
        /// </summary>
        /// <param name="request">
		/// notificationType - string     - 通知公告類別
		/// notificationID   - string     - 通知公告代碼
		/// notificationName - string     - 通知公告名稱
        /// page             - int        - 分頁
        /// rowsPerPage      - int        - 每頁筆數
        /// sortColumn       - string     - 排序欄位
        /// sortOrder        - string     - 排序順序
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// notificationType - string     - 通知公告類別
		/// notificationID   - string     - 通知公告代碼
		/// notificationName - string     - 通知公告名稱
		/// content          - string     - 通知公告範本
		/// displayOrder     - int        - 顯示順序
		/// isEnable         - bool       - 是否啟用?
		/// createDT         - DateTime   - 建立時間
		/// creator          - string     - 建立者
		/// updateDT         - DateTime   - 更新時間
		/// updator          - string     - 更新者
        /// updatorName      - string     - 更新者名稱
        /// </returns> 
        ValueTask<BasicResponse<List<NotificationTemplateResponse>>> QueryNotificationTemplate(NotificationTemplateQueryRequest request);

        /// <summary>
        /// 新增通知公告範本設定 
        /// </summary>
        /// <param name="request">
		/// notificationType - string     - 通知公告類別
		/// notificationID   - string     - 通知公告代碼
		/// notificationName - string     - 通知公告名稱
		/// content          - string     - 通知公告範本
		/// displayOrder     - int        - 顯示順序
		/// isEnable         - bool       - 是否啟用?
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> InsertNotificationTemplate(NotificationTemplateInsertRequest request);

        /// <summary>
        /// 更新通知公告範本設定
        /// </summary>
        /// <param name="request">
		/// seqNo            - int        - 流水號
		/// notificationType - string     - 通知公告類別
		/// notificationID   - string     - 通知公告代碼
		/// notificationName - string     - 通知公告名稱
		/// content          - string     - 通知公告範本
		/// displayOrder     - int        - 顯示順序
		/// isEnable         - bool       - 是否啟用?
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> UpdateNotificationTemplate(NotificationTemplateUpdateRequest request);

        /// <summary>
        /// 刪除通知公告範本設定
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> DeleteNotificationTemplate(int seqNo);

        /// <summary>
        /// 檢查通知公告範本是否存在
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
		/// notificationID   - string     - 通知公告代碼
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> Exists(int seqNo,string notificationID);

    }
}
