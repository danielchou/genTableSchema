using AutoMapper;
using ESUN.AGD.DataAccess.DataService.DataAccess;
using ESUN.AGD.DataAccess.Type;
using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.Common;
using ESUN.AGD.WebApi.Application.CustMemoLog;
using ESUN.AGD.WebApi.Application.CustMemoLog.Contract;
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


namespace ESUN.AGD.WebApi.Test.CustMemoLogTest
{
    [TestFixture]
    public class TestCustMemoLogService
    {
        private static IDataAccessService stubDataAccessService = Substitute.For<IDataAccessService>();
        private static IGetTokenService stubGetTokenService = Substitute.For<IGetTokenService>();
        private static IHttpContextAccessor stubHttpContextAccessor = Substitute.For<IHttpContextAccessor>();
        private static IMapper stubMapper = Substitute.For<IMapper>();
        private static IUserService stubUserService = Substitute.For<IUserService>();
        private static IAutoNextNumService autoNextNumService = Substitute.For<IAutoNextNumService>();

        [Test]
        public async ValueTask GetCustMemoLogTest()
        {  //arrange
            int seqNo = 1;
            string resCode = "U200";
           
            stubDataAccessService
                .LoadSingleData<TbCustMemoLog, object>("agdSp.uspCustMemoLogGet", Arg.Any<int>())
                .ReturnsForAnyArgs(new TbCustMemoLog { SeqNo = 1 });

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            CustMemoLogService CustMemoLogService = 
                new CustMemoLogService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await CustMemoLogService.GetCustMemoLog(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspCustMemoLogGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode,resCode);

        }

        [Test]
        public async ValueTask GetCustMemoLogTestFailed()
        {  //arrange
            int seqNo = 0;
            string resCode = "U999";
          
            stubDataAccessService.LoadSingleData<TbCustMemoLog, object>("agdSp.uspCustMemoLogGet", Arg.Any<int>())
                .ReturnsNull();          

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            CustMemoLogService CustMemoLogService = new CustMemoLogService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await CustMemoLogService.GetCustMemoLog(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<TbCustMemoLog, object>("agdSp.uspCustMemoLogGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryCustMemoLogTest()
        {  //arrange
            var queryReq = new CustMemoLogQueryRequest { 						custKey = 1,
						customerID = "12968461",
						customerMemo = "12968461",
						creator = "13370", };
            string resCode = "U200";
            
            stubDataAccessService.LoadData<TbCustMemoLog, object>
                (storeProcedure: "agdSp.uspCustMemoLogQuery", Arg.Any<object>())
               .Returns(new TbCustMemoLog[] { 
                    new TbCustMemoLog { 
						custKey = 1,
						customerID = "12968461",
						customerMemo = "12968461",
						creator = "13370",
                        Total = 2
                    },
                    new TbCustMemoLog { } 
                }.AsQueryable());
            
            stubHttpContextAccessor.HttpContext.Request.Path ="/api/CustMemoLog/query";
            
            CustMemoLogService CustMemoLogService = new 
                CustMemoLogService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await CustMemoLogService.QueryCustMemoLog(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
          
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryCustMemoLogTestFailed()
        {  //arrange
            var queryReq = new CustMemoLogQueryRequest {  
						custKey = 1,
						customerID = "12968461",
						customerMemo = "12968461",
						creator = "13370",
            };
            string resCode = "U999";
            
            stubDataAccessService.LoadData<TbCustMemoLog, object>("agdSp.uspCustMemoLogQuery", Arg.Any<object>())
                .ReturnsNull();
            
            stubHttpContextAccessor.HttpContext.Request.Path = "/api/CustMemoLog/query";
            CustMemoLogService CustMemoLogService = new CustMemoLogService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await CustMemoLogService.QueryCustMemoLog(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspCustMemoLogGet", Arg.Any<object>());
            Assert.AreEqual(result.total, 0);

        }

        [Test]
        public async ValueTask InsertCustMemoLogTest()
        {  //arrange
            var insertReq = new CustMemoLogInsertRequest
            {
				custKey = 1,
				customerID = "12968461",
				customerName = "測試法雅客股份有限公司",
				customerMemo = "12968461",
				creator = "13370",
				creatorName = "傅O芳",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspCustMemoLogInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result:new SqlResponse() { ErrorMsg="",Msg="" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            CustMemoLogService CustMemoLogService = new
                CustMemoLogService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await CustMemoLogService.InsertCustMemoLog(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask InsertCustMemoLogTestFailed()
        {  //arrange
            var insertReq = new CustMemoLogInsertRequest
            {
				custKey = 1,
				customerID = "12968461",
				customerName = "測試法雅客股份有限公司",
				customerMemo = "12968461",
				creator = "13370",
				creatorName = "傅O芳",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspCustMemoLogInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            CustMemoLogService CustMemoLogService = new
                CustMemoLogService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await CustMemoLogService.InsertCustMemoLog(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }


        [Test]
        public async ValueTask UpdateCustMemoLogTest()
        {  //arrange
            var updateReq = new CustMemoLogUpdateRequest
            {
				custKey = 5,
				customerID = "M106371509",
				customerName = "測試",
				customerMemo = "FF",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspCustMemoLogUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            CustMemoLogService CustMemoLogService = new
                CustMemoLogService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await CustMemoLogService.UpdateCustMemoLog(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask UpdateCustMemoLogTestFailed()
        {  //arrange
            var updateReq = new CustMemoLogUpdateRequest
            {
				custKey = 5,
				customerID = "M106371509",
				customerName = "測試",
				customerMemo = "FF",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspCustMemoLogUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            CustMemoLogService CustMemoLogService = new
                CustMemoLogService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await CustMemoLogService.UpdateCustMemoLog(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }




    }
}
