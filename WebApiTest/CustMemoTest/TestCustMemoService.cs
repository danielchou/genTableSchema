using AutoMapper;
using ESUN.AGD.DataAccess.DataService.DataAccess;
using ESUN.AGD.DataAccess.Type;
using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.Common;
using ESUN.AGD.WebApi.Application.CustMemo;
using ESUN.AGD.WebApi.Application.CustMemo.Contract;
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


namespace ESUN.AGD.WebApi.Test.CustMemoTest
{
    [TestFixture]
    public class TestCustMemoService
    {
        private static IDataAccessService stubDataAccessService = Substitute.For<IDataAccessService>();
        private static IGetTokenService stubGetTokenService = Substitute.For<IGetTokenService>();
        private static IHttpContextAccessor stubHttpContextAccessor = Substitute.For<IHttpContextAccessor>();
        private static IMapper stubMapper = Substitute.For<IMapper>();
        private static IUserService stubUserService = Substitute.For<IUserService>();
        private static IAutoNextNumService autoNextNumService = Substitute.For<IAutoNextNumService>();

        [Test]
        public async ValueTask GetCustMemoTest()
        {  //arrange
            int seqNo = 1;
            string resCode = "U200";
           
            stubDataAccessService
                .LoadSingleData<TbCustMemo, object>("agdSp.uspCustMemoGet", Arg.Any<int>())
                .ReturnsForAnyArgs(new TbCustMemo { SeqNo = 1 });

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            CustMemoService CustMemoService = 
                new CustMemoService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await CustMemoService.GetCustMemo(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspCustMemoGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode,resCode);

        }

        [Test]
        public async ValueTask GetCustMemoTestFailed()
        {  //arrange
            int seqNo = 0;
            string resCode = "U999";
          
            stubDataAccessService.LoadSingleData<TbCustMemo, object>("agdSp.uspCustMemoGet", Arg.Any<int>())
                .ReturnsNull();          

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            CustMemoService CustMemoService = new CustMemoService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await CustMemoService.GetCustMemo(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<TbCustMemo, object>("agdSp.uspCustMemoGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryCustMemoTest()
        {  //arrange
            var queryReq = new CustMemoQueryRequest {  };
            string resCode = "U200";
            
            stubDataAccessService.LoadData<TbCustMemo, object>
                (storeProcedure: "agdSp.uspCustMemoQuery", Arg.Any<object>())
               .Returns(new TbCustMemo[] { 
                    new TbCustMemo { 

                        Total = 2
                    },
                    new TbCustMemo { } 
                }.AsQueryable());
            
            stubHttpContextAccessor.HttpContext.Request.Path ="/api/CustMemo/query";
            
            CustMemoService CustMemoService = new 
                CustMemoService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await CustMemoService.QueryCustMemo(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
          
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryCustMemoTestFailed()
        {  //arrange
            var queryReq = new CustMemoQueryRequest {  

            };
            string resCode = "U999";
            
            stubDataAccessService.LoadData<TbCustMemo, object>("agdSp.uspCustMemoQuery", Arg.Any<object>())
                .ReturnsNull();
            
            stubHttpContextAccessor.HttpContext.Request.Path = "/api/CustMemo/query";
            CustMemoService CustMemoService = new CustMemoService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await CustMemoService.QueryCustMemo(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspCustMemoGet", Arg.Any<object>());
            Assert.AreEqual(result.total, 0);

        }

        [Test]
        public async ValueTask InsertCustMemoTest()
        {  //arrange
            var insertReq = new CustMemoInsertRequest
            {
				custKey = 1,
				customerMemo = "12968461",
				creator = "13370",
				creatorName = "傅O芳",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspCustMemoInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result:new SqlResponse() { ErrorMsg="",Msg="" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            CustMemoService CustMemoService = new
                CustMemoService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await CustMemoService.InsertCustMemo(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask InsertCustMemoTestFailed()
        {  //arrange
            var insertReq = new CustMemoInsertRequest
            {
				custKey = 1,
				customerMemo = "12968461",
				creator = "13370",
				creatorName = "傅O芳",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspCustMemoInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            CustMemoService CustMemoService = new
                CustMemoService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await CustMemoService.InsertCustMemo(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }


        [Test]
        public async ValueTask UpdateCustMemoTest()
        {  //arrange
            var updateReq = new CustMemoUpdateRequest
            {
				custKey = 5,
				customerMemo = "FF",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspCustMemoUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            CustMemoService CustMemoService = new
                CustMemoService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await CustMemoService.UpdateCustMemo(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask UpdateCustMemoTestFailed()
        {  //arrange
            var updateReq = new CustMemoUpdateRequest
            {
				custKey = 5,
				customerMemo = "FF",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspCustMemoUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            CustMemoService CustMemoService = new
                CustMemoService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await CustMemoService.UpdateCustMemo(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }




    }
}
