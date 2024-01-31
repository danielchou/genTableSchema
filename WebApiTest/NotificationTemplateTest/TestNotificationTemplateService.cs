using AutoMapper;
using ESUN.AGD.DataAccess.DataService.DataAccess;
using ESUN.AGD.DataAccess.Type;
using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.Common;
using ESUN.AGD.WebApi.Application.NotificationTemplate;
using ESUN.AGD.WebApi.Application.NotificationTemplate.Contract;
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


namespace ESUN.AGD.WebApi.Test.NotificationTemplateTest
{
    [TestFixture]
    public class TestNotificationTemplateService
    {
        private static IDataAccessService stubDataAccessService = Substitute.For<IDataAccessService>();
        private static IGetTokenService stubGetTokenService = Substitute.For<IGetTokenService>();
        private static IHttpContextAccessor stubHttpContextAccessor = Substitute.For<IHttpContextAccessor>();
        private static IMapper stubMapper = Substitute.For<IMapper>();
        private static IUserService stubUserService = Substitute.For<IUserService>();
        private static IAutoNextNumService autoNextNumService = Substitute.For<IAutoNextNumService>();

        [Test]
        public async ValueTask GetNotificationTemplateTest()
        {  //arrange
            int seqNo = 1;
            string resCode = "U200";
           
            stubDataAccessService
                .LoadSingleData<TbNotificationTemplate, object>("agdSp.uspNotificationTemplateGet", Arg.Any<int>())
                .ReturnsForAnyArgs(new TbNotificationTemplate { SeqNo = 1 });

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            NotificationTemplateService NotificationTemplateService = 
                new NotificationTemplateService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await NotificationTemplateService.GetNotificationTemplate(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspNotificationTemplateGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode,resCode);

        }

        [Test]
        public async ValueTask GetNotificationTemplateTestFailed()
        {  //arrange
            int seqNo = 0;
            string resCode = "U999";
          
            stubDataAccessService.LoadSingleData<TbNotificationTemplate, object>("agdSp.uspNotificationTemplateGet", Arg.Any<int>())
                .ReturnsNull();          

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            NotificationTemplateService NotificationTemplateService = new NotificationTemplateService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await NotificationTemplateService.GetNotificationTemplate(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<TbNotificationTemplate, object>("agdSp.uspNotificationTemplateGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryNotificationTemplateTest()
        {  //arrange
            var queryReq = new NotificationTemplateQueryRequest { 						notificationTemplateID = 8,
						notificationType = "1",
						content = "請記得量體溫",
						isEnable = True, };
            string resCode = "U200";
            
            stubDataAccessService.LoadData<TbNotificationTemplate, object>
                (storeProcedure: "agdSp.uspNotificationTemplateQuery", Arg.Any<object>())
               .Returns(new TbNotificationTemplate[] { 
                    new TbNotificationTemplate { 
						notificationTemplateID = 8,
						notificationType = "1",
						content = "請記得量體溫",
						isEnable = True,
                        Total = 2
                    },
                    new TbNotificationTemplate { } 
                }.AsQueryable());
            
            stubHttpContextAccessor.HttpContext.Request.Path ="/api/NotificationTemplate/query";
            
            NotificationTemplateService NotificationTemplateService = new 
                NotificationTemplateService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await NotificationTemplateService.QueryNotificationTemplate(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
          
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryNotificationTemplateTestFailed()
        {  //arrange
            var queryReq = new NotificationTemplateQueryRequest {  
						notificationTemplateID = 8,
						notificationType = "1",
						content = "請記得量體溫",
						isEnable = True,
            };
            string resCode = "U999";
            
            stubDataAccessService.LoadData<TbNotificationTemplate, object>("agdSp.uspNotificationTemplateQuery", Arg.Any<object>())
                .ReturnsNull();
            
            stubHttpContextAccessor.HttpContext.Request.Path = "/api/NotificationTemplate/query";
            NotificationTemplateService NotificationTemplateService = new NotificationTemplateService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await NotificationTemplateService.QueryNotificationTemplate(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspNotificationTemplateGet", Arg.Any<object>());
            Assert.AreEqual(result.total, 0);

        }

        [Test]
        public async ValueTask InsertNotificationTemplateTest()
        {  //arrange
            var insertReq = new NotificationTemplateInsertRequest
            {
				notificationTemplateID = 8,
				notificationType = "1",
				notificationName = "防疫期間規定",
				content = "請記得量體溫",
				displayOrder = 1,
				isEnable = True,
				creator = "admin",
				creatorName = "林管理",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspNotificationTemplateInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result:new SqlResponse() { ErrorMsg="",Msg="" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            NotificationTemplateService NotificationTemplateService = new
                NotificationTemplateService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await NotificationTemplateService.InsertNotificationTemplate(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask InsertNotificationTemplateTestFailed()
        {  //arrange
            var insertReq = new NotificationTemplateInsertRequest
            {
				notificationTemplateID = 8,
				notificationType = "1",
				notificationName = "防疫期間規定",
				content = "請記得量體溫",
				displayOrder = 1,
				isEnable = True,
				creator = "admin",
				creatorName = "林管理",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspNotificationTemplateInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            NotificationTemplateService NotificationTemplateService = new
                NotificationTemplateService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await NotificationTemplateService.InsertNotificationTemplate(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }


        [Test]
        public async ValueTask UpdateNotificationTemplateTest()
        {  //arrange
            var updateReq = new NotificationTemplateUpdateRequest
            {
				notificationTemplateID = 13,
				notificationType = "1",
				notificationName = "TEST",
				content = "XXXX",
				displayOrder = 0,
				isEnable = True,
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspNotificationTemplateUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            NotificationTemplateService NotificationTemplateService = new
                NotificationTemplateService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await NotificationTemplateService.UpdateNotificationTemplate(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask UpdateNotificationTemplateTestFailed()
        {  //arrange
            var updateReq = new NotificationTemplateUpdateRequest
            {
				notificationTemplateID = 13,
				notificationType = "1",
				notificationName = "TEST",
				content = "XXXX",
				displayOrder = 0,
				isEnable = True,
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspNotificationTemplateUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            NotificationTemplateService NotificationTemplateService = new
                NotificationTemplateService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await NotificationTemplateService.UpdateNotificationTemplate(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }




    }
}
