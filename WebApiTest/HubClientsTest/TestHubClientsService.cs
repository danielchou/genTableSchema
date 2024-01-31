using AutoMapper;
using ESUN.AGD.DataAccess.DataService.DataAccess;
using ESUN.AGD.DataAccess.Type;
using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.Common;
using ESUN.AGD.WebApi.Application.HubClients;
using ESUN.AGD.WebApi.Application.HubClients.Contract;
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


namespace ESUN.AGD.WebApi.Test.HubClientsTest
{
    [TestFixture]
    public class TestHubClientsService
    {
        private static IDataAccessService stubDataAccessService = Substitute.For<IDataAccessService>();
        private static IGetTokenService stubGetTokenService = Substitute.For<IGetTokenService>();
        private static IHttpContextAccessor stubHttpContextAccessor = Substitute.For<IHttpContextAccessor>();
        private static IMapper stubMapper = Substitute.For<IMapper>();
        private static IUserService stubUserService = Substitute.For<IUserService>();
        private static IAutoNextNumService autoNextNumService = Substitute.For<IAutoNextNumService>();

        [Test]
        public async ValueTask GetHubClientsTest()
        {  //arrange
            int seqNo = 1;
            string resCode = "U200";
           
            stubDataAccessService
                .LoadSingleData<TbHubClients, object>("agdSp.uspHubClientsGet", Arg.Any<int>())
                .ReturnsForAnyArgs(new TbHubClients { SeqNo = 1 });

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            HubClientsService HubClientsService = 
                new HubClientsService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await HubClientsService.GetHubClients(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspHubClientsGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode,resCode);

        }

        [Test]
        public async ValueTask GetHubClientsTestFailed()
        {  //arrange
            int seqNo = 0;
            string resCode = "U999";
          
            stubDataAccessService.LoadSingleData<TbHubClients, object>("agdSp.uspHubClientsGet", Arg.Any<int>())
                .ReturnsNull();          

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            HubClientsService HubClientsService = new HubClientsService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await HubClientsService.GetHubClients(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<TbHubClients, object>("agdSp.uspHubClientsGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryHubClientsTest()
        {  //arrange
            var queryReq = new HubClientsQueryRequest { 						userID = "admin",
						empDeptCode = "",
						groupID = "G01",
						status = "1",
						connectionID = "b36189cb-29af-4cee-9475-5e0550fb5b82",
						clientIP = "1.171.212.126",
						hubName = "AGDDev", };
            string resCode = "U200";
            
            stubDataAccessService.LoadData<TbHubClients, object>
                (storeProcedure: "agdSp.uspHubClientsQuery", Arg.Any<object>())
               .Returns(new TbHubClients[] { 
                    new TbHubClients { 
						userID = "admin",
						empDeptCode = "",
						groupID = "G01",
						status = "1",
						connectionID = "b36189cb-29af-4cee-9475-5e0550fb5b82",
						clientIP = "1.171.212.126",
						hubName = "AGDDev",
                        Total = 2
                    },
                    new TbHubClients { } 
                }.AsQueryable());
            
            stubHttpContextAccessor.HttpContext.Request.Path ="/api/HubClients/query";
            
            HubClientsService HubClientsService = new 
                HubClientsService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await HubClientsService.QueryHubClients(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
          
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryHubClientsTestFailed()
        {  //arrange
            var queryReq = new HubClientsQueryRequest {  
						userID = "admin",
						empDeptCode = "",
						groupID = "G01",
						status = "1",
						connectionID = "b36189cb-29af-4cee-9475-5e0550fb5b82",
						clientIP = "1.171.212.126",
						hubName = "AGDDev",
            };
            string resCode = "U999";
            
            stubDataAccessService.LoadData<TbHubClients, object>("agdSp.uspHubClientsQuery", Arg.Any<object>())
                .ReturnsNull();
            
            stubHttpContextAccessor.HttpContext.Request.Path = "/api/HubClients/query";
            HubClientsService HubClientsService = new HubClientsService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await HubClientsService.QueryHubClients(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspHubClientsGet", Arg.Any<object>());
            Assert.AreEqual(result.total, 0);

        }

        [Test]
        public async ValueTask InsertHubClientsTest()
        {  //arrange
            var insertReq = new HubClientsInsertRequest
            {
				agentLoginID = "TP011234",
				agentLoginCode = "1234",
				userID = "admin",
				userName = "林管理",
				nickName = "MR LIN",
				location = "1",
				thisDN = "",
				officeEmail = "",
				deptNum = "3",
				empDeptCode = "",
				empDeptName = "",
				groupID = "G01",
				groupName = "0",
				isSupervisor = True,
				enableVoice = True,
				enableEmail = False,
				enableChat = False,
				enableVideo = False,
				enableCallBack = False,
				disableAutoAnswer = False,
				status = "1",
				connectionID = "b36189cb-29af-4cee-9475-5e0550fb5b82",
				clientIP = "1.171.212.126",
				hubName = "AGDDev",
				retTime = "None",
				protocol = "",
				iLine = 0,
				currLineButtonState = "",
				currLinelabelTypeText = "",
				currLinelabelStatusText = "",
				currLinelabelForeStatusText = "",
				voiceLines = "",
				voiceUpdTime = "None",
				emailLines = "",
				emailUpdTime = "None",
				chatLines = "",
				chatUpdTime = "None",
				videoLines = "",
				videoUpdTime = "None",
				agentIsFree = True,
				voiceIsFree = True,
				emailIsFree = True,
				chatIsFree = True,
				videoIsFree = True,
				creator = "AGDDev",
				creatorName = "AGDDev",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspHubClientsInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result:new SqlResponse() { ErrorMsg="",Msg="" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            HubClientsService HubClientsService = new
                HubClientsService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await HubClientsService.InsertHubClients(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask InsertHubClientsTestFailed()
        {  //arrange
            var insertReq = new HubClientsInsertRequest
            {
				agentLoginID = "TP011234",
				agentLoginCode = "1234",
				userID = "admin",
				userName = "林管理",
				nickName = "MR LIN",
				location = "1",
				thisDN = "",
				officeEmail = "",
				deptNum = "3",
				empDeptCode = "",
				empDeptName = "",
				groupID = "G01",
				groupName = "0",
				isSupervisor = True,
				enableVoice = True,
				enableEmail = False,
				enableChat = False,
				enableVideo = False,
				enableCallBack = False,
				disableAutoAnswer = False,
				status = "1",
				connectionID = "b36189cb-29af-4cee-9475-5e0550fb5b82",
				clientIP = "1.171.212.126",
				hubName = "AGDDev",
				retTime = "None",
				protocol = "",
				iLine = 0,
				currLineButtonState = "",
				currLinelabelTypeText = "",
				currLinelabelStatusText = "",
				currLinelabelForeStatusText = "",
				voiceLines = "",
				voiceUpdTime = "None",
				emailLines = "",
				emailUpdTime = "None",
				chatLines = "",
				chatUpdTime = "None",
				videoLines = "",
				videoUpdTime = "None",
				agentIsFree = True,
				voiceIsFree = True,
				emailIsFree = True,
				chatIsFree = True,
				videoIsFree = True,
				creator = "AGDDev",
				creatorName = "AGDDev",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspHubClientsInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            HubClientsService HubClientsService = new
                HubClientsService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await HubClientsService.InsertHubClients(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }


        [Test]
        public async ValueTask UpdateHubClientsTest()
        {  //arrange
            var updateReq = new HubClientsUpdateRequest
            {
				agentLoginID = "TP011234",
				agentLoginCode = "1234",
				userID = "admin",
				userName = "林管理",
				nickName = "MR LIN",
				location = "1",
				thisDN = "",
				officeEmail = "",
				deptNum = "3",
				empDeptCode = "",
				empDeptName = "",
				groupID = "G01",
				groupName = "0",
				isSupervisor = True,
				enableVoice = True,
				enableEmail = False,
				enableChat = False,
				enableVideo = False,
				enableCallBack = False,
				disableAutoAnswer = False,
				status = "1",
				connectionID = "b36189cb-29af-4cee-9475-5e0550fb5b82",
				clientIP = "1.171.212.126",
				hubName = "AGDDev",
				retTime = "None",
				protocol = "",
				iLine = 0,
				currLineButtonState = "",
				currLinelabelTypeText = "",
				currLinelabelStatusText = "",
				currLinelabelForeStatusText = "",
				voiceLines = "",
				voiceUpdTime = "None",
				emailLines = "",
				emailUpdTime = "None",
				chatLines = "",
				chatUpdTime = "None",
				videoLines = "",
				videoUpdTime = "None",
				agentIsFree = True,
				voiceIsFree = True,
				emailIsFree = True,
				chatIsFree = True,
				videoIsFree = True,
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspHubClientsUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            HubClientsService HubClientsService = new
                HubClientsService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await HubClientsService.UpdateHubClients(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask UpdateHubClientsTestFailed()
        {  //arrange
            var updateReq = new HubClientsUpdateRequest
            {
				agentLoginID = "TP011234",
				agentLoginCode = "1234",
				userID = "admin",
				userName = "林管理",
				nickName = "MR LIN",
				location = "1",
				thisDN = "",
				officeEmail = "",
				deptNum = "3",
				empDeptCode = "",
				empDeptName = "",
				groupID = "G01",
				groupName = "0",
				isSupervisor = True,
				enableVoice = True,
				enableEmail = False,
				enableChat = False,
				enableVideo = False,
				enableCallBack = False,
				disableAutoAnswer = False,
				status = "1",
				connectionID = "b36189cb-29af-4cee-9475-5e0550fb5b82",
				clientIP = "1.171.212.126",
				hubName = "AGDDev",
				retTime = "None",
				protocol = "",
				iLine = 0,
				currLineButtonState = "",
				currLinelabelTypeText = "",
				currLinelabelStatusText = "",
				currLinelabelForeStatusText = "",
				voiceLines = "",
				voiceUpdTime = "None",
				emailLines = "",
				emailUpdTime = "None",
				chatLines = "",
				chatUpdTime = "None",
				videoLines = "",
				videoUpdTime = "None",
				agentIsFree = True,
				voiceIsFree = True,
				emailIsFree = True,
				chatIsFree = True,
				videoIsFree = True,
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspHubClientsUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            HubClientsService HubClientsService = new
                HubClientsService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await HubClientsService.UpdateHubClients(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }




    }
}
