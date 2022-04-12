using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.Reason.Contract;
using ESUN.AGD.DataAccess.DataService.DataAccess;


namespace ESUN.AGD.WebApi.Application.Reason
{
    public class ReasonService : IReasonService
    {

        private readonly IDataAccessService _dataAccessService;
        private readonly IGetTokenService _getTokenService;
        

        public ReasonService(IDataAccessService dataAccessService , IGetTokenService getTokenService)
        {
            _dataAccessService = dataAccessService;
            _getTokenService = getTokenService;
        }

        public async ValueTask<BasicResponse<ReasonResponse>> GetReason(int seqNo)
        {
            var data = await _dataAccessService
                .LoadSingleData<TbReason, object>(storeProcedure: "agdSp.uspReasonGet", new { seqNo = seqNo, });
            
            if (data == null) return new BasicResponse<ReasonResponse>()
            { resultCode = "U999", resultDescription = "查無資料", data = null };
            
            var result = new ReasonResponse
            {
				seqNo = data.SeqNo,
				reasonID = data.ReasonID,
				reasonName = data.ReasonName,
				parentReasonID = data.ParentReasonID,
				level = data.Level,
				bussinessUnit = data.BussinessUnit,
				bussinessB03Type = data.BussinessB03Type,
				reviewType = data.ReviewType,
				memo = data.Memo,
				webUrl = data.WebUrl,
				kMUrl = data.KMUrl,
				displayOrder = data.DisplayOrder,
				isUsually = data.IsUsually,
				usuallyReasonName = data.UsuallyReasonName,
				usuallyDisplayOrder = data.UsuallyDisplayOrder,
				isEnable = data.IsEnable,
				createDT = data.CreateDT,
				creator = data.Creator,
				updateDT = data.UpdateDT,
				updator = data.Updator,
                updatorName = data.UpdatorName
            };
            
            return new BasicResponse<ReasonResponse>()
            { resultCode = "U200", resultDescription = "查詢成功", data = result };
        }

        public async ValueTask<BasicResponse<List<ReasonResponse>>> QueryReason(ReasonQueryRequest request)
        {
			if (string.IsNullOrEmpty(request.reasonID)) { request.reasonID = string.Empty; }
			if (string.IsNullOrEmpty(request.reasonName)) { request.reasonName = string.Empty; }
			if (string.IsNullOrEmpty(request.parentReasonID)) { request.parentReasonID = string.Empty; }
			if (string.IsNullOrEmpty(request.level)) { request.level = string.Empty; }
            

            var data = await _dataAccessService
                .LoadData<TbReason, object>(storeProcedure: "agdSp.uspReasonQuery", request);
                
            if (data.Count()==0) return new BasicResponse<List<ReasonResponse>>()
            { resultCode = "U200", resultDescription = "查無資料", data = null };

            var result = data.Select(item => new ReasonResponse
            {
				seqNo = item.SeqNo,
				reasonID = item.ReasonID,
				reasonName = item.ReasonName,
				parentReasonID = item.ParentReasonID,
				level = item.Level,
				bussinessUnit = item.BussinessUnit,
				bussinessB03Type = item.BussinessB03Type,
				reviewType = item.ReviewType,
				memo = item.Memo,
				webUrl = item.WebUrl,
				kMUrl = item.KMUrl,
				displayOrder = item.DisplayOrder,
				isUsually = item.IsUsually,
				usuallyReasonName = item.UsuallyReasonName,
				usuallyDisplayOrder = item.UsuallyDisplayOrder,
				isEnable = item.IsEnable,
				createDT = item.CreateDT,
				creator = item.Creator,
				updateDT = item.UpdateDT,
				updator = item.Updator,            
                updatorName = item.UpdatorName   
            }).ToList();

            int totalCount = data.FirstOrDefault().Total;

            return new BasicResponse<List<ReasonResponse>>()
            { resultCode = "U200", resultDescription = "查詢成功", data = result, total=totalCount };
        }

        public async ValueTask<BasicResponse<bool>> InsertReason(ReasonInsertRequest request)
        {
            var creator = _getTokenService.userID ?? "";
            
            var exists = await Exists(0, request.reasonID, request.reasonName);
            
            if (exists.data == true) return new BasicResponse<bool>()
            { resultCode = "U999", resultDescription = "資料重複，請重新設定", data=false };
                        
            request.creator = creator;

            var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.uspReasonInsert", request);

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "U999", resultDescription = "新增失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "U200", resultDescription = "新增成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> UpdateReason(ReasonUpdateRequest request)
        {
            var updator = _getTokenService.userID ?? "";

            var exists = await Exists(request.seqNo, request.reasonID, request.reasonName);
            
            if (exists.data == true) return new BasicResponse<bool>()
            { resultCode = "U999", resultDescription = "資料重複，請重新設定", data = false };

            request.updator = updator;            

            var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.uspReasonUpdate", request);

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "U999", resultDescription = "更新失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "U200", resultDescription = "更新成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> DeleteReason(int seqNo)
        {
            var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.uspReasonDelete", new { seqNo = seqNo });

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "U999", resultDescription = "刪除失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "U200", resultDescription = "刪除成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> Exists(int seqNo,string reasonID,string reasonName)
        {
            var data = await _dataAccessService
                .LoadSingleData<int, object>(storeProcedure: "agdSp.uspReasonExists", new
                {
					SeqNo = seqNo,
					ReasonID = reasonID,
					ReasonName = reasonName,               
                });

            if (data == 0) return new BasicResponse<bool>()
            { resultCode = "U999", resultDescription = "資料重複", data = false };

            return new BasicResponse<bool>()
            { resultCode = "U200", resultDescription = "資料正常", data = true };
        }
    }
}