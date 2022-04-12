using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.Code.Contract;
using ESUN.AGD.DataAccess.DataService.DataAccess;


namespace ESUN.AGD.WebApi.Application.Code
{
    public class CodeService : ICodeService
    {

        private readonly IDataAccessService _dataAccessService;
        private readonly IGetTokenService _getTokenService;
        

        public CodeService(IDataAccessService dataAccessService , IGetTokenService getTokenService)
        {
            _dataAccessService = dataAccessService;
            _getTokenService = getTokenService;
        }

        public async ValueTask<BasicResponse<CodeResponse>> GetCode(int seqNo)
        {
            var data = await _dataAccessService
                .LoadSingleData<TbCode, object>(storeProcedure: "agdSp.uspCodeGet", new { seqNo = seqNo, });
            
            if (data == null) return new BasicResponse<CodeResponse>()
            { resultCode = "U999", resultDescription = "查無資料", data = null };
            
            var result = new CodeResponse
            {
				seqNo = data.SeqNo,
				codeType = data.CodeType,
				codeID = data.CodeID,
				codeName = data.CodeName,
				content = data.Content,
				memo = data.Memo,
				displayOrder = data.DisplayOrder,
				isEnable = data.IsEnable,
				createDT = data.CreateDT,
				creator = data.Creator,
				updateDT = data.UpdateDT,
				updator = data.Updator,
                updatorName = data.UpdatorName
            };
            
            return new BasicResponse<CodeResponse>()
            { resultCode = "U200", resultDescription = "查詢成功", data = result };
        }

        public async ValueTask<BasicResponse<List<CodeResponse>>> QueryCode(CodeQueryRequest request)
        {
			if (string.IsNullOrEmpty(request.codeType)) { request.codeType = string.Empty; }
			if (string.IsNullOrEmpty(request.codeID)) { request.codeID = string.Empty; }
			if (string.IsNullOrEmpty(request.codeName)) { request.codeName = string.Empty; }
            

            var data = await _dataAccessService
                .LoadData<TbCode, object>(storeProcedure: "agdSp.uspCodeQuery", request);
                
            if (data.Count()==0) return new BasicResponse<List<CodeResponse>>()
            { resultCode = "U200", resultDescription = "查無資料", data = null };

            var result = data.Select(item => new CodeResponse
            {
				seqNo = item.SeqNo,
				codeType = item.CodeType,
				codeID = item.CodeID,
				codeName = item.CodeName,
				content = item.Content,
				memo = item.Memo,
				displayOrder = item.DisplayOrder,
				isEnable = item.IsEnable,
				createDT = item.CreateDT,
				creator = item.Creator,
				updateDT = item.UpdateDT,
				updator = item.Updator,            
                updatorName = item.UpdatorName   
            }).ToList();

            int totalCount = data.FirstOrDefault().Total;

            return new BasicResponse<List<CodeResponse>>()
            { resultCode = "U200", resultDescription = "查詢成功", data = result, total=totalCount };
        }

        public async ValueTask<BasicResponse<bool>> InsertCode(CodeInsertRequest request)
        {
            var creator = _getTokenService.userID ?? "";
            
            var exists = await Exists(0, request.codeType, request.codeID, request.codeName);
            
            if (exists.data == true) return new BasicResponse<bool>()
            { resultCode = "U999", resultDescription = "資料重複，請重新設定", data=false };
                        
            request.creator = creator;

            var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.uspCodeInsert", request);

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "U999", resultDescription = "新增失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "U200", resultDescription = "新增成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> UpdateCode(CodeUpdateRequest request)
        {
            var updator = _getTokenService.userID ?? "";

            var exists = await Exists(request.seqNo, request.codeType, request.codeID, request.codeName);
            
            if (exists.data == true) return new BasicResponse<bool>()
            { resultCode = "U999", resultDescription = "資料重複，請重新設定", data = false };

            request.updator = updator;            

            var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.uspCodeUpdate", request);

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "U999", resultDescription = "更新失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "U200", resultDescription = "更新成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> DeleteCode(int seqNo)
        {
            var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.uspCodeDelete", new { seqNo = seqNo });

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "U999", resultDescription = "刪除失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "U200", resultDescription = "刪除成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> Exists(int seqNo,string codeType,string codeID,string codeName)
        {
            var data = await _dataAccessService
                .LoadSingleData<int, object>(storeProcedure: "agdSp.uspCodeExists", new
                {
					SeqNo = seqNo,
					CodeType = codeType,
					CodeID = codeID,
					CodeName = codeName,               
                });

            if (data == 0) return new BasicResponse<bool>()
            { resultCode = "U999", resultDescription = "資料重複", data = false };

            return new BasicResponse<bool>()
            { resultCode = "U200", resultDescription = "資料正常", data = true };
        }
    }
}