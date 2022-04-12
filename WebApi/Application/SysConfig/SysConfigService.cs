using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.SysConfig.Contract;
using ESUN.AGD.DataAccess.DataService.DataAccess;


namespace ESUN.AGD.WebApi.Application.SysConfig
{
    public class SysConfigService : ISysConfigService
    {

        private readonly IDataAccessService _dataAccessService;
        private readonly IGetTokenService _getTokenService;
        

        public SysConfigService(IDataAccessService dataAccessService , IGetTokenService getTokenService)
        {
            _dataAccessService = dataAccessService;
            _getTokenService = getTokenService;
        }

        public async ValueTask<BasicResponse<SysConfigResponse>> GetSysConfig(int seqNo)
        {
            var data = await _dataAccessService
                .LoadSingleData<TbSysConfig, object>(storeProcedure: "agdSp.uspSysConfigGet", new { seqNo = seqNo, });
            
            if (data == null) return new BasicResponse<SysConfigResponse>()
            { resultCode = "U999", resultDescription = "查無資料", data = null };
            
            var result = new SysConfigResponse
            {
				seqNo = data.SeqNo,
				sysConfigType = data.SysConfigType,
				sysConfigID = data.SysConfigID,
				sysConfigName = data.SysConfigName,
				content = data.Content,
				isVisible = data.IsVisible,
				createDT = data.CreateDT,
				creator = data.Creator,
				updateDT = data.UpdateDT,
				updator = data.Updator,
                updatorName = data.UpdatorName
            };
            
            return new BasicResponse<SysConfigResponse>()
            { resultCode = "U200", resultDescription = "查詢成功", data = result };
        }

        public async ValueTask<BasicResponse<List<SysConfigResponse>>> QuerySysConfig(SysConfigQueryRequest request)
        {
			if (string.IsNullOrEmpty(request.sysConfigType)) { request.sysConfigType = string.Empty; }
			if (string.IsNullOrEmpty(request.sysConfigID)) { request.sysConfigID = string.Empty; }
			if (string.IsNullOrEmpty(request.sysConfigName)) { request.sysConfigName = string.Empty; }
            

            var data = await _dataAccessService
                .LoadData<TbSysConfig, object>(storeProcedure: "agdSp.uspSysConfigQuery", request);
                
            if (data.Count()==0) return new BasicResponse<List<SysConfigResponse>>()
            { resultCode = "U200", resultDescription = "查無資料", data = null };

            var result = data.Select(item => new SysConfigResponse
            {
				seqNo = item.SeqNo,
				sysConfigType = item.SysConfigType,
				sysConfigID = item.SysConfigID,
				sysConfigName = item.SysConfigName,
				content = item.Content,
				isVisible = item.IsVisible,
				createDT = item.CreateDT,
				creator = item.Creator,
				updateDT = item.UpdateDT,
				updator = item.Updator,            
                updatorName = item.UpdatorName   
            }).ToList();

            int totalCount = data.FirstOrDefault().Total;

            return new BasicResponse<List<SysConfigResponse>>()
            { resultCode = "U200", resultDescription = "查詢成功", data = result, total=totalCount };
        }

        public async ValueTask<BasicResponse<bool>> InsertSysConfig(SysConfigInsertRequest request)
        {
            var creator = _getTokenService.userID ?? "";
            
            var exists = await Exists(0, request.sysConfigID);
            
            if (exists.data == true) return new BasicResponse<bool>()
            { resultCode = "U999", resultDescription = "資料重複，請重新設定", data=false };
                        
            request.creator = creator;

            var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.uspSysConfigInsert", request);

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "U999", resultDescription = "新增失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "U200", resultDescription = "新增成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> UpdateSysConfig(SysConfigUpdateRequest request)
        {
            var updator = _getTokenService.userID ?? "";

            var exists = await Exists(request.seqNo, request.sysConfigID);
            
            if (exists.data == true) return new BasicResponse<bool>()
            { resultCode = "U999", resultDescription = "資料重複，請重新設定", data = false };

            request.updator = updator;            

            var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.uspSysConfigUpdate", request);

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "U999", resultDescription = "更新失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "U200", resultDescription = "更新成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> DeleteSysConfig(int seqNo)
        {
            var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.uspSysConfigDelete", new { seqNo = seqNo });

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "U999", resultDescription = "刪除失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "U200", resultDescription = "刪除成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> Exists(int seqNo,string sysConfigID)
        {
            var data = await _dataAccessService
                .LoadSingleData<int, object>(storeProcedure: "agdSp.uspSysConfigExists", new
                {
					SeqNo = seqNo,
					SysConfigID = sysConfigID,               
                });

            if (data == 0) return new BasicResponse<bool>()
            { resultCode = "U999", resultDescription = "資料重複", data = false };

            return new BasicResponse<bool>()
            { resultCode = "U200", resultDescription = "資料正常", data = true };
        }
    }
}