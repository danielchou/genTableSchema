using ESUN.AGD.WebApi.Application.RoleFunc;
using ESUN.AGD.WebApi.Application.RoleFunc.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESUN.AGD.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleFuncController : Controller
    {

        private readonly IRoleFuncService _RoleFuncService;

        public RoleFuncController(IRoleFuncService RoleFuncService)
        {
            _RoleFuncService = RoleFuncService;
        }

        /// <summary>
        /// 依序號取得角色功能配對設定
        /// </summary>
        /// <param>
		/// roleID           - string     - 角色代碼
		/// funcID           - string     - 功能代碼
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// roleID           - string     - 角色代碼
		/// funcID           - string     - 功能代碼
		/// createDT         - DateTime   - 建立時間
		/// creator          - string     - 建立者
		/// updateDT         - DateTime   - 更新時間
		/// updator          - string     - 更新者
        /// updatorName      - string     - 更新者名稱
        /// </returns>
        [Authorize]
        [HttpGet("{seqNo}")]
        public async ValueTask<IActionResult> GetRoleFunc(string roleID,string funcID)
        {
            return Ok(await _RoleFuncService.GetRoleFunc(roleID,funcID));
        }

        /// <summary>
        /// 搜尋角色功能配對設定 
        /// </summary>
        /// <param name="request">
		/// roleID           - string     - 角色代碼
		/// funcID           - string     - 功能代碼
        /// page             - int        - 分頁
        /// rowsPerPage      - int        - 每頁筆數
        /// sortColumn       - string     - 排序欄位
        /// sortOrder        - bool       - 排序順序
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// roleID           - string     - 角色代碼
		/// funcID           - string     - 功能代碼
		/// createDT         - DateTime   - 建立時間
		/// creator          - string     - 建立者
		/// updateDT         - DateTime   - 更新時間
		/// updator          - string     - 更新者
        /// updatorName      - string     - 更新者名稱
        /// </returns>        
        [Authorize]
        [HttpGet("query")]
        public async ValueTask<IActionResult> QueryRoleFunc([FromQuery] RoleFuncQueryRequest request)
        {
            return Ok(await _RoleFuncService.QueryRoleFunc(request));
        }

        /// <summary>
        /// 新增角色功能配對設定 
        /// </summary>
        /// <param name="request">
		/// roleID           - string     - 角色代碼
		/// funcID           - string     - 功能代碼
        /// </param>
        /// <returns>
        /// </returns>        
        [Authorize]
        [HttpPost]
        public async ValueTask<IActionResult> InsertRoleFunc(RoleFuncInsertRequest request)
        {
            return Ok(await _RoleFuncService.InsertRoleFunc(request));
        }

        /// <summary>
        /// 更新角色功能配對設定
        /// </summary>
        /// <param name="request">
		/// seqNo            - int        - 流水號
		/// roleID           - string     - 角色代碼
		/// funcID           - string     - 功能代碼
        /// </param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        public async ValueTask<IActionResult> UpdateRoleFunc(RoleFuncUpdateRequest request)
        {
            return Ok(await _RoleFuncService.UpdateRoleFunc(request));
        }

        /// <summary>
        /// 刪除角色功能配對設定
        /// </summary>
        /// <param>
		/// roleID           - string     - 角色代碼
		/// funcID           - string     - 功能代碼
        /// </param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        public async ValueTask<IActionResult> DeteleRoleFunc(string roleID,string funcID)
        {
            return Ok(await _RoleFuncService.DeleteRoleFunc(roleID,funcID));
        }

    }
}
