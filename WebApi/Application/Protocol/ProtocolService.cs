using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.Protocol.Contract;
using ESUN.AGD.DataAccess.DataService.DataAccess;


namespace ESUN.AGD.WebApi.Application.Protocol
{
    public class ProtocolService : IProtocolService
    {

        private readonly IDataAccessService _dataAccessService;
        private readonly IGetTokenService _getTokenService;
        

        public ProtocolService(IDataAccessService dataAccessService , IGetTokenService getTokenService)
        {
            _dataAccessService = dataAccessService;
            _getTokenService = getTokenService;
        }

        public async ValueTask<BasicResponse<ProtocolResponse>> GetProtocol(int seqNo)
        {
            var data = await _dataAccessService
                .LoadSingleData<TbProtocol, object>(storeProcedure: "agdSp.uspProtocolGet", new { seqNo = seqNo, });
            
            if (data == null) return new BasicResponse<ProtocolResponse>()
            { resultCode = "U999", resultDescription = "查無資料", data = null };
            
            var result = new ProtocolResponse
            {
				seqNo = data.SeqNo,
				protocol = data.Protocol,
				protocolName = data.ProtocolName,
				direction = data.Direction,
				displayOrder = data.DisplayOrder,
				isEnable = data.IsEnable,
				createDT = data.CreateDT,
				creator = data.Creator,
				updateDT = data.UpdateDT,
				updator = data.Updator,
                updatorName = data.UpdatorName
            };
            
            return new BasicResponse<ProtocolResponse>()
            { resultCode = "U200", resultDescription = "查詢成功", data = result };
        }

        public async ValueTask<BasicResponse<List<ProtocolResponse>>> QueryProtocol(ProtocolQueryRequest request)
        {
			if (string.IsNullOrEmpty(request.protocol)) { request.protocol = string.Empty; }
			if (string.IsNullOrEmpty(request.protocolName)) { request.protocolName = string.Empty; }
            

            var data = await _dataAccessService
                .LoadData<TbProtocol, object>(storeProcedure: "agdSp.uspProtocolQuery", request);
                
            if (data.Count()==0) return new BasicResponse<List<ProtocolResponse>>()
            { resultCode = "U200", resultDescription = "查無資料", data = null };

            var result = data.Select(item => new ProtocolResponse
            {
				seqNo = item.SeqNo,
				protocol = item.Protocol,
				protocolName = item.ProtocolName,
				direction = item.Direction,
				displayOrder = item.DisplayOrder,
				isEnable = item.IsEnable,
				createDT = item.CreateDT,
				creator = item.Creator,
				updateDT = item.UpdateDT,
				updator = item.Updator,            
                updatorName = item.UpdatorName   
            }).ToList();

            int totalCount = data.FirstOrDefault().Total;

            return new BasicResponse<List<ProtocolResponse>>()
            { resultCode = "U200", resultDescription = "查詢成功", data = result, total=totalCount };
        }

        public async ValueTask<BasicResponse<bool>> InsertProtocol(ProtocolInsertRequest request)
        {
            var creator = _getTokenService.userID ?? "";
            
            var exists = await Exists(0, request.protocol, request.protocolName);
            
            if (exists.data == true) return new BasicResponse<bool>()
            { resultCode = "U999", resultDescription = "資料重複，請重新設定", data=false };
                        
            request.creator = creator;

            var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.uspProtocolInsert", request);

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "U999", resultDescription = "新增失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "U200", resultDescription = "新增成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> UpdateProtocol(ProtocolUpdateRequest request)
        {
            var updator = _getTokenService.userID ?? "";

            var exists = await Exists(request.seqNo, request.protocol, request.protocolName);
            
            if (exists.data == true) return new BasicResponse<bool>()
            { resultCode = "U999", resultDescription = "資料重複，請重新設定", data = false };

            request.updator = updator;            

            var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.uspProtocolUpdate", request);

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "U999", resultDescription = "更新失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "U200", resultDescription = "更新成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> DeleteProtocol(int seqNo)
        {
            var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.uspProtocolDelete", new { seqNo = seqNo });

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "U999", resultDescription = "刪除失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "U200", resultDescription = "刪除成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> Exists(int seqNo,string protocol,string protocolName)
        {
            var data = await _dataAccessService
                .LoadSingleData<int, object>(storeProcedure: "agdSp.uspProtocolExists", new
                {
					SeqNo = seqNo,
					Protocol = protocol,
					ProtocolName = protocolName,               
                });

            if (data == 0) return new BasicResponse<bool>()
            { resultCode = "U999", resultDescription = "資料重複", data = false };

            return new BasicResponse<bool>()
            { resultCode = "U200", resultDescription = "資料正常", data = true };
        }
    }
}