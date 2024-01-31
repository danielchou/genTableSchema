using AutoMapper;
using ESUN.AGD.DataAccess.DataService.DataAccess;
using ESUN.AGD.DataAccess.Type;
using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.Common;
using ESUN.AGD.WebApi.Application.MarketingActionCode;
using ESUN.AGD.WebApi.Application.MarketingActionCode.Contract;
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


namespace ESUN.AGD.WebApi.Test.MarketingActionCodeTest
{
    [TestFixture]
    public class TestMarketingActionCodeService
    {
        private static IDataAccessService stubDataAccessService = Substitute.For<IDataAccessService>();
        private static IGetTokenService stubGetTokenService = Substitute.For<IGetTokenService>();
        private static IHttpContextAccessor stubHttpContextAccessor = Substitute.For<IHttpContextAccessor>();
        private static IMapper stubMapper = Substitute.For<IMapper>();
        private static IUserService stubUserService = Substitute.For<IUserService>();
        private static IAutoNextNumService autoNextNumService = Substitute.For<IAutoNextNumService>();

        [Test]
        public async ValueTask GetMarketingActionCodeTest()
        {  //arrange
            int seqNo = 1;
            string resCode = "U200";
           
            stubDataAccessService
                .LoadSingleData<TbMarketingActionCode, object>("agdSp.uspMarketingActionCodeGet", Arg.Any<int>())
                .ReturnsForAnyArgs(new TbMarketingActionCode { SeqNo = 1 });

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            MarketingActionCodeService MarketingActionCodeService = 
                new MarketingActionCodeService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await MarketingActionCodeService.GetMarketingActionCode(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspMarketingActionCodeGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode,resCode);

        }

        [Test]
        public async ValueTask GetMarketingActionCodeTestFailed()
        {  //arrange
            int seqNo = 0;
            string resCode = "U999";
          
            stubDataAccessService.LoadSingleData<TbMarketingActionCode, object>("agdSp.uspMarketingActionCodeGet", Arg.Any<int>())
                .ReturnsNull();          

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            MarketingActionCodeService MarketingActionCodeService = new MarketingActionCodeService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await MarketingActionCodeService.GetMarketingActionCode(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<TbMarketingActionCode, object>("agdSp.uspMarketingActionCodeGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryMarketingActionCodeTest()
        {  //arrange
            var queryReq = new MarketingActionCodeQueryRequest { 						actionCodeType = "A",
						actionCode = "A2",
						actionCodeName = "申請辦理", };
            string resCode = "U200";
            
            stubDataAccessService.LoadData<TbMarketingActionCode, object>
                (storeProcedure: "agdSp.uspMarketingActionCodeQuery", Arg.Any<object>())
               .Returns(new TbMarketingActionCode[] { 
                    new TbMarketingActionCode { 
						actionCodeType = "A",
						actionCode = "A2",
						actionCodeName = "申請辦理",
                        Total = 2
                    },
                    new TbMarketingActionCode { } 
                }.AsQueryable());
            
            stubHttpContextAccessor.HttpContext.Request.Path ="/api/MarketingActionCode/query";
            
            MarketingActionCodeService MarketingActionCodeService = new 
                MarketingActionCodeService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await MarketingActionCodeService.QueryMarketingActionCode(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
          
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryMarketingActionCodeTestFailed()
        {  //arrange
            var queryReq = new MarketingActionCodeQueryRequest {  
						actionCodeType = "A",
						actionCode = "A2",
						actionCodeName = "申請辦理",
            };
            string resCode = "U999";
            
            stubDataAccessService.LoadData<TbMarketingActionCode, object>("agdSp.uspMarketingActionCodeQuery", Arg.Any<object>())
                .ReturnsNull();
            
            stubHttpContextAccessor.HttpContext.Request.Path = "/api/MarketingActionCode/query";
            MarketingActionCodeService MarketingActionCodeService = new MarketingActionCodeService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await MarketingActionCodeService.QueryMarketingActionCode(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspMarketingActionCodeGet", Arg.Any<object>());
            Assert.AreEqual(result.total, 0);

        }

        [Test]
        public async ValueTask InsertMarketingActionCodeTest()
        {  //arrange
            var insertReq = new MarketingActionCodeInsertRequest
            {
				marketingID = 5,
				actionCodeType = "A",
				actionCode = "A2",
				actionCodeName = "申請辦理",
				isAccept = True,
				displayOrder = 1,
				creator = "10046",
				creatorName = "陳麗杰鰻魚飯",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspMarketingActionCodeInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result:new SqlResponse() { ErrorMsg="",Msg="" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            MarketingActionCodeService MarketingActionCodeService = new
                MarketingActionCodeService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await MarketingActionCodeService.InsertMarketingActionCode(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask InsertMarketingActionCodeTestFailed()
        {  //arrange
            var insertReq = new MarketingActionCodeInsertRequest
            {
				marketingID = 5,
				actionCodeType = "A",
				actionCode = "A2",
				actionCodeName = "申請辦理",
				isAccept = True,
				displayOrder = 1,
				creator = "10046",
				creatorName = "陳麗杰鰻魚飯",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspMarketingActionCodeInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            MarketingActionCodeService MarketingActionCodeService = new
                MarketingActionCodeService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await MarketingActionCodeService.InsertMarketingActionCode(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }


        [Test]
        public async ValueTask UpdateMarketingActionCodeTest()
        {  //arrange
            var updateReq = new MarketingActionCodeUpdateRequest
            {
				marketingID = 47,
				actionCodeType = "A",
				actionCode = "asdsadf",
				actionCodeName = "xxxx",
				isAccept = False,
				displayOrder = 0,
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspMarketingActionCodeUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            MarketingActionCodeService MarketingActionCodeService = new
                MarketingActionCodeService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await MarketingActionCodeService.UpdateMarketingActionCode(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask UpdateMarketingActionCodeTestFailed()
        {  //arrange
            var updateReq = new MarketingActionCodeUpdateRequest
            {
				marketingID = 47,
				actionCodeType = "A",
				actionCode = "asdsadf",
				actionCodeName = "xxxx",
				isAccept = False,
				displayOrder = 0,
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspMarketingActionCodeUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            MarketingActionCodeService MarketingActionCodeService = new
                MarketingActionCodeService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await MarketingActionCodeService.UpdateMarketingActionCode(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }




    }
}
