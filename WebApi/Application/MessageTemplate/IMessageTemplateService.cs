using ESUN.AGD.WebApi.Application.MessageTemplate.Contract;

namespace ESUN.AGD.WebApi.Application.MessageTemplate
{
    public interface IMessageTemplateService
    {
        /// <summary>
        /// 依序號取得訊息傳送範本設定
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// messageTemplateID - string     - 訊息傳送範本代碼
		/// messageTemplateName - string     - 訊息傳送範本名稱
		/// messageB08Code   - string     - 訊息傳送B08代碼
		/// content          - string     - 訊息傳送範本
		/// isEnable         - bool       - 是否啟用?
		/// createDT         - DateTime   - 建立時間
		/// creator          - string     - 建立者
		/// updateDT         - DateTime   - 更新時間
		/// updator          - string     - 更新者
        /// updatorName      - string     - 更新者名稱
        /// </returns>
        ValueTask<BasicResponse<MessageTemplateResponse>> GetMessageTemplate(int seqNo);

        /// <summary>
        /// 搜尋訊息傳送範本設定 
        /// </summary>
        /// <param name="request">
		/// messageTemplateID - string     - 訊息傳送範本代碼
		/// messageTemplateName - string     - 訊息傳送範本名稱
		/// messageB08Code   - string     - 訊息傳送B08代碼
        /// page             - int        - 分頁
        /// rowsPerPage      - int        - 每頁筆數
        /// sortColumn       - string     - 排序欄位
        /// sortOrder        - string     - 排序順序
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// messageTemplateID - string     - 訊息傳送範本代碼
		/// messageTemplateName - string     - 訊息傳送範本名稱
		/// messageB08Code   - string     - 訊息傳送B08代碼
		/// content          - string     - 訊息傳送範本
		/// isEnable         - bool       - 是否啟用?
		/// createDT         - DateTime   - 建立時間
		/// creator          - string     - 建立者
		/// updateDT         - DateTime   - 更新時間
		/// updator          - string     - 更新者
        /// updatorName      - string     - 更新者名稱
        /// </returns> 
        ValueTask<BasicResponse<List<MessageTemplateResponse>>> QueryMessageTemplate(MessageTemplateQueryRequest request);

        /// <summary>
        /// 新增訊息傳送範本設定 
        /// </summary>
        /// <param name="request">
		/// messageTemplateID - string     - 訊息傳送範本代碼
		/// messageTemplateName - string     - 訊息傳送範本名稱
		/// messageB08Code   - string     - 訊息傳送B08代碼
		/// content          - string     - 訊息傳送範本
		/// isEnable         - bool       - 是否啟用?
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> InsertMessageTemplate(MessageTemplateInsertRequest request);

        /// <summary>
        /// 更新訊息傳送範本設定
        /// </summary>
        /// <param name="request">
		/// seqNo            - int        - 流水號
		/// messageTemplateID - string     - 訊息傳送範本代碼
		/// messageTemplateName - string     - 訊息傳送範本名稱
		/// messageB08Code   - string     - 訊息傳送B08代碼
		/// content          - string     - 訊息傳送範本
		/// isEnable         - bool       - 是否啟用?
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> UpdateMessageTemplate(MessageTemplateUpdateRequest request);

        /// <summary>
        /// 刪除訊息傳送範本設定
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> DeleteMessageTemplate(int seqNo);

        /// <summary>
        /// 檢查訊息傳送範本是否存在
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
		/// messageTemplateID - string     - 訊息傳送範本代碼
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> Exists(int seqNo,string messageTemplateID);

    }
}
