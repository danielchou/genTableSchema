using AutoMapper;
using ESUN.AGD.DataAccess.DataService.DataAccess;
using ESUN.AGD.DataAccess.Type;
using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.Common;
using ESUN.AGD.WebApi.Application.ContactCustomer;
using ESUN.AGD.WebApi.Application.ContactCustomer.Contract;
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


namespace ESUN.AGD.WebApi.Test.ContactCustomerTest
{
    [TestFixture]
    public class TestContactCustomerService
    {
        private static IDataAccessService stubDataAccessService = Substitute.For<IDataAccessService>();
        private static IGetTokenService stubGetTokenService = Substitute.For<IGetTokenService>();
        private static IHttpContextAccessor stubHttpContextAccessor = Substitute.For<IHttpContextAccessor>();
        private static IMapper stubMapper = Substitute.For<IMapper>();
        private static IUserService stubUserService = Substitute.For<IUserService>();
        private static IAutoNextNumService autoNextNumService = Substitute.For<IAutoNextNumService>();

        [Test]
        public async ValueTask GetContactCustomerTest()
        {  //arrange
            int seqNo = 1;
            string resCode = "U200";
           
            stubDataAccessService
                .LoadSingleData<TbContactCustomer, object>("agdSp.uspContactCustomerGet", Arg.Any<int>())
                .ReturnsForAnyArgs(new TbContactCustomer { SeqNo = 1 });

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            ContactCustomerService ContactCustomerService = 
                new ContactCustomerService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ContactCustomerService.GetContactCustomer(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspContactCustomerGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode,resCode);

        }

        [Test]
        public async ValueTask GetContactCustomerTestFailed()
        {  //arrange
            int seqNo = 0;
            string resCode = "U999";
          
            stubDataAccessService.LoadSingleData<TbContactCustomer, object>("agdSp.uspContactCustomerGet", Arg.Any<int>())
                .ReturnsNull();          

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            ContactCustomerService ContactCustomerService = new ContactCustomerService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ContactCustomerService.GetContactCustomer(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<TbContactCustomer, object>("agdSp.uspContactCustomerGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryContactCustomerTest()
        {  //arrange
            var queryReq = new ContactCustomerQueryRequest { 						contactID = "32",
						contactSeq = 1,
						custKey = 5,
						customerID = "M106371509",
						memo = "AA", };
            string resCode = "U200";
            
            stubDataAccessService.LoadData<TbContactCustomer, object>
                (storeProcedure: "agdSp.uspContactCustomerQuery", Arg.Any<object>())
               .Returns(new TbContactCustomer[] { 
                    new TbContactCustomer { 
						contactID = "32",
						contactSeq = 1,
						custKey = 5,
						customerID = "M106371509",
						memo = "AA",
                        Total = 2
                    },
                    new TbContactCustomer { } 
                }.AsQueryable());
            
            stubHttpContextAccessor.HttpContext.Request.Path ="/api/ContactCustomer/query";
            
            ContactCustomerService ContactCustomerService = new 
                ContactCustomerService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ContactCustomerService.QueryContactCustomer(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
          
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryContactCustomerTestFailed()
        {  //arrange
            var queryReq = new ContactCustomerQueryRequest {  
						contactID = "32",
						contactSeq = 1,
						custKey = 5,
						customerID = "M106371509",
						memo = "AA",
            };
            string resCode = "U999";
            
            stubDataAccessService.LoadData<TbContactCustomer, object>("agdSp.uspContactCustomerQuery", Arg.Any<object>())
                .ReturnsNull();
            
            stubHttpContextAccessor.HttpContext.Request.Path = "/api/ContactCustomer/query";
            ContactCustomerService ContactCustomerService = new ContactCustomerService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ContactCustomerService.QueryContactCustomer(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspContactCustomerGet", Arg.Any<object>());
            Assert.AreEqual(result.total, 0);

        }

        [Test]
        public async ValueTask InsertContactCustomerTest()
        {  //arrange
            var insertReq = new ContactCustomerInsertRequest
            {
				contactID = "32",
				contactSeq = 1,
				custKey = 5,
				customerID = "M106371509",
				customerName = "測試",
				combineReason = "其他 / 確認電話/簡訊/信件/email / 確認是否為本行(行銷)人員(信貸、保險、卡處…)",
				combineReview = "特殊",
				memo = "AA",
				creator = "ag001",
				creatorName = "王先生",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspContactCustomerInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result:new SqlResponse() { ErrorMsg="",Msg="" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            ContactCustomerService ContactCustomerService = new
                ContactCustomerService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ContactCustomerService.InsertContactCustomer(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask InsertContactCustomerTestFailed()
        {  //arrange
            var insertReq = new ContactCustomerInsertRequest
            {
				contactID = "32",
				contactSeq = 1,
				custKey = 5,
				customerID = "M106371509",
				customerName = "測試",
				combineReason = "其他 / 確認電話/簡訊/信件/email / 確認是否為本行(行銷)人員(信貸、保險、卡處…)",
				combineReview = "特殊",
				memo = "AA",
				creator = "ag001",
				creatorName = "王先生",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspContactCustomerInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            ContactCustomerService ContactCustomerService = new
                ContactCustomerService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ContactCustomerService.InsertContactCustomer(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }


        [Test]
        public async ValueTask UpdateContactCustomerTest()
        {  //arrange
            var updateReq = new ContactCustomerUpdateRequest
            {
				contactID = "64",
				contactSeq = 1,
				custKey = 0,
				customerID = "NEW",
				customerName = "匿名者",
				combineReason = "電話轉接 / 轉接卡處經辨 / 債管組(備註經辦名字)",
				combineReview = "特殊",
				memo = "",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspContactCustomerUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            ContactCustomerService ContactCustomerService = new
                ContactCustomerService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ContactCustomerService.UpdateContactCustomer(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask UpdateContactCustomerTestFailed()
        {  //arrange
            var updateReq = new ContactCustomerUpdateRequest
            {
				contactID = "64",
				contactSeq = 1,
				custKey = 0,
				customerID = "NEW",
				customerName = "匿名者",
				combineReason = "電話轉接 / 轉接卡處經辨 / 債管組(備註經辦名字)",
				combineReview = "特殊",
				memo = "",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspContactCustomerUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            ContactCustomerService ContactCustomerService = new
                ContactCustomerService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ContactCustomerService.UpdateContactCustomer(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }




    }
}
