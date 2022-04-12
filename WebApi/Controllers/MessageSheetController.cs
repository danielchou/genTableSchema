using ESUN.AGD.WebApi.Application.MessageSheet;
using ESUN.AGD.WebApi.Application.MessageSheet.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESUN.AGD.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageSheetController : Controller
    {

        private readonly IMessageSheetService _MessageSheetService;

        public MessageSheetController(IMessageSheetService MessageSheetService)
        {
            _MessageSheetService = MessageSheetService;
        }

        /// <summary>
        /// 依序號取得訊息傳送頁籤設定
        /// </summary>
        /// <param>
        /// seqNo           - int     - 流水號
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// messageSheetType - string     - 訊息傳送頁籤類別
		/// messageSheetID   - string     - 訊息傳送頁籤代碼
		/// messageSheetName - string     - 訊息傳送頁籤名稱
		/// displayOrder     - int        - 顯示順序
		/// isEnable         - bool       - 是否啟用?
		/// createDT         - DateTime   - 建立時間
		/// creator          - string     - 建立者
		/// updateDT         - DateTime   - 更新時間
		/// updator          - string     - 更新者
        /// updatorName      - string     - 更新者名稱
        /// </returns>
        [Authorize]
        [HttpGet("{seqNo}")]
        public async ValueTask<IActionResult> GetMessageSheet(int seqNo)
        {
            return Ok(await _MessageSheetService.GetMessageSheet(seqNo));
        }

        /// <summary>
        /// 搜尋訊息傳送頁籤設定 
        /// </summary>
        /// <param name="request">
		/// messageSheetType - string     - 訊息傳送頁籤類別
		/// messageSheetID   - string     - 訊息傳送頁籤代碼
		/// messageSheetName - string     - 訊息傳送頁籤名稱
        /// page             - int        - 分頁
        /// rowsPerPage      - int        - 每頁筆數
        /// sortColumn       - string     - 排序欄位
        /// sortOrder        - bool       - 排序順序
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// messageSheetType - string     - 訊息傳送頁籤類別
		/// messageSheetID   - string     - 訊息傳送頁籤代碼
		/// messageSheetName - string     - 訊息傳送頁籤名稱
		/// displayOrder     - int        - 顯示順序
		/// isEnable         - bool       - 是否啟用?
		/// createDT         - DateTime   - 建立時間
		/// creator          - string     - 建立者
		/// updateDT         - DateTime   - 更新時間
		/// updator          - string     - 更新者
        /// updatorName      - string     - 更新者名稱
        /// </returns>        
        [Authorize]
        [HttpGet("query")]
        public async ValueTask<IActionResult> QueryMessageSheet([FromQuery] MessageSheetQueryRequest request)
        {
            return Ok(await _MessageSheetService.QueryMessageSheet(request));
        }

        /// <summary>
        /// 新增訊息傳送頁籤設定 
        /// </summary>
        /// <param name="request">
		/// messageSheetType - string     - 訊息傳送頁籤類別
		/// messageSheetID   - string     - 訊息傳送頁籤代碼
		/// messageSheetName - string     - 訊息傳送頁籤名稱
		/// displayOrder     - int        - 顯示順序
		/// isEnable         - bool       - 是否啟用?
        /// </param>
        /// <returns>
        /// </returns>        
        [Authorize]
        [HttpPost]
        public async ValueTask<IActionResult> InsertMessageSheet(MessageSheetInsertRequest request)
        {
            return Ok(await _MessageSheetService.InsertMessageSheet(request));
        }

        /// <summary>
        /// 更新訊息傳送頁籤設定
        /// </summary>
        /// <param name="request">
		/// seqNo            - int        - 流水號
		/// messageSheetType - string     - 訊息傳送頁籤類別
		/// messageSheetID   - string     - 訊息傳送頁籤代碼
		/// messageSheetName - string     - 訊息傳送頁籤名稱
		/// displayOrder     - int        - 顯示順序
		/// isEnable         - bool       - 是否啟用?
        /// </param>
        /// <returns>
        /// </returns>
        [Authorize]
        [HttpPut]
        public async ValueTask<IActionResult> UpdateMessageSheet(MessageSheetUpdateRequest request)
        {
            return Ok(await _MessageSheetService.UpdateMessageSheet(request));
        }

        /// <summary>
        /// 刪除訊息傳送頁籤設定
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
        /// </param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        public async ValueTask<IActionResult> DeteleMessageSheet(int seqNo)
        {
            return Ok(await _MessageSheetService.DeleteMessageSheet(seqNo));
        }

    }
}
