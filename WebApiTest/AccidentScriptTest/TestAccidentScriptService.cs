using AutoMapper;
using ESUN.AGD.DataAccess.DataService.DataAccess;
using ESUN.AGD.DataAccess.Type;
using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.Common;
using ESUN.AGD.WebApi.Application.AccidentScript;
using ESUN.AGD.WebApi.Application.AccidentScript.Contract;
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


namespace ESUN.AGD.WebApi.Test.AccidentScriptTest
{
    [TestFixture]
    public class TestAccidentScriptService
    {
        private static IDataAccessService stubDataAccessService = Substitute.For<IDataAccessService>();
        private static IGetTokenService stubGetTokenService = Substitute.For<IGetTokenService>();
        private static IHttpContextAccessor stubHttpContextAccessor = Substitute.For<IHttpContextAccessor>();
        private static IMapper stubMapper = Substitute.For<IMapper>();
        private static IUserService stubUserService = Substitute.For<IUserService>();
        private static IAutoNextNumService autoNextNumService = Substitute.For<IAutoNextNumService>();

        [Test]
        public async ValueTask GetAccidentScriptTest()
        {  //arrange
            int seqNo = 1;
            string resCode = "U200";
           
            stubDataAccessService
                .LoadSingleData<TbAccidentScript, object>("agdSp.uspAccidentScriptGet", Arg.Any<int>())
                .ReturnsForAnyArgs(new TbAccidentScript { SeqNo = 1 });

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            AccidentScriptService AccidentScriptService = 
                new AccidentScriptService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await AccidentScriptService.GetAccidentScript(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspAccidentScriptGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode,resCode);

        }

        [Test]
        public async ValueTask GetAccidentScriptTestFailed()
        {  //arrange
            int seqNo = 0;
            string resCode = "U999";
          
            stubDataAccessService.LoadSingleData<TbAccidentScript, object>("agdSp.uspAccidentScriptGet", Arg.Any<int>())
                .ReturnsNull();          

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            AccidentScriptService AccidentScriptService = new AccidentScriptService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await AccidentScriptService.GetAccidentScript(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<TbAccidentScript, object>("agdSp.uspAccidentScriptGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryAccidentScriptTest()
        {  //arrange
            var queryReq = new AccidentScriptQueryRequest { 						accidentCode = "34",
						txnResultID = "0000", };
            string resCode = "U200";
            
            stubDataAccessService.LoadData<TbAccidentScript, object>
                (storeProcedure: "agdSp.uspAccidentScriptQuery", Arg.Any<object>())
               .Returns(new TbAccidentScript[] { 
                    new TbAccidentScript { 
						accidentCode = "34",
						txnResultID = "0000",
                        Total = 2
                    },
                    new TbAccidentScript { } 
                }.AsQueryable());
            
            stubHttpContextAccessor.HttpContext.Request.Path ="/api/AccidentScript/query";
            
            AccidentScriptService AccidentScriptService = new 
                AccidentScriptService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await AccidentScriptService.QueryAccidentScript(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
          
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryAccidentScriptTestFailed()
        {  //arrange
            var queryReq = new AccidentScriptQueryRequest {  
						accidentCode = "34",
						txnResultID = "0000",
            };
            string resCode = "U999";
            
            stubDataAccessService.LoadData<TbAccidentScript, object>("agdSp.uspAccidentScriptQuery", Arg.Any<object>())
                .ReturnsNull();
            
            stubHttpContextAccessor.HttpContext.Request.Path = "/api/AccidentScript/query";
            AccidentScriptService AccidentScriptService = new AccidentScriptService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await AccidentScriptService.QueryAccidentScript(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspAccidentScriptGet", Arg.Any<object>());
            Assert.AreEqual(result.total, 0);

        }

        [Test]
        public async ValueTask InsertAccidentScriptTest()
        {  //arrange
            var insertReq = new AccidentScriptInsertRequest
            {
				accidentTxnType = "1",
				accidentCode = "34",
				txnResultID = "0000",
				content = "已凍結",
				creator = "sys",
				creatorName = "系統",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspAccidentScriptInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result:new SqlResponse() { ErrorMsg="",Msg="" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            AccidentScriptService AccidentScriptService = new
                AccidentScriptService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await AccidentScriptService.InsertAccidentScript(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask InsertAccidentScriptTestFailed()
        {  //arrange
            var insertReq = new AccidentScriptInsertRequest
            {
				accidentTxnType = "1",
				accidentCode = "34",
				txnResultID = "0000",
				content = "已凍結",
				creator = "sys",
				creatorName = "系統",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspAccidentScriptInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            AccidentScriptService AccidentScriptService = new
                AccidentScriptService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await AccidentScriptService.InsertAccidentScript(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }


        [Test]
        public async ValueTask UpdateAccidentScriptTest()
        {  //arrange
            var updateReq = new AccidentScriptUpdateRequest
            {
				accidentTxnType = "3",
				accidentCode = "13",
				txnResultID = "E063",
				content = "已無存款餘額",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspAccidentScriptUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            AccidentScriptService AccidentScriptService = new
                AccidentScriptService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await AccidentScriptService.UpdateAccidentScript(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask UpdateAccidentScriptTestFailed()
        {  //arrange
            var updateReq = new AccidentScriptUpdateRequest
            {
				accidentTxnType = "3",
				accidentCode = "13",
				txnResultID = "E063",
				content = "已無存款餘額",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspAccidentScriptUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            AccidentScriptService AccidentScriptService = new
                AccidentScriptService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await AccidentScriptService.UpdateAccidentScript(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }




    }
}
