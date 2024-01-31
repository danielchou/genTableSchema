using AutoMapper;
using ESUN.AGD.DataAccess.DataService.DataAccess;
using ESUN.AGD.DataAccess.Type;
using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.Common;
using ESUN.AGD.WebApi.Application.ReasonTxn;
using ESUN.AGD.WebApi.Application.ReasonTxn.Contract;
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


namespace ESUN.AGD.WebApi.Test.ReasonTxnTest
{
    [TestFixture]
    public class TestReasonTxnService
    {
        private static IDataAccessService stubDataAccessService = Substitute.For<IDataAccessService>();
        private static IGetTokenService stubGetTokenService = Substitute.For<IGetTokenService>();
        private static IHttpContextAccessor stubHttpContextAccessor = Substitute.For<IHttpContextAccessor>();
        private static IMapper stubMapper = Substitute.For<IMapper>();
        private static IUserService stubUserService = Substitute.For<IUserService>();
        private static IAutoNextNumService autoNextNumService = Substitute.For<IAutoNextNumService>();

        [Test]
        public async ValueTask GetReasonTxnTest()
        {  //arrange
            int seqNo = 1;
            string resCode = "U200";
           
            stubDataAccessService
                .LoadSingleData<TbReasonTxn, object>("agdSp.uspReasonTxnGet", Arg.Any<int>())
                .ReturnsForAnyArgs(new TbReasonTxn { SeqNo = 1 });

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            ReasonTxnService ReasonTxnService = 
                new ReasonTxnService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ReasonTxnService.GetReasonTxn(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspReasonTxnGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode,resCode);

        }

        [Test]
        public async ValueTask GetReasonTxnTestFailed()
        {  //arrange
            int seqNo = 0;
            string resCode = "U999";
          
            stubDataAccessService.LoadSingleData<TbReasonTxn, object>("agdSp.uspReasonTxnGet", Arg.Any<int>())
                .ReturnsNull();          

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            ReasonTxnService ReasonTxnService = new ReasonTxnService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ReasonTxnService.GetReasonTxn(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<TbReasonTxn, object>("agdSp.uspReasonTxnGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryReasonTxnTest()
        {  //arrange
            var queryReq = new ReasonTxnQueryRequest { 						txnItemCode = "TIC01",
						reasonID = "Y0303", };
            string resCode = "U200";
            
            stubDataAccessService.LoadData<TbReasonTxn, object>
                (storeProcedure: "agdSp.uspReasonTxnQuery", Arg.Any<object>())
               .Returns(new TbReasonTxn[] { 
                    new TbReasonTxn { 
						txnItemCode = "TIC01",
						reasonID = "Y0303",
                        Total = 2
                    },
                    new TbReasonTxn { } 
                }.AsQueryable());
            
            stubHttpContextAccessor.HttpContext.Request.Path ="/api/ReasonTxn/query";
            
            ReasonTxnService ReasonTxnService = new 
                ReasonTxnService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ReasonTxnService.QueryReasonTxn(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
          
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryReasonTxnTestFailed()
        {  //arrange
            var queryReq = new ReasonTxnQueryRequest {  
						txnItemCode = "TIC01",
						reasonID = "Y0303",
            };
            string resCode = "U999";
            
            stubDataAccessService.LoadData<TbReasonTxn, object>("agdSp.uspReasonTxnQuery", Arg.Any<object>())
                .ReturnsNull();
            
            stubHttpContextAccessor.HttpContext.Request.Path = "/api/ReasonTxn/query";
            ReasonTxnService ReasonTxnService = new ReasonTxnService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ReasonTxnService.QueryReasonTxn(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspReasonTxnGet", Arg.Any<object>());
            Assert.AreEqual(result.total, 0);

        }

        [Test]
        public async ValueTask InsertReasonTxnTest()
        {  //arrange
            var insertReq = new ReasonTxnInsertRequest
            {
				txnItemCode = "TIC01",
				reasonID = "Y0303",
				creator = "admin",
				creatorName = "林管理",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspReasonTxnInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result:new SqlResponse() { ErrorMsg="",Msg="" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            ReasonTxnService ReasonTxnService = new
                ReasonTxnService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ReasonTxnService.InsertReasonTxn(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask InsertReasonTxnTestFailed()
        {  //arrange
            var insertReq = new ReasonTxnInsertRequest
            {
				txnItemCode = "TIC01",
				reasonID = "Y0303",
				creator = "admin",
				creatorName = "林管理",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspReasonTxnInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            ReasonTxnService ReasonTxnService = new
                ReasonTxnService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ReasonTxnService.InsertReasonTxn(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }


        [Test]
        public async ValueTask UpdateReasonTxnTest()
        {  //arrange
            var updateReq = new ReasonTxnUpdateRequest
            {
				txnItemCode = "TIC08",
				reasonID = "Y0303",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspReasonTxnUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            ReasonTxnService ReasonTxnService = new
                ReasonTxnService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ReasonTxnService.UpdateReasonTxn(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask UpdateReasonTxnTestFailed()
        {  //arrange
            var updateReq = new ReasonTxnUpdateRequest
            {
				txnItemCode = "TIC08",
				reasonID = "Y0303",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspReasonTxnUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            ReasonTxnService ReasonTxnService = new
                ReasonTxnService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ReasonTxnService.UpdateReasonTxn(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }




    }
}
