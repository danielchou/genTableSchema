using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.ReasonTxn.Contract;
using ESUN.AGD.DataAccess.DataService.DataAccess;


namespace ESUN.AGD.WebApi.Application.ReasonTxn
{
    public class ReasonTxnService : IReasonTxnService
    {

        private readonly IDataAccessService _dataAccessService;
        private readonly IGetTokenService _getTokenService;
        

        public ReasonTxnService(IDataAccessService dataAccessService , IGetTokenService getTokenService)
        {
            _dataAccessService = dataAccessService;
            _getTokenService = getTokenService;
        }

        public async ValueTask<BasicResponse<ReasonTxnResponse>> GetReasonTxn(int seqNo)
        {
            var data = await _dataAccessService
                .LoadSingleData<TbReasonTxn, object>(storeProcedure: "agdSp.uspReasonTxnGet", new { seqNo = seqNo, });
            
            if (data == null) return new BasicResponse<ReasonTxnResponse>()
            { resultCode = "U999", resultDescription = "查無資料", data = null };
            
            var result = new ReasonTxnResponse
            {
				seqNo = data.SeqNo,
				txnItem = data.TxnItem,
				reasonID = data.ReasonID,
				createDT = data.CreateDT,
				creator = data.Creator,
				updateDT = data.UpdateDT,
				updator = data.Updator,
                updatorName = data.UpdatorName
            };
            
            return new BasicResponse<ReasonTxnResponse>()
            { resultCode = "U200", resultDescription = "查詢成功", data = result };
        }

        public async ValueTask<BasicResponse<List<ReasonTxnResponse>>> QueryReasonTxn(ReasonTxnQueryRequest request)
        {
			if (string.IsNullOrEmpty(request.txnItem)) { request.txnItem = string.Empty; }
			if (string.IsNullOrEmpty(request.reasonID)) { request.reasonID = string.Empty; }
            

            var data = await _dataAccessService
                .LoadData<TbReasonTxn, object>(storeProcedure: "agdSp.uspReasonTxnQuery", request);
                
            if (data.Count()==0) return new BasicResponse<List<ReasonTxnResponse>>()
            { resultCode = "U200", resultDescription = "查無資料", data = null };

            var result = data.Select(item => new ReasonTxnResponse
            {
				seqNo = item.SeqNo,
				txnItem = item.TxnItem,
				reasonID = item.ReasonID,
				createDT = item.CreateDT,
				creator = item.Creator,
				updateDT = item.UpdateDT,
				updator = item.Updator,            
                updatorName = item.UpdatorName   
            }).ToList();

            int totalCount = data.FirstOrDefault().Total;

            return new BasicResponse<List<ReasonTxnResponse>>()
            { resultCode = "U200", resultDescription = "查詢成功", data = result, total=totalCount };
        }

        public async ValueTask<BasicResponse<bool>> InsertReasonTxn(ReasonTxnInsertRequest request)
        {
            var creator = _getTokenService.userID ?? "";
            
            var exists = await Exists(0, request.txnItem, request.reasonID);
            
            if (exists.data == true) return new BasicResponse<bool>()
            { resultCode = "U999", resultDescription = "資料重複，請重新設定", data=false };
                        
            request.creator = creator;

            var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.uspReasonTxnInsert", request);

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "U999", resultDescription = "新增失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "U200", resultDescription = "新增成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> UpdateReasonTxn(ReasonTxnUpdateRequest request)
        {
            var updator = _getTokenService.userID ?? "";

            var exists = await Exists(request.seqNo, request.txnItem, request.reasonID);
            
            if (exists.data == true) return new BasicResponse<bool>()
            { resultCode = "U999", resultDescription = "資料重複，請重新設定", data = false };

            request.updator = updator;            

            var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.uspReasonTxnUpdate", request);

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "U999", resultDescription = "更新失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "U200", resultDescription = "更新成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> DeleteReasonTxn(int seqNo)
        {
            var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.uspReasonTxnDelete", new { seqNo = seqNo });

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "U999", resultDescription = "刪除失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "U200", resultDescription = "刪除成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> Exists(int seqNo,string txnItem,string? reasonID)
        {
            var data = await _dataAccessService
                .LoadSingleData<int, object>(storeProcedure: "agdSp.uspReasonTxnExists", new
                {
					SeqNo = seqNo,
					TxnItem = txnItem,
					ReasonID = reasonID,               
                });

            if (data == 0) return new BasicResponse<bool>()
            { resultCode = "U999", resultDescription = "資料重複", data = false };

            return new BasicResponse<bool>()
            { resultCode = "U200", resultDescription = "資料正常", data = true };
        }
    }
}