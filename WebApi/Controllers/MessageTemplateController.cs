using ESUN.AGD.WebApi.Application.MessageTemplate;
using ESUN.AGD.WebApi.Application.MessageTemplate.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESUN.AGD.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageTemplateController : Controller
    {

        private readonly IMessageTemplateService _MessageTemplateService;

        public MessageTemplateController(IMessageTemplateService MessageTemplateService)
        {
            _MessageTemplateService = MessageTemplateService;
        }

        /// <summary>
        /// 依序號取得訊息傳送範本設定
        /// </summary>
        /// <param>
        /// seqNo           - int     - 流水號
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
        [Authorize]
        [HttpGet("{seqNo}")]
        public async ValueTask<IActionResult> GetMessageTemplate(int seqNo)
        {
            return Ok(await _MessageTemplateService.GetMessageTemplate(seqNo));
        }

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
        /// sortOrder        - bool       - 排序順序
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
        [Authorize]
        [HttpGet("query")]
        public async ValueTask<IActionResult> QueryMessageTemplate([FromQuery] MessageTemplateQueryRequest request)
        {
            return Ok(await _MessageTemplateService.QueryMessageTemplate(request));
        }

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
        [Authorize]
        [HttpPost]
        public async ValueTask<IActionResult> InsertMessageTemplate(MessageTemplateInsertRequest request)
        {
            return Ok(await _MessageTemplateService.InsertMessageTemplate(request));
        }

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
        [Authorize]
        [HttpPut]
        public async ValueTask<IActionResult> UpdateMessageTemplate(MessageTemplateUpdateRequest request)
        {
            return Ok(await _MessageTemplateService.UpdateMessageTemplate(request));
        }

        /// <summary>
        /// 刪除訊息傳送範本設定
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
        /// </param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        public async ValueTask<IActionResult> DeteleMessageTemplate(int seqNo)
        {
            return Ok(await _MessageTemplateService.DeleteMessageTemplate(seqNo));
        }

    }
}
