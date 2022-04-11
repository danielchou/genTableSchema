using ESUN.AGD.WebApi.Application.Func;
using ESUN.AGD.WebApi.Application.Func.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESUN.AGD.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncController : Controller
    {

        private readonly IFuncService _FuncService;

        public FuncController(IFuncService FuncService)
        {
            _FuncService = FuncService;
        }

        /// <summary>
        /// 依序號取得功能設定
        /// </summary>
        /// <param>
		/// funcID           - string     - 功能代碼
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// funcID           - string     - 功能代碼
		/// funcName         - string     - 功能名稱
		/// parentFuncID     - string     - 上層功能代碼
		/// level            -            - 階層
		/// systemType       - string     - 系統類別
		/// routeName        - string     - 路由名稱
		/// displayOrder     - int        - 顯示順序
		/// createDT         - DateTime   - 建立時間
		/// creator          - string     - 建立者
		/// updateDT         - DateTime   - 更新時間
		/// updator          - string     - 更新者
        /// updatorName      - string     - 更新者名稱
        /// </returns>
        [Authorize]
        [HttpGet("{seqNo}")]
        public async ValueTask<IActionResult> GetFunc(string funcID)
        {
            return Ok(await _FuncService.GetFunc(funcID));
        }

        /// <summary>
        /// 搜尋功能設定 
        /// </summary>
        /// <param name="request">
		/// funcID           - string     - 功能代碼
		/// funcName         - string     - 功能名稱
		/// parentFuncID     - string     - 上層功能代碼
		/// level            -            - 階層
		/// systemType       - string     - 系統類別
		/// routeName        - string     - 路由名稱
		/// displayOrder     - int        - 顯示順序
        /// page             - int        - 分頁
        /// rowsPerPage      - int        - 每頁筆數
        /// sortColumn       - string     - 排序欄位
        /// sortOrder        - bool       - 排序順序
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// funcID           - string     - 功能代碼
		/// funcName         - string     - 功能名稱
		/// parentFuncID     - string     - 上層功能代碼
		/// level            -            - 階層
		/// systemType       - string     - 系統類別
		/// routeName        - string     - 路由名稱
		/// displayOrder     - int        - 顯示順序
		/// createDT         - DateTime   - 建立時間
		/// creator          - string     - 建立者
		/// updateDT         - DateTime   - 更新時間
		/// updator          - string     - 更新者
        /// updatorName      - string     - 更新者名稱
        /// </returns>        
        [Authorize]
        [HttpGet("query")]
        public async ValueTask<IActionResult> QueryFunc([FromQuery] FuncQueryRequest request)
        {
            return Ok(await _FuncService.QueryFunc(request));
        }

        /// <summary>
        /// 新增功能設定 
        /// </summary>
        /// <param name="request">
		/// funcID           - string     - 功能代碼
		/// funcName         - string     - 功能名稱
		/// parentFuncID     - string     - 上層功能代碼
		/// level            -            - 階層
		/// systemType       - string     - 系統類別
		/// routeName        - string     - 路由名稱
		/// displayOrder     - int        - 顯示順序
        /// </param>
        /// <returns>
        /// </returns>        
        [Authorize]
        [HttpPost]
        public async ValueTask<IActionResult> InsertFunc(FuncInsertRequest request)
        {
            return Ok(await _FuncService.InsertFunc(request));
        }

        /// <summary>
        /// 更新功能設定
        /// </summary>
        /// <param name="request">
		/// seqNo            - int        - 流水號
		/// funcID           - string     - 功能代碼
		/// funcName         - string     - 功能名稱
		/// parentFuncID     - string     - 上層功能代碼
		/// level            -            - 階層
		/// systemType       - string     - 系統類別
		/// routeName        - string     - 路由名稱
		/// displayOrder     - int        - 顯示順序
        /// </param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        public async ValueTask<IActionResult> UpdateFunc(FuncUpdateRequest request)
        {
            return Ok(await _FuncService.UpdateFunc(request));
        }

        /// <summary>
        /// 刪除功能設定
        /// </summary>
        /// <param>
		/// funcID           - string     - 功能代碼
        /// </param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        public async ValueTask<IActionResult> DeteleFunc(string funcID)
        {
            return Ok(await _FuncService.DeleteFunc(funcID));
        }

    }
}
