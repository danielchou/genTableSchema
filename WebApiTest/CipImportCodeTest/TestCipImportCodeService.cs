using AutoMapper;
using ESUN.AGD.DataAccess.DataService.DataAccess;
using ESUN.AGD.DataAccess.Type;
using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.Common;
using ESUN.AGD.WebApi.Application.CipImportCode;
using ESUN.AGD.WebApi.Application.CipImportCode.Contract;
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


namespace ESUN.AGD.WebApi.Test.CipImportCodeTest
{
    [TestFixture]
    public class TestCipImportCodeService
    {
        private static IDataAccessService stubDataAccessService = Substitute.For<IDataAccessService>();
        private static IGetTokenService stubGetTokenService = Substitute.For<IGetTokenService>();
        private static IHttpContextAccessor stubHttpContextAccessor = Substitute.For<IHttpContextAccessor>();
        private static IMapper stubMapper = Substitute.For<IMapper>();
        private static IUserService stubUserService = Substitute.For<IUserService>();
        private static IAutoNextNumService autoNextNumService = Substitute.For<IAutoNextNumService>();

        [Test]
        public async ValueTask GetCipImportCodeTest()
        {  //arrange
            int seqNo = 1;
            string resCode = "U200";
           
            stubDataAccessService
                .LoadSingleData<TbCipImportCode, object>("agdSp.uspCipImportCodeGet", Arg.Any<int>())
                .ReturnsForAnyArgs(new TbCipImportCode { SeqNo = 1 });

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            CipImportCodeService CipImportCodeService = 
                new CipImportCodeService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await CipImportCodeService.GetCipImportCode(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspCipImportCodeGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode,resCode);

        }

        [Test]
        public async ValueTask GetCipImportCodeTestFailed()
        {  //arrange
            int seqNo = 0;
            string resCode = "U999";
          
            stubDataAccessService.LoadSingleData<TbCipImportCode, object>("agdSp.uspCipImportCodeGet", Arg.Any<int>())
                .ReturnsNull();          

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            CipImportCodeService CipImportCodeService = new CipImportCodeService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await CipImportCodeService.GetCipImportCode(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<TbCipImportCode, object>("agdSp.uspCipImportCodeGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryCipImportCodeTest()
        {  //arrange
            var queryReq = new CipImportCodeQueryRequest { 						cipCodeID = " ",
						cipCodeName = "未申請/註銷",
						priorityDegree = 0, };
            string resCode = "U200";
            
            stubDataAccessService.LoadData<TbCipImportCode, object>
                (storeProcedure: "agdSp.uspCipImportCodeQuery", Arg.Any<object>())
               .Returns(new TbCipImportCode[] { 
                    new TbCipImportCode { 
						cipCodeID = " ",
						cipCodeName = "未申請/註銷",
						priorityDegree = 0,
                        Total = 2
                    },
                    new TbCipImportCode { } 
                }.AsQueryable());
            
            stubHttpContextAccessor.HttpContext.Request.Path ="/api/CipImportCode/query";
            
            CipImportCodeService CipImportCodeService = new 
                CipImportCodeService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await CipImportCodeService.QueryCipImportCode(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
          
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryCipImportCodeTestFailed()
        {  //arrange
            var queryReq = new CipImportCodeQueryRequest {  
						cipCodeID = " ",
						cipCodeName = "未申請/註銷",
						priorityDegree = 0,
            };
            string resCode = "U999";
            
            stubDataAccessService.LoadData<TbCipImportCode, object>("agdSp.uspCipImportCodeQuery", Arg.Any<object>())
                .ReturnsNull();
            
            stubHttpContextAccessor.HttpContext.Request.Path = "/api/CipImportCode/query";
            CipImportCodeService CipImportCodeService = new CipImportCodeService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await CipImportCodeService.QueryCipImportCode(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspCipImportCodeGet", Arg.Any<object>());
            Assert.AreEqual(result.total, 0);

        }

        [Test]
        public async ValueTask InsertCipImportCodeTest()
        {  //arrange
            var insertReq = new CipImportCodeInsertRequest
            {
				cipCodeType = "otpServiceFlag",
				cipCodeID = " ",
				cipCodeName = "未申請/註銷",
				priorityDegree = 0,
				creator = "SYS",
				creatorName = "系統觸發",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspCipImportCodeInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result:new SqlResponse() { ErrorMsg="",Msg="" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            CipImportCodeService CipImportCodeService = new
                CipImportCodeService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await CipImportCodeService.InsertCipImportCode(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask InsertCipImportCodeTestFailed()
        {  //arrange
            var insertReq = new CipImportCodeInsertRequest
            {
				cipCodeType = "otpServiceFlag",
				cipCodeID = " ",
				cipCodeName = "未申請/註銷",
				priorityDegree = 0,
				creator = "SYS",
				creatorName = "系統觸發",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspCipImportCodeInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            CipImportCodeService CipImportCodeService = new
                CipImportCodeService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await CipImportCodeService.InsertCipImportCode(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }


        [Test]
        public async ValueTask UpdateCipImportCodeTest()
        {  //arrange
            var updateReq = new CipImportCodeUpdateRequest
            {
				cipCodeType = "otpServiceFlag",
				cipCodeID = "2",
				cipCodeName = "停用中",
				priorityDegree = 0,
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspCipImportCodeUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            CipImportCodeService CipImportCodeService = new
                CipImportCodeService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await CipImportCodeService.UpdateCipImportCode(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask UpdateCipImportCodeTestFailed()
        {  //arrange
            var updateReq = new CipImportCodeUpdateRequest
            {
				cipCodeType = "otpServiceFlag",
				cipCodeID = "2",
				cipCodeName = "停用中",
				priorityDegree = 0,
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspCipImportCodeUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            CipImportCodeService CipImportCodeService = new
                CipImportCodeService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await CipImportCodeService.UpdateCipImportCode(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }




    }
}
