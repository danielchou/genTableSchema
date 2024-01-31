using AutoMapper;
using ESUN.AGD.DataAccess.DataService.DataAccess;
using ESUN.AGD.DataAccess.Type;
using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.Common;
using ESUN.AGD.WebApi.Application.NotificationLog;
using ESUN.AGD.WebApi.Application.NotificationLog.Contract;
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


namespace ESUN.AGD.WebApi.Test.NotificationLogTest
{
    [TestFixture]
    public class TestNotificationLogService
    {
        private static IDataAccessService stubDataAccessService = Substitute.For<IDataAccessService>();
        private static IGetTokenService stubGetTokenService = Substitute.For<IGetTokenService>();
        private static IHttpContextAccessor stubHttpContextAccessor = Substitute.For<IHttpContextAccessor>();
        private static IMapper stubMapper = Substitute.For<IMapper>();
        private static IUserService stubUserService = Substitute.For<IUserService>();
        private static IAutoNextNumService autoNextNumService = Substitute.For<IAutoNextNumService>();

        [Test]
        public async ValueTask GetNotificationLogTest()
        {  //arrange
            int seqNo = 1;
            string resCode = "U200";
           
            stubDataAccessService
                .LoadSingleData<TbNotificationLog, object>("agdSp.uspNotificationLogGet", Arg.Any<int>())
                .ReturnsForAnyArgs(new TbNotificationLog { SeqNo = 1 });

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            NotificationLogService NotificationLogService = 
                new NotificationLogService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await NotificationLogService.GetNotificationLog(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspNotificationLogGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode,resCode);

        }

        [Test]
        public async ValueTask GetNotificationLogTestFailed()
        {  //arrange
            int seqNo = 0;
            string resCode = "U999";
          
            stubDataAccessService.LoadSingleData<TbNotificationLog, object>("agdSp.uspNotificationLogGet", Arg.Any<int>())
                .ReturnsNull();          

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            NotificationLogService NotificationLogService = new NotificationLogService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await NotificationLogService.GetNotificationLog(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<TbNotificationLog, object>("agdSp.uspNotificationLogGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryNotificationLogTest()
        {  //arrange
            var queryReq = new NotificationLogQueryRequest { 						notificationType = "1",
						notificationID = 6,
						notificationName = "防疫期間規定", };
            string resCode = "U200";
            
            stubDataAccessService.LoadData<TbNotificationLog, object>
                (storeProcedure: "agdSp.uspNotificationLogQuery", Arg.Any<object>())
               .Returns(new TbNotificationLog[] { 
                    new TbNotificationLog { 
						notificationType = "1",
						notificationID = 6,
						notificationName = "防疫期間規定",
                        Total = 2
                    },
                    new TbNotificationLog { } 
                }.AsQueryable());
            
            stubHttpContextAccessor.HttpContext.Request.Path ="/api/NotificationLog/query";
            
            NotificationLogService NotificationLogService = new 
                NotificationLogService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await NotificationLogService.QueryNotificationLog(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
          
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryNotificationLogTestFailed()
        {  //arrange
            var queryReq = new NotificationLogQueryRequest {  
						notificationType = "1",
						notificationID = 6,
						notificationName = "防疫期間規定",
            };
            string resCode = "U999";
            
            stubDataAccessService.LoadData<TbNotificationLog, object>("agdSp.uspNotificationLogQuery", Arg.Any<object>())
                .ReturnsNull();
            
            stubHttpContextAccessor.HttpContext.Request.Path = "/api/NotificationLog/query";
            NotificationLogService NotificationLogService = new NotificationLogService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await NotificationLogService.QueryNotificationLog(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspNotificationLogGet", Arg.Any<object>());
            Assert.AreEqual(result.total, 0);

        }

        [Test]
        public async ValueTask InsertNotificationLogTest()
        {  //arrange
            var insertReq = new NotificationLogInsertRequest
            {
				notificationType = "1",
				notificationID = 6,
				notificationName = "防疫期間規定",
				content = "請記得量體溫",
				sendType = "1",
				sendTo = "0",
				creator = "admin",
				creatorName = "林管理",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspNotificationLogInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result:new SqlResponse() { ErrorMsg="",Msg="" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            NotificationLogService NotificationLogService = new
                NotificationLogService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await NotificationLogService.InsertNotificationLog(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask InsertNotificationLogTestFailed()
        {  //arrange
            var insertReq = new NotificationLogInsertRequest
            {
				notificationType = "1",
				notificationID = 6,
				notificationName = "防疫期間規定",
				content = "請記得量體溫",
				sendType = "1",
				sendTo = "0",
				creator = "admin",
				creatorName = "林管理",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspNotificationLogInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            NotificationLogService NotificationLogService = new
                NotificationLogService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await NotificationLogService.InsertNotificationLog(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }


        [Test]
        public async ValueTask UpdateNotificationLogTest()
        {  //arrange
            var updateReq = new NotificationLogUpdateRequest
            {
				notificationType = "6",
				notificationID = 25,
				notificationName = "ffff",
				content = "aaaaaaaaaa",
				sendType = "5",
				sendTo = "14388",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspNotificationLogUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            NotificationLogService NotificationLogService = new
                NotificationLogService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await NotificationLogService.UpdateNotificationLog(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask UpdateNotificationLogTestFailed()
        {  //arrange
            var updateReq = new NotificationLogUpdateRequest
            {
				notificationType = "6",
				notificationID = 25,
				notificationName = "ffff",
				content = "aaaaaaaaaa",
				sendType = "5",
				sendTo = "14388",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspNotificationLogUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            NotificationLogService NotificationLogService = new
                NotificationLogService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await NotificationLogService.UpdateNotificationLog(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }




    }
}
