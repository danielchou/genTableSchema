using ESUN.AGD.WebApi.Application.PcPhone;
using ESUN.AGD.WebApi.Application.PcPhone.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESUN.AGD.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PcPhoneController : Controller
    {

        private readonly IPcPhoneService _PcPhoneService;

        public PcPhoneController(IPcPhoneService PcPhoneService)
        {
            _PcPhoneService = PcPhoneService;
        }

        /// <summary>
        /// 依序號取得電腦電話配對設定
        /// </summary>
        /// <param>
		/// computerIP       - string     - 電腦IP
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// computerIP       - string     - 電腦IP
		/// computerName     - string     - 電腦名稱
		/// extCode          - string     - 分機號碼
		/// memo             - string     - 備註
		/// createDT         - DateTime   - 建立時間
		/// creator          - string     - 建立者
		/// updateDT         - DateTime   - 更新時間
		/// updator          - string     - 更新者
        /// updatorName      - string     - 更新者名稱
        /// </returns>
        [Authorize]
        [HttpGet("{seqNo}")]
        public async ValueTask<IActionResult> GetPcPhone(string computerIP)
        {
            return Ok(await _PcPhoneService.GetPcPhone(computerIP));
        }

        /// <summary>
        /// 搜尋電腦電話配對設定 
        /// </summary>
        /// <param name="request">
		/// computerIP       - string     - 電腦IP
		/// computerName     - string     - 電腦名稱
		/// extCode          - string     - 分機號碼
		/// memo             - string     - 備註
        /// page             - int        - 分頁
        /// rowsPerPage      - int        - 每頁筆數
        /// sortColumn       - string     - 排序欄位
        /// sortOrder        - bool       - 排序順序
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// computerIP       - string     - 電腦IP
		/// computerName     - string     - 電腦名稱
		/// extCode          - string     - 分機號碼
		/// memo             - string     - 備註
		/// createDT         - DateTime   - 建立時間
		/// creator          - string     - 建立者
		/// updateDT         - DateTime   - 更新時間
		/// updator          - string     - 更新者
        /// updatorName      - string     - 更新者名稱
        /// </returns>        
        [Authorize]
        [HttpGet("query")]
        public async ValueTask<IActionResult> QueryPcPhone([FromQuery] PcPhoneQueryRequest request)
        {
            return Ok(await _PcPhoneService.QueryPcPhone(request));
        }

        /// <summary>
        /// 新增電腦電話配對設定 
        /// </summary>
        /// <param name="request">
		/// computerIP       - string     - 電腦IP
		/// computerName     - string     - 電腦名稱
		/// extCode          - string     - 分機號碼
		/// memo             - string     - 備註
        /// </param>
        /// <returns>
        /// </returns>        
        [Authorize]
        [HttpPost]
        public async ValueTask<IActionResult> InsertPcPhone(PcPhoneInsertRequest request)
        {
            return Ok(await _PcPhoneService.InsertPcPhone(request));
        }

        /// <summary>
        /// 更新電腦電話配對設定
        /// </summary>
        /// <param name="request">
		/// seqNo            - int        - 流水號
		/// computerIP       - string     - 電腦IP
		/// computerName     - string     - 電腦名稱
		/// extCode          - string     - 分機號碼
		/// memo             - string     - 備註
        /// </param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        public async ValueTask<IActionResult> UpdatePcPhone(PcPhoneUpdateRequest request)
        {
            return Ok(await _PcPhoneService.UpdatePcPhone(request));
        }

        /// <summary>
        /// 刪除電腦電話配對設定
        /// </summary>
        /// <param>
		/// computerIP       - string     - 電腦IP
        /// </param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        public async ValueTask<IActionResult> DetelePcPhone(string computerIP)
        {
            return Ok(await _PcPhoneService.DeletePcPhone(computerIP));
        }

    }
}
