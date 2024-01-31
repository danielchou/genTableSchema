using AutoMapper;
using ESUN.AGD.DataAccess.DataService.DataAccess;
using ESUN.AGD.DataAccess.Type;
using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.Common;
using ESUN.AGD.WebApi.Application.UserOvertime;
using ESUN.AGD.WebApi.Application.UserOvertime.Contract;
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


namespace ESUN.AGD.WebApi.Test.UserOvertimeTest
{
    [TestFixture]
    public class TestUserOvertimeService
    {
        private static IDataAccessService stubDataAccessService = Substitute.For<IDataAccessService>();
        private static IGetTokenService stubGetTokenService = Substitute.For<IGetTokenService>();
        private static IHttpContextAccessor stubHttpContextAccessor = Substitute.For<IHttpContextAccessor>();
        private static IMapper stubMapper = Substitute.For<IMapper>();
        private static IUserService stubUserService = Substitute.For<IUserService>();
        private static IAutoNextNumService autoNextNumService = Substitute.For<IAutoNextNumService>();

        [Test]
        public async ValueTask GetUserOvertimeTest()
        {  //arrange
            int seqNo = 1;
            string resCode = "U200";
           
            stubDataAccessService
                .LoadSingleData<TbUserOvertime, object>("agdSp.uspUserOvertimeGet", Arg.Any<int>())
                .ReturnsForAnyArgs(new TbUserOvertime { SeqNo = 1 });

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            UserOvertimeService UserOvertimeService = 
                new UserOvertimeService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await UserOvertimeService.GetUserOvertime(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspUserOvertimeGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode,resCode);

        }

        [Test]
        public async ValueTask GetUserOvertimeTestFailed()
        {  //arrange
            int seqNo = 0;
            string resCode = "U999";
          
            stubDataAccessService.LoadSingleData<TbUserOvertime, object>("agdSp.uspUserOvertimeGet", Arg.Any<int>())
                .ReturnsNull();          

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            UserOvertimeService UserOvertimeService = new UserOvertimeService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await UserOvertimeService.GetUserOvertime(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<TbUserOvertime, object>("agdSp.uspUserOvertimeGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryUserOvertimeTest()
        {  //arrange
            var queryReq = new UserOvertimeQueryRequest {  };
            string resCode = "U200";
            
            stubDataAccessService.LoadData<TbUserOvertime, object>
                (storeProcedure: "agdSp.uspUserOvertimeQuery", Arg.Any<object>())
               .Returns(new TbUserOvertime[] { 
                    new TbUserOvertime { 

                        Total = 2
                    },
                    new TbUserOvertime { } 
                }.AsQueryable());
            
            stubHttpContextAccessor.HttpContext.Request.Path ="/api/UserOvertime/query";
            
            UserOvertimeService UserOvertimeService = new 
                UserOvertimeService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await UserOvertimeService.QueryUserOvertime(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
          
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryUserOvertimeTestFailed()
        {  //arrange
            var queryReq = new UserOvertimeQueryRequest {  

            };
            string resCode = "U999";
            
            stubDataAccessService.LoadData<TbUserOvertime, object>("agdSp.uspUserOvertimeQuery", Arg.Any<object>())
                .ReturnsNull();
            
            stubHttpContextAccessor.HttpContext.Request.Path = "/api/UserOvertime/query";
            UserOvertimeService UserOvertimeService = new UserOvertimeService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await UserOvertimeService.QueryUserOvertime(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspUserOvertimeGet", Arg.Any<object>());
            Assert.AreEqual(result.total, 0);

        }

        [Test]
        public async ValueTask InsertUserOvertimeTest()
        {  //arrange
            var insertReq = new UserOvertimeInsertRequest
            {
				userID = "06916",
				overtimeDate = "2022-04-04",
				creator = "10046",
				creatorName = "陳麗杰鰻魚飯",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspUserOvertimeInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result:new SqlResponse() { ErrorMsg="",Msg="" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            UserOvertimeService UserOvertimeService = new
                UserOvertimeService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await UserOvertimeService.InsertUserOvertime(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask InsertUserOvertimeTestFailed()
        {  //arrange
            var insertReq = new UserOvertimeInsertRequest
            {
				userID = "06916",
				overtimeDate = "2022-04-04",
				creator = "10046",
				creatorName = "陳麗杰鰻魚飯",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspUserOvertimeInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            UserOvertimeService UserOvertimeService = new
                UserOvertimeService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await UserOvertimeService.InsertUserOvertime(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }


        [Test]
        public async ValueTask UpdateUserOvertimeTest()
        {  //arrange
            var updateReq = new UserOvertimeUpdateRequest
            {
				userID = "21272",
				overtimeDate = "2022-04-21",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspUserOvertimeUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            UserOvertimeService UserOvertimeService = new
                UserOvertimeService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await UserOvertimeService.UpdateUserOvertime(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask UpdateUserOvertimeTestFailed()
        {  //arrange
            var updateReq = new UserOvertimeUpdateRequest
            {
				userID = "21272",
				overtimeDate = "2022-04-21",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspUserOvertimeUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            UserOvertimeService UserOvertimeService = new
                UserOvertimeService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await UserOvertimeService.UpdateUserOvertime(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }




    }
}
