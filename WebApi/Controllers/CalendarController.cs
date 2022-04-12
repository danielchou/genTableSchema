using ESUN.AGD.WebApi.Application.Calendar;
using ESUN.AGD.WebApi.Application.Calendar.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESUN.AGD.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarController : Controller
    {

        private readonly ICalendarService _CalendarService;

        public CalendarController(ICalendarService CalendarService)
        {
            _CalendarService = CalendarService;
        }

        /// <summary>
        /// 依序號取得班表資訊設定
        /// </summary>
        /// <param>
        /// seqNo           - int     - 流水號
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
        [Authorize]
        [HttpGet("{seqNo}")]
        public async ValueTask<IActionResult> GetCalendar(int seqNo)
        {
            return Ok(await _CalendarService.GetCalendar(seqNo));
        }

        /// <summary>
        /// 搜尋班表資訊設定 
        /// </summary>
        /// <param name="request">
		/// userID           - string     - 使用者帳號
		/// scheduleDate     - DateTime   - 排定日期
        /// page             - int        - 分頁
        /// rowsPerPage      - int        - 每頁筆數
        /// sortColumn       - string     - 排序欄位
        /// sortOrder        - bool       - 排序順序
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
        [Authorize]
        [HttpGet("query")]
        public async ValueTask<IActionResult> QueryCalendar([FromQuery] CalendarQueryRequest request)
        {
            return Ok(await _CalendarService.QueryCalendar(request));
        }

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
        [Authorize]
        [HttpPost]
        public async ValueTask<IActionResult> InsertCalendar(CalendarInsertRequest request)
        {
            return Ok(await _CalendarService.InsertCalendar(request));
        }

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
        [Authorize]
        [HttpPut]
        public async ValueTask<IActionResult> UpdateCalendar(CalendarUpdateRequest request)
        {
            return Ok(await _CalendarService.UpdateCalendar(request));
        }

        /// <summary>
        /// 刪除班表資訊設定
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
        /// </param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        public async ValueTask<IActionResult> DeteleCalendar(int seqNo)
        {
            return Ok(await _CalendarService.DeleteCalendar(seqNo));
        }

    }
}
