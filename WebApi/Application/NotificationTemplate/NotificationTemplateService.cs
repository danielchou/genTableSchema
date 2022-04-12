using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.NotificationTemplate.Contract;
using ESUN.AGD.DataAccess.DataService.DataAccess;


namespace ESUN.AGD.WebApi.Application.NotificationTemplate
{
    public class NotificationTemplateService : INotificationTemplateService
    {

        private readonly IDataAccessService _dataAccessService;
        private readonly IGetTokenService _getTokenService;
        

        public NotificationTemplateService(IDataAccessService dataAccessService , IGetTokenService getTokenService)
        {
            _dataAccessService = dataAccessService;
            _getTokenService = getTokenService;
        }

        public async ValueTask<BasicResponse<NotificationTemplateResponse>> GetNotificationTemplate(int seqNo)
        {
            var data = await _dataAccessService
                .LoadSingleData<TbNotificationTemplate, object>(storeProcedure: "agdSp.uspNotificationTemplateGet", new { seqNo = seqNo, });
            
            if (data == null) return new BasicResponse<NotificationTemplateResponse>()
            { resultCode = "U999", resultDescription = "查無資料", data = null };
            
            var result = new NotificationTemplateResponse
            {
				seqNo = data.SeqNo,
				notificationType = data.NotificationType,
				notificationID = data.NotificationID,
				notificationName = data.NotificationName,
				content = data.Content,
				displayOrder = data.DisplayOrder,
				isEnable = data.IsEnable,
				createDT = data.CreateDT,
				creator = data.Creator,
				updateDT = data.UpdateDT,
				updator = data.Updator,
                updatorName = data.UpdatorName
            };
            
            return new BasicResponse<NotificationTemplateResponse>()
            { resultCode = "U200", resultDescription = "查詢成功", data = result };
        }

        public async ValueTask<BasicResponse<List<NotificationTemplateResponse>>> QueryNotificationTemplate(NotificationTemplateQueryRequest request)
        {
			if (string.IsNullOrEmpty(request.notificationType)) { request.notificationType = string.Empty; }
			if (string.IsNullOrEmpty(request.notificationID)) { request.notificationID = string.Empty; }
			if (string.IsNullOrEmpty(request.notificationName)) { request.notificationName = string.Empty; }
            

            var data = await _dataAccessService
                .LoadData<TbNotificationTemplate, object>(storeProcedure: "agdSp.uspNotificationTemplateQuery", request);
                
            if (data.Count()==0) return new BasicResponse<List<NotificationTemplateResponse>>()
            { resultCode = "U200", resultDescription = "查無資料", data = null };

            var result = data.Select(item => new NotificationTemplateResponse
            {
				seqNo = item.SeqNo,
				notificationType = item.NotificationType,
				notificationID = item.NotificationID,
				notificationName = item.NotificationName,
				content = item.Content,
				displayOrder = item.DisplayOrder,
				isEnable = item.IsEnable,
				createDT = item.CreateDT,
				creator = item.Creator,
				updateDT = item.UpdateDT,
				updator = item.Updator,            
                updatorName = item.UpdatorName   
            }).ToList();

            int totalCount = data.FirstOrDefault().Total;

            return new BasicResponse<List<NotificationTemplateResponse>>()
            { resultCode = "U200", resultDescription = "查詢成功", data = result, total=totalCount };
        }

        public async ValueTask<BasicResponse<bool>> InsertNotificationTemplate(NotificationTemplateInsertRequest request)
        {
            var creator = _getTokenService.userID ?? "";
            
            var exists = await Exists(0, request.notificationID);
            
            if (exists.data == true) return new BasicResponse<bool>()
            { resultCode = "U999", resultDescription = "資料重複，請重新設定", data=false };
                        
            request.creator = creator;

            var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.uspNotificationTemplateInsert", request);

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "U999", resultDescription = "新增失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "U200", resultDescription = "新增成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> UpdateNotificationTemplate(NotificationTemplateUpdateRequest request)
        {
            var updator = _getTokenService.userID ?? "";

            var exists = await Exists(request.seqNo, request.notificationID);
            
            if (exists.data == true) return new BasicResponse<bool>()
            { resultCode = "U999", resultDescription = "資料重複，請重新設定", data = false };

            request.updator = updator;            

            var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.uspNotificationTemplateUpdate", request);

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "U999", resultDescription = "更新失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "U200", resultDescription = "更新成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> DeleteNotificationTemplate(int seqNo)
        {
            var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.uspNotificationTemplateDelete", new { seqNo = seqNo });

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "U999", resultDescription = "刪除失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "U200", resultDescription = "刪除成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> Exists(int seqNo,string notificationID)
        {
            var data = await _dataAccessService
                .LoadSingleData<int, object>(storeProcedure: "agdSp.uspNotificationTemplateExists", new
                {
					SeqNo = seqNo,
					NotificationID = notificationID,               
                });

            if (data == 0) return new BasicResponse<bool>()
            { resultCode = "U999", resultDescription = "資料重複", data = false };

            return new BasicResponse<bool>()
            { resultCode = "U200", resultDescription = "資料正常", data = true };
        }
    }
}