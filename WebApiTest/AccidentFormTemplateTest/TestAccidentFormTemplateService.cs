using AutoMapper;
using ESUN.AGD.DataAccess.DataService.DataAccess;
using ESUN.AGD.DataAccess.Type;
using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.Common;
using ESUN.AGD.WebApi.Application.AccidentFormTemplate;
using ESUN.AGD.WebApi.Application.AccidentFormTemplate.Contract;
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


namespace ESUN.AGD.WebApi.Test.AccidentFormTemplateTest
{
    [TestFixture]
    public class TestAccidentFormTemplateService
    {
        private static IDataAccessService stubDataAccessService = Substitute.For<IDataAccessService>();
        private static IGetTokenService stubGetTokenService = Substitute.For<IGetTokenService>();
        private static IHttpContextAccessor stubHttpContextAccessor = Substitute.For<IHttpContextAccessor>();
        private static IMapper stubMapper = Substitute.For<IMapper>();
        private static IUserService stubUserService = Substitute.For<IUserService>();
        private static IAutoNextNumService autoNextNumService = Substitute.For<IAutoNextNumService>();

        [Test]
        public async ValueTask GetAccidentFormTemplateTest()
        {  //arrange
            int seqNo = 1;
            string resCode = "U200";
           
            stubDataAccessService
                .LoadSingleData<TbAccidentFormTemplate, object>("agdSp.uspAccidentFormTemplateGet", Arg.Any<int>())
                .ReturnsForAnyArgs(new TbAccidentFormTemplate { SeqNo = 1 });

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            AccidentFormTemplateService AccidentFormTemplateService = 
                new AccidentFormTemplateService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await AccidentFormTemplateService.GetAccidentFormTemplate(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspAccidentFormTemplateGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode,resCode);

        }

        [Test]
        public async ValueTask GetAccidentFormTemplateTestFailed()
        {  //arrange
            int seqNo = 0;
            string resCode = "U999";
          
            stubDataAccessService.LoadSingleData<TbAccidentFormTemplate, object>("agdSp.uspAccidentFormTemplateGet", Arg.Any<int>())
                .ReturnsNull();          

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            AccidentFormTemplateService AccidentFormTemplateService = new AccidentFormTemplateService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await AccidentFormTemplateService.GetAccidentFormTemplate(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<TbAccidentFormTemplate, object>("agdSp.uspAccidentFormTemplateGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryAccidentFormTemplateTest()
        {  //arrange
            var queryReq = new AccidentFormTemplateQueryRequest { 						informSource = "1",
						formType = 5,
						formSubType = 6,
						subject = "test", };
            string resCode = "U200";
            
            stubDataAccessService.LoadData<TbAccidentFormTemplate, object>
                (storeProcedure: "agdSp.uspAccidentFormTemplateQuery", Arg.Any<object>())
               .Returns(new TbAccidentFormTemplate[] { 
                    new TbAccidentFormTemplate { 
						informSource = "1",
						formType = 5,
						formSubType = 6,
						subject = "test",
                        Total = 2
                    },
                    new TbAccidentFormTemplate { } 
                }.AsQueryable());
            
            stubHttpContextAccessor.HttpContext.Request.Path ="/api/AccidentFormTemplate/query";
            
            AccidentFormTemplateService AccidentFormTemplateService = new 
                AccidentFormTemplateService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await AccidentFormTemplateService.QueryAccidentFormTemplate(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
          
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryAccidentFormTemplateTestFailed()
        {  //arrange
            var queryReq = new AccidentFormTemplateQueryRequest {  
						informSource = "1",
						formType = 5,
						formSubType = 6,
						subject = "test",
            };
            string resCode = "U999";
            
            stubDataAccessService.LoadData<TbAccidentFormTemplate, object>("agdSp.uspAccidentFormTemplateQuery", Arg.Any<object>())
                .ReturnsNull();
            
            stubHttpContextAccessor.HttpContext.Request.Path = "/api/AccidentFormTemplate/query";
            AccidentFormTemplateService AccidentFormTemplateService = new AccidentFormTemplateService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await AccidentFormTemplateService.QueryAccidentFormTemplate(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspAccidentFormTemplateGet", Arg.Any<object>());
            Assert.AreEqual(result.total, 0);

        }

        [Test]
        public async ValueTask InsertAccidentFormTemplateTest()
        {  //arrange
            var insertReq = new AccidentFormTemplateInsertRequest
            {
				informType = "1",
				informSource = "1",
				formType = 5,
				formSubType = 6,
				subject = "test",
				description = "123412341324132",
				creator = "10046",
				creatorName = "陳麗杰鰻魚飯",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspAccidentFormTemplateInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result:new SqlResponse() { ErrorMsg="",Msg="" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            AccidentFormTemplateService AccidentFormTemplateService = new
                AccidentFormTemplateService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await AccidentFormTemplateService.InsertAccidentFormTemplate(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask InsertAccidentFormTemplateTestFailed()
        {  //arrange
            var insertReq = new AccidentFormTemplateInsertRequest
            {
				informType = "1",
				informSource = "1",
				formType = 5,
				formSubType = 6,
				subject = "test",
				description = "123412341324132",
				creator = "10046",
				creatorName = "陳麗杰鰻魚飯",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspAccidentFormTemplateInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            AccidentFormTemplateService AccidentFormTemplateService = new
                AccidentFormTemplateService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await AccidentFormTemplateService.InsertAccidentFormTemplate(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }


        [Test]
        public async ValueTask UpdateAccidentFormTemplateTest()
        {  //arrange
            var updateReq = new AccidentFormTemplateUpdateRequest
            {
				informType = "2",
				informSource = "1",
				formType = 1,
				formSubType = 2,
				subject = "XXXX",
				description = "",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspAccidentFormTemplateUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            AccidentFormTemplateService AccidentFormTemplateService = new
                AccidentFormTemplateService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await AccidentFormTemplateService.UpdateAccidentFormTemplate(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask UpdateAccidentFormTemplateTestFailed()
        {  //arrange
            var updateReq = new AccidentFormTemplateUpdateRequest
            {
				informType = "2",
				informSource = "1",
				formType = 1,
				formSubType = 2,
				subject = "XXXX",
				description = "",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspAccidentFormTemplateUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            AccidentFormTemplateService AccidentFormTemplateService = new
                AccidentFormTemplateService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await AccidentFormTemplateService.UpdateAccidentFormTemplate(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }




    }
}
