using ESUN.AGD.WebApi.Application.Calendar.Contract;

namespace ESUN.AGD.WebApi.Application.Calendar
{
    public interface ICalendarService
    {
        /// <summary>
        /// 依序號取得班表資訊設定
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// userID           - string     - 使用者帳號
		/// scheduleDate     - DateTime   - 排定日期
		/// content          - string     - 班表內容
		/// createDT         - DateTime   - 建立時間
		/// creator          - string     - 建立者
		/// updateDT         - DateTime   - 更新時間
		/// updator          - string     - 更新者
        /// updatorName      - string     - 更新者名稱
        /// </returns>
        ValueTask<BasicResponse<CalendarResponse>> GetCalendar(int seqNo);

        /// <summary>
        /// 搜尋班表資訊設定 
        /// </summary>
        /// <param name="request">
		/// userID           - string     - 使用者帳號
		/// scheduleDate     - DateTime   - 排定日期
        /// page             - int        - 分頁
        /// rowsPerPage      - int        - 每頁筆數
        /// sortColumn       - string     - 排序欄位
        /// sortOrder        - string     - 排序順序
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// userID           - string     - 使用者帳號
		/// scheduleDate     - DateTime   - 排定日期
		/// content          - string     - 班表內容
		/// createDT         - DateTime   - 建立時間
		/// creator          - string     - 建立者
		/// updateDT         - DateTime   - 更新時間
		/// updator          - string     - 更新者
        /// updatorName      - string     - 更新者名稱
        /// </returns> 
        ValueTask<BasicResponse<List<CalendarResponse>>> QueryCalendar(CalendarQueryRequest request);

        /// <summary>
        /// 新增班表資訊設定 
        /// </summary>
        /// <param name="request">
		/// userID           - string     - 使用者帳號
		/// scheduleDate     - DateTime   - 排定日期
		/// content          - string     - 班表內容
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> InsertCalendar(CalendarInsertRequest request);

        /// <summary>
        /// 更新班表資訊設定
        /// </summary>
        /// <param name="request">
		/// seqNo            - int        - 流水號
		/// userID           - string     - 使用者帳號
		/// scheduleDate     - DateTime   - 排定日期
		/// content          - string     - 班表內容
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> UpdateCalendar(CalendarUpdateRequest request);

        /// <summary>
        /// 刪除班表資訊設定
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> DeleteCalendar(int seqNo);

        /// <summary>
        /// 檢查班表資訊是否存在
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
		/// userID           - string     - 使用者帳號
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> Exists(int seqNo,string userID);

    }
}
