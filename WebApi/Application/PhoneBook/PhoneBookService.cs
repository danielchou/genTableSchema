using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.PhoneBook.Contract;
using ESUN.AGD.DataAccess.DataService.DataAccess;


namespace ESUN.AGD.WebApi.Application.PhoneBook
{
    public class PhoneBookService : IPhoneBookService
    {

        private readonly IDataAccessService _dataAccessService;
        private readonly IGetTokenService _getTokenService;
        

        public PhoneBookService(IDataAccessService dataAccessService , IGetTokenService getTokenService)
        {
            _dataAccessService = dataAccessService;
            _getTokenService = getTokenService;
        }

        public async ValueTask<BasicResponse<PhoneBookResponse>> GetPhoneBook(int seqNo)
        {
            var data = await _dataAccessService
                .LoadSingleData<TbPhoneBook, object>(storeProcedure: "agdSp.uspPhoneBookGet", new { seqNo = seqNo, });
            
            if (data == null) return new BasicResponse<PhoneBookResponse>()
            { resultCode = "U999", resultDescription = "查無資料", data = null };
            
            var result = new PhoneBookResponse
            {
				seqNo = data.SeqNo,
				phoneBookID = data.PhoneBookID,
				phoneBookName = data.PhoneBookName,
				parentPhoneBookID = data.ParentPhoneBookID,
				phoneBookNumber = data.PhoneBookNumber,
				level = data.Level,
				memo = data.Memo,
				displayOrder = data.DisplayOrder,
				createDT = data.CreateDT,
				creator = data.Creator,
				updateDT = data.UpdateDT,
				updator = data.Updator,
                updatorName = data.UpdatorName
            };
            
            return new BasicResponse<PhoneBookResponse>()
            { resultCode = "U200", resultDescription = "查詢成功", data = result };
        }

        public async ValueTask<BasicResponse<List<PhoneBookResponse>>> QueryPhoneBook(PhoneBookQueryRequest request)
        {
			if (string.IsNullOrEmpty(request.phoneBookID)) { request.phoneBookID = string.Empty; }
			if (string.IsNullOrEmpty(request.phoneBookName)) { request.phoneBookName = string.Empty; }
			if (string.IsNullOrEmpty(request.parentPhoneBookID)) { request.parentPhoneBookID = string.Empty; }
			if (string.IsNullOrEmpty(request.phoneBookNumber)) { request.phoneBookNumber = string.Empty; }
            

            var data = await _dataAccessService
                .LoadData<TbPhoneBook, object>(storeProcedure: "agdSp.uspPhoneBookQuery", request);
                
            if (data.Count()==0) return new BasicResponse<List<PhoneBookResponse>>()
            { resultCode = "U200", resultDescription = "查無資料", data = null };

            var result = data.Select(item => new PhoneBookResponse
            {
				seqNo = item.SeqNo,
				phoneBookID = item.PhoneBookID,
				phoneBookName = item.PhoneBookName,
				parentPhoneBookID = item.ParentPhoneBookID,
				phoneBookNumber = item.PhoneBookNumber,
				level = item.Level,
				memo = item.Memo,
				displayOrder = item.DisplayOrder,
				createDT = item.CreateDT,
				creator = item.Creator,
				updateDT = item.UpdateDT,
				updator = item.Updator,            
                updatorName = item.UpdatorName   
            }).ToList();

            int totalCount = data.FirstOrDefault().Total;

            return new BasicResponse<List<PhoneBookResponse>>()
            { resultCode = "U200", resultDescription = "查詢成功", data = result, total=totalCount };
        }

        public async ValueTask<BasicResponse<bool>> InsertPhoneBook(PhoneBookInsertRequest request)
        {
            var creator = _getTokenService.userID ?? "";
            
            var exists = await Exists(0, request.phoneBookID, request.phoneBookName);
            
            if (exists.data == true) return new BasicResponse<bool>()
            { resultCode = "U999", resultDescription = "資料重複，請重新設定", data=false };
                        
            request.creator = creator;

            var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.uspPhoneBookInsert", request);

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "U999", resultDescription = "新增失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "U200", resultDescription = "新增成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> UpdatePhoneBook(PhoneBookUpdateRequest request)
        {
            var updator = _getTokenService.userID ?? "";

            var exists = await Exists(request.seqNo, request.phoneBookID, request.phoneBookName);
            
            if (exists.data == true) return new BasicResponse<bool>()
            { resultCode = "U999", resultDescription = "資料重複，請重新設定", data = false };

            request.updator = updator;            

            var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.uspPhoneBookUpdate", request);

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "U999", resultDescription = "更新失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "U200", resultDescription = "更新成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> DeletePhoneBook(int seqNo)
        {
            var data = await _dataAccessService
                .OperateData(storeProcedure: "agdSp.uspPhoneBookDelete", new { seqNo = seqNo });

            if (data == 0) return new BasicResponse<bool>() 
            { resultCode = "U999", resultDescription = "刪除失敗", data = false };
            
            return new BasicResponse<bool>() 
            { resultCode = "U200", resultDescription = "刪除成功", data = true };
        }

        public async ValueTask<BasicResponse<bool>> Exists(int seqNo,string phoneBookID,string phoneBookName)
        {
            var data = await _dataAccessService
                .LoadSingleData<int, object>(storeProcedure: "agdSp.uspPhoneBookExists", new
                {
					SeqNo = seqNo,
					PhoneBookID = phoneBookID,
					PhoneBookName = phoneBookName,               
                });

            if (data == 0) return new BasicResponse<bool>()
            { resultCode = "U999", resultDescription = "資料重複", data = false };

            return new BasicResponse<bool>()
            { resultCode = "U200", resultDescription = "資料正常", data = true };
        }
    }
}