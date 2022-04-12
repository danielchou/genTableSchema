using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.Func.Contract;
using ESUN.AGD.DataAccess.DataService.DataAccess;


namespace ESUN.AGD.WebApi.Application.Func
{
    public class FuncService : IFuncService
    {

        private readonly IDataAccessService _dataAccessService;
        private readonly IGetTokenService _getTokenService;
        

        public FuncService(IDataAccessService dataAccessService , IGetTokenService getTokenService)
        {
            _dataAccessService = dataAccessService;
            _getTokenService = getTokenService;
        }

        public async ValueTask<BasicResponse<FuncResponse>> GetFunc(int seqNo)
        {
            var data = await _dataAccessService
                .LoadSingleData<TbFunc, object>(storeProcedure: "agdSp.uspFuncGet", new { seqNo = seqNo, });
            
            if (data == null) return new BasicResponse<FuncResponse>()
            { resultCode = "U999", resultDescription = "查無資料", data = null };
            
            var result = new FuncResponse
            {
				seqNo = data.SeqNo,
				funcID = data.FuncID,
				funcName = data.FuncName,
				parentFuncID = data.ParentFuncID,
				level = data.Level,
				systemType = data.SystemType,
				iconName = data.IconName,
				routeName = data.RouteName,
				displayOrder = data.DisplayOrder,
				createDT = data.CreateDT,
				creator = data.Creator,
				updateDT = data.UpdateDT,
				updator = data.Updator,
                updatorName = data.UpdatorName
            };
            
            return new BasicResponse<FuncResponse>()
            { resultCode = "U200", resultDescription = "查詢成功", data = result };
        }

        public async ValueTask<BasicResponse<List<FuncResponse>>> QueryFunc(FuncQueryRequest request)
        {
			if (string.IsNullOrEmpty(request.funcID)) { request.funcID = string.Empty; }
			if (string.IsNullOrEmpty(request.funcName)) { request.funcName = string.Empty; }
            

            var data = await _dataAccessService
                .LoadData<TbFunc, object>(storeProcedure: "agdSp.uspFuncQuery", request);
                
            if (data.Count()==0) return new BasicResponse<List<FuncResponse>>()
            { resultCode = "U200", resultDescription = "查無資料", data = null };

            var result = data.Select(item => new FuncResponse
            {
				seqNo = item.SeqNo,
				funcID = item.FuncID,
				funcName = item.FuncName,
				parentFuncID = item.ParentFuncID,
				level = item.Level,
				systemType = item.SystemType,
				iconName = item.IconName,
				routeName = item.RouteName,
				displayOrder = item.DisplayOrder,
				createDT = item.CreateDT,
				creator = item.Creator,
				updateDT = item.UpdateDT,
				updator = item.Updator,            
                updatorName = item.UpdatorName   
            }).ToList();

            int totalCount = data.FirstOrDefault().Total;

            return new BasicResponse<List<FuncResponse>>()
            { resultCode = "U200", resultDescription = "查詢成功", data = result, total=totalCount };
        }

        public async ValueTask<BasicResponse<bool>> InsertFunc(FuncInsertRequest request)
        {
            var creator = _getTokenService.userID ?? "";
            
            var exists = await Exists(0, request.funcID, request.funcName);
            
            if (exists.data == true) return new BasicResponse<bool>()
            { resultCode = "U999", resultDescription = "資料重複，請重新設定", data=false };
                        
            request.creator = creator;

            var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.uspFuncInsert", request);

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "U999", resultDescription = "新增失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "U200", resultDescription = "新增成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> UpdateFunc(FuncUpdateRequest request)
        {
            var updator = _getTokenService.userID ?? "";

            var exists = await Exists(request.seqNo, request.funcID, request.funcName);
            
            if (exists.data == true) return new BasicResponse<bool>()
            { resultCode = "U999", resultDescription = "資料重複，請重新設定", data = false };

            request.updator = updator;            

            var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.uspFuncUpdate", request);

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "U999", resultDescription = "更新失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "U200", resultDescription = "更新成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> DeleteFunc(int seqNo)
        {
            var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.uspFuncDelete", new { seqNo = seqNo });

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "U999", resultDescription = "刪除失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "U200", resultDescription = "刪除成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> Exists(int seqNo,string funcID,string funcName)
        {
            var data = await _dataAccessService
                .LoadSingleData<int, object>(storeProcedure: "agdSp.uspFuncExists", new
                {
					SeqNo = seqNo,
					FuncID = funcID,
					FuncName = funcName,               
                });

            if (data == 0) return new BasicResponse<bool>()
            { resultCode = "U999", resultDescription = "資料重複", data = false };

            return new BasicResponse<bool>()
            { resultCode = "U200", resultDescription = "資料正常", data = true };
        }
    }
}