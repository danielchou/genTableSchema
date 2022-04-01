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
        /// 依序號取得系統代碼設定
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// codeType         - string     - 代碼分類
		/// codeId           - string     - 系統代碼檔代碼
		/// codeName         - string     - 系統代碼檔名稱
		/// isEnable         - bool       - 是否啟用?
		/// creator          - string     - 建立者
		/// updator          - string     - 異動者
		/// createDt         - DateTime   - 建立時間
		/// updateDt         - DateTime   - 異動時間
        /// updatorName - 更新者名稱
        /// </returns>
        [Authorize]
        [HttpGet("{seqNo}")]
        public async ValueTask<IActionResult> GetCode(int seqNo)
        {
            return Ok(await _CodeService.GetCode(SeqNo));
        }

        /// <summary>
        /// 搜尋系統代碼設定 
        /// </summary>
        /// <param name="request">
		/// codeType         - string     - 代碼分類
		/// codeId           - string     - 系統代碼檔代碼
		/// codeName         - string     - 系統代碼檔名稱
		/// isEnable         - bool       - 是否啟用?
        /// page             - int        - 分頁
        /// rowsPerPage      - int        - 每頁筆數
        /// sortColumn       - string     - 排序欄位
        /// sortOrder        - bool       - 排序順序
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// codeType         - string     - 代碼分類
		/// codeId           - string     - 系統代碼檔代碼
		/// codeName         - string     - 系統代碼檔名稱
		/// isEnable         - bool       - 是否啟用?
		/// creator          - string     - 建立者
		/// updator          - string     - 異動者
		/// createDt         - DateTime   - 建立時間
		/// updateDt         - DateTime   - 異動時間
        /// updatorName - 更新者名稱
        /// </returns>        
        [Authorize]
        [HttpGet("query")]
        public async ValueTask<IActionResult> QueryCode([FromQuery] CodeQueryRequest request)
        {
            return Ok(await _CodeService.QueryCode(request));
        }

        /// <summary>
        /// 新增系統代碼設定 
        /// </summary>
        /// <param name="request">
		/// codeType         - string     - 代碼分類
		/// codeId           - string     - 系統代碼檔代碼
		/// codeName         - string     - 系統代碼檔名稱
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
        /// 更新系統代碼設定
        /// </summary>
        /// <param name="request">
		/// seqNo            - int        - 流水號
		/// codeType         - string     - 代碼分類
		/// codeId           - string     - 系統代碼檔代碼
		/// codeName         - string     - 系統代碼檔名稱
		/// isEnable         - bool       - 是否啟用?
		/// updator          - string     - 異動者
        /// </param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        public async ValueTask<IActionResult> UpdateCode(CodeUpdateRequest request)
        {
            return Ok(await _CodeService.UpdateCode(request));
        }

        /// <summary>
        /// 刪除系統代碼設定
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
        /// </param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        public async ValueTask<IActionResult> DeteleCode(int seqNo)
        {
            return Ok(await _CodeService.DeleteCode(SeqNo));
        }

    }
}
