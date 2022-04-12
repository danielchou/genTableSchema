using ESUN.AGD.WebApi.Application.Reason;
using ESUN.AGD.WebApi.Application.Reason.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESUN.AGD.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReasonController : Controller
    {

        private readonly IReasonService _ReasonService;

        public ReasonController(IReasonService ReasonService)
        {
            _ReasonService = ReasonService;
        }

        /// <summary>
        /// 依序號取得聯繫原因碼設定
        /// </summary>
        /// <param>
        /// seqNo           - int     - 流水號
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// reasonID         - string     - 聯繫原因碼代碼
		/// reasonName       - string     - 聯繫原因碼名稱
		/// parentReasonID   - string     - 上層聯繫原因碼代碼
		/// level            - int        - 階層
		/// bussinessUnit    - string     - 事業處
		/// bussinessB03Type - string     - B03業務別
		/// reviewType       - string     - 覆核類別
		/// memo             - string     - 備註
		/// webUrl           - string     - 網頁連結
		/// kMUrl            - string     - KM連結
		/// displayOrder     - int        - 顯示順序
		/// isUsually        - bool       - 是否常用
		/// usuallyReasonName - string     - 常用名稱
		/// usuallyDisplayOrder - int        - 常用顯示順序
		/// isEnable         - bool       - 是否啟用?
		/// createDT         - DateTime   - 建立時間
		/// creator          - string     - 建立者
		/// updateDT         - DateTime   - 更新時間
		/// updator          - string     - 更新者
        /// updatorName      - string     - 更新者名稱
        /// </returns>
        [Authorize]
        [HttpGet("{seqNo}")]
        public async ValueTask<IActionResult> GetReason(int seqNo)
        {
            return Ok(await _ReasonService.GetReason(seqNo));
        }

        /// <summary>
        /// 搜尋聯繫原因碼設定 
        /// </summary>
        /// <param name="request">
		/// reasonID         - string     - 聯繫原因碼代碼
		/// reasonName       - string     - 聯繫原因碼名稱
		/// parentReasonID   - string     - 上層聯繫原因碼代碼
		/// level            - int        - 階層
        /// page             - int        - 分頁
        /// rowsPerPage      - int        - 每頁筆數
        /// sortColumn       - string     - 排序欄位
        /// sortOrder        - bool       - 排序順序
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// reasonID         - string     - 聯繫原因碼代碼
		/// reasonName       - string     - 聯繫原因碼名稱
		/// parentReasonID   - string     - 上層聯繫原因碼代碼
		/// level            - int        - 階層
		/// bussinessUnit    - string     - 事業處
		/// bussinessB03Type - string     - B03業務別
		/// reviewType       - string     - 覆核類別
		/// memo             - string     - 備註
		/// webUrl           - string     - 網頁連結
		/// kMUrl            - string     - KM連結
		/// displayOrder     - int        - 顯示順序
		/// isUsually        - bool       - 是否常用
		/// usuallyReasonName - string     - 常用名稱
		/// usuallyDisplayOrder - int        - 常用顯示順序
		/// isEnable         - bool       - 是否啟用?
		/// createDT         - DateTime   - 建立時間
		/// creator          - string     - 建立者
		/// updateDT         - DateTime   - 更新時間
		/// updator          - string     - 更新者
        /// updatorName      - string     - 更新者名稱
        /// </returns>        
        [Authorize]
        [HttpGet("query")]
        public async ValueTask<IActionResult> QueryReason([FromQuery] ReasonQueryRequest request)
        {
            return Ok(await _ReasonService.QueryReason(request));
        }

        /// <summary>
        /// 新增聯繫原因碼設定 
        /// </summary>
        /// <param name="request">
		/// reasonID         - string     - 聯繫原因碼代碼
		/// reasonName       - string     - 聯繫原因碼名稱
		/// parentReasonID   - string     - 上層聯繫原因碼代碼
		/// level            - int        - 階層
		/// bussinessUnit    - string     - 事業處
		/// bussinessB03Type - string     - B03業務別
		/// reviewType       - string     - 覆核類別
		/// memo             - string     - 備註
		/// webUrl           - string     - 網頁連結
		/// kMUrl            - string     - KM連結
		/// displayOrder     - int        - 顯示順序
		/// isUsually        - bool       - 是否常用
		/// usuallyReasonName - string     - 常用名稱
		/// usuallyDisplayOrder - int        - 常用顯示順序
		/// isEnable         - bool       - 是否啟用?
        /// </param>
        /// <returns>
        /// </returns>        
        [Authorize]
        [HttpPost]
        public async ValueTask<IActionResult> InsertReason(ReasonInsertRequest request)
        {
            return Ok(await _ReasonService.InsertReason(request));
        }

        /// <summary>
        /// 更新聯繫原因碼設定
        /// </summary>
        /// <param name="request">
		/// seqNo            - int        - 流水號
		/// reasonID         - string     - 聯繫原因碼代碼
		/// reasonName       - string     - 聯繫原因碼名稱
		/// parentReasonID   - string     - 上層聯繫原因碼代碼
		/// level            - int        - 階層
		/// bussinessUnit    - string     - 事業處
		/// bussinessB03Type - string     - B03業務別
		/// reviewType       - string     - 覆核類別
		/// memo             - string     - 備註
		/// webUrl           - string     - 網頁連結
		/// kMUrl            - string     - KM連結
		/// displayOrder     - int        - 顯示順序
		/// isUsually        - bool       - 是否常用
		/// usuallyReasonName - string     - 常用名稱
		/// usuallyDisplayOrder - int        - 常用顯示順序
		/// isEnable         - bool       - 是否啟用?
        /// </param>
        /// <returns>
        /// </returns>
        [Authorize]
        [HttpPut]
        public async ValueTask<IActionResult> UpdateReason(ReasonUpdateRequest request)
        {
            return Ok(await _ReasonService.UpdateReason(request));
        }

        /// <summary>
        /// 刪除聯繫原因碼設定
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
        /// </param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        public async ValueTask<IActionResult> DeteleReason(int seqNo)
        {
            return Ok(await _ReasonService.DeleteReason(seqNo));
        }

    }
}
