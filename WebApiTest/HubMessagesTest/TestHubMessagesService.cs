using AutoMapper;
using ESUN.AGD.DataAccess.DataService.DataAccess;
using ESUN.AGD.DataAccess.Type;
using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.Common;
using ESUN.AGD.WebApi.Application.HubMessages;
using ESUN.AGD.WebApi.Application.HubMessages.Contract;
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


namespace ESUN.AGD.WebApi.Test.HubMessagesTest
{
    [TestFixture]
    public class TestHubMessagesService
    {
        private static IDataAccessService stubDataAccessService = Substitute.For<IDataAccessService>();
        private static IGetTokenService stubGetTokenService = Substitute.For<IGetTokenService>();
        private static IHttpContextAccessor stubHttpContextAccessor = Substitute.For<IHttpContextAccessor>();
        private static IMapper stubMapper = Substitute.For<IMapper>();
        private static IUserService stubUserService = Substitute.For<IUserService>();
        private static IAutoNextNumService autoNextNumService = Substitute.For<IAutoNextNumService>();

        [Test]
        public async ValueTask GetHubMessagesTest()
        {  //arrange
            int seqNo = 1;
            string resCode = "U200";
           
            stubDataAccessService
                .LoadSingleData<TbHubMessages, object>("agdSp.uspHubMessagesGet", Arg.Any<int>())
                .ReturnsForAnyArgs(new TbHubMessages { SeqNo = 1 });

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            HubMessagesService HubMessagesService = 
                new HubMessagesService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await HubMessagesService.GetHubMessages(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspHubMessagesGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode,resCode);

        }

        [Test]
        public async ValueTask GetHubMessagesTestFailed()
        {  //arrange
            int seqNo = 0;
            string resCode = "U999";
          
            stubDataAccessService.LoadSingleData<TbHubMessages, object>("agdSp.uspHubMessagesGet", Arg.Any<int>())
                .ReturnsNull();          

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            HubMessagesService HubMessagesService = new HubMessagesService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await HubMessagesService.GetHubMessages(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<TbHubMessages, object>("agdSp.uspHubMessagesGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryHubMessagesTest()
        {  //arrange
            var queryReq = new HubMessagesQueryRequest { 						msgEnable = True,
						msgStartDT = "2022-08-22 13:04:50",
						msgEndDT = "2022-08-22 13:07:50",
						msgType = "0",
						msgStatus = "1",
						doneDT = "2022-08-22 13:04:53",
						doneHubNameList = "AGDDev", };
            string resCode = "U200";
            
            stubDataAccessService.LoadData<TbHubMessages, object>
                (storeProcedure: "agdSp.uspHubMessagesQuery", Arg.Any<object>())
               .Returns(new TbHubMessages[] { 
                    new TbHubMessages { 
						msgEnable = True,
						msgStartDT = "2022-08-22 13:04:50",
						msgEndDT = "2022-08-22 13:07:50",
						msgType = "0",
						msgStatus = "1",
						doneDT = "2022-08-22 13:04:53",
						doneHubNameList = "AGDDev",
                        Total = 2
                    },
                    new TbHubMessages { } 
                }.AsQueryable());
            
            stubHttpContextAccessor.HttpContext.Request.Path ="/api/HubMessages/query";
            
            HubMessagesService HubMessagesService = new 
                HubMessagesService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await HubMessagesService.QueryHubMessages(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
          
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryHubMessagesTestFailed()
        {  //arrange
            var queryReq = new HubMessagesQueryRequest {  
						msgEnable = True,
						msgStartDT = "2022-08-22 13:04:50",
						msgEndDT = "2022-08-22 13:07:50",
						msgType = "0",
						msgStatus = "1",
						doneDT = "2022-08-22 13:04:53",
						doneHubNameList = "AGDDev",
            };
            string resCode = "U999";
            
            stubDataAccessService.LoadData<TbHubMessages, object>("agdSp.uspHubMessagesQuery", Arg.Any<object>())
                .ReturnsNull();
            
            stubHttpContextAccessor.HttpContext.Request.Path = "/api/HubMessages/query";
            HubMessagesService HubMessagesService = new HubMessagesService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await HubMessagesService.QueryHubMessages(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspHubMessagesGet", Arg.Any<object>());
            Assert.AreEqual(result.total, 0);

        }

        [Test]
        public async ValueTask InsertHubMessagesTest()
        {  //arrange
            var insertReq = new HubMessagesInsertRequest
            {
				senderAgentLoginID = "10046S",
				senderUserName = "陳麗杰鰻魚飯",
				senderConnectionID = "cc5a99c5-56b9-4e5d-aaed-eb071ee133c0",
				senderClientIP = "20.127.58.130",
				senderHubName = "AGDDev",
				msgContent = "reloadContactReason",
				msgEnable = True,
				msgStartDT = "2022-08-22 13:04:50",
				msgEndDT = "2022-08-22 13:07:50",
				msgType = "0",
				toAgentLoginID = "",
				toUserName = "",
				toEmpDeptCode = "",
				toEmpDeptName = "",
				toGroupID = "",
				toGroupName = "",
				msgStatus = "1",
				msgStatusMemo = "",
				doneDT = "2022-08-22 13:04:53",
				doneHubName = "AGDDev",
				doneHubNameList = "AGDDev",
				creator = "10046S",
				creatorName = "陳麗杰鰻魚飯",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspHubMessagesInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result:new SqlResponse() { ErrorMsg="",Msg="" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            HubMessagesService HubMessagesService = new
                HubMessagesService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await HubMessagesService.InsertHubMessages(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask InsertHubMessagesTestFailed()
        {  //arrange
            var insertReq = new HubMessagesInsertRequest
            {
				senderAgentLoginID = "10046S",
				senderUserName = "陳麗杰鰻魚飯",
				senderConnectionID = "cc5a99c5-56b9-4e5d-aaed-eb071ee133c0",
				senderClientIP = "20.127.58.130",
				senderHubName = "AGDDev",
				msgContent = "reloadContactReason",
				msgEnable = True,
				msgStartDT = "2022-08-22 13:04:50",
				msgEndDT = "2022-08-22 13:07:50",
				msgType = "0",
				toAgentLoginID = "",
				toUserName = "",
				toEmpDeptCode = "",
				toEmpDeptName = "",
				toGroupID = "",
				toGroupName = "",
				msgStatus = "1",
				msgStatusMemo = "",
				doneDT = "2022-08-22 13:04:53",
				doneHubName = "AGDDev",
				doneHubNameList = "AGDDev",
				creator = "10046S",
				creatorName = "陳麗杰鰻魚飯",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspHubMessagesInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            HubMessagesService HubMessagesService = new
                HubMessagesService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await HubMessagesService.InsertHubMessages(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }


        [Test]
        public async ValueTask UpdateHubMessagesTest()
        {  //arrange
            var updateReq = new HubMessagesUpdateRequest
            {
				senderAgentLoginID = "TP15adminS",
				senderUserName = "林管理",
				senderConnectionID = "cce74f85-f481-477a-bf13-7fc2a2df7e01",
				senderClientIP = "1.171.232.237",
				senderHubName = "AGDDev",
				msgContent = "{"type":"broadcast","msg":"reloadContactReason","receiveUserID":""}",
				msgEnable = True,
				msgStartDT = "2022-09-14 18:14:26",
				msgEndDT = "2022-09-14 21:14:26",
				msgType = "0",
				toAgentLoginID = "",
				toUserName = "",
				toEmpDeptCode = "",
				toEmpDeptName = "",
				toGroupID = "",
				toGroupName = "",
				msgStatus = "1",
				msgStatusMemo = "",
				doneDT = "2022-09-14 18:14:28",
				doneHubName = "AGDDev",
				doneHubNameList = "AGDDev",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspHubMessagesUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            HubMessagesService HubMessagesService = new
                HubMessagesService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await HubMessagesService.UpdateHubMessages(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask UpdateHubMessagesTestFailed()
        {  //arrange
            var updateReq = new HubMessagesUpdateRequest
            {
				senderAgentLoginID = "TP15adminS",
				senderUserName = "林管理",
				senderConnectionID = "cce74f85-f481-477a-bf13-7fc2a2df7e01",
				senderClientIP = "1.171.232.237",
				senderHubName = "AGDDev",
				msgContent = "{"type":"broadcast","msg":"reloadContactReason","receiveUserID":""}",
				msgEnable = True,
				msgStartDT = "2022-09-14 18:14:26",
				msgEndDT = "2022-09-14 21:14:26",
				msgType = "0",
				toAgentLoginID = "",
				toUserName = "",
				toEmpDeptCode = "",
				toEmpDeptName = "",
				toGroupID = "",
				toGroupName = "",
				msgStatus = "1",
				msgStatusMemo = "",
				doneDT = "2022-09-14 18:14:28",
				doneHubName = "AGDDev",
				doneHubNameList = "AGDDev",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspHubMessagesUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            HubMessagesService HubMessagesService = new
                HubMessagesService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await HubMessagesService.UpdateHubMessages(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }




    }
}
