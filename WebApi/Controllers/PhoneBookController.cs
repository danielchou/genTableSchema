using ESUN.AGD.WebApi.Application.PhoneBook;
using ESUN.AGD.WebApi.Application.PhoneBook.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESUN.AGD.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhoneBookController : Controller
    {

        private readonly IPhoneBookService _PhoneBookService;

        public PhoneBookController(IPhoneBookService PhoneBookService)
        {
            _PhoneBookService = PhoneBookService;
        }

        /// <summary>
        /// 依序號取得電話簿設定
        /// </summary>
        /// <param>
        /// seqNo           - int     - 流水號
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// phoneBookID      - string     - 電話簿代碼
		/// phoneBookName    - string     - 電話簿名稱
		/// parentPhoneBookID - string     - 上層電話簿代碼
		/// phoneBookNumber  - string     - 電話號碼
		/// level            - int        - 階層
		/// memo             - string     - 備註
		/// displayOrder     - int        - 顯示順序
		/// createDT         - DateTime   - 建立時間
		/// creator          - string     - 建立者
		/// updateDT         - DateTime   - 更新時間
		/// updator          - string     - 更新者
        /// updatorName      - string     - 更新者名稱
        /// </returns>
        [Authorize]
        [HttpGet("{seqNo}")]
        public async ValueTask<IActionResult> GetPhoneBook(int seqNo)
        {
            return Ok(await _PhoneBookService.GetPhoneBook(seqNo));
        }

        /// <summary>
        /// 搜尋電話簿設定 
        /// </summary>
        /// <param name="request">
		/// phoneBookID      - string     - 電話簿代碼
		/// phoneBookName    - string     - 電話簿名稱
		/// parentPhoneBookID - string     - 上層電話簿代碼
		/// phoneBookNumber  - string     - 電話號碼
        /// page             - int        - 分頁
        /// rowsPerPage      - int        - 每頁筆數
        /// sortColumn       - string     - 排序欄位
        /// sortOrder        - bool       - 排序順序
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// phoneBookID      - string     - 電話簿代碼
		/// phoneBookName    - string     - 電話簿名稱
		/// parentPhoneBookID - string     - 上層電話簿代碼
		/// phoneBookNumber  - string     - 電話號碼
		/// level            - int        - 階層
		/// memo             - string     - 備註
		/// displayOrder     - int        - 顯示順序
		/// createDT         - DateTime   - 建立時間
		/// creator          - string     - 建立者
		/// updateDT         - DateTime   - 更新時間
		/// updator          - string     - 更新者
        /// updatorName      - string     - 更新者名稱
        /// </returns>        
        [Authorize]
        [HttpGet("query")]
        public async ValueTask<IActionResult> QueryPhoneBook([FromQuery] PhoneBookQueryRequest request)
        {
            return Ok(await _PhoneBookService.QueryPhoneBook(request));
        }

        /// <summary>
        /// 新增電話簿設定 
        /// </summary>
        /// <param name="request">
		/// phoneBookID      - string     - 電話簿代碼
		/// phoneBookName    - string     - 電話簿名稱
		/// parentPhoneBookID - string     - 上層電話簿代碼
		/// phoneBookNumber  - string     - 電話號碼
		/// level            - int        - 階層
		/// memo             - string     - 備註
		/// displayOrder     - int        - 顯示順序
        /// </param>
        /// <returns>
        /// </returns>        
        [Authorize]
        [HttpPost]
        public async ValueTask<IActionResult> InsertPhoneBook(PhoneBookInsertRequest request)
        {
            return Ok(await _PhoneBookService.InsertPhoneBook(request));
        }

        /// <summary>
        /// 更新電話簿設定
        /// </summary>
        /// <param name="request">
		/// seqNo            - int        - 流水號
		/// phoneBookID      - string     - 電話簿代碼
		/// phoneBookName    - string     - 電話簿名稱
		/// parentPhoneBookID - string     - 上層電話簿代碼
		/// phoneBookNumber  - string     - 電話號碼
		/// level            - int        - 階層
		/// memo             - string     - 備註
		/// displayOrder     - int        - 顯示順序
        /// </param>
        /// <returns>
        /// </returns>
        [Authorize]
        [HttpPut]
        public async ValueTask<IActionResult> UpdatePhoneBook(PhoneBookUpdateRequest request)
        {
            return Ok(await _PhoneBookService.UpdatePhoneBook(request));
        }

        /// <summary>
        /// 刪除電話簿設定
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
        /// </param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        public async ValueTask<IActionResult> DetelePhoneBook(int seqNo)
        {
            return Ok(await _PhoneBookService.DeletePhoneBook(seqNo));
        }

    }
}
