using ESUN.AGD.WebApi.Application.SysConfig;
using ESUN.AGD.WebApi.Application.SysConfig.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESUN.AGD.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SysConfigController : Controller
    {

        private readonly ISysConfigService _SysConfigService;

        public SysConfigController(ISysConfigService SysConfigService)
        {
            _SysConfigService = SysConfigService;
        }

        /// <summary>
        /// 依序號取得系統參數設定
        /// </summary>
        /// <param>
        /// seqNo           - int     - 流水號
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// sysConfigType    - string     - 系統參數類別
		/// sysConfigID      - string     - 系統參數代碼
		/// sysConfigName    - string     - 系統參數名稱
		/// content          - string     - 系統參數內容
		/// isVisible        - bool       - 是否顯示?
		/// createDT         - DateTime   - 建立時間
		/// creator          - string     - 建立者
		/// updateDT         - DateTime   - 更新時間
		/// updator          - string     - 更新者
        /// updatorName      - string     - 更新者名稱
        /// </returns>
        [Authorize]
        [HttpGet("{seqNo}")]
        public async ValueTask<IActionResult> GetSysConfig(int seqNo)
        {
            return Ok(await _SysConfigService.GetSysConfig(seqNo));
        }

        /// <summary>
        /// 搜尋系統參數設定 
        /// </summary>
        /// <param name="request">
		/// sysConfigType    - string     - 系統參數類別
		/// sysConfigID      - string     - 系統參數代碼
		/// sysConfigName    - string     - 系統參數名稱
        /// page             - int        - 分頁
        /// rowsPerPage      - int        - 每頁筆數
        /// sortColumn       - string     - 排序欄位
        /// sortOrder        - bool       - 排序順序
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// sysConfigType    - string     - 系統參數類別
		/// sysConfigID      - string     - 系統參數代碼
		/// sysConfigName    - string     - 系統參數名稱
		/// content          - string     - 系統參數內容
		/// isVisible        - bool       - 是否顯示?
		/// createDT         - DateTime   - 建立時間
		/// creator          - string     - 建立者
		/// updateDT         - DateTime   - 更新時間
		/// updator          - string     - 更新者
        /// updatorName      - string     - 更新者名稱
        /// </returns>        
        [Authorize]
        [HttpGet("query")]
        public async ValueTask<IActionResult> QuerySysConfig([FromQuery] SysConfigQueryRequest request)
        {
            return Ok(await _SysConfigService.QuerySysConfig(request));
        }

        /// <summary>
        /// 新增系統參數設定 
        /// </summary>
        /// <param name="request">
		/// sysConfigType    - string     - 系統參數類別
		/// sysConfigID      - string     - 系統參數代碼
		/// sysConfigName    - string     - 系統參數名稱
		/// content          - string     - 系統參數內容
		/// isVisible        - bool       - 是否顯示?
        /// </param>
        /// <returns>
        /// </returns>        
        [Authorize]
        [HttpPost]
        public async ValueTask<IActionResult> InsertSysConfig(SysConfigInsertRequest request)
        {
            return Ok(await _SysConfigService.InsertSysConfig(request));
        }

        /// <summary>
        /// 更新系統參數設定
        /// </summary>
        /// <param name="request">
		/// seqNo            - int        - 流水號
		/// sysConfigType    - string     - 系統參數類別
		/// sysConfigID      - string     - 系統參數代碼
		/// sysConfigName    - string     - 系統參數名稱
		/// content          - string     - 系統參數內容
		/// isVisible        - bool       - 是否顯示?
        /// </param>
        /// <returns>
        /// </returns>
        [Authorize]
        [HttpPut]
        public async ValueTask<IActionResult> UpdateSysConfig(SysConfigUpdateRequest request)
        {
            return Ok(await _SysConfigService.UpdateSysConfig(request));
        }

        /// <summary>
        /// 刪除系統參數設定
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
        /// </param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        public async ValueTask<IActionResult> DeteleSysConfig(int seqNo)
        {
            return Ok(await _SysConfigService.DeleteSysConfig(seqNo));
        }

    }
}
