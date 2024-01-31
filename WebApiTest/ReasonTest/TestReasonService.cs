using AutoMapper;
using ESUN.AGD.DataAccess.DataService.DataAccess;
using ESUN.AGD.DataAccess.Type;
using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.Common;
using ESUN.AGD.WebApi.Application.Reason;
using ESUN.AGD.WebApi.Application.Reason.Contract;
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


namespace ESUN.AGD.WebApi.Test.ReasonTest
{
    [TestFixture]
    public class TestReasonService
    {
        private static IDataAccessService stubDataAccessService = Substitute.For<IDataAccessService>();
        private static IGetTokenService stubGetTokenService = Substitute.For<IGetTokenService>();
        private static IHttpContextAccessor stubHttpContextAccessor = Substitute.For<IHttpContextAccessor>();
        private static IMapper stubMapper = Substitute.For<IMapper>();
        private static IUserService stubUserService = Substitute.For<IUserService>();
        private static IAutoNextNumService autoNextNumService = Substitute.For<IAutoNextNumService>();

        [Test]
        public async ValueTask GetReasonTest()
        {  //arrange
            int seqNo = 1;
            string resCode = "U200";
           
            stubDataAccessService
                .LoadSingleData<TbReason, object>("agdSp.uspReasonGet", Arg.Any<int>())
                .ReturnsForAnyArgs(new TbReason { SeqNo = 1 });

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            ReasonService ReasonService = 
                new ReasonService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ReasonService.GetReason(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspReasonGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode,resCode);

        }

        [Test]
        public async ValueTask GetReasonTestFailed()
        {  //arrange
            int seqNo = 0;
            string resCode = "U999";
          
            stubDataAccessService.LoadSingleData<TbReason, object>("agdSp.uspReasonGet", Arg.Any<int>())
                .ReturnsNull();          

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            ReasonService ReasonService = new ReasonService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ReasonService.GetReason(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<TbReason, object>("agdSp.uspReasonGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryReasonTest()
        {  //arrange
            var queryReq = new ReasonQueryRequest { 						reasonID = "8",
						reasonName = "t",
						parentReasonID = "ROOT",
						level = 1,
						businessRISEType = "None",
						reviewType = "None",
						isFrequently = False,
						isEnable = False, };
            string resCode = "U200";
            
            stubDataAccessService.LoadData<TbReason, object>
                (storeProcedure: "agdSp.uspReasonQuery", Arg.Any<object>())
               .Returns(new TbReason[] { 
                    new TbReason { 
						reasonID = "8",
						reasonName = "t",
						parentReasonID = "ROOT",
						level = 1,
						businessRISEType = "None",
						reviewType = "None",
						isFrequently = False,
						isEnable = False,
                        Total = 2
                    },
                    new TbReason { } 
                }.AsQueryable());
            
            stubHttpContextAccessor.HttpContext.Request.Path ="/api/Reason/query";
            
            ReasonService ReasonService = new 
                ReasonService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ReasonService.QueryReason(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
          
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryReasonTestFailed()
        {  //arrange
            var queryReq = new ReasonQueryRequest {  
						reasonID = "8",
						reasonName = "t",
						parentReasonID = "ROOT",
						level = 1,
						businessRISEType = "None",
						reviewType = "None",
						isFrequently = False,
						isEnable = False,
            };
            string resCode = "U999";
            
            stubDataAccessService.LoadData<TbReason, object>("agdSp.uspReasonQuery", Arg.Any<object>())
                .ReturnsNull();
            
            stubHttpContextAccessor.HttpContext.Request.Path = "/api/Reason/query";
            ReasonService ReasonService = new ReasonService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ReasonService.QueryReason(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspReasonGet", Arg.Any<object>());
            Assert.AreEqual(result.total, 0);

        }

        [Test]
        public async ValueTask InsertReasonTest()
        {  //arrange
            var insertReq = new ReasonInsertRequest
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
				displayOrder = 0,
				isFrequently = False,
				frequentlyReasonName = "",
				frequentlyDisplayOrder = 0,
				isEnable = False,
				creator = "admin",
				creatorName = "林管理",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspReasonInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result:new SqlResponse() { ErrorMsg="",Msg="" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            ReasonService ReasonService = new
                ReasonService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ReasonService.InsertReason(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask InsertReasonTestFailed()
        {  //arrange
            var insertReq = new ReasonInsertRequest
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
				displayOrder = 0,
				isFrequently = False,
				frequentlyReasonName = "",
				frequentlyDisplayOrder = 0,
				isEnable = False,
				creator = "admin",
				creatorName = "林管理",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspReasonInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            ReasonService ReasonService = new
                ReasonService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ReasonService.InsertReason(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }


        [Test]
        public async ValueTask UpdateReasonTest()
        {  //arrange
            var updateReq = new ReasonUpdateRequest
            {
				reasonID = "Y0603",
				reasonName = "疑似非本人冒用",
				parentReasonID = "Y06",
				level = 3,
				businessRISEType = "1",
				reviewType = "1",
				memo = "None",
				webUrl = "",
				kMUrl = "",
				displayOrder = 1,
				isFrequently = False,
				frequentlyReasonName = "",
				frequentlyDisplayOrder = 0,
				isEnable = True,
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspReasonUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            ReasonService ReasonService = new
                ReasonService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ReasonService.UpdateReason(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask UpdateReasonTestFailed()
        {  //arrange
            var updateReq = new ReasonUpdateRequest
            {
				reasonID = "Y0603",
				reasonName = "疑似非本人冒用",
				parentReasonID = "Y06",
				level = 3,
				businessRISEType = "1",
				reviewType = "1",
				memo = "None",
				webUrl = "",
				kMUrl = "",
				displayOrder = 1,
				isFrequently = False,
				frequentlyReasonName = "",
				frequentlyDisplayOrder = 0,
				isEnable = True,
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspReasonUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            ReasonService ReasonService = new
                ReasonService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ReasonService.UpdateReason(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }




    }
}
