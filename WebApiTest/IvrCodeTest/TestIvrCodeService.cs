using AutoMapper;
using ESUN.AGD.DataAccess.DataService.DataAccess;
using ESUN.AGD.DataAccess.Type;
using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.Common;
using ESUN.AGD.WebApi.Application.IvrCode;
using ESUN.AGD.WebApi.Application.IvrCode.Contract;
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


namespace ESUN.AGD.WebApi.Test.IvrCodeTest
{
    [TestFixture]
    public class TestIvrCodeService
    {
        private static IDataAccessService stubDataAccessService = Substitute.For<IDataAccessService>();
        private static IGetTokenService stubGetTokenService = Substitute.For<IGetTokenService>();
        private static IHttpContextAccessor stubHttpContextAccessor = Substitute.For<IHttpContextAccessor>();
        private static IMapper stubMapper = Substitute.For<IMapper>();
        private static IUserService stubUserService = Substitute.For<IUserService>();
        private static IAutoNextNumService autoNextNumService = Substitute.For<IAutoNextNumService>();

        [Test]
        public async ValueTask GetIvrCodeTest()
        {  //arrange
            int seqNo = 1;
            string resCode = "U200";
           
            stubDataAccessService
                .LoadSingleData<TbIvrCode, object>("agdSp.uspIvrCodeGet", Arg.Any<int>())
                .ReturnsForAnyArgs(new TbIvrCode { SeqNo = 1 });

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            IvrCodeService IvrCodeService = 
                new IvrCodeService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await IvrCodeService.GetIvrCode(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspIvrCodeGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode,resCode);

        }

        [Test]
        public async ValueTask GetIvrCodeTestFailed()
        {  //arrange
            int seqNo = 0;
            string resCode = "U999";
          
            stubDataAccessService.LoadSingleData<TbIvrCode, object>("agdSp.uspIvrCodeGet", Arg.Any<int>())
                .ReturnsNull();          

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            IvrCodeService IvrCodeService = new IvrCodeService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await IvrCodeService.GetIvrCode(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<TbIvrCode, object>("agdSp.uspIvrCodeGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryIvrCodeTest()
        {  //arrange
            var queryReq = new IvrCodeQueryRequest { 						ivrName = "安養信託禮賓服務選單", };
            string resCode = "U200";
            
            stubDataAccessService.LoadData<TbIvrCode, object>
                (storeProcedure: "agdSp.uspIvrCodeQuery", Arg.Any<object>())
               .Returns(new TbIvrCode[] { 
                    new TbIvrCode { 
						ivrName = "安養信託禮賓服務選單",
                        Total = 2
                    },
                    new TbIvrCode { } 
                }.AsQueryable());
            
            stubHttpContextAccessor.HttpContext.Request.Path ="/api/IvrCode/query";
            
            IvrCodeService IvrCodeService = new 
                IvrCodeService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await IvrCodeService.QueryIvrCode(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
          
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryIvrCodeTestFailed()
        {  //arrange
            var queryReq = new IvrCodeQueryRequest {  
						ivrName = "安養信託禮賓服務選單",
            };
            string resCode = "U999";
            
            stubDataAccessService.LoadData<TbIvrCode, object>("agdSp.uspIvrCodeQuery", Arg.Any<object>())
                .ReturnsNull();
            
            stubHttpContextAccessor.HttpContext.Request.Path = "/api/IvrCode/query";
            IvrCodeService IvrCodeService = new IvrCodeService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await IvrCodeService.QueryIvrCode(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspIvrCodeGet", Arg.Any<object>());
            Assert.AreEqual(result.total, 0);

        }

        [Test]
        public async ValueTask InsertIvrCodeTest()
        {  //arrange
            var insertReq = new IvrCodeInsertRequest
            {
				ivrCode = "Aspire_Lifestyles",
				ivrName = "安養信託禮賓服務選單",
				isHighlight = False,
				creator = "admin",
				creatorName = "林管理",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspIvrCodeInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result:new SqlResponse() { ErrorMsg="",Msg="" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            IvrCodeService IvrCodeService = new
                IvrCodeService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await IvrCodeService.InsertIvrCode(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask InsertIvrCodeTestFailed()
        {  //arrange
            var insertReq = new IvrCodeInsertRequest
            {
				ivrCode = "Aspire_Lifestyles",
				ivrName = "安養信託禮賓服務選單",
				isHighlight = False,
				creator = "admin",
				creatorName = "林管理",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspIvrCodeInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            IvrCodeService IvrCodeService = new
                IvrCodeService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await IvrCodeService.InsertIvrCode(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }


        [Test]
        public async ValueTask UpdateIvrCodeTest()
        {  //arrange
            var updateReq = new IvrCodeUpdateRequest
            {
				ivrCode = "T5000",
				ivrName = "詐騙宣導",
				isHighlight = False,
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspIvrCodeUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            IvrCodeService IvrCodeService = new
                IvrCodeService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await IvrCodeService.UpdateIvrCode(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask UpdateIvrCodeTestFailed()
        {  //arrange
            var updateReq = new IvrCodeUpdateRequest
            {
				ivrCode = "T5000",
				ivrName = "詐騙宣導",
				isHighlight = False,
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspIvrCodeUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            IvrCodeService IvrCodeService = new
                IvrCodeService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await IvrCodeService.UpdateIvrCode(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }




    }
}
