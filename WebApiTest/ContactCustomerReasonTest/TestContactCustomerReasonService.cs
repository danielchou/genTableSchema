using AutoMapper;
using ESUN.AGD.DataAccess.DataService.DataAccess;
using ESUN.AGD.DataAccess.Type;
using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.Common;
using ESUN.AGD.WebApi.Application.ContactCustomerReason;
using ESUN.AGD.WebApi.Application.ContactCustomerReason.Contract;
using ESUN.AGD.WebApi.Models;
using ESUN.AGD.WebApi.Application.User;
using ESUN.AGD.WebApi.Application.AutoNextNum;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Linq;
using System.Threading.Tasks;


namespace ESUN.AGD.WebApi.Test.ContactCustomerReasonTest
{
    [TestFixture]
    public class TestContactCustomerReasonService
    {
        private static IDataAccessService stubDataAccessService = Substitute.For<IDataAccessService>();
        private static IGetTokenService stubGetTokenService = Substitute.For<IGetTokenService>();
        private static IHttpContextAccessor stubHttpContextAccessor = Substitute.For<IHttpContextAccessor>();
        private static IMapper stubMapper = Substitute.For<IMapper>();
        private static IUserService stubUserService = Substitute.For<IUserService>();
        private static IAutoNextNumService autoNextNumService = Substitute.For<IAutoNextNumService>();

        [Test]
        public async ValueTask GetContactCustomerReasonTest()
        {  //arrange
            int seqNo = 1;
            string resCode = "U200";
           
            stubDataAccessService
                .LoadSingleData<TbContactCustomerReason, object>("agdSp.uspContactCustomerReasonGet", Arg.Any<int>())
                .ReturnsForAnyArgs(new TbContactCustomerReason { SeqNo = 1 });

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            ContactCustomerReasonService ContactCustomerReasonService = 
                new ContactCustomerReasonService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ContactCustomerReasonService.GetContactCustomerReason(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspContactCustomerReasonGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode,resCode);

        }

        [Test]
        public async ValueTask GetContactCustomerReasonTestFailed()
        {  //arrange
            int seqNo = 0;
            string resCode = "U999";
          
            stubDataAccessService.LoadSingleData<TbContactCustomerReason, object>("agdSp.uspContactCustomerReasonGet", Arg.Any<int>())
                .ReturnsNull();          

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            ContactCustomerReasonService ContactCustomerReasonService = new ContactCustomerReasonService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ContactCustomerReasonService.GetContactCustomerReason(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<TbContactCustomerReason, object>("agdSp.uspContactCustomerReasonGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryContactCustomerReasonTest()
        {  //arrange
            var queryReq = new ContactCustomerReasonQueryRequest { 						contactID = "32",
						custKey = 5,
						customerID = "M106371509",
						reasonID3 = "F1010",
						isPrimary = True,
						reviewType = "1", };
            string resCode = "U200";
            
            stubDataAccessService.LoadData<TbContactCustomerReason, object>
                (storeProcedure: "agdSp.uspContactCustomerReasonQuery", Arg.Any<object>())
               .Returns(new TbContactCustomerReason[] { 
                    new TbContactCustomerReason { 
						contactID = "32",
						custKey = 5,
						customerID = "M106371509",
						reasonID3 = "F1010",
						isPrimary = True,
						reviewType = "1",
                        Total = 2
                    },
                    new TbContactCustomerReason { } 
                }.AsQueryable());
            
            stubHttpContextAccessor.HttpContext.Request.Path ="/api/ContactCustomerReason/query";
            
            ContactCustomerReasonService ContactCustomerReasonService = new 
                ContactCustomerReasonService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ContactCustomerReasonService.QueryContactCustomerReason(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
          
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryContactCustomerReasonTestFailed()
        {  //arrange
            var queryReq = new ContactCustomerReasonQueryRequest {  
						contactID = "32",
						custKey = 5,
						customerID = "M106371509",
						reasonID3 = "F1010",
						isPrimary = True,
						reviewType = "1",
            };
            string resCode = "U999";
            
            stubDataAccessService.LoadData<TbContactCustomerReason, object>("agdSp.uspContactCustomerReasonQuery", Arg.Any<object>())
                .ReturnsNull();
            
            stubHttpContextAccessor.HttpContext.Request.Path = "/api/ContactCustomerReason/query";
            ContactCustomerReasonService ContactCustomerReasonService = new ContactCustomerReasonService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ContactCustomerReasonService.QueryContactCustomerReason(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspContactCustomerReasonGet", Arg.Any<object>());
            Assert.AreEqual(result.total, 0);

        }

        [Test]
        public async ValueTask InsertContactCustomerReasonTest()
        {  //arrange
            var insertReq = new ContactCustomerReasonInsertRequest
            {
				contactID = "32",
				contactSeq = 1,
				custKey = 5,
				customerID = "M106371509",
				customerName = "測試",
				reasonID1 = "F",
				reasonName1 = "其他",
				reasonID2 = "F10",
				reasonName2 = "確認電話/簡訊/信件/email",
				reasonID3 = "F1010",
				reasonName3 = "確認是否為本行(行銷)人員(信貸、保險、卡處…)",
				isPrimary = True,
				splitDuration = 0,
				reviewType = "1",
				creator = "ag001",
				creatorName = "王先生",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspContactCustomerReasonInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result:new SqlResponse() { ErrorMsg="",Msg="" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            ContactCustomerReasonService ContactCustomerReasonService = new
                ContactCustomerReasonService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ContactCustomerReasonService.InsertContactCustomerReason(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask InsertContactCustomerReasonTestFailed()
        {  //arrange
            var insertReq = new ContactCustomerReasonInsertRequest
            {
				contactID = "32",
				contactSeq = 1,
				custKey = 5,
				customerID = "M106371509",
				customerName = "測試",
				reasonID1 = "F",
				reasonName1 = "其他",
				reasonID2 = "F10",
				reasonName2 = "確認電話/簡訊/信件/email",
				reasonID3 = "F1010",
				reasonName3 = "確認是否為本行(行銷)人員(信貸、保險、卡處…)",
				isPrimary = True,
				splitDuration = 0,
				reviewType = "1",
				creator = "ag001",
				creatorName = "王先生",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspContactCustomerReasonInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            ContactCustomerReasonService ContactCustomerReasonService = new
                ContactCustomerReasonService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ContactCustomerReasonService.InsertContactCustomerReason(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }


        [Test]
        public async ValueTask UpdateContactCustomerReasonTest()
        {  //arrange
            var updateReq = new ContactCustomerReasonUpdateRequest
            {
				contactID = "64",
				contactSeq = 1,
				custKey = 0,
				customerID = "NEW",
				customerName = "匿名者",
				reasonID1 = "I",
				reasonName1 = "電話轉接",
				reasonID2 = "I04",
				reasonName2 = "轉接卡處經辨",
				reasonID3 = "I0402",
				reasonName3 = "債管組(備註經辦名字)",
				isPrimary = True,
				splitDuration = 0,
				reviewType = "1",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspContactCustomerReasonUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            ContactCustomerReasonService ContactCustomerReasonService = new
                ContactCustomerReasonService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ContactCustomerReasonService.UpdateContactCustomerReason(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask UpdateContactCustomerReasonTestFailed()
        {  //arrange
            var updateReq = new ContactCustomerReasonUpdateRequest
            {
				contactID = "64",
				contactSeq = 1,
				custKey = 0,
				customerID = "NEW",
				customerName = "匿名者",
				reasonID1 = "I",
				reasonName1 = "電話轉接",
				reasonID2 = "I04",
				reasonName2 = "轉接卡處經辨",
				reasonID3 = "I0402",
				reasonName3 = "債管組(備註經辦名字)",
				isPrimary = True,
				splitDuration = 0,
				reviewType = "1",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspContactCustomerReasonUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            ContactCustomerReasonService ContactCustomerReasonService = new
                ContactCustomerReasonService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ContactCustomerReasonService.UpdateContactCustomerReason(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }




    }
}
