using ESUN.AGD.WebApi.Application.Role;
using ESUN.AGD.WebApi.Application.Role.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESUN.AGD.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : Controller
    {

        private readonly IRoleService _RoleService;

        public RoleController(IRoleService RoleService)
        {
            _RoleService = RoleService;
        }

        /// <summary>
        /// 依序號取得角色設定
        /// </summary>
        /// <param>
        /// seqNo           - int     - 流水號
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// roleID           - string     - 角色代碼
		/// roleName         - string     - 角色名稱
		/// createDT         - DateTime   - 建立時間
		/// creator          - string     - 建立者
		/// updateDT         - DateTime   - 更新時間
		/// updator          - string     - 更新者
        /// updatorName      - string     - 更新者名稱
        /// </returns>
        [Authorize]
        [HttpGet("{seqNo}")]
        public async ValueTask<IActionResult> GetRole(int seqNo)
        {
            return Ok(await _RoleService.GetRole(seqNo));
        }

        /// <summary>
        /// 搜尋角色設定 
        /// </summary>
        /// <param name="request">
		/// roleID           - string     - 角色代碼
		/// roleName         - string     - 角色名稱
        /// page             - int        - 分頁
        /// rowsPerPage      - int        - 每頁筆數
        /// sortColumn       - string     - 排序欄位
        /// sortOrder        - bool       - 排序順序
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// roleID           - string     - 角色代碼
		/// roleName         - string     - 角色名稱
		/// createDT         - DateTime   - 建立時間
		/// creator          - string     - 建立者
		/// updateDT         - DateTime   - 更新時間
		/// updator          - string     - 更新者
        /// updatorName      - string     - 更新者名稱
        /// </returns>        
        [Authorize]
        [HttpGet("query")]
        public async ValueTask<IActionResult> QueryRole([FromQuery] RoleQueryRequest request)
        {
            return Ok(await _RoleService.QueryRole(request));
        }

        /// <summary>
        /// 新增角色設定 
        /// </summary>
        /// <param name="request">
		/// roleID           - string     - 角色代碼
		/// roleName         - string     - 角色名稱
        /// </param>
        /// <returns>
        /// </returns>        
        [Authorize]
        [HttpPost]
        public async ValueTask<IActionResult> InsertRole(RoleInsertRequest request)
        {
            return Ok(await _RoleService.InsertRole(request));
        }

        /// <summary>
        /// 更新角色設定
        /// </summary>
        /// <param name="request">
		/// seqNo            - int        - 流水號
		/// roleID           - string     - 角色代碼
		/// roleName         - string     - 角色名稱
        /// </param>
        /// <returns>
        /// </returns>
        [Authorize]
        [HttpPut]
        public async ValueTask<IActionResult> UpdateRole(RoleUpdateRequest request)
        {
            return Ok(await _RoleService.UpdateRole(request));
        }

        /// <summary>
        /// 刪除角色設定
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
        /// </param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        public async ValueTask<IActionResult> DeteleRole(int seqNo)
        {
            return Ok(await _RoleService.DeleteRole(seqNo));
        }

    }
}
