using AutoMapper;
using ESUN.AGD.DataAccess.DataService.DataAccess;
using ESUN.AGD.DataAccess.Type;
using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.Common;
using ESUN.AGD.WebApi.Application.Func;
using ESUN.AGD.WebApi.Application.Func.Contract;
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


namespace ESUN.AGD.WebApi.Test.FuncTest
{
    [TestFixture]
    public class TestFuncService
    {
        private static IDataAccessService stubDataAccessService = Substitute.For<IDataAccessService>();
        private static IGetTokenService stubGetTokenService = Substitute.For<IGetTokenService>();
        private static IHttpContextAccessor stubHttpContextAccessor = Substitute.For<IHttpContextAccessor>();
        private static IMapper stubMapper = Substitute.For<IMapper>();
        private static IUserService stubUserService = Substitute.For<IUserService>();
        private static IAutoNextNumService autoNextNumService = Substitute.For<IAutoNextNumService>();

        [Test]
        public async ValueTask GetFuncTest()
        {  //arrange
            int seqNo = 1;
            string resCode = "U200";
           
            stubDataAccessService
                .LoadSingleData<TbFunc, object>("agdSp.uspFuncGet", Arg.Any<int>())
                .ReturnsForAnyArgs(new TbFunc { SeqNo = 1 });

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            FuncService FuncService = 
                new FuncService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await FuncService.GetFunc(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspFuncGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode,resCode);

        }

        [Test]
        public async ValueTask GetFuncTestFailed()
        {  //arrange
            int seqNo = 0;
            string resCode = "U999";
          
            stubDataAccessService.LoadSingleData<TbFunc, object>("agdSp.uspFuncGet", Arg.Any<int>())
                .ReturnsNull();          

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            FuncService FuncService = new FuncService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await FuncService.GetFunc(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<TbFunc, object>("agdSp.uspFuncGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryFuncTest()
        {  //arrange
            var queryReq = new FuncQueryRequest { 						funcID = "A0100",
						funcName = "AGD功能",
						parentFuncID = "ROOT",
						level = 1,
						systemType = "agd", };
            string resCode = "U200";
            
            stubDataAccessService.LoadData<TbFunc, object>
                (storeProcedure: "agdSp.uspFuncQuery", Arg.Any<object>())
               .Returns(new TbFunc[] { 
                    new TbFunc { 
						funcID = "A0100",
						funcName = "AGD功能",
						parentFuncID = "ROOT",
						level = 1,
						systemType = "agd",
                        Total = 2
                    },
                    new TbFunc { } 
                }.AsQueryable());
            
            stubHttpContextAccessor.HttpContext.Request.Path ="/api/Func/query";
            
            FuncService FuncService = new 
                FuncService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await FuncService.QueryFunc(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
          
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryFuncTestFailed()
        {  //arrange
            var queryReq = new FuncQueryRequest {  
						funcID = "A0100",
						funcName = "AGD功能",
						parentFuncID = "ROOT",
						level = 1,
						systemType = "agd",
            };
            string resCode = "U999";
            
            stubDataAccessService.LoadData<TbFunc, object>("agdSp.uspFuncQuery", Arg.Any<object>())
                .ReturnsNull();
            
            stubHttpContextAccessor.HttpContext.Request.Path = "/api/Func/query";
            FuncService FuncService = new FuncService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await FuncService.QueryFunc(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspFuncGet", Arg.Any<object>());
            Assert.AreEqual(result.total, 0);

        }

        [Test]
        public async ValueTask InsertFuncTest()
        {  //arrange
            var insertReq = new FuncInsertRequest
            {
				funcID = "A0100",
				funcName = "AGD功能",
				parentFuncID = "ROOT",
				level = 1,
				systemType = "agd",
				iconName = "o_account_circle",
				routeName = "",
				displayOrder = 1,
				creator = "sys",
				creatorName = "系統",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspFuncInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result:new SqlResponse() { ErrorMsg="",Msg="" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            FuncService FuncService = new
                FuncService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await FuncService.InsertFunc(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask InsertFuncTestFailed()
        {  //arrange
            var insertReq = new FuncInsertRequest
            {
				funcID = "A0100",
				funcName = "AGD功能",
				parentFuncID = "ROOT",
				level = 1,
				systemType = "agd",
				iconName = "o_account_circle",
				routeName = "",
				displayOrder = 1,
				creator = "sys",
				creatorName = "系統",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspFuncInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            FuncService FuncService = new
                FuncService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await FuncService.InsertFunc(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }


        [Test]
        public async ValueTask UpdateFuncTest()
        {  //arrange
            var updateReq = new FuncUpdateRequest
            {
				funcID = "S1204",
				funcName = "系統維護",
				parentFuncID = "S1200",
				level = 2,
				systemType = "supervisor",
				iconName = "None",
				routeName = "SysIndex",
				displayOrder = 4,
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspFuncUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            FuncService FuncService = new
                FuncService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await FuncService.UpdateFunc(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask UpdateFuncTestFailed()
        {  //arrange
            var updateReq = new FuncUpdateRequest
            {
				funcID = "S1204",
				funcName = "系統維護",
				parentFuncID = "S1200",
				level = 2,
				systemType = "supervisor",
				iconName = "None",
				routeName = "SysIndex",
				displayOrder = 4,
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspFuncUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            FuncService FuncService = new
                FuncService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await FuncService.UpdateFunc(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }




    }
}
