using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.Code.Contract;
using ESUN.AGD.DataAccess.DataService.DataAccess;


namespace ESUN.AGD.WebApi.Application.Code
{
    public class CodeService : ICodeService
    {

        private readonly IDataAccessService _dataAccessService;
        private readonly IGetTokenService _getTokenService;
        

        public CodeService(IDataAccessService dataAccessService   
                                    , IGetTokenService getTokenService)
        {
            _dataAccessService = dataAccessService;
            _getTokenService = getTokenService;
        }

        public async ValueTask<BasicResponse<CodeResponse>> GetCode(int seqNo)
        {
            var data = await _dataAccessService
                .LoadSingData<TbCode, object>(storeProcedure: "agdSp.uspCodeGet", new { seqNo = seqNo, });
            if (data == null) return new BasicResponse<CodeResponse>()
            { resultCode = "9999", resultDescription = "查無資料", data = null };
            var result = new CodeResponse
            {
				seqNo = data.seqNo,
				codeType = data.CodeType,
				codeId = data.CodeId,
				codeName = data.CodeName,
				isEnable = data.IsEnable,
				creator = data.Creator,
				updator = data.Updator,
				createDt = data.CreateDt,
				updateDt = data.UpdateDt,
                updatorName = data.UpdatorName
            };
            return new BasicResponse<CodeResponse>()
            { resultCode = "0000", resultDescription = "查詢成功", data = result };
        }

        public async ValueTask<BasicResponse<List<CodeResponse>>> QueryCode(CodeQueryRequest request)
        {

            if (string.IsNullOrEmpty(request.extCode)) { request.extCode = string.Empty; }            
            if (string.IsNullOrEmpty(request.computerName)) { request.computerName = string.Empty; }            

            var data = await _dataAccessService
                .LoadData<TbCode, object>(storeProcedure: "agdSp.uspCodeQuery", request);
            if (data.Count()==0) return new BasicResponse<List<CodeResponse>>()
            { resultCode = "0000", resultDescription = "查無資料", data = null };

            var result = data.Select(item => new CodeResponse
            {
				seqNo = item.seqNo,
				codeType = item.CodeType,
				codeId = item.CodeId,
				codeName = item.CodeName,
				isEnable = item.IsEnable,
				creator = item.Creator,
				updator = item.Updator,
				createDt = item.CreateDt,
				updateDt = item.UpdateDt,            
                updatorName = item.UpdatorName   
            }).ToList();
            int totalCount = data.FirstOrDefault().Total;

            return new BasicResponse<List<CodeResponse>>()
            { resultCode = "0000", resultDescription = "查詢成功", data = result, total=totalCount };
        }

        public async ValueTask<BasicResponse<bool>> InsertCode(CodeInsertRequest request)
        {
           
            var creator = _getTokenService.userId ?? "";

            var exists = await Exists(0, request.extCode, request.computerIp);
            if (exists.data == true) return new BasicResponse<bool>()
            { resultCode = "9999", resultDescription = "資料重複，請重新設定", data=false };
                        
            request.creator = creator;

            var data = await _dataAccessService
                .OpreatData(storeProcedure: "agdSp.uspCodeInsert", request);

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "9999", resultDescription = "新增失敗", data = false };
            return new BasicResponse<bool>() 
            { resultCode = "0000", resultDescription = "新增成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> UpdateCode(CodeUpdateRequest request)
        {
            var updator = _getTokenService.userId ?? "";

            var exists = await Exists(request.seqNo, request.extCode, request.computerIp);
            if (exists.data == true) return new BasicResponse<bool>()
            { resultCode = "9999", resultDescription = "資料重複，請重新設定", data = false };


            request.updator = updator;            

            var data = await _dataAccessService
                .OpreatData(storeProcedure: "agdSp.uspCodeUpdate", request);

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "9999", resultDescription = "更新失敗", data = false };
            return new BasicResponse<bool>() 
            { resultCode = "0000", resultDescription = "更新成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> DeleteCode(int seqNo)
        {
            var data = await _dataAccessService
                   .OpreatData(storeProcedure: "agdSp.uspCodeDelete", new { seqNo = seqNo });

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "9999", resultDescription = "刪除失敗", data = false };
            return new BasicResponse<bool>() 
            { resultCode = "0000", resultDescription = "刪除成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> Exists(int seqNo,string codeType,string codeId,string codeName)
        {
            var exist = await _dataAccessService
                .LoadSingData<int, object>(storeProcedure: "agdSp.uspCodeExists", new
                {
					seqNo = seqNo,
					CodeType = codeType,
					CodeId = codeId,
					CodeName = codeName,               
                });

            if (exist == 0) return new BasicResponse<bool>()
            { resultCode = "9999", resultDescription = "資料重複", data = false };

            return new BasicResponse<bool>()
            { resultCode = "0000", resultDescription = "資料正常", data = true };
        }
    }
}