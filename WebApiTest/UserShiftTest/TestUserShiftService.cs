using AutoMapper;
using ESUN.AGD.DataAccess.DataService.DataAccess;
using ESUN.AGD.DataAccess.Type;
using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.Common;
using ESUN.AGD.WebApi.Application.UserShift;
using ESUN.AGD.WebApi.Application.UserShift.Contract;
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


namespace ESUN.AGD.WebApi.Test.UserShiftTest
{
    [TestFixture]
    public class TestUserShiftService
    {
        private static IDataAccessService stubDataAccessService = Substitute.For<IDataAccessService>();
        private static IGetTokenService stubGetTokenService = Substitute.For<IGetTokenService>();
        private static IHttpContextAccessor stubHttpContextAccessor = Substitute.For<IHttpContextAccessor>();
        private static IMapper stubMapper = Substitute.For<IMapper>();
        private static IUserService stubUserService = Substitute.For<IUserService>();
        private static IAutoNextNumService autoNextNumService = Substitute.For<IAutoNextNumService>();

        [Test]
        public async ValueTask GetUserShiftTest()
        {  //arrange
            int seqNo = 1;
            string resCode = "U200";
           
            stubDataAccessService
                .LoadSingleData<TbUserShift, object>("agdSp.uspUserShiftGet", Arg.Any<int>())
                .ReturnsForAnyArgs(new TbUserShift { SeqNo = 1 });

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            UserShiftService UserShiftService = 
                new UserShiftService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await UserShiftService.GetUserShift(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspUserShiftGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode,resCode);

        }

        [Test]
        public async ValueTask GetUserShiftTestFailed()
        {  //arrange
            int seqNo = 0;
            string resCode = "U999";
          
            stubDataAccessService.LoadSingleData<TbUserShift, object>("agdSp.uspUserShiftGet", Arg.Any<int>())
                .ReturnsNull();          

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            UserShiftService UserShiftService = new UserShiftService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await UserShiftService.GetUserShift(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<TbUserShift, object>("agdSp.uspUserShiftGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryUserShiftTest()
        {  //arrange
            var queryReq = new UserShiftQueryRequest {  };
            string resCode = "U200";
            
            stubDataAccessService.LoadData<TbUserShift, object>
                (storeProcedure: "agdSp.uspUserShiftQuery", Arg.Any<object>())
               .Returns(new TbUserShift[] { 
                    new TbUserShift { 

                        Total = 2
                    },
                    new TbUserShift { } 
                }.AsQueryable());
            
            stubHttpContextAccessor.HttpContext.Request.Path ="/api/UserShift/query";
            
            UserShiftService UserShiftService = new 
                UserShiftService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await UserShiftService.QueryUserShift(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
          
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryUserShiftTestFailed()
        {  //arrange
            var queryReq = new UserShiftQueryRequest {  

            };
            string resCode = "U999";
            
            stubDataAccessService.LoadData<TbUserShift, object>("agdSp.uspUserShiftQuery", Arg.Any<object>())
                .ReturnsNull();
            
            stubHttpContextAccessor.HttpContext.Request.Path = "/api/UserShift/query";
            UserShiftService UserShiftService = new UserShiftService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await UserShiftService.QueryUserShift(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspUserShiftGet", Arg.Any<object>());
            Assert.AreEqual(result.total, 0);

        }

        [Test]
        public async ValueTask InsertUserShiftTest()
        {  //arrange
            var insertReq = new UserShiftInsertRequest
            {
				userID = "03932",
				shiftDate = "2022-03-01",
				content = "Q1 1030-1930",
				creator = "10046",
				creatorName = "陳麗杰鰻魚飯",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspUserShiftInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result:new SqlResponse() { ErrorMsg="",Msg="" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            UserShiftService UserShiftService = new
                UserShiftService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await UserShiftService.InsertUserShift(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask InsertUserShiftTestFailed()
        {  //arrange
            var insertReq = new UserShiftInsertRequest
            {
				userID = "03932",
				shiftDate = "2022-03-01",
				content = "Q1 1030-1930",
				creator = "10046",
				creatorName = "陳麗杰鰻魚飯",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspUserShiftInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            UserShiftService UserShiftService = new
                UserShiftService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await UserShiftService.InsertUserShift(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }


        [Test]
        public async ValueTask UpdateUserShiftTest()
        {  //arrange
            var updateReq = new UserShiftUpdateRequest
            {
				userID = "21720",
				shiftDate = "2022-12-31",
				content = "NULL",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspUserShiftUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            UserShiftService UserShiftService = new
                UserShiftService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await UserShiftService.UpdateUserShift(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask UpdateUserShiftTestFailed()
        {  //arrange
            var updateReq = new UserShiftUpdateRequest
            {
				userID = "21720",
				shiftDate = "2022-12-31",
				content = "NULL",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspUserShiftUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            UserShiftService UserShiftService = new
                UserShiftService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await UserShiftService.UpdateUserShift(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }




    }
}
