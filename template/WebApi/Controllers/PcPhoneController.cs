using ESUN.AGD.WebApi.Application.$pt_TableName;
using ESUN.AGD.WebApi.Application.$pt_TableName.Contract;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESUN.AGD.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class $pt_TableName$controller : Controller
    {

        private readonly I$pt_TableName$service _$pt_TableName$service;

        public $pt_TableName$controller(I$pt_TableName$service $pt_TableName$service)
        {
            _$pt_TableName$service = $pt_TableName$service;
        }

        /// <summary>
        /// 依序號取得$TbDscr設定
        /// </summary>
        /// <param>
$pt_ColDscr_ParasPK
        /// </param>
        /// <returns>
$pt_ColDscr_GetReturnAll
        /// </returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("{$pt_InputServicePK}")]
        public async ValueTask<IActionResult> Get$pt_TableName($pt_InputPK)
        {
            return Ok(await _$pt_TableName$service.Get$pt_TableName($pt_InputServicePK));
        }

        /// <summary>
        /// 搜尋$TbDscr設定 
        /// </summary>
        /// <param name="request">
$pt_ColDscr_QueryParas_Query
        /// page             - int        - 分頁
        /// rowsPerPage      - int        - 每頁筆數
        /// sortColumn       - string     - 排序欄位
        /// sortOrder        - bool       - 排序順序
        /// </param>
        /// <returns>
$pt_ColDscr_GetReturnAll
        /// </returns>        
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("Query")]
        [EncryptFilter]
        public async ValueTask<IActionResult> Query$pt_TableName([FromQuery] $pt_TableName$queryRequest request)
        {
            return Ok(await _$pt_TableName$service.Query$pt_TableName(request));
        }

        /// <summary>
        /// 新增$TbDscr設定 
        /// </summary>
        /// <param name="request">
$pt_ColDscr_InsertParas
        /// </param>
        /// <returns>
        /// </returns>        
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async ValueTask<IActionResult> Insert$pt_TableName($pt_TableName$insertRequest request)
        {
            return Ok(await _$pt_TableName$service.Insert$pt_TableName(request));
        }

        /// <summary>
        /// 更新$TbDscr設定
        /// </summary>
        /// <param name="request">
$pt_colDscr_UpdateParas
        /// </param>
        /// <returns>
        /// </returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut]
        public async ValueTask<IActionResult> Update$pt_TableName($pt_TableName$updateRequest request)
        {
            return Ok(await _$pt_TableName$service.Update$pt_TableName(request));
        }

        /// <summary>
        /// 刪除$TbDscr設定
        /// </summary>
        /// <param>
$pt_ColDscr_ParasPK
        /// </param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete]
        public async ValueTask<IActionResult> Detele$pt_TableName($pt_InputPK)
        {
            return Ok(await _$pt_TableName$service.Delete$pt_TableName($pt_InputServicePK));
        }

    }
}
