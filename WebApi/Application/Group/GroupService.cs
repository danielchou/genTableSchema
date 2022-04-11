using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.Group.Contract;
using ESUN.AGD.DataAccess.DataService.DataAccess;


namespace ESUN.AGD.WebApi.Application.Group
{
    public class GroupService : IGroupService
    {

        private readonly IDataAccessService _dataAccessService;
        private readonly IGetTokenService _getTokenService;
        

        public GroupService(IDataAccessService dataAccessService , IGetTokenService getTokenService)
        {
            _dataAccessService = dataAccessService;
            _getTokenService = getTokenService;
        }

        public async ValueTask<BasicResponse<GroupResponse>> GetGroup(string groupID)
        {
            var data = await _dataAccessService
                .LoadSingleData<TbGroup, object>(storeProcedure: "agdSp.uspGroupGet", new { seqNo = seqNo, });
            
            if (data == null) return new BasicResponse<GroupResponse>()
            { resultCode = "U999", resultDescription = "查無資料", data = null };
            
            var result = new GroupResponse
            {
				seqNo = data.SeqNo,
				groupID = data.GroupID,
				groupName = data.GroupName,
				createDT = data.CreateDT,
				creator = data.Creator,
				updateDT = data.UpdateDT,
				updator = data.Updator,
                updatorName = data.UpdatorName
            };
            
            return new BasicResponse<GroupResponse>()
            { resultCode = "U200", resultDescription = "查詢成功", data = result };
        }

        public async ValueTask<BasicResponse<List<GroupResponse>>> QueryGroup(GroupQueryRequest request)
        {

            if (string.IsNullOrEmpty(request.extCode)) { request.extCode = string.Empty; }            
            if (string.IsNullOrEmpty(request.computerName)) { request.computerName = string.Empty; }            

            var data = await _dataAccessService
                .LoadData<TbGroup, object>(storeProcedure: "agdSp.uspGroupQuery", request);
                
            if (data.Count()==0) return new BasicResponse<List<GroupResponse>>()
            { resultCode = "U200", resultDescription = "查無資料", data = null };

            var result = data.Select(item => new GroupResponse
            {
				seqNo = item.SeqNo,
				groupID = item.GroupID,
				groupName = item.GroupName,
				createDT = item.CreateDT,
				creator = item.Creator,
				updateDT = item.UpdateDT,
				updator = item.Updator,            
                updatorName = item.UpdatorName   
            }).ToList();

            int totalCount = data.FirstOrDefault().Total;

            return new BasicResponse<List<GroupResponse>>()
            { resultCode = "U200", resultDescription = "查詢成功", data = result, total=totalCount };
        }

        public async ValueTask<BasicResponse<bool>> InsertGroup(GroupInsertRequest request)
        {
            var creator = _getTokenService.userId ?? "";

            var exists = await Exists(0, request.extCode, request.computerIp);
            
            if (exists.data == true) return new BasicResponse<bool>()
            { resultCode = "U999", resultDescription = "資料重複，請重新設定", data=false };
                        
            request.creator = creator;

            var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.uspGroupInsert", request);

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "U999", resultDescription = "新增失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "U200", resultDescription = "新增成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> UpdateGroup(GroupUpdateRequest request)
        {
            var updator = _getTokenService.userId ?? "";

            var exists = await Exists(request.seqNo, request.extCode, request.computerIp);
            
            if (exists.data == true) return new BasicResponse<bool>()
            { resultCode = "U999", resultDescription = "資料重複，請重新設定", data = false };

            request.updator = updator;            

            var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.uspGroupUpdate", request);

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "U999", resultDescription = "更新失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "U200", resultDescription = "更新成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> DeleteGroup(string groupID)
        {
            var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.uspGroupDelete", new { seqNo = seqNo });

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "U999", resultDescription = "刪除失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "U200", resultDescription = "刪除成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> Exists(string groupID,string groupName)
        {
            var data = await _dataAccessService
                .LoadSingleData<int, object>(storeProcedure: "agdSp.uspGroupExists", new
                {
					GroupID = groupID,
					GroupName = groupName,               
                });

            if (data == 0) return new BasicResponse<bool>()
            { resultCode = "U999", resultDescription = "資料重複", data = false };

            return new BasicResponse<bool>()
            { resultCode = "U200", resultDescription = "資料正常", data = true };
        }
    }
}