using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.Message.Contract;
using ESUN.AGD.DataAccess.DataService.DataAccess;


namespace ESUN.AGD.WebApi.Application.Message
{
    public class MessageService : IMessageService
    {

        private readonly IDataAccessService _dataAccessService;
        private readonly IGetTokenService _getTokenService;
        

        public MessageService(IDataAccessService dataAccessService , IGetTokenService getTokenService)
        {
            _dataAccessService = dataAccessService;
            _getTokenService = getTokenService;
        }

        public async ValueTask<BasicResponse<MessageResponse>> GetMessage(int seqNo)
        {
            var data = await _dataAccessService
                .LoadSingleData<TbMessage, object>(storeProcedure: "agdSp.uspMessageGet", new { seqNo = seqNo, });
            
            if (data == null) return new BasicResponse<MessageResponse>()
            { resultCode = "U999", resultDescription = "查無資料", data = null };
            
            var result = new MessageResponse
            {
				seqNo = data.SeqNo,
				messageSheetID = data.MessageSheetID,
				messageTemplateID = data.MessageTemplateID,
				messageName = data.MessageName,
				displayOrder = data.DisplayOrder,
				isEnable = data.IsEnable,
				createDT = data.CreateDT,
				creator = data.Creator,
				updateDT = data.UpdateDT,
				updator = data.Updator,
                updatorName = data.UpdatorName
            };
            
            return new BasicResponse<MessageResponse>()
            { resultCode = "U200", resultDescription = "查詢成功", data = result };
        }

        public async ValueTask<BasicResponse<List<MessageResponse>>> QueryMessage(MessageQueryRequest request)
        {
			if (string.IsNullOrEmpty(request.messageSheetID)) { request.messageSheetID = string.Empty; }
			if (string.IsNullOrEmpty(request.messageTemplateID)) { request.messageTemplateID = string.Empty; }
			if (string.IsNullOrEmpty(request.messageName)) { request.messageName = string.Empty; }
            

            var data = await _dataAccessService
                .LoadData<TbMessage, object>(storeProcedure: "agdSp.uspMessageQuery", request);
                
            if (data.Count()==0) return new BasicResponse<List<MessageResponse>>()
            { resultCode = "U200", resultDescription = "查無資料", data = null };

            var result = data.Select(item => new MessageResponse
            {
				seqNo = item.SeqNo,
				messageSheetID = item.MessageSheetID,
				messageTemplateID = item.MessageTemplateID,
				messageName = item.MessageName,
				displayOrder = item.DisplayOrder,
				isEnable = item.IsEnable,
				createDT = item.CreateDT,
				creator = item.Creator,
				updateDT = item.UpdateDT,
				updator = item.Updator,            
                updatorName = item.UpdatorName   
            }).ToList();

            int totalCount = data.FirstOrDefault().Total;

            return new BasicResponse<List<MessageResponse>>()
            { resultCode = "U200", resultDescription = "查詢成功", data = result, total=totalCount };
        }

        public async ValueTask<BasicResponse<bool>> InsertMessage(MessageInsertRequest request)
        {
            var creator = _getTokenService.userID ?? "";
            
            var exists = await Exists(0, request.messageSheetID);
            
            if (exists.data == true) return new BasicResponse<bool>()
            { resultCode = "U999", resultDescription = "資料重複，請重新設定", data=false };
                        
            request.creator = creator;

            var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.uspMessageInsert", request);

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "U999", resultDescription = "新增失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "U200", resultDescription = "新增成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> UpdateMessage(MessageUpdateRequest request)
        {
            var updator = _getTokenService.userID ?? "";

            var exists = await Exists(request.seqNo, request.messageSheetID);
            
            if (exists.data == true) return new BasicResponse<bool>()
            { resultCode = "U999", resultDescription = "資料重複，請重新設定", data = false };

            request.updator = updator;            

            var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.uspMessageUpdate", request);

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "U999", resultDescription = "更新失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "U200", resultDescription = "更新成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> DeleteMessage(int seqNo)
        {
            var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.uspMessageDelete", new { seqNo = seqNo });

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "U999", resultDescription = "刪除失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "U200", resultDescription = "刪除成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> Exists(int seqNo,string messageSheetID)
        {
            var data = await _dataAccessService
                .LoadSingleData<int, object>(storeProcedure: "agdSp.uspMessageExists", new
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