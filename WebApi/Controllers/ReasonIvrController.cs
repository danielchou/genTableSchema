using ESUN.AGD.WebApi.Application.ReasonIvr;
using ESUN.AGD.WebApi.Application.ReasonIvr.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESUN.AGD.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReasonIvrController : Controller
    {

        private readonly IReasonIvrService _ReasonIvrService;

        public ReasonIvrController(IReasonIvrService ReasonIvrService)
        {
            _ReasonIvrService = ReasonIvrService;
        }

        /// <summary>
        /// 依序號取得聯繫原因Ivr配對設定
        /// </summary>
        /// <param>
        /// seqNo           - int     - 流水號
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// ivrID            - string     - Ivr代碼
		/// reasonID         - string     - 原因代碼
		/// createDT         - DateTime   - 建立時間
		/// creator          - string     - 建立者
		/// updateDT         - DateTime   - 更新時間
		/// updator          - string     - 更新者
        /// updatorName      - string     - 更新者名稱
        /// </returns>
        [Authorize]
        [HttpGet("{seqNo}")]
        public async ValueTask<IActionResult> GetReasonIvr(int seqNo)
        {
            return Ok(await _ReasonIvrService.GetReasonIvr(seqNo));
        }

        /// <summary>
        /// 搜尋聯繫原因Ivr配對設定 
        /// </summary>
        /// <param name="request">
		/// ivrID            - string     - Ivr代碼
		/// reasonID         - string     - 原因代碼
        /// page             - int        - 分頁
        /// rowsPerPage      - int        - 每頁筆數
        /// sortColumn       - string     - 排序欄位
        /// sortOrder        - bool       - 排序順序
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// ivrID            - string     - Ivr代碼
		/// reasonID         - string     - 原因代碼
		/// createDT         - DateTime   - 建立時間
		/// creator          - string     - 建立者
		/// updateDT         - DateTime   - 更新時間
		/// updator          - string     - 更新者
        /// updatorName      - string     - 更新者名稱
        /// </returns>        
        [Authorize]
        [HttpGet("query")]
        public async ValueTask<IActionResult> QueryReasonIvr([FromQuery] ReasonIvrQueryRequest request)
        {
            return Ok(await _ReasonIvrService.QueryReasonIvr(request));
        }

        /// <summary>
        /// 新增聯繫原因Ivr配對設定 
        /// </summary>
        /// <param name="request">
		/// ivrID            - string     - Ivr代碼
		/// reasonID         - string     - 原因代碼
        /// </param>
        /// <returns>
        /// </returns>        
        [Authorize]
        [HttpPost]
        public async ValueTask<IActionResult> InsertReasonIvr(ReasonIvrInsertRequest request)
        {
            return Ok(await _ReasonIvrService.InsertReasonIvr(request));
        }

        /// <summary>
        /// 更新聯繫原因Ivr配對設定
        /// </summary>
        /// <param name="request">
		/// seqNo            - int        - 流水號
		/// ivrID            - string     - Ivr代碼
		/// reasonID         - string     - 原因代碼
        /// </param>
        /// <returns>
        /// </returns>
        [Authorize]
        [HttpPut]
        public async ValueTask<IActionResult> UpdateReasonIvr(ReasonIvrUpdateRequest request)
        {
            return Ok(await _ReasonIvrService.UpdateReasonIvr(request));
        }

        /// <summary>
        /// 刪除聯繫原因Ivr配對設定
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
        /// </param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        public async ValueTask<IActionResult> DeteleReasonIvr(int seqNo)
        {
            return Ok(await _ReasonIvrService.DeleteReasonIvr(seqNo));
        }

    }
}
