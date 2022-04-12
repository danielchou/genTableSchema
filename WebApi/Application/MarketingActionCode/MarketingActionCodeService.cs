using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.MarketingActionCode.Contract;
using ESUN.AGD.DataAccess.DataService.DataAccess;


namespace ESUN.AGD.WebApi.Application.MarketingActionCode
{
    public class MarketingActionCodeService : IMarketingActionCodeService
    {

        private readonly IDataAccessService _dataAccessService;
        private readonly IGetTokenService _getTokenService;
        

        public MarketingActionCodeService(IDataAccessService dataAccessService , IGetTokenService getTokenService)
        {
            _dataAccessService = dataAccessService;
            _getTokenService = getTokenService;
        }

        public async ValueTask<BasicResponse<MarketingActionCodeResponse>> GetMarketingActionCode(int seqNo)
        {
            var data = await _dataAccessService
                .LoadSingleData<TbMarketingActionCode, object>(storeProcedure: "agdSp.uspMarketingActionCodeGet", new { seqNo = seqNo, });
            
            if (data == null) return new BasicResponse<MarketingActionCodeResponse>()
            { resultCode = "U999", resultDescription = "查無資料", data = null };
            
            var result = new MarketingActionCodeResponse
            {
				seqNo = data.SeqNo,
				actionCodeType = data.ActionCodeType,
				marketingID = data.MarketingID,
				actionCode = data.ActionCode,
				content = data.Content,
				isAccept = data.IsAccept,
				displayOrder = data.DisplayOrder,
				isEnable = data.IsEnable,
				createDT = data.CreateDT,
				creator = data.Creator,
				updateDT = data.UpdateDT,
				updator = data.Updator,
                updatorName = data.UpdatorName
            };
            
            return new BasicResponse<MarketingActionCodeResponse>()
            { resultCode = "U200", resultDescription = "查詢成功", data = result };
        }

        public async ValueTask<BasicResponse<List<MarketingActionCodeResponse>>> QueryMarketingActionCode(MarketingActionCodeQueryRequest request)
        {
			if (string.IsNullOrEmpty(request.actionCodeType)) { request.actionCodeType = string.Empty; }
			if (string.IsNullOrEmpty(request.marketingID)) { request.marketingID = string.Empty; }
			if (string.IsNullOrEmpty(request.actionCode)) { request.actionCode = string.Empty; }
            

            var data = await _dataAccessService
                .LoadData<TbMarketingActionCode, object>(storeProcedure: "agdSp.uspMarketingActionCodeQuery", request);
                
            if (data.Count()==0) return new BasicResponse<List<MarketingActionCodeResponse>>()
            { resultCode = "U200", resultDescription = "查無資料", data = null };

            var result = data.Select(item => new MarketingActionCodeResponse
            {
				seqNo = item.SeqNo,
				actionCodeType = item.ActionCodeType,
				marketingID = item.MarketingID,
				actionCode = item.ActionCode,
				content = item.Content,
				isAccept = item.IsAccept,
				displayOrder = item.DisplayOrder,
				isEnable = item.IsEnable,
				createDT = item.CreateDT,
				creator = item.Creator,
				updateDT = item.UpdateDT,
				updator = item.Updator,            
                updatorName = item.UpdatorName   
            }).ToList();

            int totalCount = data.FirstOrDefault().Total;

            return new BasicResponse<List<MarketingActionCodeResponse>>()
            { resultCode = "U200", resultDescription = "查詢成功", data = result, total=totalCount };
        }

        public async ValueTask<BasicResponse<bool>> InsertMarketingActionCode(MarketingActionCodeInsertRequest request)
        {
            var creator = _getTokenService.userID ?? "";
            
            var exists = await Exists(0, request.marketingID);
            
            if (exists.data == true) return new BasicResponse<bool>()
            { resultCode = "U999", resultDescription = "資料重複，請重新設定", data=false };
                        
            request.creator = creator;

            var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.uspMarketingActionCodeInsert", request);

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "U999", resultDescription = "新增失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "U200", resultDescription = "新增成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> UpdateMarketingActionCode(MarketingActionCodeUpdateRequest request)
        {
            var updator = _getTokenService.userID ?? "";

            var exists = await Exists(request.seqNo, request.marketingID);
            
            if (exists.data == true) return new BasicResponse<bool>()
            { resultCode = "U999", resultDescription = "資料重複，請重新設定", data = false };

            request.updator = updator;            

            var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.uspMarketingActionCodeUpdate", request);

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "U999", resultDescription = "更新失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "U200", resultDescription = "更新成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> DeleteMarketingActionCode(int seqNo)
        {
            var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.uspMarketingActionCodeDelete", new { seqNo = seqNo });

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "U999", resultDescription = "刪除失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "U200", resultDescription = "刪除成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> Exists(int seqNo,string marketingID)
        {
            var data = await _dataAccessService
                .LoadSingleData<int, object>(storeProcedure: "agdSp.uspMarketingActionCodeExists", new
                {
					SeqNo = seqNo,
					MarketingID = marketingID,               
                });

            if (data == 0) return new BasicResponse<bool>()
            { resultCode = "U999", resultDescription = "資料重複", data = false };

            return new BasicResponse<bool>()
            { resultCode = "U200", resultDescription = "資料正常", data = true };
        }
    }
}