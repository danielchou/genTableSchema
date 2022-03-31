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
        /// 依序號取得電腦電話設定
        /// </summary>
        /// <param>
		/// SeqNo            - int        - Seq No.
        /// </param>
        /// <returns>
		/// SeqNo            - int        - Seq No.
		/// ComputerName     - string     - 電腦名稱
		/// ComputerIp       - string     - IP 位址
		/// ExtCode          - string     - 電話分機
		/// Memo             - string     - 備註
		/// IsEnable         - bool       - 是否啟用?
		/// Creator          - string     - 建立者
		/// Updator          - string     - 更新者
		/// CreateDt         - DateTime   - 建立時間
		/// UpdateDt         - DateTime   - 異動時間
        /// updatorName - 更新者名稱
        /// </returns>
        [Authorize]
        [HttpGet("{seqNo}")]
        public async ValueTask<IActionResult> GetPcPhone(int SeqNo)
        {
            return Ok(await _PcPhoneService.GetPcPhone(SeqNo));
        }

        /// <summary>
        /// 搜尋電腦電話設定 
        /// </summary>
        /// <param name="request">
		/// ComputerName     - string     - 電腦名稱
		/// ComputerIp       - string     - IP 位址
		/// ExtCode          - string     - 電話分機
		/// Memo             - string     - 備註
		/// IsEnable         - bool       - 是否啟用?
        /// page             - int        - 分頁
        /// rowsPerPage      - int        - 每頁筆數
        /// sortColumn       - string     - 排序欄位
        /// sortOrder        - bool       - 排序順序
        /// </param>
        /// <returns>
		/// SeqNo            - int        - Seq No.
		/// ComputerName     - string     - 電腦名稱
		/// ComputerIp       - string     - IP 位址
		/// ExtCode          - string     - 電話分機
		/// Memo             - string     - 備註
		/// IsEnable         - bool       - 是否啟用?
		/// Creator          - string     - 建立者
		/// Updator          - string     - 更新者
		/// CreateDt         - DateTime   - 建立時間
		/// UpdateDt         - DateTime   - 異動時間
        /// updatorName - 更新者名稱
        /// </returns>        
        [Authorize]
        [HttpGet("query")]
        public async ValueTask<IActionResult> QueryPcPhone([FromQuery] PcPhoneQueryRequest request)
        {
            return Ok(await _PcPhoneService.QueryPcPhone(request));
        }

        /// <summary>
        /// 新增電腦電話設定 
        /// </summary>
        /// <param name="request">
		/// ComputerName     - string     - 電腦名稱
		/// ComputerIp       - string     - IP 位址
		/// ExtCode          - string     - 電話分機
		/// Memo             - string     - 備註
		/// IsEnable         - bool       - 是否啟用?
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
        /// 更新電腦電話設定
        /// </summary>
        /// <param name="request">
		/// SeqNo            - int        - Seq No.
		/// ComputerName     - string     - 電腦名稱
		/// ComputerIp       - string     - IP 位址
		/// ExtCode          - string     - 電話分機
		/// Memo             - string     - 備註
		/// IsEnable         - bool       - 是否啟用?
		/// Updator          - string     - 更新者
        /// </param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        public async ValueTask<IActionResult> UpdatePcPhone(PcPhoneUpdateRequest request)
        {
            return Ok(await _PcPhoneService.UpdatePcPhone(request));
        }

        /// <summary>
        /// 刪除電腦電話設定
        /// </summary>
        /// <param>
		/// SeqNo            - int        - Seq No.
        /// </param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        public async ValueTask<IActionResult> DetelePcPhone(int SeqNo)
        {
            return Ok(await _PcPhoneService.DeletePcPhone(SeqNo));
        }

    }
}
