using AutoMapper;
using ESUN.AGD.DataAccess.DataService.DataAccess;
using ESUN.AGD.DataAccess.Type;
using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.Common;
using ESUN.AGD.WebApi.Application.ReasonIvr;
using ESUN.AGD.WebApi.Application.ReasonIvr.Contract;
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


namespace ESUN.AGD.WebApi.Test.ReasonIvrTest
{
    [TestFixture]
    public class TestReasonIvrService
    {
        private static IDataAccessService stubDataAccessService = Substitute.For<IDataAccessService>();
        private static IGetTokenService stubGetTokenService = Substitute.For<IGetTokenService>();
        private static IHttpContextAccessor stubHttpContextAccessor = Substitute.For<IHttpContextAccessor>();
        private static IMapper stubMapper = Substitute.For<IMapper>();
        private static IUserService stubUserService = Substitute.For<IUserService>();
        private static IAutoNextNumService autoNextNumService = Substitute.For<IAutoNextNumService>();

        [Test]
        public async ValueTask GetReasonIvrTest()
        {  //arrange
            int seqNo = 1;
            string resCode = "U200";
           
            stubDataAccessService
                .LoadSingleData<TbReasonIvr, object>("agdSp.uspReasonIvrGet", Arg.Any<int>())
                .ReturnsForAnyArgs(new TbReasonIvr { SeqNo = 1 });

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            ReasonIvrService ReasonIvrService = 
                new ReasonIvrService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ReasonIvrService.GetReasonIvr(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspReasonIvrGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode,resCode);

        }

        [Test]
        public async ValueTask GetReasonIvrTestFailed()
        {  //arrange
            int seqNo = 0;
            string resCode = "U999";
          
            stubDataAccessService.LoadSingleData<TbReasonIvr, object>("agdSp.uspReasonIvrGet", Arg.Any<int>())
                .ReturnsNull();          

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            ReasonIvrService ReasonIvrService = new ReasonIvrService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ReasonIvrService.GetReasonIvr(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<TbReasonIvr, object>("agdSp.uspReasonIvrGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryReasonIvrTest()
        {  //arrange
            var queryReq = new ReasonIvrQueryRequest { 						reasonID = "I1903", };
            string resCode = "U200";
            
            stubDataAccessService.LoadData<TbReasonIvr, object>
                (storeProcedure: "agdSp.uspReasonIvrQuery", Arg.Any<object>())
               .Returns(new TbReasonIvr[] { 
                    new TbReasonIvr { 
						reasonID = "I1903",
                        Total = 2
                    },
                    new TbReasonIvr { } 
                }.AsQueryable());
            
            stubHttpContextAccessor.HttpContext.Request.Path ="/api/ReasonIvr/query";
            
            ReasonIvrService ReasonIvrService = new 
                ReasonIvrService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ReasonIvrService.QueryReasonIvr(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
          
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryReasonIvrTestFailed()
        {  //arrange
            var queryReq = new ReasonIvrQueryRequest {  
						reasonID = "I1903",
            };
            string resCode = "U999";
            
            stubDataAccessService.LoadData<TbReasonIvr, object>("agdSp.uspReasonIvrQuery", Arg.Any<object>())
                .ReturnsNull();
            
            stubHttpContextAccessor.HttpContext.Request.Path = "/api/ReasonIvr/query";
            ReasonIvrService ReasonIvrService = new ReasonIvrService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ReasonIvrService.QueryReasonIvr(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspReasonIvrGet", Arg.Any<object>());
            Assert.AreEqual(result.total, 0);

        }

        [Test]
        public async ValueTask InsertReasonIvrTest()
        {  //arrange
            var insertReq = new ReasonIvrInsertRequest
            {
				ivrCode = "Aspire_Lifestyles",
				reasonID = "I1903",
				ivrSuggestName = "安養貴賓來了",
				creator = "admin",
				creatorName = "林管理",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspReasonIvrInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result:new SqlResponse() { ErrorMsg="",Msg="" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            ReasonIvrService ReasonIvrService = new
                ReasonIvrService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ReasonIvrService.InsertReasonIvr(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask InsertReasonIvrTestFailed()
        {  //arrange
            var insertReq = new ReasonIvrInsertRequest
            {
				ivrCode = "Aspire_Lifestyles",
				reasonID = "I1903",
				ivrSuggestName = "安養貴賓來了",
				creator = "admin",
				creatorName = "林管理",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspReasonIvrInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            ReasonIvrService ReasonIvrService = new
                ReasonIvrService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ReasonIvrService.InsertReasonIvr(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }


        [Test]
        public async ValueTask UpdateReasonIvrTest()
        {  //arrange
            var updateReq = new ReasonIvrUpdateRequest
            {
				ivrCode = "T1130",
				reasonID = "M0603",
				ivrSuggestName = "YES",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspReasonIvrUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            ReasonIvrService ReasonIvrService = new
                ReasonIvrService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ReasonIvrService.UpdateReasonIvr(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask UpdateReasonIvrTestFailed()
        {  //arrange
            var updateReq = new ReasonIvrUpdateRequest
            {
				ivrCode = "T1130",
				reasonID = "M0603",
				ivrSuggestName = "YES",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspReasonIvrUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            ReasonIvrService ReasonIvrService = new
                ReasonIvrService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ReasonIvrService.UpdateReasonIvr(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }




    }
}
