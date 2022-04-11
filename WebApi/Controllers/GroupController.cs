using ESUN.AGD.WebApi.Application.Group;
using ESUN.AGD.WebApi.Application.Group.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESUN.AGD.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : Controller
    {

        private readonly IGroupService _GroupService;

        public GroupController(IGroupService GroupService)
        {
            _GroupService = GroupService;
        }

        /// <summary>
        /// 依序號取得群組設定
        /// </summary>
        /// <param>
		/// groupID          - string     - 群組代碼
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// groupID          - string     - 群組代碼
		/// groupName        - string     - 群組名稱
		/// createDT         - DateTime   - 建立時間
		/// creator          - string     - 建立者
		/// updateDT         - DateTime   - 更新時間
		/// updator          - string     - 更新者
        /// updatorName      - string     - 更新者名稱
        /// </returns>
        [Authorize]
        [HttpGet("{seqNo}")]
        public async ValueTask<IActionResult> GetGroup(string groupID)
        {
            return Ok(await _GroupService.GetGroup(groupID));
        }

        /// <summary>
        /// 搜尋群組設定 
        /// </summary>
        /// <param name="request">
		/// groupID          - string     - 群組代碼
		/// groupName        - string     - 群組名稱
        /// page             - int        - 分頁
        /// rowsPerPage      - int        - 每頁筆數
        /// sortColumn       - string     - 排序欄位
        /// sortOrder        - bool       - 排序順序
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// groupID          - string     - 群組代碼
		/// groupName        - string     - 群組名稱
		/// createDT         - DateTime   - 建立時間
		/// creator          - string     - 建立者
		/// updateDT         - DateTime   - 更新時間
		/// updator          - string     - 更新者
        /// updatorName      - string     - 更新者名稱
        /// </returns>        
        [Authorize]
        [HttpGet("query")]
        public async ValueTask<IActionResult> QueryGroup([FromQuery] GroupQueryRequest request)
        {
            return Ok(await _GroupService.QueryGroup(request));
        }

        /// <summary>
        /// 新增群組設定 
        /// </summary>
        /// <param name="request">
		/// groupID          - string     - 群組代碼
		/// groupName        - string     - 群組名稱
        /// </param>
        /// <returns>
        /// </returns>        
        [Authorize]
        [HttpPost]
        public async ValueTask<IActionResult> InsertGroup(GroupInsertRequest request)
        {
            return Ok(await _GroupService.InsertGroup(request));
        }

        /// <summary>
        /// 更新群組設定
        /// </summary>
        /// <param name="request">
		/// seqNo            - int        - 流水號
		/// groupID          - string     - 群組代碼
		/// groupName        - string     - 群組名稱
        /// </param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        public async ValueTask<IActionResult> UpdateGroup(GroupUpdateRequest request)
        {
            return Ok(await _GroupService.UpdateGroup(request));
        }

        /// <summary>
        /// 刪除群組設定
        /// </summary>
        /// <param>
		/// groupID          - string     - 群組代碼
        /// </param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        public async ValueTask<IActionResult> DeteleGroup(string groupID)
        {
            return Ok(await _GroupService.DeleteGroup(groupID));
        }

    }
}
