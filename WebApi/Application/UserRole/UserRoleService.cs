using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.UserRole.Contract;
using ESUN.AGD.DataAccess.DataService.DataAccess;


namespace ESUN.AGD.WebApi.Application.UserRole
{
    public class UserRoleService : IUserRoleService
    {

        private readonly IDataAccessService _dataAccessService;
        private readonly IGetTokenService _getTokenService;
        

        public UserRoleService(IDataAccessService dataAccessService , IGetTokenService getTokenService)
        {
            _dataAccessService = dataAccessService;
            _getTokenService = getTokenService;
        }

        public async ValueTask<BasicResponse<UserRoleResponse>> GetUserRole(int seqNo)
        {
            var data = await _dataAccessService
                .LoadSingleData<TbUserRole, object>(storeProcedure: "agdSp.uspUserRoleGet", new { seqNo = seqNo, });
            
            if (data == null) return new BasicResponse<UserRoleResponse>()
            { resultCode = "U999", resultDescription = "查無資料", data = null };
            
            var result = new UserRoleResponse
            {
				seqNo = data.SeqNo,
				userID = data.UserID,
				roleID = data.RoleID,
				createDT = data.CreateDT,
				creator = data.Creator,
				updateDT = data.UpdateDT,
				updator = data.Updator,
                updatorName = data.UpdatorName
            };
            
            return new BasicResponse<UserRoleResponse>()
            { resultCode = "U200", resultDescription = "查詢成功", data = result };
        }

        public async ValueTask<BasicResponse<List<UserRoleResponse>>> QueryUserRole(UserRoleQueryRequest request)
        {
			if (string.IsNullOrEmpty(request.userID)) { request.userID = string.Empty; }
			if (string.IsNullOrEmpty(request.roleID)) { request.roleID = string.Empty; }
            

            var data = await _dataAccessService
                .LoadData<TbUserRole, object>(storeProcedure: "agdSp.uspUserRoleQuery", request);
                
            if (data.Count()==0) return new BasicResponse<List<UserRoleResponse>>()
            { resultCode = "U200", resultDescription = "查無資料", data = null };

            var result = data.Select(item => new UserRoleResponse
            {
				seqNo = item.SeqNo,
				userID = item.UserID,
				roleID = item.RoleID,
				createDT = item.CreateDT,
				creator = item.Creator,
				updateDT = item.UpdateDT,
				updator = item.Updator,            
                updatorName = item.UpdatorName   
            }).ToList();

            int totalCount = data.FirstOrDefault().Total;

            return new BasicResponse<List<UserRoleResponse>>()
            { resultCode = "U200", resultDescription = "查詢成功", data = result, total=totalCount };
        }

        public async ValueTask<BasicResponse<bool>> InsertUserRole(UserRoleInsertRequest request)
        {
            var creator = _getTokenService.userID ?? "";
            
            var exists = await Exists(0, request.userID, request.roleID);
            
            if (exists.data == true) return new BasicResponse<bool>()
            { resultCode = "U999", resultDescription = "資料重複，請重新設定", data=false };
                        
            request.creator = creator;

            var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.uspUserRoleInsert", request);

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "U999", resultDescription = "新增失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "U200", resultDescription = "新增成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> UpdateUserRole(UserRoleUpdateRequest request)
        {
            var updator = _getTokenService.userID ?? "";

            var exists = await Exists(request.seqNo, request.userID, request.roleID);
            
            if (exists.data == true) return new BasicResponse<bool>()
            { resultCode = "U999", resultDescription = "資料重複，請重新設定", data = false };

            request.updator = updator;            

            var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.uspUserRoleUpdate", request);

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "U999", resultDescription = "更新失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "U200", resultDescription = "更新成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> DeleteUserRole(int seqNo)
        {
            var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.uspUserRoleDelete", new { seqNo = seqNo });

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "U999", resultDescription = "刪除失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "U200", resultDescription = "刪除成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> Exists(int seqNo,string userID,string roleID)
        {
            var data = await _dataAccessService
                .LoadSingleData<int, object>(storeProcedure: "agdSp.uspUserRoleExists", new
                {
					SeqNo = seqNo,
					UserID = userID,
					RoleID = roleID,               
                });

            if (data == 0) return new BasicResponse<bool>()
            { resultCode = "U999", resultDescription = "資料重複", data = false };

            return new BasicResponse<bool>()
            { resultCode = "U200", resultDescription = "資料正常", data = true };
        }
    }
}