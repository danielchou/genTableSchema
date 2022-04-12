using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.RoleFunc.Contract;
using ESUN.AGD.DataAccess.DataService.DataAccess;


namespace ESUN.AGD.WebApi.Application.RoleFunc
{
    public class RoleFuncService : IRoleFuncService
    {

        private readonly IDataAccessService _dataAccessService;
        private readonly IGetTokenService _getTokenService;
        

        public RoleFuncService(IDataAccessService dataAccessService , IGetTokenService getTokenService)
        {
            _dataAccessService = dataAccessService;
            _getTokenService = getTokenService;
        }

        public async ValueTask<BasicResponse<RoleFuncResponse>> GetRoleFunc(int seqNo)
        {
            var data = await _dataAccessService
                .LoadSingleData<TbRoleFunc, object>(storeProcedure: "agdSp.uspRoleFuncGet", new { seqNo = seqNo, });
            
            if (data == null) return new BasicResponse<RoleFuncResponse>()
            { resultCode = "U999", resultDescription = "查無資料", data = null };
            
            var result = new RoleFuncResponse
            {
				seqNo = data.SeqNo,
				roleID = data.RoleID,
				funcID = data.FuncID,
				createDT = data.CreateDT,
				creator = data.Creator,
				updateDT = data.UpdateDT,
				updator = data.Updator,
                updatorName = data.UpdatorName
            };
            
            return new BasicResponse<RoleFuncResponse>()
            { resultCode = "U200", resultDescription = "查詢成功", data = result };
        }

        public async ValueTask<BasicResponse<List<RoleFuncResponse>>> QueryRoleFunc(RoleFuncQueryRequest request)
        {
			if (string.IsNullOrEmpty(request.roleID)) { request.roleID = string.Empty; }
			if (string.IsNullOrEmpty(request.funcID)) { request.funcID = string.Empty; }
            

            var data = await _dataAccessService
                .LoadData<TbRoleFunc, object>(storeProcedure: "agdSp.uspRoleFuncQuery", request);
                
            if (data.Count()==0) return new BasicResponse<List<RoleFuncResponse>>()
            { resultCode = "U200", resultDescription = "查無資料", data = null };

            var result = data.Select(item => new RoleFuncResponse
            {
				seqNo = item.SeqNo,
				roleID = item.RoleID,
				funcID = item.FuncID,
				createDT = item.CreateDT,
				creator = item.Creator,
				updateDT = item.UpdateDT,
				updator = item.Updator,            
                updatorName = item.UpdatorName   
            }).ToList();

            int totalCount = data.FirstOrDefault().Total;

            return new BasicResponse<List<RoleFuncResponse>>()
            { resultCode = "U200", resultDescription = "查詢成功", data = result, total=totalCount };
        }

        public async ValueTask<BasicResponse<bool>> InsertRoleFunc(RoleFuncInsertRequest request)
        {
            var creator = _getTokenService.userID ?? "";
            
            var exists = await Exists(0, request.roleID, request.funcID);
            
            if (exists.data == true) return new BasicResponse<bool>()
            { resultCode = "U999", resultDescription = "資料重複，請重新設定", data=false };
                        
            request.creator = creator;

            var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.uspRoleFuncInsert", request);

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "U999", resultDescription = "新增失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "U200", resultDescription = "新增成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> UpdateRoleFunc(RoleFuncUpdateRequest request)
        {
            var updator = _getTokenService.userID ?? "";

            var exists = await Exists(request.seqNo, request.roleID, request.funcID);
            
            if (exists.data == true) return new BasicResponse<bool>()
            { resultCode = "U999", resultDescription = "資料重複，請重新設定", data = false };

            request.updator = updator;            

            var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.uspRoleFuncUpdate", request);

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "U999", resultDescription = "更新失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "U200", resultDescription = "更新成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> DeleteRoleFunc(int seqNo)
        {
            var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.uspRoleFuncDelete", new { seqNo = seqNo });

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "U999", resultDescription = "刪除失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "U200", resultDescription = "刪除成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> Exists(int seqNo,string roleID,string funcID)
        {
            var data = await _dataAccessService
                .LoadSingleData<int, object>(storeProcedure: "agdSp.uspRoleFuncExists", new
                {
					SeqNo = seqNo,
					RoleID = roleID,
					FuncID = funcID,               
                });

            if (data == 0) return new BasicResponse<bool>()
            { resultCode = "U999", resultDescription = "資料重複", data = false };

            return new BasicResponse<bool>()
            { resultCode = "U200", resultDescription = "資料正常", data = true };
        }
    }
}