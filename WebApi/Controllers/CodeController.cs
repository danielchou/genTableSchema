using ESUN.AGD.WebApi.Application.Code;
using ESUN.AGD.WebApi.Application.Code.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESUN.AGD.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CodeController : Controller
    {

        private readonly ICodeService _CodeService;

        public CodeController(ICodeService CodeService)
        {
            _CodeService = CodeService;
        }

        /// <summary>
        /// 依序號取得共用碼設定
        /// </summary>
        /// <param>
        /// seqNo           - int     - 流水號
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// codeType         - string     - 共用碼類別
		/// codeID           - string     - 共用碼代碼
		/// codeName         - string     - 共用碼名稱
		/// content          - string     - 共用碼內容
		/// memo             - string     - 備註
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
        public async ValueTask<IActionResult> GetCode(int seqNo)
        {
            return Ok(await _CodeService.GetCode(seqNo));
        }

        /// <summary>
        /// 搜尋共用碼設定 
        /// </summary>
        /// <param name="request">
		/// codeType         - string     - 共用碼類別
		/// codeID           - string     - 共用碼代碼
		/// codeName         - string     - 共用碼名稱
        /// page             - int        - 分頁
        /// rowsPerPage      - int        - 每頁筆數
        /// sortColumn       - string     - 排序欄位
        /// sortOrder        - bool       - 排序順序
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// codeType         - string     - 共用碼類別
		/// codeID           - string     - 共用碼代碼
		/// codeName         - string     - 共用碼名稱
		/// content          - string     - 共用碼內容
		/// memo             - string     - 備註
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
        public async ValueTask<IActionResult> QueryCode([FromQuery] CodeQueryRequest request)
        {
            return Ok(await _CodeService.QueryCode(request));
        }

        /// <summary>
        /// 新增共用碼設定 
        /// </summary>
        /// <param name="request">
		/// codeType         - string     - 共用碼類別
		/// codeID           - string     - 共用碼代碼
		/// codeName         - string     - 共用碼名稱
		/// content          - string     - 共用碼內容
		/// memo             - string     - 備註
		/// displayOrder     - int        - 顯示順序
		/// isEnable         - bool       - 是否啟用?
        /// </param>
        /// <returns>
        /// </returns>        
        [Authorize]
        [HttpPost]
        public async ValueTask<IActionResult> InsertCode(CodeInsertRequest request)
        {
            return Ok(await _CodeService.InsertCode(request));
        }

        /// <summary>
        /// 更新共用碼設定
        /// </summary>
        /// <param name="request">
		/// seqNo            - int        - 流水號
		/// codeType         - string     - 共用碼類別
		/// codeID           - string     - 共用碼代碼
		/// codeName         - string     - 共用碼名稱
		/// content          - string     - 共用碼內容
		/// memo             - string     - 備註
		/// displayOrder     - int        - 顯示順序
		/// isEnable         - bool       - 是否啟用?
        /// </param>
        /// <returns>
        /// </returns>
        [Authorize]
        [HttpPut]
        public async ValueTask<IActionResult> UpdateCode(CodeUpdateRequest request)
        {
            return Ok(await _CodeService.UpdateCode(request));
        }

        /// <summary>
        /// 刪除共用碼設定
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
        /// </param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        public async ValueTask<IActionResult> DeteleCode(int seqNo)
        {
            return Ok(await _CodeService.DeleteCode(seqNo));
        }

    }
}
