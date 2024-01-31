using AutoMapper;
using ESUN.AGD.DataAccess.DataService.DataAccess;
using ESUN.AGD.DataAccess.Type;
using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.Common;
using ESUN.AGD.WebApi.Application.EvaluationForm;
using ESUN.AGD.WebApi.Application.EvaluationForm.Contract;
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


namespace ESUN.AGD.WebApi.Test.EvaluationFormTest
{
    [TestFixture]
    public class TestEvaluationFormService
    {
        private static IDataAccessService stubDataAccessService = Substitute.For<IDataAccessService>();
        private static IGetTokenService stubGetTokenService = Substitute.For<IGetTokenService>();
        private static IHttpContextAccessor stubHttpContextAccessor = Substitute.For<IHttpContextAccessor>();
        private static IMapper stubMapper = Substitute.For<IMapper>();
        private static IUserService stubUserService = Substitute.For<IUserService>();
        private static IAutoNextNumService autoNextNumService = Substitute.For<IAutoNextNumService>();

        [Test]
        public async ValueTask GetEvaluationFormTest()
        {  //arrange
            int seqNo = 1;
            string resCode = "U200";
           
            stubDataAccessService
                .LoadSingleData<TbEvaluationForm, object>("agdSp.uspEvaluationFormGet", Arg.Any<int>())
                .ReturnsForAnyArgs(new TbEvaluationForm { SeqNo = 1 });

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            EvaluationFormService EvaluationFormService = 
                new EvaluationFormService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await EvaluationFormService.GetEvaluationForm(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspEvaluationFormGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode,resCode);

        }

        [Test]
        public async ValueTask GetEvaluationFormTestFailed()
        {  //arrange
            int seqNo = 0;
            string resCode = "U999";
          
            stubDataAccessService.LoadSingleData<TbEvaluationForm, object>("agdSp.uspEvaluationFormGet", Arg.Any<int>())
                .ReturnsNull();          

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            EvaluationFormService EvaluationFormService = new EvaluationFormService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await EvaluationFormService.GetEvaluationForm(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<TbEvaluationForm, object>("agdSp.uspEvaluationFormGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryEvaluationFormTest()
        {  //arrange
            var queryReq = new EvaluationFormQueryRequest { 						evaluationFormID = "A",
						evaluationFormName = "評分表1",
						parentEvaluationFormID = "ROOT",
						level = 1,
						weight = 0,
						displayOrder = 0,
						memo = "評分表1的備註",
						isEnable = False,
						isOnline = False, };
            string resCode = "U200";
            
            stubDataAccessService.LoadData<TbEvaluationForm, object>
                (storeProcedure: "agdSp.uspEvaluationFormQuery", Arg.Any<object>())
               .Returns(new TbEvaluationForm[] { 
                    new TbEvaluationForm { 
						evaluationFormID = "A",
						evaluationFormName = "評分表1",
						parentEvaluationFormID = "ROOT",
						level = 1,
						weight = 0,
						displayOrder = 0,
						memo = "評分表1的備註",
						isEnable = False,
						isOnline = False,
                        Total = 2
                    },
                    new TbEvaluationForm { } 
                }.AsQueryable());
            
            stubHttpContextAccessor.HttpContext.Request.Path ="/api/EvaluationForm/query";
            
            EvaluationFormService EvaluationFormService = new 
                EvaluationFormService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await EvaluationFormService.QueryEvaluationForm(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
          
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryEvaluationFormTestFailed()
        {  //arrange
            var queryReq = new EvaluationFormQueryRequest {  
						evaluationFormID = "A",
						evaluationFormName = "評分表1",
						parentEvaluationFormID = "ROOT",
						level = 1,
						weight = 0,
						displayOrder = 0,
						memo = "評分表1的備註",
						isEnable = False,
						isOnline = False,
            };
            string resCode = "U999";
            
            stubDataAccessService.LoadData<TbEvaluationForm, object>("agdSp.uspEvaluationFormQuery", Arg.Any<object>())
                .ReturnsNull();
            
            stubHttpContextAccessor.HttpContext.Request.Path = "/api/EvaluationForm/query";
            EvaluationFormService EvaluationFormService = new EvaluationFormService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await EvaluationFormService.QueryEvaluationForm(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspEvaluationFormGet", Arg.Any<object>());
            Assert.AreEqual(result.total, 0);

        }

        [Test]
        public async ValueTask InsertEvaluationFormTest()
        {  //arrange
            var insertReq = new EvaluationFormInsertRequest
            {
				evaluationFormID = "A",
				evaluationFormName = "評分表1",
				parentEvaluationFormID = "ROOT",
				level = 1,
				weight = 0,
				displayOrder = 0,
				memo = "評分表1的備註",
				isEnable = False,
				isOnline = False,
				creator = "10046",
				creatorName = "陳麗杰鰻魚飯",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspEvaluationFormInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result:new SqlResponse() { ErrorMsg="",Msg="" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            EvaluationFormService EvaluationFormService = new
                EvaluationFormService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await EvaluationFormService.InsertEvaluationForm(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask InsertEvaluationFormTestFailed()
        {  //arrange
            var insertReq = new EvaluationFormInsertRequest
            {
				evaluationFormID = "A",
				evaluationFormName = "評分表1",
				parentEvaluationFormID = "ROOT",
				level = 1,
				weight = 0,
				displayOrder = 0,
				memo = "評分表1的備註",
				isEnable = False,
				isOnline = False,
				creator = "10046",
				creatorName = "陳麗杰鰻魚飯",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspEvaluationFormInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            EvaluationFormService EvaluationFormService = new
                EvaluationFormService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await EvaluationFormService.InsertEvaluationForm(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }


        [Test]
        public async ValueTask UpdateEvaluationFormTest()
        {  //arrange
            var updateReq = new EvaluationFormUpdateRequest
            {
				evaluationFormID = "r33",
				evaluationFormName = "r33題目",
				parentEvaluationFormID = "r3",
				level = 3,
				weight = 2,
				displayOrder = 2,
				memo = "",
				isEnable = None,
				isOnline = None,
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspEvaluationFormUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            EvaluationFormService EvaluationFormService = new
                EvaluationFormService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await EvaluationFormService.UpdateEvaluationForm(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask UpdateEvaluationFormTestFailed()
        {  //arrange
            var updateReq = new EvaluationFormUpdateRequest
            {
				evaluationFormID = "r33",
				evaluationFormName = "r33題目",
				parentEvaluationFormID = "r3",
				level = 3,
				weight = 2,
				displayOrder = 2,
				memo = "",
				isEnable = None,
				isOnline = None,
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspEvaluationFormUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            EvaluationFormService EvaluationFormService = new
                EvaluationFormService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await EvaluationFormService.UpdateEvaluationForm(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }




    }
}
