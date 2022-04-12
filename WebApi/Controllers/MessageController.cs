using ESUN.AGD.WebApi.Application.Message;
using ESUN.AGD.WebApi.Application.Message.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESUN.AGD.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : Controller
    {

        private readonly IMessageService _MessageService;

        public MessageController(IMessageService MessageService)
        {
            _MessageService = MessageService;
        }

        /// <summary>
        /// 依序號取得訊息傳送設定
        /// </summary>
        /// <param>
        /// seqNo           - int     - 流水號
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
        [Authorize]
        [HttpGet("{seqNo}")]
        public async ValueTask<IActionResult> GetMessage(int seqNo)
        {
            return Ok(await _MessageService.GetMessage(seqNo));
        }

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
        /// sortOrder        - bool       - 排序順序
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
        [Authorize]
        [HttpGet("query")]
        public async ValueTask<IActionResult> QueryMessage([FromQuery] MessageQueryRequest request)
        {
            return Ok(await _MessageService.QueryMessage(request));
        }

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
        [Authorize]
        [HttpPost]
        public async ValueTask<IActionResult> InsertMessage(MessageInsertRequest request)
        {
            return Ok(await _MessageService.InsertMessage(request));
        }

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
        [Authorize]
        [HttpPut]
        public async ValueTask<IActionResult> UpdateMessage(MessageUpdateRequest request)
        {
            return Ok(await _MessageService.UpdateMessage(request));
        }

        /// <summary>
        /// 刪除訊息傳送設定
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
        /// </param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        public async ValueTask<IActionResult> DeteleMessage(int seqNo)
        {
            return Ok(await _MessageService.DeleteMessage(seqNo));
        }

    }
}
