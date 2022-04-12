using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.User.Contract;
using ESUN.AGD.DataAccess.DataService.DataAccess;


namespace ESUN.AGD.WebApi.Application.User
{
    public class UserService : IUserService
    {

        private readonly IDataAccessService _dataAccessService;
        private readonly IGetTokenService _getTokenService;
        

        public UserService(IDataAccessService dataAccessService , IGetTokenService getTokenService)
        {
            _dataAccessService = dataAccessService;
            _getTokenService = getTokenService;
        }

        public async ValueTask<BasicResponse<UserResponse>> GetUser(int seqNo)
        {
            var data = await _dataAccessService
                .LoadSingleData<TbUser, object>(storeProcedure: "agdSp.uspUserGet", new { seqNo = seqNo, });
            
            if (data == null) return new BasicResponse<UserResponse>()
            { resultCode = "U999", resultDescription = "查無資料", data = null };
            
            var result = new UserResponse
            {
				seqNo = data.SeqNo,
				userID = data.UserID,
				userName = data.UserName,
				userCode = data.UserCode,
				agentLoginID = data.AgentLoginID,
				agentLoginCode = data.AgentLoginCode,
				employeeNo = data.EmployeeNo,
				nickName = data.NickName,
				empDept = data.EmpDept,
				groupID = data.GroupID,
				officeEmail = data.OfficeEmail,
				employedStatusCode = data.EmployedStatusCode,
				isSupervisor = data.IsSupervisor,
				b08Code1 = data.B08Code1,
				b08Code2 = data.B08Code2,
				b08Code3 = data.B08Code3,
				b08Code4 = data.B08Code4,
				b08Code5 = data.B08Code5,
				isEnable = data.IsEnable,
				createDT = data.CreateDT,
				creator = data.Creator,
				updateDT = data.UpdateDT,
				updator = data.Updator,
                updatorName = data.UpdatorName
            };
            
            return new BasicResponse<UserResponse>()
            { resultCode = "U200", resultDescription = "查詢成功", data = result };
        }

        public async ValueTask<BasicResponse<List<UserResponse>>> QueryUser(UserQueryRequest request)
        {
			if (string.IsNullOrEmpty(request.userID)) { request.userID = string.Empty; }
			if (string.IsNullOrEmpty(request.userName)) { request.userName = string.Empty; }
			if (string.IsNullOrEmpty(request.agentLoginID)) { request.agentLoginID = string.Empty; }
			if (string.IsNullOrEmpty(request.groupID)) { request.groupID = string.Empty; }
            

            var data = await _dataAccessService
                .LoadData<TbUser, object>(storeProcedure: "agdSp.uspUserQuery", request);
                
            if (data.Count()==0) return new BasicResponse<List<UserResponse>>()
            { resultCode = "U200", resultDescription = "查無資料", data = null };

            var result = data.Select(item => new UserResponse
            {
				seqNo = item.SeqNo,
				userID = item.UserID,
				userName = item.UserName,
				userCode = item.UserCode,
				agentLoginID = item.AgentLoginID,
				agentLoginCode = item.AgentLoginCode,
				employeeNo = item.EmployeeNo,
				nickName = item.NickName,
				empDept = item.EmpDept,
				groupID = item.GroupID,
				officeEmail = item.OfficeEmail,
				employedStatusCode = item.EmployedStatusCode,
				isSupervisor = item.IsSupervisor,
				b08Code1 = item.B08Code1,
				b08Code2 = item.B08Code2,
				b08Code3 = item.B08Code3,
				b08Code4 = item.B08Code4,
				b08Code5 = item.B08Code5,
				isEnable = item.IsEnable,
				createDT = item.CreateDT,
				creator = item.Creator,
				updateDT = item.UpdateDT,
				updator = item.Updator,            
                updatorName = item.UpdatorName   
            }).ToList();

            int totalCount = data.FirstOrDefault().Total;

            return new BasicResponse<List<UserResponse>>()
            { resultCode = "U200", resultDescription = "查詢成功", data = result, total=totalCount };
        }

        public async ValueTask<BasicResponse<bool>> InsertUser(UserInsertRequest request)
        {
            var creator = _getTokenService.userID ?? "";
            
            var exists = await Exists(0, request.userID, request.agentLoginID);
            
            if (exists.data == true) return new BasicResponse<bool>()
            { resultCode = "U999", resultDescription = "資料重複，請重新設定", data=false };
                        
            request.creator = creator;

            var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.uspUserInsert", request);

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "U999", resultDescription = "新增失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "U200", resultDescription = "新增成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> UpdateUser(UserUpdateRequest request)
        {
            var updator = _getTokenService.userID ?? "";

            var exists = await Exists(request.seqNo, request.userID, request.agentLoginID);
            
            if (exists.data == true) return new BasicResponse<bool>()
            { resultCode = "U999", resultDescription = "資料重複，請重新設定", data = false };

            request.updator = updator;            

            var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.uspUserUpdate", request);

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "U999", resultDescription = "更新失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "U200", resultDescription = "更新成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> DeleteUser(int seqNo)
        {
            var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.uspUserDelete", new { seqNo = seqNo });

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "U999", resultDescription = "刪除失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "U200", resultDescription = "刪除成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> Exists(int seqNo,string userID,string agentLoginID)
        {
            var data = await _dataAccessService
                .LoadSingleData<int, object>(storeProcedure: "agdSp.uspUserExists", new
                {
					SeqNo = seqNo,
					UserID = userID,
					AgentLoginID = agentLoginID,               
                });

            if (data == 0) return new BasicResponse<bool>()
            { resultCode = "U999", resultDescription = "資料重複", data = false };

            return new BasicResponse<bool>()
            { resultCode = "U200", resultDescription = "資料正常", data = true };
        }
    }
}