using AutoMapper;
using ESUN.AGD.DataAccess.DataService.DataAccess;
using ESUN.AGD.DataAccess.Type;
using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.Common;
using ESUN.AGD.WebApi.Application.UserHist;
using ESUN.AGD.WebApi.Application.UserHist.Contract;
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


namespace ESUN.AGD.WebApi.Test.UserHistTest
{
    [TestFixture]
    public class TestUserHistService
    {
        private static IDataAccessService stubDataAccessService = Substitute.For<IDataAccessService>();
        private static IGetTokenService stubGetTokenService = Substitute.For<IGetTokenService>();
        private static IHttpContextAccessor stubHttpContextAccessor = Substitute.For<IHttpContextAccessor>();
        private static IMapper stubMapper = Substitute.For<IMapper>();
        private static IUserService stubUserService = Substitute.For<IUserService>();
        private static IAutoNextNumService autoNextNumService = Substitute.For<IAutoNextNumService>();

        [Test]
        public async ValueTask GetUserHistTest()
        {  //arrange
            int seqNo = 1;
            string resCode = "U200";
           
            stubDataAccessService
                .LoadSingleData<TbUserHist, object>("agdSp.uspUserHistGet", Arg.Any<int>())
                .ReturnsForAnyArgs(new TbUserHist { SeqNo = 1 });

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            UserHistService UserHistService = 
                new UserHistService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await UserHistService.GetUserHist(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspUserHistGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode,resCode);

        }

        [Test]
        public async ValueTask GetUserHistTestFailed()
        {  //arrange
            int seqNo = 0;
            string resCode = "U999";
          
            stubDataAccessService.LoadSingleData<TbUserHist, object>("agdSp.uspUserHistGet", Arg.Any<int>())
                .ReturnsNull();          

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            UserHistService UserHistService = new UserHistService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await UserHistService.GetUserHist(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<TbUserHist, object>("agdSp.uspUserHistGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryUserHistTest()
        {  //arrange
            var queryReq = new UserHistQueryRequest { 						userID = "10001",
						agentLoginID = "",
						actionDT = "2022-09-29 17:50:21",
						actor = "admin", };
            string resCode = "U200";
            
            stubDataAccessService.LoadData<TbUserHist, object>
                (storeProcedure: "agdSp.uspUserHistQuery", Arg.Any<object>())
               .Returns(new TbUserHist[] { 
                    new TbUserHist { 
						userID = "10001",
						agentLoginID = "",
						actionDT = "2022-09-29 17:50:21",
						actor = "admin",
                        Total = 2
                    },
                    new TbUserHist { } 
                }.AsQueryable());
            
            stubHttpContextAccessor.HttpContext.Request.Path ="/api/UserHist/query";
            
            UserHistService UserHistService = new 
                UserHistService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await UserHistService.QueryUserHist(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
          
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryUserHistTestFailed()
        {  //arrange
            var queryReq = new UserHistQueryRequest {  
						userID = "10001",
						agentLoginID = "",
						actionDT = "2022-09-29 17:50:21",
						actor = "admin",
            };
            string resCode = "U999";
            
            stubDataAccessService.LoadData<TbUserHist, object>("agdSp.uspUserHistQuery", Arg.Any<object>())
                .ReturnsNull();
            
            stubHttpContextAccessor.HttpContext.Request.Path = "/api/UserHist/query";
            UserHistService UserHistService = new UserHistService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await UserHistService.QueryUserHist(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspUserHistGet", Arg.Any<object>());
            Assert.AreEqual(result.total, 0);

        }

        [Test]
        public async ValueTask InsertUserHistTest()
        {  //arrange
            var insertReq = new UserHistInsertRequest
            {
				userID = "10001",
				userName = "蔡欣蓁",
				nickName = "",
				agentLoginID = "",
				agentLoginCode = "",
				empDeptCode = "D01",
				empDept = "業務單位",
				empSectCode = "SE01",
				empSectDept = "金融組",
				officeEmail = "A0001@gmail.com",
				employedStatusCode = "1",
				groupID = "",
				isSupervisor = False,
				rACECode1 = "",
				rACECode2 = "",
				rACECode3 = "",
				rACECode4 = "",
				rACECode5 = "",
				isEnable = True,
				creator = "admin",
				creatorName = "林管理",
				actionType = "I",
				actionDT = "2022-09-29 17:50:21",
				actor = "admin",
				actorName = "林管理",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspUserHistInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result:new SqlResponse() { ErrorMsg="",Msg="" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            UserHistService UserHistService = new
                UserHistService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await UserHistService.InsertUserHist(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask InsertUserHistTestFailed()
        {  //arrange
            var insertReq = new UserHistInsertRequest
            {
				userID = "10001",
				userName = "蔡欣蓁",
				nickName = "",
				agentLoginID = "",
				agentLoginCode = "",
				empDeptCode = "D01",
				empDept = "業務單位",
				empSectCode = "SE01",
				empSectDept = "金融組",
				officeEmail = "A0001@gmail.com",
				employedStatusCode = "1",
				groupID = "",
				isSupervisor = False,
				rACECode1 = "",
				rACECode2 = "",
				rACECode3 = "",
				rACECode4 = "",
				rACECode5 = "",
				isEnable = True,
				creator = "admin",
				creatorName = "林管理",
				actionType = "I",
				actionDT = "2022-09-29 17:50:21",
				actor = "admin",
				actorName = "林管理",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspUserHistInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            UserHistService UserHistService = new
                UserHistService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await UserHistService.InsertUserHist(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }


        [Test]
        public async ValueTask UpdateUserHistTest()
        {  //arrange
            var updateReq = new UserHistUpdateRequest
            {
				userID = "sys",
				userName = "陳麗杰鰻魚飯",
				nickName = "MR SYS",
				agentLoginID = "1234",
				agentLoginCode = "1234",
				empDeptCode = "DAA",
				empDept = "行政一單位",
				empSectCode = "SEAA",
				empSectDept = "金融一組",
				officeEmail = "A0046AAA@gmail.com",
				employedStatusCode = "3",
				groupID = "G01",
				isSupervisor = True,
				rACECode1 = "RACE1",
				rACECode2 = "RACE2",
				rACECode3 = "RACE3",
				rACECode4 = "RACE4",
				rACECode5 = "RACE5",
				isEnable = True,
				actionType = "U",
				actionDT = "2022-08-17 17:38:27",
				actor = "SYS",
				actorName = "系統觸發",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspUserHistUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            UserHistService UserHistService = new
                UserHistService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await UserHistService.UpdateUserHist(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask UpdateUserHistTestFailed()
        {  //arrange
            var updateReq = new UserHistUpdateRequest
            {
				userID = "sys",
				userName = "陳麗杰鰻魚飯",
				nickName = "MR SYS",
				agentLoginID = "1234",
				agentLoginCode = "1234",
				empDeptCode = "DAA",
				empDept = "行政一單位",
				empSectCode = "SEAA",
				empSectDept = "金融一組",
				officeEmail = "A0046AAA@gmail.com",
				employedStatusCode = "3",
				groupID = "G01",
				isSupervisor = True,
				rACECode1 = "RACE1",
				rACECode2 = "RACE2",
				rACECode3 = "RACE3",
				rACECode4 = "RACE4",
				rACECode5 = "RACE5",
				isEnable = True,
				actionType = "U",
				actionDT = "2022-08-17 17:38:27",
				actor = "SYS",
				actorName = "系統觸發",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspUserHistUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            UserHistService UserHistService = new
                UserHistService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await UserHistService.UpdateUserHist(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }




    }
}
