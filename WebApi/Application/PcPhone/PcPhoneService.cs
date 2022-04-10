using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.PcPhone.Contract;
using ESUN.AGD.DataAccess.DataService.DataAccess;


namespace ESUN.AGD.WebApi.Application.PcPhone
{
    public class PcPhoneService : IPcPhoneService
    {

        private readonly IDataAccessService _dataAccessService;
        private readonly IGetTokenService _getTokenService;
        

        public PcPhoneService(IDataAccessService dataAccessService , IGetTokenService getTokenService)
        {
            _dataAccessService = dataAccessService;
            _getTokenService = getTokenService;
        }

        public async ValueTask<BasicResponse<PcPhoneResponse>> GetPcPhone(int seqNo)
        {
            var data = await _dataAccessService
                .LoadSingleData<TbPcPhone, object>(storeProcedure: "agdSp.uspPcPhoneGet", new { seqNo = seqNo, });
            
            if (data == null) return new BasicResponse<PcPhoneResponse>()
            { resultCode = "U999", resultDescription = "查無資料", data = null };
            
            var result = new PcPhoneResponse
            {
				seqNo = data.SeqNo,
				extCode = data.ExtCode,
				computerName = data.ComputerName,
				computerIp = data.ComputerIp,
				memo = data.Memo,
				isEnable = data.IsEnable,
				creator = data.Creator,
				updator = data.Updator,
				createDt = data.CreateDt,
				updateDt = data.UpdateDt,
                updatorName = data.UpdatorName
            };
            
            return new BasicResponse<PcPhoneResponse>()
            { resultCode = "U200", resultDescription = "查詢成功", data = result };
        }

        public async ValueTask<BasicResponse<List<PcPhoneResponse>>> QueryPcPhone(PcPhoneQueryRequest request)
        {

            if (string.IsNullOrEmpty(request.extCode)) { request.extCode = string.Empty; }            
            if (string.IsNullOrEmpty(request.computerName)) { request.computerName = string.Empty; }            

            var data = await _dataAccessService
                .LoadData<TbPcPhone, object>(storeProcedure: "agdSp.uspPcPhoneQuery", request);
                
            if (data.Count()==0) return new BasicResponse<List<PcPhoneResponse>>()
            { resultCode = "U200", resultDescription = "查無資料", data = null };

            var result = data.Select(item => new PcPhoneResponse
            {
				seqNo = item.SeqNo,
				extCode = item.ExtCode,
				computerName = item.ComputerName,
				computerIp = item.ComputerIp,
				memo = item.Memo,
				isEnable = item.IsEnable,
				creator = item.Creator,
				updator = item.Updator,
				createDt = item.CreateDt,
				updateDt = item.UpdateDt,            
                updatorName = item.UpdatorName   
            }).ToList();

            int totalCount = data.FirstOrDefault().Total;

            return new BasicResponse<List<PcPhoneResponse>>()
            { resultCode = "U200", resultDescription = "查詢成功", data = result, total=totalCount };
        }

        public async ValueTask<BasicResponse<bool>> InsertPcPhone(PcPhoneInsertRequest request)
        {
            var creator = _getTokenService.userId ?? "";

            var exists = await Exists(0, request.extCode, request.computerIp);
            
            if (exists.data == true) return new BasicResponse<bool>()
            { resultCode = "U999", resultDescription = "資料重複，請重新設定", data=false };
                        
            request.creator = creator;

            var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.uspPcPhoneInsert", request);

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "U999", resultDescription = "新增失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "U200", resultDescription = "新增成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> UpdatePcPhone(PcPhoneUpdateRequest request)
        {
            var updator = _getTokenService.userId ?? "";

            var exists = await Exists(request.seqNo, request.extCode, request.computerIp);
            
            if (exists.data == true) return new BasicResponse<bool>()
            { resultCode = "U999", resultDescription = "資料重複，請重新設定", data = false };

            request.updator = updator;            

            var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.uspPcPhoneUpdate", request);

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "U999", resultDescription = "更新失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "U200", resultDescription = "更新成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> DeletePcPhone(int seqNo)
        {
            var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.uspPcPhoneDelete", new { seqNo = seqNo });

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "U999", resultDescription = "刪除失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "U200", resultDescription = "刪除成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> Exists(int seqNo,string extCode,string computerIp)
        {
            var data = await _dataAccessService
                .LoadSingleData<int, object>(storeProcedure: "agdSp.uspPcPhoneExists", new
                {
					SeqNo = seqNo,
					ExtCode = extCode,
					ComputerIp = computerIp,               
                });

            if (data == 0) return new BasicResponse<bool>()
            { resultCode = "U999", resultDescription = "資料重複", data = false };

            return new BasicResponse<bool>()
            { resultCode = "U200", resultDescription = "資料正常", data = true };
        }
    }
}