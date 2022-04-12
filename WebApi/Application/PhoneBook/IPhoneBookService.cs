using ESUN.AGD.WebApi.Application.PhoneBook.Contract;

namespace ESUN.AGD.WebApi.Application.PhoneBook
{
    public interface IPhoneBookService
    {
        /// <summary>
        /// 依序號取得電話簿設定
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
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
        ValueTask<BasicResponse<PhoneBookResponse>> GetPhoneBook(int seqNo);

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
        /// sortOrder        - string     - 排序順序
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
        ValueTask<BasicResponse<List<PhoneBookResponse>>> QueryPhoneBook(PhoneBookQueryRequest request);

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
        ValueTask<BasicResponse<bool>> InsertPhoneBook(PhoneBookInsertRequest request);

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
        ValueTask<BasicResponse<bool>> UpdatePhoneBook(PhoneBookUpdateRequest request);

        /// <summary>
        /// 刪除電話簿設定
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> DeletePhoneBook(int seqNo);

        /// <summary>
        /// 檢查電話簿是否存在
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
		/// phoneBookID      - string     - 電話簿代碼
		/// phoneBookName    - string     - 電話簿名稱
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> Exists(int seqNo,string phoneBookID,string phoneBookName);

    }
}
