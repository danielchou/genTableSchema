using ESUN.AGD.WebApi.Application.ReasonTxn;
using ESUN.AGD.WebApi.Application.ReasonTxn.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESUN.AGD.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReasonTxnController : Controller
    {

        private readonly IReasonTxnService _ReasonTxnService;

        public ReasonTxnController(IReasonTxnService ReasonTxnService)
        {
            _ReasonTxnService = ReasonTxnService;
        }

        /// <summary>
        /// 依序號取得聯繫原因Txn配對設定
        /// </summary>
        /// <param>
        /// seqNo           - int     - 流水號
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// txnItem          - string     - Txn交易類型
		/// reasonID         - string     - 原因代碼
		/// createDT         - DateTime   - 建立時間
		/// creator          - string     - 建立者
		/// updateDT         - DateTime   - 更新時間
		/// updator          - string     - 更新者
        /// updatorName      - string     - 更新者名稱
        /// </returns>
        [Authorize]
        [HttpGet("{seqNo}")]
        public async ValueTask<IActionResult> GetReasonTxn(int seqNo)
        {
            return Ok(await _ReasonTxnService.GetReasonTxn(seqNo));
        }

        /// <summary>
        /// 搜尋聯繫原因Txn配對設定 
        /// </summary>
        /// <param name="request">
		/// txnItem          - string     - Txn交易類型
		/// reasonID         - string     - 原因代碼
        /// page             - int        - 分頁
        /// rowsPerPage      - int        - 每頁筆數
        /// sortColumn       - string     - 排序欄位
        /// sortOrder        - bool       - 排序順序
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// txnItem          - string     - Txn交易類型
		/// reasonID         - string     - 原因代碼
		/// createDT         - DateTime   - 建立時間
		/// creator          - string     - 建立者
		/// updateDT         - DateTime   - 更新時間
		/// updator          - string     - 更新者
        /// updatorName      - string     - 更新者名稱
        /// </returns>        
        [Authorize]
        [HttpGet("query")]
        public async ValueTask<IActionResult> QueryReasonTxn([FromQuery] ReasonTxnQueryRequest request)
        {
            return Ok(await _ReasonTxnService.QueryReasonTxn(request));
        }

        /// <summary>
        /// 新增聯繫原因Txn配對設定 
        /// </summary>
        /// <param name="request">
		/// txnItem          - string     - Txn交易類型
		/// reasonID         - string     - 原因代碼
        /// </param>
        /// <returns>
        /// </returns>        
        [Authorize]
        [HttpPost]
        public async ValueTask<IActionResult> InsertReasonTxn(ReasonTxnInsertRequest request)
        {
            return Ok(await _ReasonTxnService.InsertReasonTxn(request));
        }

        /// <summary>
        /// 更新聯繫原因Txn配對設定
        /// </summary>
        /// <param name="request">
		/// seqNo            - int        - 流水號
		/// txnItem          - string     - Txn交易類型
		/// reasonID         - string     - 原因代碼
        /// </param>
        /// <returns>
        /// </returns>
        [Authorize]
        [HttpPut]
        public async ValueTask<IActionResult> UpdateReasonTxn(ReasonTxnUpdateRequest request)
        {
            return Ok(await _ReasonTxnService.UpdateReasonTxn(request));
        }

        /// <summary>
        /// 刪除聯繫原因Txn配對設定
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
        /// </param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        public async ValueTask<IActionResult> DeteleReasonTxn(int seqNo)
        {
            return Ok(await _ReasonTxnService.DeleteReasonTxn(seqNo));
        }

    }
}
