using AutoMapper;
using ESUN.AGD.DataAccess.DataService.DataAccess;
using ESUN.AGD.DataAccess.Type;
using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.Common;
using ESUN.AGD.WebApi.Application.UserRoleHist;
using ESUN.AGD.WebApi.Application.UserRoleHist.Contract;
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


namespace ESUN.AGD.WebApi.Test.UserRoleHistTest
{
    [TestFixture]
    public class TestUserRoleHistService
    {
        private static IDataAccessService stubDataAccessService = Substitute.For<IDataAccessService>();
        private static IGetTokenService stubGetTokenService = Substitute.For<IGetTokenService>();
        private static IHttpContextAccessor stubHttpContextAccessor = Substitute.For<IHttpContextAccessor>();
        private static IMapper stubMapper = Substitute.For<IMapper>();
        private static IUserService stubUserService = Substitute.For<IUserService>();
        private static IAutoNextNumService autoNextNumService = Substitute.For<IAutoNextNumService>();

        [Test]
        public async ValueTask GetUserRoleHistTest()
        {  //arrange
            int seqNo = 1;
            string resCode = "U200";
           
            stubDataAccessService
                .LoadSingleData<TbUserRoleHist, object>("agdSp.uspUserRoleHistGet", Arg.Any<int>())
                .ReturnsForAnyArgs(new TbUserRoleHist { SeqNo = 1 });

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            UserRoleHistService UserRoleHistService = 
                new UserRoleHistService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await UserRoleHistService.GetUserRoleHist(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspUserRoleHistGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode,resCode);

        }

        [Test]
        public async ValueTask GetUserRoleHistTestFailed()
        {  //arrange
            int seqNo = 0;
            string resCode = "U999";
          
            stubDataAccessService.LoadSingleData<TbUserRoleHist, object>("agdSp.uspUserRoleHistGet", Arg.Any<int>())
                .ReturnsNull();          

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            UserRoleHistService UserRoleHistService = new UserRoleHistService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await UserRoleHistService.GetUserRoleHist(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<TbUserRoleHist, object>("agdSp.uspUserRoleHistGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryUserRoleHistTest()
        {  //arrange
            var queryReq = new UserRoleHistQueryRequest { 						userID = "10001",
						roleID = "R000",
						actionDT = "2022-09-29 17:57:21",
						actor = "admin", };
            string resCode = "U200";
            
            stubDataAccessService.LoadData<TbUserRoleHist, object>
                (storeProcedure: "agdSp.uspUserRoleHistQuery", Arg.Any<object>())
               .Returns(new TbUserRoleHist[] { 
                    new TbUserRoleHist { 
						userID = "10001",
						roleID = "R000",
						actionDT = "2022-09-29 17:57:21",
						actor = "admin",
                        Total = 2
                    },
                    new TbUserRoleHist { } 
                }.AsQueryable());
            
            stubHttpContextAccessor.HttpContext.Request.Path ="/api/UserRoleHist/query";
            
            UserRoleHistService UserRoleHistService = new 
                UserRoleHistService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await UserRoleHistService.QueryUserRoleHist(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
          
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryUserRoleHistTestFailed()
        {  //arrange
            var queryReq = new UserRoleHistQueryRequest {  
						userID = "10001",
						roleID = "R000",
						actionDT = "2022-09-29 17:57:21",
						actor = "admin",
            };
            string resCode = "U999";
            
            stubDataAccessService.LoadData<TbUserRoleHist, object>("agdSp.uspUserRoleHistQuery", Arg.Any<object>())
                .ReturnsNull();
            
            stubHttpContextAccessor.HttpContext.Request.Path = "/api/UserRoleHist/query";
            UserRoleHistService UserRoleHistService = new UserRoleHistService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await UserRoleHistService.QueryUserRoleHist(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspUserRoleHistGet", Arg.Any<object>());
            Assert.AreEqual(result.total, 0);

        }

        [Test]
        public async ValueTask InsertUserRoleHistTest()
        {  //arrange
            var insertReq = new UserRoleHistInsertRequest
            {
				userID = "10001",
				roleID = "R000",
				creator = "admin",
				creatorName = "林管理",
				actionType = "I",
				actionDT = "2022-09-29 17:57:21",
				actor = "admin",
				actorName = "林管理",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspUserRoleHistInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result:new SqlResponse() { ErrorMsg="",Msg="" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            UserRoleHistService UserRoleHistService = new
                UserRoleHistService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await UserRoleHistService.InsertUserRoleHist(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask InsertUserRoleHistTestFailed()
        {  //arrange
            var insertReq = new UserRoleHistInsertRequest
            {
				userID = "10001",
				roleID = "R000",
				creator = "admin",
				creatorName = "林管理",
				actionType = "I",
				actionDT = "2022-09-29 17:57:21",
				actor = "admin",
				actorName = "林管理",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspUserRoleHistInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            UserRoleHistService UserRoleHistService = new
                UserRoleHistService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await UserRoleHistService.InsertUserRoleHist(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }


        [Test]
        public async ValueTask UpdateUserRoleHistTest()
        {  //arrange
            var updateReq = new UserRoleHistUpdateRequest
            {
				userID = "sys",
				roleID = "R010",
				actionType = "D",
				actionDT = "2022-08-30 17:06:48",
				actor = "admin",
				actorName = "林管理",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspUserRoleHistUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            UserRoleHistService UserRoleHistService = new
                UserRoleHistService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await UserRoleHistService.UpdateUserRoleHist(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask UpdateUserRoleHistTestFailed()
        {  //arrange
            var updateReq = new UserRoleHistUpdateRequest
            {
				userID = "sys",
				roleID = "R010",
				actionType = "D",
				actionDT = "2022-08-30 17:06:48",
				actor = "admin",
				actorName = "林管理",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspUserRoleHistUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            UserRoleHistService UserRoleHistService = new
                UserRoleHistService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await UserRoleHistService.UpdateUserRoleHist(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }




    }
}
