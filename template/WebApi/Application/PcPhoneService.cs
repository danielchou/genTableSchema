using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.$pt_TableName.Contract;
using ESUN.AGD.DataAccess.DataService.DataAccess;


namespace ESUN.AGD.WebApi.Application.$pt_TableName
{
    public class $pt_TableName$service : I$pt_TableName$service
    {

        private readonly IDataAccessService _dataAccessService;
        private readonly IGetTokenService _getTokenService;
        
        public $pt_TableName$service(IDataAccessService dataAccessService, IGetTokenService getTokenService)
        {
            _dataAccessService = dataAccessService;
            _getTokenService = getTokenService;
        }

        public async ValueTask<BasicResponse<$pt_TableName$response>> Get$pt_TableName($pt_InputPK)
        {
            var data = await _dataAccessService
                .LoadSingleData<Tb$pt_TableName, object>(storeProcedure: "agdSp.usp$pt_TableName$get", new { seqNo = seqNo, });
            
            if (data == null) return new BasicResponse<$pt_TableName$response>()
            { resultCode = "U999", resultDescription = "查無資料", data = null };
            
            var result = new $pt_TableName$response
            {
$pt_data2Json
                updatorName = data.UpdatorName
            };
            
            return new BasicResponse<$pt_TableName$response>()
            { resultCode = "U200", resultDescription = "查詢成功", data = result };
        }

        public async ValueTask<BasicResponse<List<$pt_TableName$response>>> Query$pt_TableName($pt_TableName$queryRequest request)
        {
$pt_requestIsNullOrEmpty            

            var data = await _dataAccessService
                .LoadData<Tb$pt_TableName, object>(storeProcedure: "agdSp.usp$pt_TableName$query", request);
                
            if (data.Count()==0) return new BasicResponse<List<$pt_TableName$response>>()
            { resultCode = "U200", resultDescription = "查無資料", data = null };

            var result = data.Select(item => new $pt_TableName$response
            {
$pt_item2Json            
                updatorName = item.UpdatorName   
            }).ToList();

            int totalCount = data.FirstOrDefault().Total;

            return new BasicResponse<List<$pt_TableName$response>>()
            { resultCode = "U200", resultDescription = "查詢成功", data = result, total=totalCount };
        }

        public async ValueTask<BasicResponse<bool>> Insert$pt_TableName($pt_TableName$insertRequest request)
        {
            var creator = _getTokenService.userID ?? "";
            
            var exists = await Exists(0, $pt_requstInsertIsExist);
            
            if (exists.data == true) return new BasicResponse<bool>()
            { resultCode = "U999", resultDescription = "資料重複，請重新設定", data=false };
                        
            request.creator = creator;

            var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.usp$pt_TableName$insert", request);

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "U999", resultDescription = "新增失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "U200", resultDescription = "新增成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> Update$pt_TableName($pt_TableName$updateRequest request)
        {
            var updator = _getTokenService.userID ?? "";

            var exists = await Exists($pt_requstInsertIsExistWithSeqNo);
            
            if (exists.data == true) return new BasicResponse<bool>()
            { resultCode = "U999", resultDescription = "資料重複，請重新設定", data = false };

            request.updator = updator;            

            var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.usp$pt_TableName$update", request);

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "U999", resultDescription = "更新失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "U200", resultDescription = "更新成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> Delete$pt_TableName($pt_InputPK)
        {
            var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.usp$pt_TableName$delete", new { seqNo = seqNo });

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "U999", resultDescription = "刪除失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "U200", resultDescription = "刪除成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> Exists($pt_InputIsExist)
        {
            var data = await _dataAccessService
                .LoadSingleData<int, object>(storeProcedure: "agdSp.usp$pt_TableName$exists", new
                {
$pt_json2Data               
                });

            if (data == 0) return new BasicResponse<bool>()
            { resultCode = "U999", resultDescription = "資料重複", data = false };

            return new BasicResponse<bool>()
            { resultCode = "U200", resultDescription = "資料正常", data = true };
        }
    }
}