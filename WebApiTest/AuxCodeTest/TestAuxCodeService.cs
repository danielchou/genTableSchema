using AutoMapper;
using ESUN.AGD.DataAccess.DataService.DataAccess;
using ESUN.AGD.DataAccess.Type;
using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.Common;
using ESUN.AGD.WebApi.Application.AuxCode;
using ESUN.AGD.WebApi.Application.AuxCode.Contract;
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


namespace ESUN.AGD.WebApi.Test.AuxCodeTest
{
    [TestFixture]
    public class TestAuxCodeService
    {
        private static IDataAccessService stubDataAccessService = Substitute.For<IDataAccessService>();
        private static IGetTokenService stubGetTokenService = Substitute.For<IGetTokenService>();
        private static IHttpContextAccessor stubHttpContextAccessor = Substitute.For<IHttpContextAccessor>();
        private static IMapper stubMapper = Substitute.For<IMapper>();
        private static IUserService stubUserService = Substitute.For<IUserService>();
        private static IAutoNextNumService autoNextNumService = Substitute.For<IAutoNextNumService>();

        [Test]
        public async ValueTask GetAuxCodeTest()
        {  //arrange
            int seqNo = 1;
            string resCode = "U200";
           
            stubDataAccessService
                .LoadSingleData<TbAuxCode, object>("agdSp.uspAuxCodeGet", Arg.Any<int>())
                .ReturnsForAnyArgs(new TbAuxCode { SeqNo = 1 });

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            AuxCodeService AuxCodeService = 
                new AuxCodeService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await AuxCodeService.GetAuxCode(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspAuxCodeGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode,resCode);

        }

        [Test]
        public async ValueTask GetAuxCodeTestFailed()
        {  //arrange
            int seqNo = 0;
            string resCode = "U999";
          
            stubDataAccessService.LoadSingleData<TbAuxCode, object>("agdSp.uspAuxCodeGet", Arg.Any<int>())
                .ReturnsNull();          

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            AuxCodeService AuxCodeService = new AuxCodeService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await AuxCodeService.GetAuxCode(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<TbAuxCode, object>("agdSp.uspAuxCodeGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryAuxCodeTest()
        {  //arrange
            var queryReq = new AuxCodeQueryRequest { 						auxName = "銀行作業處理", };
            string resCode = "U200";
            
            stubDataAccessService.LoadData<TbAuxCode, object>
                (storeProcedure: "agdSp.uspAuxCodeQuery", Arg.Any<object>())
               .Returns(new TbAuxCode[] { 
                    new TbAuxCode { 
						auxName = "銀行作業處理",
                        Total = 2
                    },
                    new TbAuxCode { } 
                }.AsQueryable());
            
            stubHttpContextAccessor.HttpContext.Request.Path ="/api/AuxCode/query";
            
            AuxCodeService AuxCodeService = new 
                AuxCodeService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await AuxCodeService.QueryAuxCode(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
          
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryAuxCodeTestFailed()
        {  //arrange
            var queryReq = new AuxCodeQueryRequest {  
						auxName = "銀行作業處理",
            };
            string resCode = "U999";
            
            stubDataAccessService.LoadData<TbAuxCode, object>("agdSp.uspAuxCodeQuery", Arg.Any<object>())
                .ReturnsNull();
            
            stubHttpContextAccessor.HttpContext.Request.Path = "/api/AuxCode/query";
            AuxCodeService AuxCodeService = new AuxCodeService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await AuxCodeService.QueryAuxCode(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspAuxCodeGet", Arg.Any<object>());
            Assert.AreEqual(result.total, 0);

        }

        [Test]
        public async ValueTask InsertAuxCodeTest()
        {  //arrange
            var insertReq = new AuxCodeInsertRequest
            {
				auxID = "AccountHandling",
				auxName = "銀行作業處理",
				isLongTimeAux = True,
				displayOrder = 0,
				isVisible = True,
				creator = "admin",
				creatorName = "林管理",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspAuxCodeInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result:new SqlResponse() { ErrorMsg="",Msg="" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            AuxCodeService AuxCodeService = new
                AuxCodeService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await AuxCodeService.InsertAuxCode(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask InsertAuxCodeTestFailed()
        {  //arrange
            var insertReq = new AuxCodeInsertRequest
            {
				auxID = "AccountHandling",
				auxName = "銀行作業處理",
				isLongTimeAux = True,
				displayOrder = 0,
				isVisible = True,
				creator = "admin",
				creatorName = "林管理",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspAuxCodeInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            AuxCodeService AuxCodeService = new
                AuxCodeService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await AuxCodeService.InsertAuxCode(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }


        [Test]
        public async ValueTask UpdateAuxCodeTest()
        {  //arrange
            var updateReq = new AuxCodeUpdateRequest
            {
				auxID = "Training",
				auxName = "教育訓練",
				isLongTimeAux = True,
				displayOrder = 0,
				isVisible = True,
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspAuxCodeUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            AuxCodeService AuxCodeService = new
                AuxCodeService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await AuxCodeService.UpdateAuxCode(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask UpdateAuxCodeTestFailed()
        {  //arrange
            var updateReq = new AuxCodeUpdateRequest
            {
				auxID = "Training",
				auxName = "教育訓練",
				isLongTimeAux = True,
				displayOrder = 0,
				isVisible = True,
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspAuxCodeUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            AuxCodeService AuxCodeService = new
                AuxCodeService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await AuxCodeService.UpdateAuxCode(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }




    }
}
