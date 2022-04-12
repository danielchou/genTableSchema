using ESUN.AGD.WebApi.Application.UserRole;
using ESUN.AGD.WebApi.Application.UserRole.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESUN.AGD.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : Controller
    {

        private readonly IUserRoleService _UserRoleService;

        public UserRoleController(IUserRoleService UserRoleService)
        {
            _UserRoleService = UserRoleService;
        }

        /// <summary>
        /// 依序號取得使用者角色配對設定
        /// </summary>
        /// <param>
        /// seqNo           - int     - 流水號
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// userID           - string     - 使用者帳號
		/// roleID           - string     - 角色代碼
		/// createDT         - DateTime   - 建立時間
		/// creator          - string     - 建立者
		/// updateDT         - DateTime   - 更新時間
		/// updator          - string     - 更新者
        /// updatorName      - string     - 更新者名稱
        /// </returns>
        [Authorize]
        [HttpGet("{seqNo}")]
        public async ValueTask<IActionResult> GetUserRole(int seqNo)
        {
            return Ok(await _UserRoleService.GetUserRole(seqNo));
        }

        /// <summary>
        /// 搜尋使用者角色配對設定 
        /// </summary>
        /// <param name="request">
		/// userID           - string     - 使用者帳號
		/// roleID           - string     - 角色代碼
        /// page             - int        - 分頁
        /// rowsPerPage      - int        - 每頁筆數
        /// sortColumn       - string     - 排序欄位
        /// sortOrder        - bool       - 排序順序
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// userID           - string     - 使用者帳號
		/// roleID           - string     - 角色代碼
		/// createDT         - DateTime   - 建立時間
		/// creator          - string     - 建立者
		/// updateDT         - DateTime   - 更新時間
		/// updator          - string     - 更新者
        /// updatorName      - string     - 更新者名稱
        /// </returns>        
        [Authorize]
        [HttpGet("query")]
        public async ValueTask<IActionResult> QueryUserRole([FromQuery] UserRoleQueryRequest request)
        {
            return Ok(await _UserRoleService.QueryUserRole(request));
        }

        /// <summary>
        /// 新增使用者角色配對設定 
        /// </summary>
        /// <param name="request">
		/// userID           - string     - 使用者帳號
		/// roleID           - string     - 角色代碼
        /// </param>
        /// <returns>
        /// </returns>        
        [Authorize]
        [HttpPost]
        public async ValueTask<IActionResult> InsertUserRole(UserRoleInsertRequest request)
        {
            return Ok(await _UserRoleService.InsertUserRole(request));
        }

        /// <summary>
        /// 更新使用者角色配對設定
        /// </summary>
        /// <param name="request">
		/// seqNo            - int        - 流水號
		/// userID           - string     - 使用者帳號
		/// roleID           - string     - 角色代碼
        /// </param>
        /// <returns>
        /// </returns>
        [Authorize]
        [HttpPut]
        public async ValueTask<IActionResult> UpdateUserRole(UserRoleUpdateRequest request)
        {
            return Ok(await _UserRoleService.UpdateUserRole(request));
        }

        /// <summary>
        /// 刪除使用者角色配對設定
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
        /// </param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        public async ValueTask<IActionResult> DeteleUserRole(int seqNo)
        {
            return Ok(await _UserRoleService.DeleteUserRole(seqNo));
        }

    }
}
