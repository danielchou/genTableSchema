using AutoMapper;
using ESUN.AGD.DataAccess.DataService.DataAccess;
using ESUN.AGD.DataAccess.Type;
using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.Common;
using ESUN.AGD.WebApi.Application.MarketingOpenHist;
using ESUN.AGD.WebApi.Application.MarketingOpenHist.Contract;
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


namespace ESUN.AGD.WebApi.Test.MarketingOpenHistTest
{
    [TestFixture]
    public class TestMarketingOpenHistService
    {
        private static IDataAccessService stubDataAccessService = Substitute.For<IDataAccessService>();
        private static IGetTokenService stubGetTokenService = Substitute.For<IGetTokenService>();
        private static IHttpContextAccessor stubHttpContextAccessor = Substitute.For<IHttpContextAccessor>();
        private static IMapper stubMapper = Substitute.For<IMapper>();
        private static IUserService stubUserService = Substitute.For<IUserService>();
        private static IAutoNextNumService autoNextNumService = Substitute.For<IAutoNextNumService>();

        [Test]
        public async ValueTask GetMarketingOpenHistTest()
        {  //arrange
            int seqNo = 1;
            string resCode = "U200";
           
            stubDataAccessService
                .LoadSingleData<TbMarketingOpenHist, object>("agdSp.uspMarketingOpenHistGet", Arg.Any<int>())
                .ReturnsForAnyArgs(new TbMarketingOpenHist { SeqNo = 1 });

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            MarketingOpenHistService MarketingOpenHistService = 
                new MarketingOpenHistService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await MarketingOpenHistService.GetMarketingOpenHist(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspMarketingOpenHistGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode,resCode);

        }

        [Test]
        public async ValueTask GetMarketingOpenHistTestFailed()
        {  //arrange
            int seqNo = 0;
            string resCode = "U999";
          
            stubDataAccessService.LoadSingleData<TbMarketingOpenHist, object>("agdSp.uspMarketingOpenHistGet", Arg.Any<int>())
                .ReturnsNull();          

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            MarketingOpenHistService MarketingOpenHistService = new MarketingOpenHistService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await MarketingOpenHistService.GetMarketingOpenHist(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<TbMarketingOpenHist, object>("agdSp.uspMarketingOpenHistGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryMarketingOpenHistTest()
        {  //arrange
            var queryReq = new MarketingOpenHistQueryRequest { 						isMarketingOpen = False,
						actionDT = "2022-06-24 18:22:46",
						actor = "admin", };
            string resCode = "U200";
            
            stubDataAccessService.LoadData<TbMarketingOpenHist, object>
                (storeProcedure: "agdSp.uspMarketingOpenHistQuery", Arg.Any<object>())
               .Returns(new TbMarketingOpenHist[] { 
                    new TbMarketingOpenHist { 
						isMarketingOpen = False,
						actionDT = "2022-06-24 18:22:46",
						actor = "admin",
                        Total = 2
                    },
                    new TbMarketingOpenHist { } 
                }.AsQueryable());
            
            stubHttpContextAccessor.HttpContext.Request.Path ="/api/MarketingOpenHist/query";
            
            MarketingOpenHistService MarketingOpenHistService = new 
                MarketingOpenHistService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await MarketingOpenHistService.QueryMarketingOpenHist(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
          
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryMarketingOpenHistTestFailed()
        {  //arrange
            var queryReq = new MarketingOpenHistQueryRequest {  
						isMarketingOpen = False,
						actionDT = "2022-06-24 18:22:46",
						actor = "admin",
            };
            string resCode = "U999";
            
            stubDataAccessService.LoadData<TbMarketingOpenHist, object>("agdSp.uspMarketingOpenHistQuery", Arg.Any<object>())
                .ReturnsNull();
            
            stubHttpContextAccessor.HttpContext.Request.Path = "/api/MarketingOpenHist/query";
            MarketingOpenHistService MarketingOpenHistService = new MarketingOpenHistService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await MarketingOpenHistService.QueryMarketingOpenHist(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspMarketingOpenHistGet", Arg.Any<object>());
            Assert.AreEqual(result.total, 0);

        }

        [Test]
        public async ValueTask InsertMarketingOpenHistTest()
        {  //arrange
            var insertReq = new MarketingOpenHistInsertRequest
            {
				isMarketingOpen = False,
				closeReason = "test1",
				creator = "sys",
				creatorName = "系統",
				actionType = "U",
				actionDT = "2022-06-24 18:22:46",
				actor = "admin",
				actorName = "林管理",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspMarketingOpenHistInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result:new SqlResponse() { ErrorMsg="",Msg="" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            MarketingOpenHistService MarketingOpenHistService = new
                MarketingOpenHistService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await MarketingOpenHistService.InsertMarketingOpenHist(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask InsertMarketingOpenHistTestFailed()
        {  //arrange
            var insertReq = new MarketingOpenHistInsertRequest
            {
				isMarketingOpen = False,
				closeReason = "test1",
				creator = "sys",
				creatorName = "系統",
				actionType = "U",
				actionDT = "2022-06-24 18:22:46",
				actor = "admin",
				actorName = "林管理",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspMarketingOpenHistInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            MarketingOpenHistService MarketingOpenHistService = new
                MarketingOpenHistService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await MarketingOpenHistService.InsertMarketingOpenHist(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }


        [Test]
        public async ValueTask UpdateMarketingOpenHistTest()
        {  //arrange
            var updateReq = new MarketingOpenHistUpdateRequest
            {
				isMarketingOpen = True,
				closeReason = "",
				actionType = "U",
				actionDT = "2022-08-26 15:55:37",
				actor = "10046",
				actorName = "陳麗杰鰻魚飯",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspMarketingOpenHistUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            MarketingOpenHistService MarketingOpenHistService = new
                MarketingOpenHistService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await MarketingOpenHistService.UpdateMarketingOpenHist(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask UpdateMarketingOpenHistTestFailed()
        {  //arrange
            var updateReq = new MarketingOpenHistUpdateRequest
            {
				isMarketingOpen = True,
				closeReason = "",
				actionType = "U",
				actionDT = "2022-08-26 15:55:37",
				actor = "10046",
				actorName = "陳麗杰鰻魚飯",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspMarketingOpenHistUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            MarketingOpenHistService MarketingOpenHistService = new
                MarketingOpenHistService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await MarketingOpenHistService.UpdateMarketingOpenHist(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }




    }
}
