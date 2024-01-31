using AutoMapper;
using ESUN.AGD.DataAccess.DataService.DataAccess;
using ESUN.AGD.DataAccess.Type;
using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.Common;
using ESUN.AGD.WebApi.Application.UserNotifyLog;
using ESUN.AGD.WebApi.Application.UserNotifyLog.Contract;
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


namespace ESUN.AGD.WebApi.Test.UserNotifyLogTest
{
    [TestFixture]
    public class TestUserNotifyLogService
    {
        private static IDataAccessService stubDataAccessService = Substitute.For<IDataAccessService>();
        private static IGetTokenService stubGetTokenService = Substitute.For<IGetTokenService>();
        private static IHttpContextAccessor stubHttpContextAccessor = Substitute.For<IHttpContextAccessor>();
        private static IMapper stubMapper = Substitute.For<IMapper>();
        private static IUserService stubUserService = Substitute.For<IUserService>();
        private static IAutoNextNumService autoNextNumService = Substitute.For<IAutoNextNumService>();

        [Test]
        public async ValueTask GetUserNotifyLogTest()
        {  //arrange
            int seqNo = 1;
            string resCode = "U200";
           
            stubDataAccessService
                .LoadSingleData<TbUserNotifyLog, object>("agdSp.uspUserNotifyLogGet", Arg.Any<int>())
                .ReturnsForAnyArgs(new TbUserNotifyLog { SeqNo = 1 });

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            UserNotifyLogService UserNotifyLogService = 
                new UserNotifyLogService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await UserNotifyLogService.GetUserNotifyLog(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspUserNotifyLogGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode,resCode);

        }

        [Test]
        public async ValueTask GetUserNotifyLogTestFailed()
        {  //arrange
            int seqNo = 0;
            string resCode = "U999";
          
            stubDataAccessService.LoadSingleData<TbUserNotifyLog, object>("agdSp.uspUserNotifyLogGet", Arg.Any<int>())
                .ReturnsNull();          

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            UserNotifyLogService UserNotifyLogService = new UserNotifyLogService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await UserNotifyLogService.GetUserNotifyLog(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<TbUserNotifyLog, object>("agdSp.uspUserNotifyLogGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryUserNotifyLogTest()
        {  //arrange
            var queryReq = new UserNotifyLogQueryRequest { 						notificationID = 4,
						receiveUserID = "10046", };
            string resCode = "U200";
            
            stubDataAccessService.LoadData<TbUserNotifyLog, object>
                (storeProcedure: "agdSp.uspUserNotifyLogQuery", Arg.Any<object>())
               .Returns(new TbUserNotifyLog[] { 
                    new TbUserNotifyLog { 
						notificationID = 4,
						receiveUserID = "10046",
                        Total = 2
                    },
                    new TbUserNotifyLog { } 
                }.AsQueryable());
            
            stubHttpContextAccessor.HttpContext.Request.Path ="/api/UserNotifyLog/query";
            
            UserNotifyLogService UserNotifyLogService = new 
                UserNotifyLogService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await UserNotifyLogService.QueryUserNotifyLog(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
          
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryUserNotifyLogTestFailed()
        {  //arrange
            var queryReq = new UserNotifyLogQueryRequest {  
						notificationID = 4,
						receiveUserID = "10046",
            };
            string resCode = "U999";
            
            stubDataAccessService.LoadData<TbUserNotifyLog, object>("agdSp.uspUserNotifyLogQuery", Arg.Any<object>())
                .ReturnsNull();
            
            stubHttpContextAccessor.HttpContext.Request.Path = "/api/UserNotifyLog/query";
            UserNotifyLogService UserNotifyLogService = new UserNotifyLogService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await UserNotifyLogService.QueryUserNotifyLog(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspUserNotifyLogGet", Arg.Any<object>());
            Assert.AreEqual(result.total, 0);

        }

        [Test]
        public async ValueTask InsertUserNotifyLogTest()
        {  //arrange
            var insertReq = new UserNotifyLogInsertRequest
            {
				notificationID = 4,
				receiveUserID = "10046",
				isRead = False,
				isTop = False,
				creator = "admin",
				creatorName = "林管理",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspUserNotifyLogInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result:new SqlResponse() { ErrorMsg="",Msg="" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            UserNotifyLogService UserNotifyLogService = new
                UserNotifyLogService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await UserNotifyLogService.InsertUserNotifyLog(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask InsertUserNotifyLogTestFailed()
        {  //arrange
            var insertReq = new UserNotifyLogInsertRequest
            {
				notificationID = 4,
				receiveUserID = "10046",
				isRead = False,
				isTop = False,
				creator = "admin",
				creatorName = "林管理",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspUserNotifyLogInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            UserNotifyLogService UserNotifyLogService = new
                UserNotifyLogService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await UserNotifyLogService.InsertUserNotifyLog(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }


        [Test]
        public async ValueTask UpdateUserNotifyLogTest()
        {  //arrange
            var updateReq = new UserNotifyLogUpdateRequest
            {
				notificationID = 32,
				receiveUserID = "sys",
				isRead = False,
				isTop = False,
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspUserNotifyLogUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            UserNotifyLogService UserNotifyLogService = new
                UserNotifyLogService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await UserNotifyLogService.UpdateUserNotifyLog(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask UpdateUserNotifyLogTestFailed()
        {  //arrange
            var updateReq = new UserNotifyLogUpdateRequest
            {
				notificationID = 32,
				receiveUserID = "sys",
				isRead = False,
				isTop = False,
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspUserNotifyLogUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            UserNotifyLogService UserNotifyLogService = new
                UserNotifyLogService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await UserNotifyLogService.UpdateUserNotifyLog(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }




    }
}
