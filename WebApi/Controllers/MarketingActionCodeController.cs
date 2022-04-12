using ESUN.AGD.WebApi.Application.MarketingActionCode;
using ESUN.AGD.WebApi.Application.MarketingActionCode.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESUN.AGD.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarketingActionCodeController : Controller
    {

        private readonly IMarketingActionCodeService _MarketingActionCodeService;

        public MarketingActionCodeController(IMarketingActionCodeService MarketingActionCodeService)
        {
            _MarketingActionCodeService = MarketingActionCodeService;
        }

        /// <summary>
        /// 依序號取得行銷方案結果設定
        /// </summary>
        /// <param>
        /// seqNo           - int     - 流水號
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// actionCodeType   - string     - 客群方案類別
		/// marketingID      - string     - 行銷方案代碼
		/// actionCode       - string     - 行銷結果代碼
		/// content          - string     - 行銷結果說明
		/// isAccept         - bool       - 是否接受?
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
        public async ValueTask<IActionResult> GetMarketingActionCode(int seqNo)
        {
            return Ok(await _MarketingActionCodeService.GetMarketingActionCode(seqNo));
        }

        /// <summary>
        /// 搜尋行銷方案結果設定 
        /// </summary>
        /// <param name="request">
		/// actionCodeType   - string     - 客群方案類別
		/// marketingID      - string     - 行銷方案代碼
		/// actionCode       - string     - 行銷結果代碼
        /// page             - int        - 分頁
        /// rowsPerPage      - int        - 每頁筆數
        /// sortColumn       - string     - 排序欄位
        /// sortOrder        - bool       - 排序順序
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// actionCodeType   - string     - 客群方案類別
		/// marketingID      - string     - 行銷方案代碼
		/// actionCode       - string     - 行銷結果代碼
		/// content          - string     - 行銷結果說明
		/// isAccept         - bool       - 是否接受?
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
        public async ValueTask<IActionResult> QueryMarketingActionCode([FromQuery] MarketingActionCodeQueryRequest request)
        {
            return Ok(await _MarketingActionCodeService.QueryMarketingActionCode(request));
        }

        /// <summary>
        /// 新增行銷方案結果設定 
        /// </summary>
        /// <param name="request">
		/// actionCodeType   - string     - 客群方案類別
		/// marketingID      - string     - 行銷方案代碼
		/// actionCode       - string     - 行銷結果代碼
		/// content          - string     - 行銷結果說明
		/// isAccept         - bool       - 是否接受?
		/// displayOrder     - int        - 顯示順序
		/// isEnable         - bool       - 是否啟用?
        /// </param>
        /// <returns>
        /// </returns>        
        [Authorize]
        [HttpPost]
        public async ValueTask<IActionResult> InsertMarketingActionCode(MarketingActionCodeInsertRequest request)
        {
            return Ok(await _MarketingActionCodeService.InsertMarketingActionCode(request));
        }

        /// <summary>
        /// 更新行銷方案結果設定
        /// </summary>
        /// <param name="request">
		/// seqNo            - int        - 流水號
		/// actionCodeType   - string     - 客群方案類別
		/// marketingID      - string     - 行銷方案代碼
		/// actionCode       - string     - 行銷結果代碼
		/// content          - string     - 行銷結果說明
		/// isAccept         - bool       - 是否接受?
		/// displayOrder     - int        - 顯示順序
		/// isEnable         - bool       - 是否啟用?
        /// </param>
        /// <returns>
        /// </returns>
        [Authorize]
        [HttpPut]
        public async ValueTask<IActionResult> UpdateMarketingActionCode(MarketingActionCodeUpdateRequest request)
        {
            return Ok(await _MarketingActionCodeService.UpdateMarketingActionCode(request));
        }

        /// <summary>
        /// 刪除行銷方案結果設定
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
        /// </param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        public async ValueTask<IActionResult> DeteleMarketingActionCode(int seqNo)
        {
            return Ok(await _MarketingActionCodeService.DeleteMarketingActionCode(seqNo));
        }

    }
}
