using AutoMapper;
using ESUN.AGD.DataAccess.DataService.DataAccess;
using ESUN.AGD.DataAccess.Type;
using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.Common;
using ESUN.AGD.WebApi.Application.DebitCardCode;
using ESUN.AGD.WebApi.Application.DebitCardCode.Contract;
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


namespace ESUN.AGD.WebApi.Test.DebitCardCodeTest
{
    [TestFixture]
    public class TestDebitCardCodeService
    {
        private static IDataAccessService stubDataAccessService = Substitute.For<IDataAccessService>();
        private static IGetTokenService stubGetTokenService = Substitute.For<IGetTokenService>();
        private static IHttpContextAccessor stubHttpContextAccessor = Substitute.For<IHttpContextAccessor>();
        private static IMapper stubMapper = Substitute.For<IMapper>();
        private static IUserService stubUserService = Substitute.For<IUserService>();
        private static IAutoNextNumService autoNextNumService = Substitute.For<IAutoNextNumService>();

        [Test]
        public async ValueTask GetDebitCardCodeTest()
        {  //arrange
            int seqNo = 1;
            string resCode = "U200";
           
            stubDataAccessService
                .LoadSingleData<TbDebitCardCode, object>("agdSp.uspDebitCardCodeGet", Arg.Any<int>())
                .ReturnsForAnyArgs(new TbDebitCardCode { SeqNo = 1 });

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            DebitCardCodeService DebitCardCodeService = 
                new DebitCardCodeService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await DebitCardCodeService.GetDebitCardCode(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspDebitCardCodeGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode,resCode);

        }

        [Test]
        public async ValueTask GetDebitCardCodeTestFailed()
        {  //arrange
            int seqNo = 0;
            string resCode = "U999";
          
            stubDataAccessService.LoadSingleData<TbDebitCardCode, object>("agdSp.uspDebitCardCodeGet", Arg.Any<int>())
                .ReturnsNull();          

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            DebitCardCodeService DebitCardCodeService = new DebitCardCodeService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await DebitCardCodeService.GetDebitCardCode(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<TbDebitCardCode, object>("agdSp.uspDebitCardCodeGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryDebitCardCodeTest()
        {  //arrange
            var queryReq = new DebitCardCodeQueryRequest { 						cardNo = "11",
						cardName = "悠遊簽帳金融卡",
						debitType = "2", };
            string resCode = "U200";
            
            stubDataAccessService.LoadData<TbDebitCardCode, object>
                (storeProcedure: "agdSp.uspDebitCardCodeQuery", Arg.Any<object>())
               .Returns(new TbDebitCardCode[] { 
                    new TbDebitCardCode { 
						cardNo = "11",
						cardName = "悠遊簽帳金融卡",
						debitType = "2",
                        Total = 2
                    },
                    new TbDebitCardCode { } 
                }.AsQueryable());
            
            stubHttpContextAccessor.HttpContext.Request.Path ="/api/DebitCardCode/query";
            
            DebitCardCodeService DebitCardCodeService = new 
                DebitCardCodeService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await DebitCardCodeService.QueryDebitCardCode(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
          
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryDebitCardCodeTestFailed()
        {  //arrange
            var queryReq = new DebitCardCodeQueryRequest {  
						cardNo = "11",
						cardName = "悠遊簽帳金融卡",
						debitType = "2",
            };
            string resCode = "U999";
            
            stubDataAccessService.LoadData<TbDebitCardCode, object>("agdSp.uspDebitCardCodeQuery", Arg.Any<object>())
                .ReturnsNull();
            
            stubHttpContextAccessor.HttpContext.Request.Path = "/api/DebitCardCode/query";
            DebitCardCodeService DebitCardCodeService = new DebitCardCodeService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await DebitCardCodeService.QueryDebitCardCode(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspDebitCardCodeGet", Arg.Any<object>());
            Assert.AreEqual(result.total, 0);

        }

        [Test]
        public async ValueTask InsertDebitCardCodeTest()
        {  //arrange
            var insertReq = new DebitCardCodeInsertRequest
            {
				cardNo = "11",
				cardName = "悠遊簽帳金融卡",
				debitType = "2",
				creator = "admin",
				creatorName = "林管理",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspDebitCardCodeInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result:new SqlResponse() { ErrorMsg="",Msg="" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            DebitCardCodeService DebitCardCodeService = new
                DebitCardCodeService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await DebitCardCodeService.InsertDebitCardCode(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask InsertDebitCardCodeTestFailed()
        {  //arrange
            var insertReq = new DebitCardCodeInsertRequest
            {
				cardNo = "11",
				cardName = "悠遊簽帳金融卡",
				debitType = "2",
				creator = "admin",
				creatorName = "林管理",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspDebitCardCodeInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            DebitCardCodeService DebitCardCodeService = new
                DebitCardCodeService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await DebitCardCodeService.InsertDebitCardCode(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }


        [Test]
        public async ValueTask UpdateDebitCardCodeTest()
        {  //arrange
            var updateReq = new DebitCardCodeUpdateRequest
            {
				cardNo = "53",
				cardName = "夥伴款簽帳金融卡",
				debitType = "1",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspDebitCardCodeUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            DebitCardCodeService DebitCardCodeService = new
                DebitCardCodeService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await DebitCardCodeService.UpdateDebitCardCode(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask UpdateDebitCardCodeTestFailed()
        {  //arrange
            var updateReq = new DebitCardCodeUpdateRequest
            {
				cardNo = "53",
				cardName = "夥伴款簽帳金融卡",
				debitType = "1",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspDebitCardCodeUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            DebitCardCodeService DebitCardCodeService = new
                DebitCardCodeService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await DebitCardCodeService.UpdateDebitCardCode(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }




    }
}
