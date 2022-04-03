using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.Role.Contract;
using ESUN.AGD.DataAccess.DataService.DataAccess;


namespace ESUN.AGD.WebApi.Application.Role
{
    public class RoleService : IRoleService
    {

        private readonly IDataAccessService _dataAccessService;
        private readonly IGetTokenService _getTokenService;
        

        public RoleService(IDataAccessService dataAccessService , IGetTokenService getTokenService)
        {
            _dataAccessService = dataAccessService;
            _getTokenService = getTokenService;
        }

        public async ValueTask<BasicResponse<RoleResponse>> GetRole(int seqNo)
        {
            var data = await _dataAccessService
                .LoadSingData<TbRole, object>(storeProcedure: "agdSp.uspRoleGet", new { seqNo = seqNo, });
            
            if (data == null) return new BasicResponse<RoleResponse>()
            { resultCode = "9999", resultDescription = "查無資料", data = null };
            
            var result = new RoleResponse
            {
				seqNo = data.SeqNo,
				roleId = data.RoleId,
				roleName = data.RoleName,
				isEnable = data.IsEnable,
				creator = data.Creator,
				updator = data.Updator,
				createDt = data.CreateDt,
				updateDt = data.UpdateDt,
                updatorName = data.UpdatorName
            };
            
            return new BasicResponse<RoleResponse>()
            { resultCode = "0000", resultDescription = "查詢成功", data = result };
        }

        public async ValueTask<BasicResponse<List<RoleResponse>>> QueryRole(RoleQueryRequest request)
        {

            if (string.IsNullOrEmpty(request.extCode)) { request.extCode = string.Empty; }            
            if (string.IsNullOrEmpty(request.computerName)) { request.computerName = string.Empty; }            

            var data = await _dataAccessService
                .LoadData<TbRole, object>(storeProcedure: "agdSp.uspRoleQuery", request);
            if (data.Count()==0) return new BasicResponse<List<RoleResponse>>()
            { resultCode = "0000", resultDescription = "查無資料", data = null };

            var result = data.Select(item => new RoleResponse
            {
				seqNo = item.SeqNo,
				roleId = item.RoleId,
				roleName = item.RoleName,
				isEnable = item.IsEnable,
				creator = item.Creator,
				updator = item.Updator,
				createDt = item.CreateDt,
				updateDt = item.UpdateDt,            
                updatorName = item.UpdatorName   
            }).ToList();

            int totalCount = data.FirstOrDefault().Total;

            return new BasicResponse<List<RoleResponse>>()
            { resultCode = "0000", resultDescription = "查詢成功", data = result, total=totalCount };
        }

        public async ValueTask<BasicResponse<bool>> InsertRole(RoleInsertRequest request)
        {
           
            var creator = _getTokenService.userId ?? "";

            var exists = await Exists(0, request.extCode, request.computerIp);
            
            if (exists.data == true) return new BasicResponse<bool>()
            { resultCode = "9999", resultDescription = "資料重複，請重新設定", data=false };
                        
            request.creator = creator;

            var data = await _dataAccessService
                .OpreatData(storeProcedure: "agdSp.uspRoleInsert", request);

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "9999", resultDescription = "新增失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "0000", resultDescription = "新增成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> UpdateRole(RoleUpdateRequest request)
        {
            var updator = _getTokenService.userId ?? "";

            var exists = await Exists(request.seqNo, request.extCode, request.computerIp);
            
            if (exists.data == true) return new BasicResponse<bool>()
            { resultCode = "9999", resultDescription = "資料重複，請重新設定", data = false };

            request.updator = updator;            

            var data = await _dataAccessService
                .OpreatData(storeProcedure: "agdSp.uspRoleUpdate", request);

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "9999", resultDescription = "更新失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "0000", resultDescription = "更新成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> DeleteRole(int seqNo)
        {
            var data = await _dataAccessService
                .OpreatData(storeProcedure: "agdSp.uspRoleDelete", new { seqNo = seqNo });

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "9999", resultDescription = "刪除失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "0000", resultDescription = "刪除成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> Exists(int seqNo,string roleId,string roleName)
        {
            var exist = await _dataAccessService
                .LoadSingData<int, object>(storeProcedure: "agdSp.uspRoleExists", new
                {
					SeqNo = seqNo,
					RoleId = roleId,
					RoleName = roleName,               
                });

            if (exist == 0) return new BasicResponse<bool>()
            { resultCode = "9999", resultDescription = "資料重複", data = false };

            return new BasicResponse<bool>()
            { resultCode = "0000", resultDescription = "資料正常", data = true };
        }
    }
}