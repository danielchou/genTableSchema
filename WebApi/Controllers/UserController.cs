using ESUN.AGD.WebApi.Application.User;
using ESUN.AGD.WebApi.Application.User.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESUN.AGD.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {

        private readonly IUserService _UserService;

        public UserController(IUserService UserService)
        {
            _UserService = UserService;
        }

        /// <summary>
        /// 依序號取得使用者設定
        /// </summary>
        /// <param>
        /// seqNo           - int     - 流水號
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// userID           - string     - 使用者帳號
		/// userName         - string     - 使用者名稱
		/// userCode         - string     - 使用者代碼
		/// agentLoginID     - string     - CTI登入帳號
		/// agentLoginCode   - string     - CTI登入代碼
		/// employeeNo       - string     - 員工編號
		/// nickName         - string     - 使用者暱稱
		/// empDept          - string     - 所屬單位
		/// groupID          - string     - 群組代碼
		/// officeEmail      - string     - 公司Email
		/// employedStatusCode - string     - 在職狀態代碼
		/// isSupervisor     - bool       - 是否為主管?
		/// b08Code1         - string     - B08_code1
		/// b08Code2         - string     - B08_code2
		/// b08Code3         - string     - B08_code3
		/// b08Code4         - string     - B08_code4
		/// b08Code5         - string     - B08_code5
		/// isEnable         - bool       - 是否啟用?
		/// createDT         - DateTime   - 建立時間
		/// creator          - string     - 建立者
		/// updateDT         - DateTime   - 更新時間
		/// updator          - string     - 更新者
        /// updatorName      - string     - 更新者名稱
        /// </returns>
        [Authorize]
        [HttpGet("{seqNo}")]
        public async ValueTask<IActionResult> GetUser(int seqNo)
        {
            return Ok(await _UserService.GetUser(seqNo));
        }

        /// <summary>
        /// 搜尋使用者設定 
        /// </summary>
        /// <param name="request">
		/// userID           - string     - 使用者帳號
		/// userName         - string     - 使用者名稱
		/// agentLoginID     - string     - CTI登入帳號
		/// groupID          - string     - 群組代碼
		/// isEnable         - string     - 是否啟用?
        /// page             - int        - 分頁
        /// rowsPerPage      - int        - 每頁筆數
        /// sortColumn       - string     - 排序欄位
        /// sortOrder        - bool       - 排序順序
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// userID           - string     - 使用者帳號
		/// userName         - string     - 使用者名稱
		/// userCode         - string     - 使用者代碼
		/// agentLoginID     - string     - CTI登入帳號
		/// agentLoginCode   - string     - CTI登入代碼
		/// employeeNo       - string     - 員工編號
		/// nickName         - string     - 使用者暱稱
		/// empDept          - string     - 所屬單位
		/// groupID          - string     - 群組代碼
		/// officeEmail      - string     - 公司Email
		/// employedStatusCode - string     - 在職狀態代碼
		/// isSupervisor     - bool       - 是否為主管?
		/// b08Code1         - string     - B08_code1
		/// b08Code2         - string     - B08_code2
		/// b08Code3         - string     - B08_code3
		/// b08Code4         - string     - B08_code4
		/// b08Code5         - string     - B08_code5
		/// isEnable         - bool       - 是否啟用?
		/// createDT         - DateTime   - 建立時間
		/// creator          - string     - 建立者
		/// updateDT         - DateTime   - 更新時間
		/// updator          - string     - 更新者
        /// updatorName      - string     - 更新者名稱
        /// </returns>        
        [Authorize]
        [HttpGet("query")]
        public async ValueTask<IActionResult> QueryUser([FromQuery] UserQueryRequest request)
        {
            return Ok(await _UserService.QueryUser(request));
        }

        /// <summary>
        /// 新增使用者設定 
        /// </summary>
        /// <param name="request">
		/// userID           - string     - 使用者帳號
		/// userName         - string     - 使用者名稱
		/// userCode         - string     - 使用者代碼
		/// agentLoginID     - string     - CTI登入帳號
		/// agentLoginCode   - string     - CTI登入代碼
		/// employeeNo       - string     - 員工編號
		/// nickName         - string     - 使用者暱稱
		/// empDept          - string     - 所屬單位
		/// groupID          - string     - 群組代碼
		/// officeEmail      - string     - 公司Email
		/// employedStatusCode - string     - 在職狀態代碼
		/// isSupervisor     - bool       - 是否為主管?
		/// b08Code1         - string     - B08_code1
		/// b08Code2         - string     - B08_code2
		/// b08Code3         - string     - B08_code3
		/// b08Code4         - string     - B08_code4
		/// b08Code5         - string     - B08_code5
		/// isEnable         - bool       - 是否啟用?
        /// </param>
        /// <returns>
        /// </returns>        
        [Authorize]
        [HttpPost]
        public async ValueTask<IActionResult> InsertUser(UserInsertRequest request)
        {
            return Ok(await _UserService.InsertUser(request));
        }

        /// <summary>
        /// 更新使用者設定
        /// </summary>
        /// <param name="request">
		/// seqNo            - int        - 流水號
		/// userID           - string     - 使用者帳號
		/// userName         - string     - 使用者名稱
		/// userCode         - string     - 使用者代碼
		/// agentLoginID     - string     - CTI登入帳號
		/// agentLoginCode   - string     - CTI登入代碼
		/// employeeNo       - string     - 員工編號
		/// nickName         - string     - 使用者暱稱
		/// empDept          - string     - 所屬單位
		/// groupID          - string     - 群組代碼
		/// officeEmail      - string     - 公司Email
		/// employedStatusCode - string     - 在職狀態代碼
		/// isSupervisor     - bool       - 是否為主管?
		/// b08Code1         - string     - B08_code1
		/// b08Code2         - string     - B08_code2
		/// b08Code3         - string     - B08_code3
		/// b08Code4         - string     - B08_code4
		/// b08Code5         - string     - B08_code5
		/// isEnable         - bool       - 是否啟用?
        /// </param>
        /// <returns>
        /// </returns>
        [Authorize]
        [HttpPut]
        public async ValueTask<IActionResult> UpdateUser(UserUpdateRequest request)
        {
            return Ok(await _UserService.UpdateUser(request));
        }

        /// <summary>
        /// 刪除使用者設定
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
        /// </param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        public async ValueTask<IActionResult> DeteleUser(int seqNo)
        {
            return Ok(await _UserService.DeleteUser(seqNo));
        }

    }
}
