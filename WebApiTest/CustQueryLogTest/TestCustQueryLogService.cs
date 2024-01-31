using AutoMapper;
using ESUN.AGD.DataAccess.DataService.DataAccess;
using ESUN.AGD.DataAccess.Type;
using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.Common;
using ESUN.AGD.WebApi.Application.CustQueryLog;
using ESUN.AGD.WebApi.Application.CustQueryLog.Contract;
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


namespace ESUN.AGD.WebApi.Test.CustQueryLogTest
{
    [TestFixture]
    public class TestCustQueryLogService
    {
        private static IDataAccessService stubDataAccessService = Substitute.For<IDataAccessService>();
        private static IGetTokenService stubGetTokenService = Substitute.For<IGetTokenService>();
        private static IHttpContextAccessor stubHttpContextAccessor = Substitute.For<IHttpContextAccessor>();
        private static IMapper stubMapper = Substitute.For<IMapper>();
        private static IUserService stubUserService = Substitute.For<IUserService>();
        private static IAutoNextNumService autoNextNumService = Substitute.For<IAutoNextNumService>();

        [Test]
        public async ValueTask GetCustQueryLogTest()
        {  //arrange
            int seqNo = 1;
            string resCode = "U200";
           
            stubDataAccessService
                .LoadSingleData<TbCustQueryLog, object>("agdSp.uspCustQueryLogGet", Arg.Any<int>())
                .ReturnsForAnyArgs(new TbCustQueryLog { SeqNo = 1 });

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            CustQueryLogService CustQueryLogService = 
                new CustQueryLogService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await CustQueryLogService.GetCustQueryLog(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspCustQueryLogGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode,resCode);

        }

        [Test]
        public async ValueTask GetCustQueryLogTestFailed()
        {  //arrange
            int seqNo = 0;
            string resCode = "U999";
          
            stubDataAccessService.LoadSingleData<TbCustQueryLog, object>("agdSp.uspCustQueryLogGet", Arg.Any<int>())
                .ReturnsNull();          

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            CustQueryLogService CustQueryLogService = new CustQueryLogService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await CustQueryLogService.GetCustQueryLog(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<TbCustQueryLog, object>("agdSp.uspCustQueryLogGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryCustQueryLogTest()
        {  //arrange
            var queryReq = new CustQueryLogQueryRequest { 						custKey = 0,
						customerID = "E258604860",
						creator = "admin", };
            string resCode = "U200";
            
            stubDataAccessService.LoadData<TbCustQueryLog, object>
                (storeProcedure: "agdSp.uspCustQueryLogQuery", Arg.Any<object>())
               .Returns(new TbCustQueryLog[] { 
                    new TbCustQueryLog { 
						custKey = 0,
						customerID = "E258604860",
						creator = "admin",
                        Total = 2
                    },
                    new TbCustQueryLog { } 
                }.AsQueryable());
            
            stubHttpContextAccessor.HttpContext.Request.Path ="/api/CustQueryLog/query";
            
            CustQueryLogService CustQueryLogService = new 
                CustQueryLogService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await CustQueryLogService.QueryCustQueryLog(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
          
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryCustQueryLogTestFailed()
        {  //arrange
            var queryReq = new CustQueryLogQueryRequest {  
						custKey = 0,
						customerID = "E258604860",
						creator = "admin",
            };
            string resCode = "U999";
            
            stubDataAccessService.LoadData<TbCustQueryLog, object>("agdSp.uspCustQueryLogQuery", Arg.Any<object>())
                .ReturnsNull();
            
            stubHttpContextAccessor.HttpContext.Request.Path = "/api/CustQueryLog/query";
            CustQueryLogService CustQueryLogService = new CustQueryLogService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await CustQueryLogService.QueryCustQueryLog(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspCustQueryLogGet", Arg.Any<object>());
            Assert.AreEqual(result.total, 0);

        }

        [Test]
        public async ValueTask InsertCustQueryLogTest()
        {  //arrange
            var insertReq = new CustQueryLogInsertRequest
            {
				custKey = 0,
				customerID = "E258604860",
				customerName = "測試曾蕭碧珠",
				creator = "admin",
				creatorName = "林管理",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspCustQueryLogInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result:new SqlResponse() { ErrorMsg="",Msg="" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            CustQueryLogService CustQueryLogService = new
                CustQueryLogService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await CustQueryLogService.InsertCustQueryLog(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask InsertCustQueryLogTestFailed()
        {  //arrange
            var insertReq = new CustQueryLogInsertRequest
            {
				custKey = 0,
				customerID = "E258604860",
				customerName = "測試曾蕭碧珠",
				creator = "admin",
				creatorName = "林管理",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspCustQueryLogInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            CustQueryLogService CustQueryLogService = new
                CustQueryLogService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await CustQueryLogService.InsertCustQueryLog(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }


        [Test]
        public async ValueTask UpdateCustQueryLogTest()
        {  //arrange
            var updateReq = new CustQueryLogUpdateRequest
            {
				custKey = 8,
				customerID = "Y223276666",
				customerName = "測試陳怡伶",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspCustQueryLogUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            CustQueryLogService CustQueryLogService = new
                CustQueryLogService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await CustQueryLogService.UpdateCustQueryLog(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask UpdateCustQueryLogTestFailed()
        {  //arrange
            var updateReq = new CustQueryLogUpdateRequest
            {
				custKey = 8,
				customerID = "Y223276666",
				customerName = "測試陳怡伶",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspCustQueryLogUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            CustQueryLogService CustQueryLogService = new
                CustQueryLogService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await CustQueryLogService.UpdateCustQueryLog(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }




    }
}
