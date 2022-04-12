using ESUN.AGD.WebApi.Application.Protocol;
using ESUN.AGD.WebApi.Application.Protocol.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESUN.AGD.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProtocolController : Controller
    {

        private readonly IProtocolService _ProtocolService;

        public ProtocolController(IProtocolService ProtocolService)
        {
            _ProtocolService = ProtocolService;
        }

        /// <summary>
        /// 依序號取得通路碼設定
        /// </summary>
        /// <param>
        /// seqNo           - int     - 流水號
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// protocol         - string     - 通路碼代碼
		/// protocolName     - string     - 通路碼名稱
		/// direction        - string     - IN/OUT方向
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
        public async ValueTask<IActionResult> GetProtocol(int seqNo)
        {
            return Ok(await _ProtocolService.GetProtocol(seqNo));
        }

        /// <summary>
        /// 搜尋通路碼設定 
        /// </summary>
        /// <param name="request">
		/// protocol         - string     - 通路碼代碼
		/// protocolName     - string     - 通路碼名稱
        /// page             - int        - 分頁
        /// rowsPerPage      - int        - 每頁筆數
        /// sortColumn       - string     - 排序欄位
        /// sortOrder        - bool       - 排序順序
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// protocol         - string     - 通路碼代碼
		/// protocolName     - string     - 通路碼名稱
		/// direction        - string     - IN/OUT方向
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
        public async ValueTask<IActionResult> QueryProtocol([FromQuery] ProtocolQueryRequest request)
        {
            return Ok(await _ProtocolService.QueryProtocol(request));
        }

        /// <summary>
        /// 新增通路碼設定 
        /// </summary>
        /// <param name="request">
		/// protocol         - string     - 通路碼代碼
		/// protocolName     - string     - 通路碼名稱
		/// direction        - string     - IN/OUT方向
		/// displayOrder     - int        - 顯示順序
		/// isEnable         - bool       - 是否啟用?
        /// </param>
        /// <returns>
        /// </returns>        
        [Authorize]
        [HttpPost]
        public async ValueTask<IActionResult> InsertProtocol(ProtocolInsertRequest request)
        {
            return Ok(await _ProtocolService.InsertProtocol(request));
        }

        /// <summary>
        /// 更新通路碼設定
        /// </summary>
        /// <param name="request">
		/// seqNo            - int        - 流水號
		/// protocol         - string     - 通路碼代碼
		/// protocolName     - string     - 通路碼名稱
		/// direction        - string     - IN/OUT方向
		/// displayOrder     - int        - 顯示順序
		/// isEnable         - bool       - 是否啟用?
        /// </param>
        /// <returns>
        /// </returns>
        [Authorize]
        [HttpPut]
        public async ValueTask<IActionResult> UpdateProtocol(ProtocolUpdateRequest request)
        {
            return Ok(await _ProtocolService.UpdateProtocol(request));
        }

        /// <summary>
        /// 刪除通路碼設定
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
        /// </param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        public async ValueTask<IActionResult> DeteleProtocol(int seqNo)
        {
            return Ok(await _ProtocolService.DeleteProtocol(seqNo));
        }

    }
}
