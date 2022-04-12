﻿using ESUN.AGD.WebApi.Application.Message.Contract;

namespace ESUN.AGD.WebApi.Application.Message
{
    public interface IMessageService
    {
        /// <summary>
        /// 依序號取得訊息傳送設定
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// messageSheetID   - string     - 訊息傳送頁籤代碼
		/// messageTemplateID - string     - 訊息傳送範本代碼
		/// messageName      - string     - 訊息傳送名稱
		/// displayOrder     - int        - 顯示順序
		/// isEnable         - bool       - 是否啟用?
		/// createDT         - DateTime   - 建立時間
		/// creator          - string     - 建立者
		/// updateDT         - DateTime   - 更新時間
		/// updator          - string     - 更新者
        /// updatorName      - string     - 更新者名稱
        /// </returns>
        ValueTask<BasicResponse<MessageResponse>> GetMessage(int seqNo);

        /// <summary>
        /// 搜尋訊息傳送設定 
        /// </summary>
        /// <param name="request">
		/// messageSheetID   - string     - 訊息傳送頁籤代碼
		/// messageTemplateID - string     - 訊息傳送範本代碼
		/// messageName      - string     - 訊息傳送名稱
        /// page             - int        - 分頁
        /// rowsPerPage      - int        - 每頁筆數
        /// sortColumn       - string     - 排序欄位
        /// sortOrder        - string     - 排序順序
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// messageSheetID   - string     - 訊息傳送頁籤代碼
		/// messageTemplateID - string     - 訊息傳送範本代碼
		/// messageName      - string     - 訊息傳送名稱
		/// displayOrder     - int        - 顯示順序
		/// isEnable         - bool       - 是否啟用?
		/// createDT         - DateTime   - 建立時間
		/// creator          - string     - 建立者
		/// updateDT         - DateTime   - 更新時間
		/// updator          - string     - 更新者
        /// updatorName      - string     - 更新者名稱
        /// </returns> 
        ValueTask<BasicResponse<List<MessageResponse>>> QueryMessage(MessageQueryRequest request);

        /// <summary>
        /// 新增訊息傳送設定 
        /// </summary>
        /// <param name="request">
		/// messageSheetID   - string     - 訊息傳送頁籤代碼
		/// messageTemplateID - string     - 訊息傳送範本代碼
		/// messageName      - string     - 訊息傳送名稱
		/// displayOrder     - int        - 顯示順序
		/// isEnable         - bool       - 是否啟用?
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> InsertMessage(MessageInsertRequest request);

        /// <summary>
        /// 更新訊息傳送設定
        /// </summary>
        /// <param name="request">
		/// seqNo            - int        - 流水號
		/// messageSheetID   - string     - 訊息傳送頁籤代碼
		/// messageTemplateID - string     - 訊息傳送範本代碼
		/// messageName      - string     - 訊息傳送名稱
		/// displayOrder     - int        - 顯示順序
		/// isEnable         - bool       - 是否啟用?
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> UpdateMessage(MessageUpdateRequest request);

        /// <summary>
        /// 刪除訊息傳送設定
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> DeleteMessage(int seqNo);

        /// <summary>
        /// 檢查訊息傳送是否存在
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
		/// messageSheetID   - string     - 訊息傳送頁籤代碼
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> Exists(int seqNo,string messageSheetID);

    }
}
