using ESUN.AGD.WebApi.Application.PcPhone.Contract;

namespace ESUN.AGD.WebApi.Application.PcPhone
{
    public interface IPcPhoneService
    {
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
        /// updateDt - 更新日期
        /// updator - 更新者
        /// updatorName - 更新者名稱
        /// </returns>
        ValueTask<BasicResponse<PcPhoneResponse>> GetPcPhone(int seqNo);

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
        /// createIP - 建立IP
        /// updateDt - 更新日期
        /// updator - 更新者
        /// updateIP - 更新IP
        /// updatorName - 更新者名稱
        /// </returns> 
        ValueTask<BasicResponse<List<PcPhoneResponse>>> QueryPcPhone(PcPhoneQueryRequest request);

        /// <summary>
        /// 新增電腦電話設定 
        /// </summary>
        /// <param name="request">
        /// extCde - 分機號碼
        /// computerName - 電腦名稱
        /// computerIp - 電腦IP
        /// memo - 備註
        /// isEnable - 是否啟用
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> InsertPcPhone(PcPhoneInsertRequest request);

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
        ValueTask<BasicResponse<bool>> UpdatePcPhone(PcPhoneUpdateRequest request);

        /// <summary>
        /// 刪除電腦電話設定
        /// </summary>
        /// <param>
        /// seqNo - 電腦電話序號
        /// </param>
        /// <returns></returns>
        ValueTask<BasicResponse<bool>> DeletePcPhone(int seqNo);

        /// <summary>
        /// 檢查電腦電話是否存在
        /// </summary>
        /// <param>
        /// seqNo - 電腦電話序號
        /// extCode - 分機號碼
        /// computerIp - 電腦IP
        /// </param>
        /// <returns></returns>
        ValueTask<BasicResponse<bool>> Exists(int seqNo, string? extCode, string? computerIp);

    }
}
