using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.AuxCode.Contract;
using ESUN.AGD.DataAccess.DataService.DataAccess;


namespace ESUN.AGD.WebApi.Application.AuxCode
{
    public class AuxCodeService : IAuxCodeService
    {

        private readonly IDataAccessService _dataAccessService;
        private readonly IGetTokenService _getTokenService;
        

        public AuxCodeService(IDataAccessService dataAccessService , IGetTokenService getTokenService)
        {
            _dataAccessService = dataAccessService;
            _getTokenService = getTokenService;
        }

        public async ValueTask<BasicResponse<AuxCodeResponse>> GetAuxCode(int seqNo)
        {
            var data = await _dataAccessService
                .LoadSingleData<TbAuxCode, object>(storeProcedure: "agdSp.uspAuxCodeGet", new { seqNo = seqNo, });
            
            if (data == null) return new BasicResponse<AuxCodeResponse>()
            { resultCode = "U999", resultDescription = "查無資料", data = null };
            
            var result = new AuxCodeResponse
            {
				seqNo = data.SeqNo,
				auxID = data.AuxID,
				auxName = data.AuxName,
				isLongTimeAux = data.IsLongTimeAux,
				displayOrder = data.DisplayOrder,
				createDT = data.CreateDT,
				creator = data.Creator,
				updateDT = data.UpdateDT,
				updator = data.Updator,
                updatorName = data.UpdatorName
            };
            
            return new BasicResponse<AuxCodeResponse>()
            { resultCode = "U200", resultDescription = "查詢成功", data = result };
        }

        public async ValueTask<BasicResponse<List<AuxCodeResponse>>> QueryAuxCode(AuxCodeQueryRequest request)
        {
			if (string.IsNullOrEmpty(request.auxID)) { request.auxID = string.Empty; }
			if (string.IsNullOrEmpty(request.auxName)) { request.auxName = string.Empty; }
            

            var data = await _dataAccessService
                .LoadData<TbAuxCode, object>(storeProcedure: "agdSp.uspAuxCodeQuery", request);
                
            if (data.Count()==0) return new BasicResponse<List<AuxCodeResponse>>()
            { resultCode = "U200", resultDescription = "查無資料", data = null };

            var result = data.Select(item => new AuxCodeResponse
            {
				seqNo = item.SeqNo,
				auxID = item.AuxID,
				auxName = item.AuxName,
				isLongTimeAux = item.IsLongTimeAux,
				displayOrder = item.DisplayOrder,
				createDT = item.CreateDT,
				creator = item.Creator,
				updateDT = item.UpdateDT,
				updator = item.Updator,            
                updatorName = item.UpdatorName   
            }).ToList();

            int totalCount = data.FirstOrDefault().Total;

            return new BasicResponse<List<AuxCodeResponse>>()
            { resultCode = "U200", resultDescription = "查詢成功", data = result, total=totalCount };
        }

        public async ValueTask<BasicResponse<bool>> InsertAuxCode(AuxCodeInsertRequest request)
        {
            var creator = _getTokenService.userID ?? "";
            
            var exists = await Exists(0, request.auxID, request.auxName);
            
            if (exists.data == true) return new BasicResponse<bool>()
            { resultCode = "U999", resultDescription = "資料重複，請重新設定", data=false };
                        
            request.creator = creator;

            var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.uspAuxCodeInsert", request);

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "U999", resultDescription = "新增失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "U200", resultDescription = "新增成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> UpdateAuxCode(AuxCodeUpdateRequest request)
        {
            var updator = _getTokenService.userID ?? "";

            var exists = await Exists(request.seqNo, request.auxID, request.auxName);
            
            if (exists.data == true) return new BasicResponse<bool>()
            { resultCode = "U999", resultDescription = "資料重複，請重新設定", data = false };

            request.updator = updator;            

            var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.uspAuxCodeUpdate", request);

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "U999", resultDescription = "更新失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "U200", resultDescription = "更新成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> DeleteAuxCode(int seqNo)
        {
            var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.uspAuxCodeDelete", new { seqNo = seqNo });

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "U999", resultDescription = "刪除失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "U200", resultDescription = "刪除成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> Exists(int seqNo,string auxID,string auxName)
        {
            var data = await _dataAccessService
                .LoadSingleData<int, object>(storeProcedure: "agdSp.uspAuxCodeExists", new
                {
					SeqNo = seqNo,
					AuxID = auxID,
					AuxName = auxName,               
                });

            if (data == 0) return new BasicResponse<bool>()
            { resultCode = "U999", resultDescription = "資料重複", data = false };

            return new BasicResponse<bool>()
            { resultCode = "U200", resultDescription = "資料正常", data = true };
        }
    }
}