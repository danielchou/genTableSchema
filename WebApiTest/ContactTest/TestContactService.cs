using AutoMapper;
using ESUN.AGD.DataAccess.DataService.DataAccess;
using ESUN.AGD.DataAccess.Type;
using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.Common;
using ESUN.AGD.WebApi.Application.Contact;
using ESUN.AGD.WebApi.Application.Contact.Contract;
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


namespace ESUN.AGD.WebApi.Test.ContactTest
{
    [TestFixture]
    public class TestContactService
    {
        private static IDataAccessService stubDataAccessService = Substitute.For<IDataAccessService>();
        private static IGetTokenService stubGetTokenService = Substitute.For<IGetTokenService>();
        private static IHttpContextAccessor stubHttpContextAccessor = Substitute.For<IHttpContextAccessor>();
        private static IMapper stubMapper = Substitute.For<IMapper>();
        private static IUserService stubUserService = Substitute.For<IUserService>();
        private static IAutoNextNumService autoNextNumService = Substitute.For<IAutoNextNumService>();

        [Test]
        public async ValueTask GetContactTest()
        {  //arrange
            int seqNo = 1;
            string resCode = "U200";
           
            stubDataAccessService
                .LoadSingleData<TbContact, object>("agdSp.uspContactGet", Arg.Any<int>())
                .ReturnsForAnyArgs(new TbContact { SeqNo = 1 });

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            ContactService ContactService = 
                new ContactService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ContactService.GetContact(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspContactGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode,resCode);

        }

        [Test]
        public async ValueTask GetContactTestFailed()
        {  //arrange
            int seqNo = 0;
            string resCode = "U999";
          
            stubDataAccessService.LoadSingleData<TbContact, object>("agdSp.uspContactGet", Arg.Any<int>())
                .ReturnsNull();          

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            ContactService ContactService = new ContactService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ContactService.GetContact(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<TbContact, object>("agdSp.uspContactGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryContactTest()
        {  //arrange
            var queryReq = new ContactQueryRequest { 						protocol = "voice",
						callType = "Inbound",
						beginDT = "2022-08-12 15:18:45",
						direction = "1",
						creator = "admin", };
            string resCode = "U200";
            
            stubDataAccessService.LoadData<TbContact, object>
                (storeProcedure: "agdSp.uspContactQuery", Arg.Any<object>())
               .Returns(new TbContact[] { 
                    new TbContact { 
						protocol = "voice",
						callType = "Inbound",
						beginDT = "2022-08-12 15:18:45",
						direction = "1",
						creator = "admin",
                        Total = 2
                    },
                    new TbContact { } 
                }.AsQueryable());
            
            stubHttpContextAccessor.HttpContext.Request.Path ="/api/Contact/query";
            
            ContactService ContactService = new 
                ContactService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ContactService.QueryContact(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
          
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryContactTestFailed()
        {  //arrange
            var queryReq = new ContactQueryRequest {  
						protocol = "voice",
						callType = "Inbound",
						beginDT = "2022-08-12 15:18:45",
						direction = "1",
						creator = "admin",
            };
            string resCode = "U999";
            
            stubDataAccessService.LoadData<TbContact, object>("agdSp.uspContactQuery", Arg.Any<object>())
                .ReturnsNull();
            
            stubHttpContextAccessor.HttpContext.Request.Path = "/api/Contact/query";
            ContactService ContactService = new ContactService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ContactService.QueryContact(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspContactGet", Arg.Any<object>());
            Assert.AreEqual(result.total, 0);

        }

        [Test]
        public async ValueTask InsertContactTest()
        {  //arrange
            var insertReq = new ContactInsertRequest
            {
				contactID = 10,
				protocol = "voice",
				callType = "Inbound",
				phoneNumber = "99999999",
				beginDT = "2022-08-12 15:18:45",
				endDT = "2022-08-12 15:18:45",
				duration = 0,
				direction = "1",
				primaryUUID = "",
				reviewStatus = "0",
				reviewDT = "None",
				creator = "admin",
				creatorName = "林管理",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspContactInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result:new SqlResponse() { ErrorMsg="",Msg="" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            ContactService ContactService = new
                ContactService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ContactService.InsertContact(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask InsertContactTestFailed()
        {  //arrange
            var insertReq = new ContactInsertRequest
            {
				contactID = 10,
				protocol = "voice",
				callType = "Inbound",
				phoneNumber = "99999999",
				beginDT = "2022-08-12 15:18:45",
				endDT = "2022-08-12 15:18:45",
				duration = 0,
				direction = "1",
				primaryUUID = "",
				reviewStatus = "0",
				reviewDT = "None",
				creator = "admin",
				creatorName = "林管理",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspContactInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            ContactService ContactService = new
                ContactService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ContactService.InsertContact(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }


        [Test]
        public async ValueTask UpdateContactTest()
        {  //arrange
            var updateReq = new ContactUpdateRequest
            {
				contactID = 64,
				protocol = "Voice",
				callType = "Outbound",
				phoneNumber = "0932273888",
				beginDT = "2022-10-25 16:25:57",
				endDT = "2022-10-25 16:25:57",
				duration = 0,
				direction = "2",
				primaryUUID = "None",
				reviewStatus = "1",
				reviewDT = "None",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspContactUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            ContactService ContactService = new
                ContactService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ContactService.UpdateContact(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask UpdateContactTestFailed()
        {  //arrange
            var updateReq = new ContactUpdateRequest
            {
				contactID = 64,
				protocol = "Voice",
				callType = "Outbound",
				phoneNumber = "0932273888",
				beginDT = "2022-10-25 16:25:57",
				endDT = "2022-10-25 16:25:57",
				duration = 0,
				direction = "2",
				primaryUUID = "None",
				reviewStatus = "1",
				reviewDT = "None",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspContactUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            ContactService ContactService = new
                ContactService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ContactService.UpdateContact(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }




    }
}
