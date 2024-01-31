using AutoMapper;
using ESUN.AGD.DataAccess.DataService.DataAccess;
using ESUN.AGD.DataAccess.Type;
using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.Common;
using ESUN.AGD.WebApi.Application.ReasonHist;
using ESUN.AGD.WebApi.Application.ReasonHist.Contract;
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


namespace ESUN.AGD.WebApi.Test.ReasonHistTest
{
    [TestFixture]
    public class TestReasonHistService
    {
        private static IDataAccessService stubDataAccessService = Substitute.For<IDataAccessService>();
        private static IGetTokenService stubGetTokenService = Substitute.For<IGetTokenService>();
        private static IHttpContextAccessor stubHttpContextAccessor = Substitute.For<IHttpContextAccessor>();
        private static IMapper stubMapper = Substitute.For<IMapper>();
        private static IUserService stubUserService = Substitute.For<IUserService>();
        private static IAutoNextNumService autoNextNumService = Substitute.For<IAutoNextNumService>();

        [Test]
        public async ValueTask GetReasonHistTest()
        {  //arrange
            int seqNo = 1;
            string resCode = "U200";
           
            stubDataAccessService
                .LoadSingleData<TbReasonHist, object>("agdSp.uspReasonHistGet", Arg.Any<int>())
                .ReturnsForAnyArgs(new TbReasonHist { SeqNo = 1 });

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            ReasonHistService ReasonHistService = 
                new ReasonHistService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ReasonHistService.GetReasonHist(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspReasonHistGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode,resCode);

        }

        [Test]
        public async ValueTask GetReasonHistTestFailed()
        {  //arrange
            int seqNo = 0;
            string resCode = "U999";
          
            stubDataAccessService.LoadSingleData<TbReasonHist, object>("agdSp.uspReasonHistGet", Arg.Any<int>())
                .ReturnsNull();          

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            ReasonHistService ReasonHistService = new ReasonHistService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ReasonHistService.GetReasonHist(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<TbReasonHist, object>("agdSp.uspReasonHistGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryReasonHistTest()
        {  //arrange
            var queryReq = new ReasonHistQueryRequest { 						parentReasonID = "ROOT",
						actionDT = "2022-10-25 15:49:46",
						actor = "admin", };
            string resCode = "U200";
            
            stubDataAccessService.LoadData<TbReasonHist, object>
                (storeProcedure: "agdSp.uspReasonHistQuery", Arg.Any<object>())
               .Returns(new TbReasonHist[] { 
                    new TbReasonHist { 
						parentReasonID = "ROOT",
						actionDT = "2022-10-25 15:49:46",
						actor = "admin",
                        Total = 2
                    },
                    new TbReasonHist { } 
                }.AsQueryable());
            
            stubHttpContextAccessor.HttpContext.Request.Path ="/api/ReasonHist/query";
            
            ReasonHistService ReasonHistService = new 
                ReasonHistService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ReasonHistService.QueryReasonHist(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
          
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryReasonHistTestFailed()
        {  //arrange
            var queryReq = new ReasonHistQueryRequest {  
						parentReasonID = "ROOT",
						actionDT = "2022-10-25 15:49:46",
						actor = "admin",
            };
            string resCode = "U999";
            
            stubDataAccessService.LoadData<TbReasonHist, object>("agdSp.uspReasonHistQuery", Arg.Any<object>())
                .ReturnsNull();
            
            stubHttpContextAccessor.HttpContext.Request.Path = "/api/ReasonHist/query";
            ReasonHistService ReasonHistService = new ReasonHistService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ReasonHistService.QueryReasonHist(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspReasonHistGet", Arg.Any<object>());
            Assert.AreEqual(result.total, 0);

        }

        [Test]
        public async ValueTask InsertReasonHistTest()
        {  //arrange
            var insertReq = new ReasonHistInsertRequest
            {
				reasonID = "8",
				reasonName = "t",
				parentReasonID = "ROOT",
				level = 1,
				businessRISEType = "None",
				reviewType = "None",
				memo = "None",
				webUrl = "None",
				kMUrl = "None",
				displayOrder = 200,
				isFrequently = False,
				frequentlyReasonName = "",
				frequentlyDisplayOrder = 0,
				isEnable = True,
				creator = "admin",
				creatorName = "林管理",
				actionType = "I",
				actionDT = "2022-10-25 15:49:46",
				actor = "admin",
				actorName = "林管理",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspReasonHistInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result:new SqlResponse() { ErrorMsg="",Msg="" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            ReasonHistService ReasonHistService = new
                ReasonHistService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ReasonHistService.InsertReasonHist(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask InsertReasonHistTestFailed()
        {  //arrange
            var insertReq = new ReasonHistInsertRequest
            {
				reasonID = "8",
				reasonName = "t",
				parentReasonID = "ROOT",
				level = 1,
				businessRISEType = "None",
				reviewType = "None",
				memo = "None",
				webUrl = "None",
				kMUrl = "None",
				displayOrder = 200,
				isFrequently = False,
				frequentlyReasonName = "",
				frequentlyDisplayOrder = 0,
				isEnable = True,
				creator = "admin",
				creatorName = "林管理",
				actionType = "I",
				actionDT = "2022-10-25 15:49:46",
				actor = "admin",
				actorName = "林管理",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspReasonHistInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            ReasonHistService ReasonHistService = new
                ReasonHistService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ReasonHistService.InsertReasonHist(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }


        [Test]
        public async ValueTask UpdateReasonHistTest()
        {  //arrange
            var updateReq = new ReasonHistUpdateRequest
            {
				reasonID = "Y0305",
				reasonName = "國外地區無法提款",
				parentReasonID = "Y03",
				level = 3,
				businessRISEType = "1",
				reviewType = "1",
				memo = "",
				webUrl = "",
				kMUrl = "",
				displayOrder = 5,
				isFrequently = False,
				frequentlyReasonName = "",
				frequentlyDisplayOrder = 0,
				isEnable = False,
				actionType = "U",
				actionDT = "2022-08-16 15:28:22",
				actor = "admin",
				actorName = "林管理",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspReasonHistUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            ReasonHistService ReasonHistService = new
                ReasonHistService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ReasonHistService.UpdateReasonHist(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask UpdateReasonHistTestFailed()
        {  //arrange
            var updateReq = new ReasonHistUpdateRequest
            {
				reasonID = "Y0305",
				reasonName = "國外地區無法提款",
				parentReasonID = "Y03",
				level = 3,
				businessRISEType = "1",
				reviewType = "1",
				memo = "",
				webUrl = "",
				kMUrl = "",
				displayOrder = 5,
				isFrequently = False,
				frequentlyReasonName = "",
				frequentlyDisplayOrder = 0,
				isEnable = False,
				actionType = "U",
				actionDT = "2022-08-16 15:28:22",
				actor = "admin",
				actorName = "林管理",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspReasonHistUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            ReasonHistService ReasonHistService = new
                ReasonHistService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ReasonHistService.UpdateReasonHist(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }




    }
}
