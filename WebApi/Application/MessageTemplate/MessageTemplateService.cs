using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.MessageTemplate.Contract;
using ESUN.AGD.DataAccess.DataService.DataAccess;


namespace ESUN.AGD.WebApi.Application.MessageTemplate
{
    public class MessageTemplateService : IMessageTemplateService
    {

        private readonly IDataAccessService _dataAccessService;
        private readonly IGetTokenService _getTokenService;
        

        public MessageTemplateService(IDataAccessService dataAccessService , IGetTokenService getTokenService)
        {
            _dataAccessService = dataAccessService;
            _getTokenService = getTokenService;
        }

        public async ValueTask<BasicResponse<MessageTemplateResponse>> GetMessageTemplate(int seqNo)
        {
            var data = await _dataAccessService
                .LoadSingleData<TbMessageTemplate, object>(storeProcedure: "agdSp.uspMessageTemplateGet", new { seqNo = seqNo, });
            
            if (data == null) return new BasicResponse<MessageTemplateResponse>()
            { resultCode = "U999", resultDescription = "查無資料", data = null };
            
            var result = new MessageTemplateResponse
            {
				seqNo = data.SeqNo,
				messageTemplateID = data.MessageTemplateID,
				messageTemplateName = data.MessageTemplateName,
				messageB08Code = data.MessageB08Code,
				content = data.Content,
				isEnable = data.IsEnable,
				createDT = data.CreateDT,
				creator = data.Creator,
				updateDT = data.UpdateDT,
				updator = data.Updator,
                updatorName = data.UpdatorName
            };
            
            return new BasicResponse<MessageTemplateResponse>()
            { resultCode = "U200", resultDescription = "查詢成功", data = result };
        }

        public async ValueTask<BasicResponse<List<MessageTemplateResponse>>> QueryMessageTemplate(MessageTemplateQueryRequest request)
        {
			if (string.IsNullOrEmpty(request.messageTemplateID)) { request.messageTemplateID = string.Empty; }
			if (string.IsNullOrEmpty(request.messageTemplateName)) { request.messageTemplateName = string.Empty; }
			if (string.IsNullOrEmpty(request.messageB08Code)) { request.messageB08Code = string.Empty; }
            

            var data = await _dataAccessService
                .LoadData<TbMessageTemplate, object>(storeProcedure: "agdSp.uspMessageTemplateQuery", request);
                
            if (data.Count()==0) return new BasicResponse<List<MessageTemplateResponse>>()
            { resultCode = "U200", resultDescription = "查無資料", data = null };

            var result = data.Select(item => new MessageTemplateResponse
            {
				seqNo = item.SeqNo,
				messageTemplateID = item.MessageTemplateID,
				messageTemplateName = item.MessageTemplateName,
				messageB08Code = item.MessageB08Code,
				content = item.Content,
				isEnable = item.IsEnable,
				createDT = item.CreateDT,
				creator = item.Creator,
				updateDT = item.UpdateDT,
				updator = item.Updator,            
                updatorName = item.UpdatorName   
            }).ToList();

            int totalCount = data.FirstOrDefault().Total;

            return new BasicResponse<List<MessageTemplateResponse>>()
            { resultCode = "U200", resultDescription = "查詢成功", data = result, total=totalCount };
        }

        public async ValueTask<BasicResponse<bool>> InsertMessageTemplate(MessageTemplateInsertRequest request)
        {
            var creator = _getTokenService.userID ?? "";
            
            var exists = await Exists(0, request.messageTemplateID);
            
            if (exists.data == true) return new BasicResponse<bool>()
            { resultCode = "U999", resultDescription = "資料重複，請重新設定", data=false };
                        
            request.creator = creator;

            var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.uspMessageTemplateInsert", request);

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "U999", resultDescription = "新增失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "U200", resultDescription = "新增成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> UpdateMessageTemplate(MessageTemplateUpdateRequest request)
        {
            var updator = _getTokenService.userID ?? "";

            var exists = await Exists(request.seqNo, request.messageTemplateID);
            
            if (exists.data == true) return new BasicResponse<bool>()
            { resultCode = "U999", resultDescription = "資料重複，請重新設定", data = false };

            request.updator = updator;            

            var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.uspMessageTemplateUpdate", request);

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "U999", resultDescription = "更新失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "U200", resultDescription = "更新成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> DeleteMessageTemplate(int seqNo)
        {
            var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.uspMessageTemplateDelete", new { seqNo = seqNo });

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "U999", resultDescription = "刪除失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "U200", resultDescription = "刪除成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> Exists(int seqNo,string messageTemplateID)
        {
            var data = await _dataAccessService
                .LoadSingleData<int, object>(storeProcedure: "agdSp.uspMessageTemplateExists", new
                {
					SeqNo = seqNo,
					MessageTemplateID = messageTemplateID,               
                });

            if (data == 0) return new BasicResponse<bool>()
            { resultCode = "U999", resultDescription = "資料重複", data = false };

            return new BasicResponse<bool>()
            { resultCode = "U200", resultDescription = "資料正常", data = true };
        }
    }
}