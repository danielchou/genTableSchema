using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.MessageSheet.Contract;
using ESUN.AGD.DataAccess.DataService.DataAccess;


namespace ESUN.AGD.WebApi.Application.MessageSheet
{
    public class MessageSheetService : IMessageSheetService
    {

        private readonly IDataAccessService _dataAccessService;
        private readonly IGetTokenService _getTokenService;
        

        public MessageSheetService(IDataAccessService dataAccessService , IGetTokenService getTokenService)
        {
            _dataAccessService = dataAccessService;
            _getTokenService = getTokenService;
        }

        public async ValueTask<BasicResponse<MessageSheetResponse>> GetMessageSheet(int seqNo)
        {
            var data = await _dataAccessService
                .LoadSingleData<TbMessageSheet, object>(storeProcedure: "agdSp.uspMessageSheetGet", new { seqNo = seqNo, });
            
            if (data == null) return new BasicResponse<MessageSheetResponse>()
            { resultCode = "U999", resultDescription = "查無資料", data = null };
            
            var result = new MessageSheetResponse
            {
				seqNo = data.SeqNo,
				messageSheetType = data.MessageSheetType,
				messageSheetID = data.MessageSheetID,
				messageSheetName = data.MessageSheetName,
				displayOrder = data.DisplayOrder,
				isEnable = data.IsEnable,
				createDT = data.CreateDT,
				creator = data.Creator,
				updateDT = data.UpdateDT,
				updator = data.Updator,
                updatorName = data.UpdatorName
            };
            
            return new BasicResponse<MessageSheetResponse>()
            { resultCode = "U200", resultDescription = "查詢成功", data = result };
        }

        public async ValueTask<BasicResponse<List<MessageSheetResponse>>> QueryMessageSheet(MessageSheetQueryRequest request)
        {
			if (string.IsNullOrEmpty(request.messageSheetType)) { request.messageSheetType = string.Empty; }
			if (string.IsNullOrEmpty(request.messageSheetID)) { request.messageSheetID = string.Empty; }
			if (string.IsNullOrEmpty(request.messageSheetName)) { request.messageSheetName = string.Empty; }
            

            var data = await _dataAccessService
                .LoadData<TbMessageSheet, object>(storeProcedure: "agdSp.uspMessageSheetQuery", request);
                
            if (data.Count()==0) return new BasicResponse<List<MessageSheetResponse>>()
            { resultCode = "U200", resultDescription = "查無資料", data = null };

            var result = data.Select(item => new MessageSheetResponse
            {
				seqNo = item.SeqNo,
				messageSheetType = item.MessageSheetType,
				messageSheetID = item.MessageSheetID,
				messageSheetName = item.MessageSheetName,
				displayOrder = item.DisplayOrder,
				isEnable = item.IsEnable,
				createDT = item.CreateDT,
				creator = item.Creator,
				updateDT = item.UpdateDT,
				updator = item.Updator,            
                updatorName = item.UpdatorName   
            }).ToList();

            int totalCount = data.FirstOrDefault().Total;

            return new BasicResponse<List<MessageSheetResponse>>()
            { resultCode = "U200", resultDescription = "查詢成功", data = result, total=totalCount };
        }

        public async ValueTask<BasicResponse<bool>> InsertMessageSheet(MessageSheetInsertRequest request)
        {
            var creator = _getTokenService.userID ?? "";
            
            var exists = await Exists(0, request.messageSheetID);
            
            if (exists.data == true) return new BasicResponse<bool>()
            { resultCode = "U999", resultDescription = "資料重複，請重新設定", data=false };
                        
            request.creator = creator;

            var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.uspMessageSheetInsert", request);

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "U999", resultDescription = "新增失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "U200", resultDescription = "新增成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> UpdateMessageSheet(MessageSheetUpdateRequest request)
        {
            var updator = _getTokenService.userID ?? "";

            var exists = await Exists(request.seqNo, request.messageSheetID);
            
            if (exists.data == true) return new BasicResponse<bool>()
            { resultCode = "U999", resultDescription = "資料重複，請重新設定", data = false };

            request.updator = updator;            

            var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.uspMessageSheetUpdate", request);

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "U999", resultDescription = "更新失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "U200", resultDescription = "更新成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> DeleteMessageSheet(int seqNo)
        {
            var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.uspMessageSheetDelete", new { seqNo = seqNo });

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "U999", resultDescription = "刪除失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "U200", resultDescription = "刪除成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> Exists(int seqNo,string messageSheetID)
        {
            var data = await _dataAccessService
                .LoadSingleData<int, object>(storeProcedure: "agdSp.uspMessageSheetExists", new
                {
					SeqNo = seqNo,
					MessageSheetID = messageSheetID,               
                });

            if (data == 0) return new BasicResponse<bool>()
            { resultCode = "U999", resultDescription = "資料重複", data = false };

            return new BasicResponse<bool>()
            { resultCode = "U200", resultDescription = "資料正常", data = true };
        }
    }
}