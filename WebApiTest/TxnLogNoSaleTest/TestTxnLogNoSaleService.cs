using AutoMapper;
using ESUN.AGD.DataAccess.DataService.DataAccess;
using ESUN.AGD.DataAccess.Type;
using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.Common;
using ESUN.AGD.WebApi.Application.TxnLogNoSale;
using ESUN.AGD.WebApi.Application.TxnLogNoSale.Contract;
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


namespace ESUN.AGD.WebApi.Test.TxnLogNoSaleTest
{
    [TestFixture]
    public class TestTxnLogNoSaleService
    {
        private static IDataAccessService stubDataAccessService = Substitute.For<IDataAccessService>();
        private static IGetTokenService stubGetTokenService = Substitute.For<IGetTokenService>();
        private static IHttpContextAccessor stubHttpContextAccessor = Substitute.For<IHttpContextAccessor>();
        private static IMapper stubMapper = Substitute.For<IMapper>();
        private static IUserService stubUserService = Substitute.For<IUserService>();
        private static IAutoNextNumService autoNextNumService = Substitute.For<IAutoNextNumService>();

        [Test]
        public async ValueTask GetTxnLogNoSaleTest()
        {  //arrange
            int seqNo = 1;
            string resCode = "U200";
           
            stubDataAccessService
                .LoadSingleData<TbTxnLogNoSale, object>("agdSp.uspTxnLogNoSaleGet", Arg.Any<int>())
                .ReturnsForAnyArgs(new TbTxnLogNoSale { SeqNo = 1 });

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            TxnLogNoSaleService TxnLogNoSaleService = 
                new TxnLogNoSaleService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await TxnLogNoSaleService.GetTxnLogNoSale(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspTxnLogNoSaleGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode,resCode);

        }

        [Test]
        public async ValueTask GetTxnLogNoSaleTestFailed()
        {  //arrange
            int seqNo = 0;
            string resCode = "U999";
          
            stubDataAccessService.LoadSingleData<TbTxnLogNoSale, object>("agdSp.uspTxnLogNoSaleGet", Arg.Any<int>())
                .ReturnsNull();          

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            TxnLogNoSaleService TxnLogNoSaleService = new TxnLogNoSaleService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await TxnLogNoSaleService.GetTxnLogNoSale(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<TbTxnLogNoSale, object>("agdSp.uspTxnLogNoSaleGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryTxnLogNoSaleTest()
        {  //arrange
            var queryReq = new TxnLogNoSaleQueryRequest { 						primaryUUID = "None",
						custKey = 5,
						customerID = "M106371509",
						flagType = "CROSS",
						reviewStatus = "1",
						creator = "14388",
						reviewDT = "None", };
            string resCode = "U200";
            
            stubDataAccessService.LoadData<TbTxnLogNoSale, object>
                (storeProcedure: "agdSp.uspTxnLogNoSaleQuery", Arg.Any<object>())
               .Returns(new TbTxnLogNoSale[] { 
                    new TbTxnLogNoSale { 
						primaryUUID = "None",
						custKey = 5,
						customerID = "M106371509",
						flagType = "CROSS",
						reviewStatus = "1",
						creator = "14388",
						reviewDT = "None",
                        Total = 2
                    },
                    new TbTxnLogNoSale { } 
                }.AsQueryable());
            
            stubHttpContextAccessor.HttpContext.Request.Path ="/api/TxnLogNoSale/query";
            
            TxnLogNoSaleService TxnLogNoSaleService = new 
                TxnLogNoSaleService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await TxnLogNoSaleService.QueryTxnLogNoSale(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
          
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryTxnLogNoSaleTestFailed()
        {  //arrange
            var queryReq = new TxnLogNoSaleQueryRequest {  
						primaryUUID = "None",
						custKey = 5,
						customerID = "M106371509",
						flagType = "CROSS",
						reviewStatus = "1",
						creator = "14388",
						reviewDT = "None",
            };
            string resCode = "U999";
            
            stubDataAccessService.LoadData<TbTxnLogNoSale, object>("agdSp.uspTxnLogNoSaleQuery", Arg.Any<object>())
                .ReturnsNull();
            
            stubHttpContextAccessor.HttpContext.Request.Path = "/api/TxnLogNoSale/query";
            TxnLogNoSaleService TxnLogNoSaleService = new TxnLogNoSaleService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await TxnLogNoSaleService.QueryTxnLogNoSale(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspTxnLogNoSaleGet", Arg.Any<object>());
            Assert.AreEqual(result.total, 0);

        }

        [Test]
        public async ValueTask InsertTxnLogNoSaleTest()
        {  //arrange
            var insertReq = new TxnLogNoSaleInsertRequest
            {
				primaryUUID = "None",
				custKey = 5,
				customerID = "M106371509",
				customerName = "測試",
				flagType = "CROSS",
				saleType = "Y",
				iDMark = "",
				reviewStatus = "1",
				reviewMemo = "None",
				creator = "14388",
				creatorName = "顏O紳",
				reviewDT = "None",
				reviewer = "None",
				reviewerName = "None",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspTxnLogNoSaleInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result:new SqlResponse() { ErrorMsg="",Msg="" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            TxnLogNoSaleService TxnLogNoSaleService = new
                TxnLogNoSaleService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await TxnLogNoSaleService.InsertTxnLogNoSale(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask InsertTxnLogNoSaleTestFailed()
        {  //arrange
            var insertReq = new TxnLogNoSaleInsertRequest
            {
				primaryUUID = "None",
				custKey = 5,
				customerID = "M106371509",
				customerName = "測試",
				flagType = "CROSS",
				saleType = "Y",
				iDMark = "",
				reviewStatus = "1",
				reviewMemo = "None",
				creator = "14388",
				creatorName = "顏O紳",
				reviewDT = "None",
				reviewer = "None",
				reviewerName = "None",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspTxnLogNoSaleInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            TxnLogNoSaleService TxnLogNoSaleService = new
                TxnLogNoSaleService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await TxnLogNoSaleService.InsertTxnLogNoSale(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }


        [Test]
        public async ValueTask UpdateTxnLogNoSaleTest()
        {  //arrange
            var updateReq = new TxnLogNoSaleUpdateRequest
            {
				primaryUUID = "",
				custKey = 5,
				customerID = "M106371509",
				customerName = "測試",
				flagType = "CROSS",
				saleType = "Y",
				iDMark = "",
				reviewStatus = "1",
				reviewMemo = "None",
				reviewDT = "None",
				reviewer = "None",
				reviewerName = "None",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspTxnLogNoSaleUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            TxnLogNoSaleService TxnLogNoSaleService = new
                TxnLogNoSaleService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await TxnLogNoSaleService.UpdateTxnLogNoSale(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask UpdateTxnLogNoSaleTestFailed()
        {  //arrange
            var updateReq = new TxnLogNoSaleUpdateRequest
            {
				primaryUUID = "",
				custKey = 5,
				customerID = "M106371509",
				customerName = "測試",
				flagType = "CROSS",
				saleType = "Y",
				iDMark = "",
				reviewStatus = "1",
				reviewMemo = "None",
				reviewDT = "None",
				reviewer = "None",
				reviewerName = "None",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspTxnLogNoSaleUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            TxnLogNoSaleService TxnLogNoSaleService = new
                TxnLogNoSaleService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await TxnLogNoSaleService.UpdateTxnLogNoSale(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }




    }
}
