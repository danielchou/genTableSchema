using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.$pt_TableName.Contract;
using ESUN.AGD.DataAccess.DataService.DataAccess;

namespace ESUN.AGD.WebApi.Application.$pt_TableName
{
    public class $pt_TableName$service : I$pt_TableName$service
    {

        private readonly IDataAccessService _dataAccessService;
        private readonly IGetTokenService _getTokenService;
        private IHttpContextAccessor _context;
        private string serviceName = string.Empty;
        private IMapper _mapper;
       
        public $pt_TableName$service(IDataAccessService dataAccessService
                              , IGetTokenService getTokenService
                              , IHttpContextAccessor context
                              , IMapper mapper)
        {
            _dataAccessService = dataAccessService;
            _getTokenService = getTokenService;
            _context = context;
            serviceName = this.GetType().Name;
            _mapper = mapper;
        }

        public async ValueTask<BasicResponse<$pt_TableName$response>> Get$pt_TableName($pt_InputPK)
        {
            var method = _context?.HttpContext?.Request?.Method;
            var data = await _dataAccessService
                .LoadSingleData<Tb$pt_TableName, object>(storeProcedure: "agdSp.usp$pt_TableName$get", new { $pt_InputServicePK = $pt_InputServicePK, });
            
      		return ResponseHandler.ForData<$pt_TableName$response>(data, _mapper, serviceName, method, 0);
        }

        public async ValueTask<BasicResponse<List<$pt_TableName$response>>> Query$pt_TableName($pt_TableName$queryRequest request)
        {
			var method = _context?.HttpContext?.Request.Path.ToString().Split("/")[3];

$pt_requestIsNullOrEmpty
            var data = await _dataAccessService
                .LoadData<Tb$pt_TableName, object>(storeProcedure: "agdSp.usp$pt_TableName$query", request);
                
            int totalCount = data == null ? 0 : data.FirstOrDefault().Total;
			
			return ResponseHandler.ForData<List<$pt_TableName$response>>(data, _mapper, serviceName, method, totalCount);
        }

        public async ValueTask<BasicResponse<bool>> Insert$pt_TableName($pt_TableName$insertRequest request)
        {
			var method = _context?.HttpContext?.Request?.Method;            
			
			var exists = await Exists(0, $pt_requstInsertIsExist);
            
            if (exists.data == true)
                return ResponseHandler.ForCustomBool(serviceName, false, "資料重複，$pt_requstInsertIsExistWithSeqNoColDscr重複，請重新設定");

            var creator = _getTokenService.userID ?? "";
            var creatorName = _getTokenService.userName ?? "";
                          
            request.creator = creator;
            request.creatorName = creatorName;

            var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.usp$pt_TableName$insert", request);

			return ResponseHandler.ForBool(data, serviceName, method);
        }

        public async ValueTask<BasicResponse<bool>> Update$pt_TableName($pt_TableName$updateRequest request)
        {
            var method = _context?.HttpContext?.Request?.Method;

			var exists = await Exists($pt_requstInsertIsExistWithSeqNo);
            
            if (exists.data == true)
                return ResponseHandler.ForCustomBool(serviceName, false, "資料重複，$pt_requstInsertIsExistWithSeqNoColDscr重複，請重新設定");

            var updator = _getTokenService.userID ?? "";
            var updatorName = _getTokenService.userName ?? "";

            request.updator = updator;
            request.updatorName = updatorName;

            var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.usp$pt_TableName$update", request);

			return ResponseHandler.ForBool(data, serviceName, method);
        }

        public async ValueTask<BasicResponse<bool>> Delete$pt_TableName($pt_InputPK)
        {
			var method = _context?.HttpContext?.Request?.Method;
            
			var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.usp$pt_TableName$delete", new { $pt_InputServicePK = $pt_InputServicePK });

			return ResponseHandler.ForBool(data, serviceName, method);
        }

        public async ValueTask<BasicResponse<bool>> Exists($pt_InputIsExist)
        {
            var data = await _dataAccessService
                .LoadSingleData<int, object>(storeProcedure: "agdSp.usp$pt_TableName$exists", new
                {
$pt_json2Data               
                });

            if (data == 0)
            {
                return ResponseHandler.ForCustomBool(serviceName, false, "資料不存在");
            }
            else
            {
                return ResponseHandler.ForCustomBool(serviceName, true, "資料存在");
            }
        }
    }
}