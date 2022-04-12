using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.Txn.Contract;
using ESUN.AGD.DataAccess.DataService.DataAccess;


namespace ESUN.AGD.WebApi.Application.Txn
{
    public class TxnService : ITxnService
    {

        private readonly IDataAccessService _dataAccessService;
        private readonly IGetTokenService _getTokenService;
        

        public TxnService(IDataAccessService dataAccessService , IGetTokenService getTokenService)
        {
            _dataAccessService = dataAccessService;
            _getTokenService = getTokenService;
        }

        public async ValueTask<BasicResponse<TxnResponse>> GetTxn(int seqNo)
        {
            var data = await _dataAccessService
                .LoadSingleData<TbTxn, object>(storeProcedure: "agdSp.uspTxnGet", new { seqNo = seqNo, });
            
            if (data == null) return new BasicResponse<TxnResponse>()
            { resultCode = "U999", resultDescription = "查無資料", data = null };
            
            var result = new TxnResponse
            {
				seqNo = data.SeqNo,
				txnType = data.TxnType,
				txnID = data.TxnID,
				txnName = data.TxnName,
				txnScript = data.TxnScript,
				displayOrder = data.DisplayOrder,
				isEnable = data.IsEnable,
				createDT = data.CreateDT,
				creator = data.Creator,
				updateDT = data.UpdateDT,
				updator = data.Updator,
                updatorName = data.UpdatorName
            };
            
            return new BasicResponse<TxnResponse>()
            { resultCode = "U200", resultDescription = "查詢成功", data = result };
        }

        public async ValueTask<BasicResponse<List<TxnResponse>>> QueryTxn(TxnQueryRequest request)
        {
			if (string.IsNullOrEmpty(request.txnType)) { request.txnType = string.Empty; }
			if (string.IsNullOrEmpty(request.txnID)) { request.txnID = string.Empty; }
			if (string.IsNullOrEmpty(request.txnName)) { request.txnName = string.Empty; }
            

            var data = await _dataAccessService
                .LoadData<TbTxn, object>(storeProcedure: "agdSp.uspTxnQuery", request);
                
            if (data.Count()==0) return new BasicResponse<List<TxnResponse>>()
            { resultCode = "U200", resultDescription = "查無資料", data = null };

            var result = data.Select(item => new TxnResponse
            {
				seqNo = item.SeqNo,
				txnType = item.TxnType,
				txnID = item.TxnID,
				txnName = item.TxnName,
				txnScript = item.TxnScript,
				displayOrder = item.DisplayOrder,
				isEnable = item.IsEnable,
				createDT = item.CreateDT,
				creator = item.Creator,
				updateDT = item.UpdateDT,
				updator = item.Updator,            
                updatorName = item.UpdatorName   
            }).ToList();

            int totalCount = data.FirstOrDefault().Total;

            return new BasicResponse<List<TxnResponse>>()
            { resultCode = "U200", resultDescription = "查詢成功", data = result, total=totalCount };
        }

        public async ValueTask<BasicResponse<bool>> InsertTxn(TxnInsertRequest request)
        {
            var creator = _getTokenService.userID ?? "";
            
            var exists = await Exists(0, request.txnID);
            
            if (exists.data == true) return new BasicResponse<bool>()
            { resultCode = "U999", resultDescription = "資料重複，請重新設定", data=false };
                        
            request.creator = creator;

            var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.uspTxnInsert", request);

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "U999", resultDescription = "新增失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "U200", resultDescription = "新增成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> UpdateTxn(TxnUpdateRequest request)
        {
            var updator = _getTokenService.userID ?? "";

            var exists = await Exists(request.seqNo, request.txnID);
            
            if (exists.data == true) return new BasicResponse<bool>()
            { resultCode = "U999", resultDescription = "資料重複，請重新設定", data = false };

            request.updator = updator;            

            var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.uspTxnUpdate", request);

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "U999", resultDescription = "更新失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "U200", resultDescription = "更新成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> DeleteTxn(int seqNo)
        {
            var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.uspTxnDelete", new { seqNo = seqNo });

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "U999", resultDescription = "刪除失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "U200", resultDescription = "刪除成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> Exists(int seqNo,string txnID)
        {
            var data = await _dataAccessService
                .LoadSingleData<int, object>(storeProcedure: "agdSp.uspTxnExists", new
                {
					SeqNo = seqNo,
					TxnID = txnID,               
                });

            if (data == 0) return new BasicResponse<bool>()
            { resultCode = "U999", resultDescription = "資料重複", data = false };

            return new BasicResponse<bool>()
            { resultCode = "U200", resultDescription = "資料正常", data = true };
        }
    }
}