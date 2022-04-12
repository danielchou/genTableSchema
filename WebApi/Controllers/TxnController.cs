using ESUN.AGD.WebApi.Application.Txn;
using ESUN.AGD.WebApi.Application.Txn.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESUN.AGD.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TxnController : Controller
    {

        private readonly ITxnService _TxnService;

        public TxnController(ITxnService TxnService)
        {
            _TxnService = TxnService;
        }

        /// <summary>
        /// 依序號取得交易執行設定
        /// </summary>
        /// <param>
        /// seqNo           - int     - 流水號
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// txnType          - string     - 交易執行類別
		/// txnID            - string     - 交易執行代碼
		/// txnName          - string     - 交易執行名稱
		/// txnScript        - string     - 交易執行話術
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
        public async ValueTask<IActionResult> GetTxn(int seqNo)
        {
            return Ok(await _TxnService.GetTxn(seqNo));
        }

        /// <summary>
        /// 搜尋交易執行設定 
        /// </summary>
        /// <param name="request">
		/// txnType          - string     - 交易執行類別
		/// txnID            - string     - 交易執行代碼
		/// txnName          - string     - 交易執行名稱
        /// page             - int        - 分頁
        /// rowsPerPage      - int        - 每頁筆數
        /// sortColumn       - string     - 排序欄位
        /// sortOrder        - bool       - 排序順序
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// txnType          - string     - 交易執行類別
		/// txnID            - string     - 交易執行代碼
		/// txnName          - string     - 交易執行名稱
		/// txnScript        - string     - 交易執行話術
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
        public async ValueTask<IActionResult> QueryTxn([FromQuery] TxnQueryRequest request)
        {
            return Ok(await _TxnService.QueryTxn(request));
        }

        /// <summary>
        /// 新增交易執行設定 
        /// </summary>
        /// <param name="request">
		/// txnType          - string     - 交易執行類別
		/// txnID            - string     - 交易執行代碼
		/// txnName          - string     - 交易執行名稱
		/// txnScript        - string     - 交易執行話術
		/// displayOrder     - int        - 顯示順序
		/// isEnable         - bool       - 是否啟用?
        /// </param>
        /// <returns>
        /// </returns>        
        [Authorize]
        [HttpPost]
        public async ValueTask<IActionResult> InsertTxn(TxnInsertRequest request)
        {
            return Ok(await _TxnService.InsertTxn(request));
        }

        /// <summary>
        /// 更新交易執行設定
        /// </summary>
        /// <param name="request">
		/// seqNo            - int        - 流水號
		/// txnType          - string     - 交易執行類別
		/// txnID            - string     - 交易執行代碼
		/// txnName          - string     - 交易執行名稱
		/// txnScript        - string     - 交易執行話術
		/// displayOrder     - int        - 顯示順序
		/// isEnable         - bool       - 是否啟用?
        /// </param>
        /// <returns>
        /// </returns>
        [Authorize]
        [HttpPut]
        public async ValueTask<IActionResult> UpdateTxn(TxnUpdateRequest request)
        {
            return Ok(await _TxnService.UpdateTxn(request));
        }

        /// <summary>
        /// 刪除交易執行設定
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
        /// </param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        public async ValueTask<IActionResult> DeteleTxn(int seqNo)
        {
            return Ok(await _TxnService.DeleteTxn(seqNo));
        }

    }
}
