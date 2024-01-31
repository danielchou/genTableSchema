using AutoMapper;
using ESUN.AGD.DataAccess.DataService.DataAccess;
using ESUN.AGD.DataAccess.Type;
using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.Common;
using ESUN.AGD.WebApi.Application.Marketing;
using ESUN.AGD.WebApi.Application.Marketing.Contract;
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


namespace ESUN.AGD.WebApi.Test.MarketingTest
{
    [TestFixture]
    public class TestMarketingService
    {
        private static IDataAccessService stubDataAccessService = Substitute.For<IDataAccessService>();
        private static IGetTokenService stubGetTokenService = Substitute.For<IGetTokenService>();
        private static IHttpContextAccessor stubHttpContextAccessor = Substitute.For<IHttpContextAccessor>();
        private static IMapper stubMapper = Substitute.For<IMapper>();
        private static IUserService stubUserService = Substitute.For<IUserService>();
        private static IAutoNextNumService autoNextNumService = Substitute.For<IAutoNextNumService>();

        [Test]
        public async ValueTask GetMarketingTest()
        {  //arrange
            int seqNo = 1;
            string resCode = "U200";
           
            stubDataAccessService
                .LoadSingleData<TbMarketing, object>("agdSp.uspMarketingGet", Arg.Any<int>())
                .ReturnsForAnyArgs(new TbMarketing { SeqNo = 1 });

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            MarketingService MarketingService = 
                new MarketingService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await MarketingService.GetMarketing(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspMarketingGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode,resCode);

        }

        [Test]
        public async ValueTask GetMarketingTestFailed()
        {  //arrange
            int seqNo = 0;
            string resCode = "U999";
          
            stubDataAccessService.LoadSingleData<TbMarketing, object>("agdSp.uspMarketingGet", Arg.Any<int>())
                .ReturnsNull();          

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            MarketingService MarketingService = new MarketingService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await MarketingService.GetMarketing(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<TbMarketing, object>("agdSp.uspMarketingGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryMarketingTest()
        {  //arrange
            var queryReq = new MarketingQueryRequest { 						marketingID = 5,
						marketingType = "1",
						marketingName = "信用卡方案",
						offerCode = "1",
						isEnable = False, };
            string resCode = "U200";
            
            stubDataAccessService.LoadData<TbMarketing, object>
                (storeProcedure: "agdSp.uspMarketingQuery", Arg.Any<object>())
               .Returns(new TbMarketing[] { 
                    new TbMarketing { 
						marketingID = 5,
						marketingType = "1",
						marketingName = "信用卡方案",
						offerCode = "1",
						isEnable = False,
                        Total = 2
                    },
                    new TbMarketing { } 
                }.AsQueryable());
            
            stubHttpContextAccessor.HttpContext.Request.Path ="/api/Marketing/query";
            
            MarketingService MarketingService = new 
                MarketingService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await MarketingService.QueryMarketing(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
          
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryMarketingTestFailed()
        {  //arrange
            var queryReq = new MarketingQueryRequest {  
						marketingID = 5,
						marketingType = "1",
						marketingName = "信用卡方案",
						offerCode = "1",
						isEnable = False,
            };
            string resCode = "U999";
            
            stubDataAccessService.LoadData<TbMarketing, object>("agdSp.uspMarketingQuery", Arg.Any<object>())
                .ReturnsNull();
            
            stubHttpContextAccessor.HttpContext.Request.Path = "/api/Marketing/query";
            MarketingService MarketingService = new MarketingService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await MarketingService.QueryMarketing(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspMarketingGet", Arg.Any<object>());
            Assert.AreEqual(result.total, 0);

        }

        [Test]
        public async ValueTask InsertMarketingTest()
        {  //arrange
            var insertReq = new MarketingInsertRequest
            {
				marketingID = 5,
				marketingType = "1",
				marketingName = "信用卡方案",
				content = "測試方案內容",
				marketingScript = "測試內容",
				marketingBeginDT = "2022-01-01",
				marketingEndDT = "2022-12-31",
				offerCode = "1",
				isNoSale = True,
				displayOrder = 1,
				isEnable = False,
				creator = "admin",
				creatorName = "林管理",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspMarketingInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result:new SqlResponse() { ErrorMsg="",Msg="" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            MarketingService MarketingService = new
                MarketingService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await MarketingService.InsertMarketing(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask InsertMarketingTestFailed()
        {  //arrange
            var insertReq = new MarketingInsertRequest
            {
				marketingID = 5,
				marketingType = "1",
				marketingName = "信用卡方案",
				content = "測試方案內容",
				marketingScript = "測試內容",
				marketingBeginDT = "2022-01-01",
				marketingEndDT = "2022-12-31",
				offerCode = "1",
				isNoSale = True,
				displayOrder = 1,
				isEnable = False,
				creator = "admin",
				creatorName = "林管理",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspMarketingInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            MarketingService MarketingService = new
                MarketingService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await MarketingService.InsertMarketing(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }


        [Test]
        public async ValueTask UpdateMarketingTest()
        {  //arrange
            var updateReq = new MarketingUpdateRequest
            {
				marketingID = 48,
				marketingType = "2",
				marketingName = "四八六四五六七八九十一二三四五",
				content = "四八六內容",
				marketingScript = "<ul><li>asdf</li></ul>",
				marketingBeginDT = "1900-02-01",
				marketingEndDT = "1900-02-02",
				offerCode = "",
				isNoSale = False,
				displayOrder = 2,
				isEnable = True,
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspMarketingUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            MarketingService MarketingService = new
                MarketingService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await MarketingService.UpdateMarketing(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask UpdateMarketingTestFailed()
        {  //arrange
            var updateReq = new MarketingUpdateRequest
            {
				marketingID = 48,
				marketingType = "2",
				marketingName = "四八六四五六七八九十一二三四五",
				content = "四八六內容",
				marketingScript = "<ul><li>asdf</li></ul>",
				marketingBeginDT = "1900-02-01",
				marketingEndDT = "1900-02-02",
				offerCode = "",
				isNoSale = False,
				displayOrder = 2,
				isEnable = True,
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspMarketingUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            MarketingService MarketingService = new
                MarketingService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await MarketingService.UpdateMarketing(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }




    }
}
