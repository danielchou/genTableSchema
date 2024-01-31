using AutoMapper;
using ESUN.AGD.DataAccess.DataService.DataAccess;
using ESUN.AGD.DataAccess.Type;
using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.Common;
using ESUN.AGD.WebApi.Application.AuthCallLog;
using ESUN.AGD.WebApi.Application.AuthCallLog.Contract;
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


namespace ESUN.AGD.WebApi.Test.AuthCallLogTest
{
    [TestFixture]
    public class TestAuthCallLogService
    {
        private static IDataAccessService stubDataAccessService = Substitute.For<IDataAccessService>();
        private static IGetTokenService stubGetTokenService = Substitute.For<IGetTokenService>();
        private static IHttpContextAccessor stubHttpContextAccessor = Substitute.For<IHttpContextAccessor>();
        private static IMapper stubMapper = Substitute.For<IMapper>();
        private static IUserService stubUserService = Substitute.For<IUserService>();
        private static IAutoNextNumService autoNextNumService = Substitute.For<IAutoNextNumService>();

        [Test]
        public async ValueTask GetAuthCallLogTest()
        {  //arrange
            int seqNo = 1;
            string resCode = "U200";
           
            stubDataAccessService
                .LoadSingleData<TbAuthCallLog, object>("agdSp.uspAuthCallLogGet", Arg.Any<int>())
                .ReturnsForAnyArgs(new TbAuthCallLog { SeqNo = 1 });

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            AuthCallLogService AuthCallLogService = 
                new AuthCallLogService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await AuthCallLogService.GetAuthCallLog(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspAuthCallLogGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode,resCode);

        }

        [Test]
        public async ValueTask GetAuthCallLogTestFailed()
        {  //arrange
            int seqNo = 0;
            string resCode = "U999";
          
            stubDataAccessService.LoadSingleData<TbAuthCallLog, object>("agdSp.uspAuthCallLogGet", Arg.Any<int>())
                .ReturnsNull();          

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            AuthCallLogService AuthCallLogService = new AuthCallLogService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await AuthCallLogService.GetAuthCallLog(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<TbAuthCallLog, object>("agdSp.uspAuthCallLogGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryAuthCallLogTest()
        {  //arrange
            var queryReq = new AuthCallLogQueryRequest { 						custKey = 0,
						customerID = "12345678",
						approveStatus = "2",
						creator = "14388", };
            string resCode = "U200";
            
            stubDataAccessService.LoadData<TbAuthCallLog, object>
                (storeProcedure: "agdSp.uspAuthCallLogQuery", Arg.Any<object>())
               .Returns(new TbAuthCallLog[] { 
                    new TbAuthCallLog { 
						custKey = 0,
						customerID = "12345678",
						approveStatus = "2",
						creator = "14388",
                        Total = 2
                    },
                    new TbAuthCallLog { } 
                }.AsQueryable());
            
            stubHttpContextAccessor.HttpContext.Request.Path ="/api/AuthCallLog/query";
            
            AuthCallLogService AuthCallLogService = new 
                AuthCallLogService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await AuthCallLogService.QueryAuthCallLog(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
          
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryAuthCallLogTestFailed()
        {  //arrange
            var queryReq = new AuthCallLogQueryRequest {  
						custKey = 0,
						customerID = "12345678",
						approveStatus = "2",
						creator = "14388",
            };
            string resCode = "U999";
            
            stubDataAccessService.LoadData<TbAuthCallLog, object>("agdSp.uspAuthCallLogQuery", Arg.Any<object>())
                .ReturnsNull();
            
            stubHttpContextAccessor.HttpContext.Request.Path = "/api/AuthCallLog/query";
            AuthCallLogService AuthCallLogService = new AuthCallLogService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await AuthCallLogService.QueryAuthCallLog(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspAuthCallLogGet", Arg.Any<object>());
            Assert.AreEqual(result.total, 0);

        }

        [Test]
        public async ValueTask InsertAuthCallLogTest()
        {  //arrange
            var insertReq = new AuthCallLogInsertRequest
            {
				custKey = 0,
				customerID = "12345678",
				customerName = "",
				phoneNumber = "13452234",
				authCallReason = "我想打",
				approver = "sys",
				approverName = "無需審核",
				approveDT = "2022-10-12 12:34:10",
				approveStatus = "2",
				creator = "14388",
				creatorName = "顏O紳",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspAuthCallLogInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result:new SqlResponse() { ErrorMsg="",Msg="" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            AuthCallLogService AuthCallLogService = new
                AuthCallLogService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await AuthCallLogService.InsertAuthCallLog(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask InsertAuthCallLogTestFailed()
        {  //arrange
            var insertReq = new AuthCallLogInsertRequest
            {
				custKey = 0,
				customerID = "12345678",
				customerName = "",
				phoneNumber = "13452234",
				authCallReason = "我想打",
				approver = "sys",
				approverName = "無需審核",
				approveDT = "2022-10-12 12:34:10",
				approveStatus = "2",
				creator = "14388",
				creatorName = "顏O紳",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspAuthCallLogInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            AuthCallLogService AuthCallLogService = new
                AuthCallLogService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await AuthCallLogService.InsertAuthCallLog(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }


        [Test]
        public async ValueTask UpdateAuthCallLogTest()
        {  //arrange
            var updateReq = new AuthCallLogUpdateRequest
            {
				custKey = 0,
				customerID = "B123456777",
				customerName = "",
				phoneNumber = "1784123456789012345",
				authCallReason = "回電回電回電回電回電回電回電回電回電回電回電回電回電回電回電回電回電回電回電回電回電回電回電回電回電",
				approver = "sys",
				approverName = "無需審核",
				approveDT = "2022-10-25 16:26:46",
				approveStatus = "2",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspAuthCallLogUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            AuthCallLogService AuthCallLogService = new
                AuthCallLogService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await AuthCallLogService.UpdateAuthCallLog(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask UpdateAuthCallLogTestFailed()
        {  //arrange
            var updateReq = new AuthCallLogUpdateRequest
            {
				custKey = 0,
				customerID = "B123456777",
				customerName = "",
				phoneNumber = "1784123456789012345",
				authCallReason = "回電回電回電回電回電回電回電回電回電回電回電回電回電回電回電回電回電回電回電回電回電回電回電回電回電",
				approver = "sys",
				approverName = "無需審核",
				approveDT = "2022-10-25 16:26:46",
				approveStatus = "2",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspAuthCallLogUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            AuthCallLogService AuthCallLogService = new
                AuthCallLogService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await AuthCallLogService.UpdateAuthCallLog(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }




    }
}
