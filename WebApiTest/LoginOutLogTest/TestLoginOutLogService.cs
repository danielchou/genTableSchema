using AutoMapper;
using ESUN.AGD.DataAccess.DataService.DataAccess;
using ESUN.AGD.DataAccess.Type;
using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.Common;
using ESUN.AGD.WebApi.Application.LoginOutLog;
using ESUN.AGD.WebApi.Application.LoginOutLog.Contract;
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


namespace ESUN.AGD.WebApi.Test.LoginOutLogTest
{
    [TestFixture]
    public class TestLoginOutLogService
    {
        private static IDataAccessService stubDataAccessService = Substitute.For<IDataAccessService>();
        private static IGetTokenService stubGetTokenService = Substitute.For<IGetTokenService>();
        private static IHttpContextAccessor stubHttpContextAccessor = Substitute.For<IHttpContextAccessor>();
        private static IMapper stubMapper = Substitute.For<IMapper>();
        private static IUserService stubUserService = Substitute.For<IUserService>();
        private static IAutoNextNumService autoNextNumService = Substitute.For<IAutoNextNumService>();

        [Test]
        public async ValueTask GetLoginOutLogTest()
        {  //arrange
            int seqNo = 1;
            string resCode = "U200";
           
            stubDataAccessService
                .LoadSingleData<TbLoginOutLog, object>("agdSp.uspLoginOutLogGet", Arg.Any<int>())
                .ReturnsForAnyArgs(new TbLoginOutLog { SeqNo = 1 });

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            LoginOutLogService LoginOutLogService = 
                new LoginOutLogService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await LoginOutLogService.GetLoginOutLog(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspLoginOutLogGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode,resCode);

        }

        [Test]
        public async ValueTask GetLoginOutLogTestFailed()
        {  //arrange
            int seqNo = 0;
            string resCode = "U999";
          
            stubDataAccessService.LoadSingleData<TbLoginOutLog, object>("agdSp.uspLoginOutLogGet", Arg.Any<int>())
                .ReturnsNull();          

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            LoginOutLogService LoginOutLogService = new LoginOutLogService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await LoginOutLogService.GetLoginOutLog(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<TbLoginOutLog, object>("agdSp.uspLoginOutLogGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryLoginOutLogTest()
        {  //arrange
            var queryReq = new LoginOutLogQueryRequest { 						userID = "10022",
						loginIP = "20.127.58.130",
						loginSystemType = "supervisor",
						loginDT = "2022-06-24 18:26:23", };
            string resCode = "U200";
            
            stubDataAccessService.LoadData<TbLoginOutLog, object>
                (storeProcedure: "agdSp.uspLoginOutLogQuery", Arg.Any<object>())
               .Returns(new TbLoginOutLog[] { 
                    new TbLoginOutLog { 
						userID = "10022",
						loginIP = "20.127.58.130",
						loginSystemType = "supervisor",
						loginDT = "2022-06-24 18:26:23",
                        Total = 2
                    },
                    new TbLoginOutLog { } 
                }.AsQueryable());
            
            stubHttpContextAccessor.HttpContext.Request.Path ="/api/LoginOutLog/query";
            
            LoginOutLogService LoginOutLogService = new 
                LoginOutLogService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await LoginOutLogService.QueryLoginOutLog(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
          
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryLoginOutLogTestFailed()
        {  //arrange
            var queryReq = new LoginOutLogQueryRequest {  
						userID = "10022",
						loginIP = "20.127.58.130",
						loginSystemType = "supervisor",
						loginDT = "2022-06-24 18:26:23",
            };
            string resCode = "U999";
            
            stubDataAccessService.LoadData<TbLoginOutLog, object>("agdSp.uspLoginOutLogQuery", Arg.Any<object>())
                .ReturnsNull();
            
            stubHttpContextAccessor.HttpContext.Request.Path = "/api/LoginOutLog/query";
            LoginOutLogService LoginOutLogService = new LoginOutLogService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await LoginOutLogService.QueryLoginOutLog(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspLoginOutLogGet", Arg.Any<object>());
            Assert.AreEqual(result.total, 0);

        }

        [Test]
        public async ValueTask InsertLoginOutLogTest()
        {  //arrange
            var insertReq = new LoginOutLogInsertRequest
            {
				userID = "10022",
				userName = "蔡彥文",
				loginIP = "20.127.58.130",
				loginSystemType = "supervisor",
				loginDT = "2022-06-24 18:26:23",
				logoutDT = "None",
				creator = "10022",
				creatorName = "蔡彥文",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspLoginOutLogInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result:new SqlResponse() { ErrorMsg="",Msg="" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            LoginOutLogService LoginOutLogService = new
                LoginOutLogService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await LoginOutLogService.InsertLoginOutLog(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask InsertLoginOutLogTestFailed()
        {  //arrange
            var insertReq = new LoginOutLogInsertRequest
            {
				userID = "10022",
				userName = "蔡彥文",
				loginIP = "20.127.58.130",
				loginSystemType = "supervisor",
				loginDT = "2022-06-24 18:26:23",
				logoutDT = "None",
				creator = "10022",
				creatorName = "蔡彥文",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspLoginOutLogInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            LoginOutLogService LoginOutLogService = new
                LoginOutLogService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await LoginOutLogService.InsertLoginOutLog(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }


        [Test]
        public async ValueTask UpdateLoginOutLogTest()
        {  //arrange
            var updateReq = new LoginOutLogUpdateRequest
            {
				userID = "ag002",
				userName = "李小姐",
				loginIP = "20.127.58.130",
				loginSystemType = "agd",
				loginDT = "2022-08-30 17:33:48",
				logoutDT = "None",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspLoginOutLogUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            LoginOutLogService LoginOutLogService = new
                LoginOutLogService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await LoginOutLogService.UpdateLoginOutLog(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask UpdateLoginOutLogTestFailed()
        {  //arrange
            var updateReq = new LoginOutLogUpdateRequest
            {
				userID = "ag002",
				userName = "李小姐",
				loginIP = "20.127.58.130",
				loginSystemType = "agd",
				loginDT = "2022-08-30 17:33:48",
				logoutDT = "None",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspLoginOutLogUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            LoginOutLogService LoginOutLogService = new
                LoginOutLogService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await LoginOutLogService.UpdateLoginOutLog(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }




    }
}
