using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.Marketing.Contract;
using ESUN.AGD.DataAccess.DataService.DataAccess;


namespace ESUN.AGD.WebApi.Application.Marketing
{
    public class MarketingService : IMarketingService
    {

        private readonly IDataAccessService _dataAccessService;
        private readonly IGetTokenService _getTokenService;
        

        public MarketingService(IDataAccessService dataAccessService , IGetTokenService getTokenService)
        {
            _dataAccessService = dataAccessService;
            _getTokenService = getTokenService;
        }

        public async ValueTask<BasicResponse<MarketingResponse>> GetMarketing(int seqNo)
        {
            var data = await _dataAccessService
                .LoadSingleData<TbMarketing, object>(storeProcedure: "agdSp.uspMarketingGet", new { seqNo = seqNo, });
            
            if (data == null) return new BasicResponse<MarketingResponse>()
            { resultCode = "U999", resultDescription = "查無資料", data = null };
            
            var result = new MarketingResponse
            {
				seqNo = data.SeqNo,
				marketingID = data.MarketingID,
				marketingType = data.MarketingType,
				marketingName = data.MarketingName,
				content = data.Content,
				marketingScript = data.MarketingScript,
				marketingBegintDT = data.MarketingBegintDT,
				marketingEndDT = data.MarketingEndDT,
				offerCode = data.OfferCode,
				displayOrder = data.DisplayOrder,
				isEnable = data.IsEnable,
				createDT = data.CreateDT,
				creator = data.Creator,
				updateDT = data.UpdateDT,
				updator = data.Updator,
                updatorName = data.UpdatorName
            };
            
            return new BasicResponse<MarketingResponse>()
            { resultCode = "U200", resultDescription = "查詢成功", data = result };
        }

        public async ValueTask<BasicResponse<List<MarketingResponse>>> QueryMarketing(MarketingQueryRequest request)
        {
			if (string.IsNullOrEmpty(request.marketingID)) { request.marketingID = string.Empty; }
			if (string.IsNullOrEmpty(request.marketingType)) { request.marketingType = string.Empty; }
			if (string.IsNullOrEmpty(request.marketingName)) { request.marketingName = string.Empty; }
            

            var data = await _dataAccessService
                .LoadData<TbMarketing, object>(storeProcedure: "agdSp.uspMarketingQuery", request);
                
            if (data.Count()==0) return new BasicResponse<List<MarketingResponse>>()
            { resultCode = "U200", resultDescription = "查無資料", data = null };

            var result = data.Select(item => new MarketingResponse
            {
				seqNo = item.SeqNo,
				marketingID = item.MarketingID,
				marketingType = item.MarketingType,
				marketingName = item.MarketingName,
				content = item.Content,
				marketingScript = item.MarketingScript,
				marketingBegintDT = item.MarketingBegintDT,
				marketingEndDT = item.MarketingEndDT,
				offerCode = item.OfferCode,
				displayOrder = item.DisplayOrder,
				isEnable = item.IsEnable,
				createDT = item.CreateDT,
				creator = item.Creator,
				updateDT = item.UpdateDT,
				updator = item.Updator,            
                updatorName = item.UpdatorName   
            }).ToList();

            int totalCount = data.FirstOrDefault().Total;

            return new BasicResponse<List<MarketingResponse>>()
            { resultCode = "U200", resultDescription = "查詢成功", data = result, total=totalCount };
        }

        public async ValueTask<BasicResponse<bool>> InsertMarketing(MarketingInsertRequest request)
        {
            var creator = _getTokenService.userID ?? "";
            
            var exists = await Exists(0, request.marketingID);
            
            if (exists.data == true) return new BasicResponse<bool>()
            { resultCode = "U999", resultDescription = "資料重複，請重新設定", data=false };
                        
            request.creator = creator;

            var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.uspMarketingInsert", request);

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "U999", resultDescription = "新增失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "U200", resultDescription = "新增成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> UpdateMarketing(MarketingUpdateRequest request)
        {
            var updator = _getTokenService.userID ?? "";

            var exists = await Exists(request.seqNo, request.marketingID);
            
            if (exists.data == true) return new BasicResponse<bool>()
            { resultCode = "U999", resultDescription = "資料重複，請重新設定", data = false };

            request.updator = updator;            

            var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.uspMarketingUpdate", request);

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "U999", resultDescription = "更新失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "U200", resultDescription = "更新成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> DeleteMarketing(int seqNo)
        {
            var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.uspMarketingDelete", new { seqNo = seqNo });

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "U999", resultDescription = "刪除失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "U200", resultDescription = "刪除成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> Exists(int seqNo,string marketingID)
        {
            var data = await _dataAccessService
                .LoadSingleData<int, object>(storeProcedure: "agdSp.uspMarketingExists", new
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