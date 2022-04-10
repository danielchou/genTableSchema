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
        /// seqNo - 電腦電話序號
        /// </param>
        /// <returns>
        /// seqNo - 電腦電話序號
        /// extCde - 分機號碼
        /// computerName - 電腦名稱
        /// computerIp - 電腦IP
        /// memo - 備註
        /// isEnable - 是否啟用
        /// createDt - 建立日期
        /// creator - 建立者
        /// createIP - 建立IP
        /// updateDt - 更新日期
        /// updator - 更新者
        /// updateIP - 更新IP
        /// updatorName - 更新者名稱
        /// </returns>
        [Authorize]
        [HttpGet("{seqNo}")]
        public async ValueTask<IActionResult> GetPcPhone(int seqNo)
        {
            return Ok(await _PcPhoneService.GetPcPhone(seqNo));
        }

        /// <summary>
        /// 搜尋電腦電話設定 
        /// </summary>
        /// <param name="request">
        /// extCode = 分機號碼
        /// computerName - 電腦名稱
        /// page - 分頁
        /// rowsPerPage - 每頁筆數
        /// sortColumn - 排序欄位
        /// sortOrder - 排序順序
        /// </param>
        /// <returns>
        /// seqNo - 電腦電話序號
        /// extCde - 分機號碼
        /// computerName - 電腦名稱
        /// computerIp - 電腦IP
        /// memo - 備註
        /// isEnable - 是否啟用
        /// createDt - 建立日期
        /// creator - 建立者
        /// updateDt - 更新日期
        /// updator - 更新者
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
        /// seqNo - 電腦電話序號
        /// extCde - 分機號碼
        /// computerName - 電腦名稱
        /// computerIp - 電腦IP
        /// memo - 備註
        /// isEnable - 是否啟用
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
        /// seqNo - 電腦電話序號
        /// extCde - 分機號碼
        /// computerName - 電腦名稱
        /// computerIp - 電腦IP
        /// memo - 備註
        /// isEnable - 是否啟用
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
        /// seqNo - 電腦電話序號
        /// </param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        public async ValueTask<IActionResult> DetelePcPhone(int seqNo)
        {
            return Ok(await _PcPhoneService.DeletePcPhone(seqNo));
        }

    }
}
