using ESUN.AGD.WebApi.Application.AuxCode;
using ESUN.AGD.WebApi.Application.AuxCode.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESUN.AGD.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuxCodeController : Controller
    {

        private readonly IAuxCodeService _AuxCodeService;

        public AuxCodeController(IAuxCodeService AuxCodeService)
        {
            _AuxCodeService = AuxCodeService;
        }

        /// <summary>
        /// 依序號取得休息碼設定
        /// </summary>
        /// <param>
        /// seqNo           - int     - 流水號
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// auxID            - string     - 休息碼代碼
		/// auxName          - string     - 休息碼名稱
		/// isLongTimeAux    - bool       - 是否長時間離開?
		/// displayOrder     - int        - 顯示順序
		/// createDT         - DateTime   - 建立時間
		/// creator          - string     - 建立者
		/// updateDT         - DateTime   - 更新時間
		/// updator          - string     - 更新者
        /// updatorName      - string     - 更新者名稱
        /// </returns>
        [Authorize]
        [HttpGet("{seqNo}")]
        public async ValueTask<IActionResult> GetAuxCode(int seqNo)
        {
            return Ok(await _AuxCodeService.GetAuxCode(seqNo));
        }

        /// <summary>
        /// 搜尋休息碼設定 
        /// </summary>
        /// <param name="request">
		/// auxID            - string     - 休息碼代碼
		/// auxName          - string     - 休息碼名稱
        /// page             - int        - 分頁
        /// rowsPerPage      - int        - 每頁筆數
        /// sortColumn       - string     - 排序欄位
        /// sortOrder        - bool       - 排序順序
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// auxID            - string     - 休息碼代碼
		/// auxName          - string     - 休息碼名稱
		/// isLongTimeAux    - bool       - 是否長時間離開?
		/// displayOrder     - int        - 顯示順序
		/// createDT         - DateTime   - 建立時間
		/// creator          - string     - 建立者
		/// updateDT         - DateTime   - 更新時間
		/// updator          - string     - 更新者
        /// updatorName      - string     - 更新者名稱
        /// </returns>        
        [Authorize]
        [HttpGet("query")]
        public async ValueTask<IActionResult> QueryAuxCode([FromQuery] AuxCodeQueryRequest request)
        {
            return Ok(await _AuxCodeService.QueryAuxCode(request));
        }

        /// <summary>
        /// 新增休息碼設定 
        /// </summary>
        /// <param name="request">
		/// auxID            - string     - 休息碼代碼
		/// auxName          - string     - 休息碼名稱
		/// isLongTimeAux    - bool       - 是否長時間離開?
		/// displayOrder     - int        - 顯示順序
        /// </param>
        /// <returns>
        /// </returns>        
        [Authorize]
        [HttpPost]
        public async ValueTask<IActionResult> InsertAuxCode(AuxCodeInsertRequest request)
        {
            return Ok(await _AuxCodeService.InsertAuxCode(request));
        }

        /// <summary>
        /// 更新休息碼設定
        /// </summary>
        /// <param name="request">
		/// seqNo            - int        - 流水號
		/// auxID            - string     - 休息碼代碼
		/// auxName          - string     - 休息碼名稱
		/// isLongTimeAux    - bool       - 是否長時間離開?
		/// displayOrder     - int        - 顯示順序
        /// </param>
        /// <returns>
        /// </returns>
        [Authorize]
        [HttpPut]
        public async ValueTask<IActionResult> UpdateAuxCode(AuxCodeUpdateRequest request)
        {
            return Ok(await _AuxCodeService.UpdateAuxCode(request));
        }

        /// <summary>
        /// 刪除休息碼設定
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
        /// </param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        public async ValueTask<IActionResult> DeteleAuxCode(int seqNo)
        {
            return Ok(await _AuxCodeService.DeleteAuxCode(seqNo));
        }

    }
}
