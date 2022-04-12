using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.ReasonIvr.Contract;
using ESUN.AGD.DataAccess.DataService.DataAccess;


namespace ESUN.AGD.WebApi.Application.ReasonIvr
{
    public class ReasonIvrService : IReasonIvrService
    {

        private readonly IDataAccessService _dataAccessService;
        private readonly IGetTokenService _getTokenService;
        

        public ReasonIvrService(IDataAccessService dataAccessService , IGetTokenService getTokenService)
        {
            _dataAccessService = dataAccessService;
            _getTokenService = getTokenService;
        }

        public async ValueTask<BasicResponse<ReasonIvrResponse>> GetReasonIvr(int seqNo)
        {
            var data = await _dataAccessService
                .LoadSingleData<TbReasonIvr, object>(storeProcedure: "agdSp.uspReasonIvrGet", new { seqNo = seqNo, });
            
            if (data == null) return new BasicResponse<ReasonIvrResponse>()
            { resultCode = "U999", resultDescription = "查無資料", data = null };
            
            var result = new ReasonIvrResponse
            {
				seqNo = data.SeqNo,
				ivrID = data.IvrID,
				reasonID = data.ReasonID,
				createDT = data.CreateDT,
				creator = data.Creator,
				updateDT = data.UpdateDT,
				updator = data.Updator,
                updatorName = data.UpdatorName
            };
            
            return new BasicResponse<ReasonIvrResponse>()
            { resultCode = "U200", resultDescription = "查詢成功", data = result };
        }

        public async ValueTask<BasicResponse<List<ReasonIvrResponse>>> QueryReasonIvr(ReasonIvrQueryRequest request)
        {
			if (string.IsNullOrEmpty(request.ivrID)) { request.ivrID = string.Empty; }
			if (string.IsNullOrEmpty(request.reasonID)) { request.reasonID = string.Empty; }
            

            var data = await _dataAccessService
                .LoadData<TbReasonIvr, object>(storeProcedure: "agdSp.uspReasonIvrQuery", request);
                
            if (data.Count()==0) return new BasicResponse<List<ReasonIvrResponse>>()
            { resultCode = "U200", resultDescription = "查無資料", data = null };

            var result = data.Select(item => new ReasonIvrResponse
            {
				seqNo = item.SeqNo,
				ivrID = item.IvrID,
				reasonID = item.ReasonID,
				createDT = item.CreateDT,
				creator = item.Creator,
				updateDT = item.UpdateDT,
				updator = item.Updator,            
                updatorName = item.UpdatorName   
            }).ToList();

            int totalCount = data.FirstOrDefault().Total;

            return new BasicResponse<List<ReasonIvrResponse>>()
            { resultCode = "U200", resultDescription = "查詢成功", data = result, total=totalCount };
        }

        public async ValueTask<BasicResponse<bool>> InsertReasonIvr(ReasonIvrInsertRequest request)
        {
            var creator = _getTokenService.userID ?? "";
            
            var exists = await Exists(0, request.ivrID, request.reasonID);
            
            if (exists.data == true) return new BasicResponse<bool>()
            { resultCode = "U999", resultDescription = "資料重複，請重新設定", data=false };
                        
            request.creator = creator;

            var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.uspReasonIvrInsert", request);

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "U999", resultDescription = "新增失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "U200", resultDescription = "新增成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> UpdateReasonIvr(ReasonIvrUpdateRequest request)
        {
            var updator = _getTokenService.userID ?? "";

            var exists = await Exists(request.seqNo, request.ivrID, request.reasonID);
            
            if (exists.data == true) return new BasicResponse<bool>()
            { resultCode = "U999", resultDescription = "資料重複，請重新設定", data = false };

            request.updator = updator;            

            var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.uspReasonIvrUpdate", request);

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "U999", resultDescription = "更新失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "U200", resultDescription = "更新成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> DeleteReasonIvr(int seqNo)
        {
            var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.uspReasonIvrDelete", new { seqNo = seqNo });

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "U999", resultDescription = "刪除失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "U200", resultDescription = "刪除成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> Exists(int seqNo,string ivrID,string reasonID)
        {
            var data = await _dataAccessService
                .LoadSingleData<int, object>(storeProcedure: "agdSp.uspReasonIvrExists", new
                {
					SeqNo = seqNo,
					IvrID = ivrID,
					ReasonID = reasonID,               
                });

            if (data == 0) return new BasicResponse<bool>()
            { resultCode = "U999", resultDescription = "資料重複", data = false };

            return new BasicResponse<bool>()
            { resultCode = "U200", resultDescription = "資料正常", data = true };
        }
    }
}