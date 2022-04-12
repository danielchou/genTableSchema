using ESUN.AGD.WebApi.Application.NotificationTemplate;
using ESUN.AGD.WebApi.Application.NotificationTemplate.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESUN.AGD.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationTemplateController : Controller
    {

        private readonly INotificationTemplateService _NotificationTemplateService;

        public NotificationTemplateController(INotificationTemplateService NotificationTemplateService)
        {
            _NotificationTemplateService = NotificationTemplateService;
        }

        /// <summary>
        /// 依序號取得通知公告範本設定
        /// </summary>
        /// <param>
        /// seqNo           - int     - 流水號
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
        [Authorize]
        [HttpGet("{seqNo}")]
        public async ValueTask<IActionResult> GetNotificationTemplate(int seqNo)
        {
            return Ok(await _NotificationTemplateService.GetNotificationTemplate(seqNo));
        }

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
        /// sortOrder        - bool       - 排序順序
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
        [Authorize]
        [HttpGet("query")]
        public async ValueTask<IActionResult> QueryNotificationTemplate([FromQuery] NotificationTemplateQueryRequest request)
        {
            return Ok(await _NotificationTemplateService.QueryNotificationTemplate(request));
        }

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
        [Authorize]
        [HttpPost]
        public async ValueTask<IActionResult> InsertNotificationTemplate(NotificationTemplateInsertRequest request)
        {
            return Ok(await _NotificationTemplateService.InsertNotificationTemplate(request));
        }

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
        [Authorize]
        [HttpPut]
        public async ValueTask<IActionResult> UpdateNotificationTemplate(NotificationTemplateUpdateRequest request)
        {
            return Ok(await _NotificationTemplateService.UpdateNotificationTemplate(request));
        }

        /// <summary>
        /// 刪除通知公告範本設定
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
        /// </param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        public async ValueTask<IActionResult> DeteleNotificationTemplate(int seqNo)
        {
            return Ok(await _NotificationTemplateService.DeleteNotificationTemplate(seqNo));
        }

    }
}
