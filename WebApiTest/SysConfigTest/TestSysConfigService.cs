using AutoMapper;
using ESUN.AGD.DataAccess.DataService.DataAccess;
using ESUN.AGD.DataAccess.Type;
using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.Common;
using ESUN.AGD.WebApi.Application.SysConfig;
using ESUN.AGD.WebApi.Application.SysConfig.Contract;
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


namespace ESUN.AGD.WebApi.Test.SysConfigTest
{
    [TestFixture]
    public class TestSysConfigService
    {
        private static IDataAccessService stubDataAccessService = Substitute.For<IDataAccessService>();
        private static IGetTokenService stubGetTokenService = Substitute.For<IGetTokenService>();
        private static IHttpContextAccessor stubHttpContextAccessor = Substitute.For<IHttpContextAccessor>();
        private static IMapper stubMapper = Substitute.For<IMapper>();
        private static IUserService stubUserService = Substitute.For<IUserService>();
        private static IAutoNextNumService autoNextNumService = Substitute.For<IAutoNextNumService>();

        [Test]
        public async ValueTask GetSysConfigTest()
        {  //arrange
            int seqNo = 1;
            string resCode = "U200";
           
            stubDataAccessService
                .LoadSingleData<TbSysConfig, object>("agdSp.uspSysConfigGet", Arg.Any<int>())
                .ReturnsForAnyArgs(new TbSysConfig { SeqNo = 1 });

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            SysConfigService SysConfigService = 
                new SysConfigService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await SysConfigService.GetSysConfig(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspSysConfigGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode,resCode);

        }

        [Test]
        public async ValueTask GetSysConfigTestFailed()
        {  //arrange
            int seqNo = 0;
            string resCode = "U999";
          
            stubDataAccessService.LoadSingleData<TbSysConfig, object>("agdSp.uspSysConfigGet", Arg.Any<int>())
                .ReturnsNull();          

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            SysConfigService SysConfigService = new SysConfigService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await SysConfigService.GetSysConfig(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<TbSysConfig, object>("agdSp.uspSysConfigGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QuerySysConfigTest()
        {  //arrange
            var queryReq = new SysConfigQueryRequest { 						sysConfigName = "文書-警示值", };
            string resCode = "U200";
            
            stubDataAccessService.LoadData<TbSysConfig, object>
                (storeProcedure: "agdSp.uspSysConfigQuery", Arg.Any<object>())
               .Returns(new TbSysConfig[] { 
                    new TbSysConfig { 
						sysConfigName = "文書-警示值",
                        Total = 2
                    },
                    new TbSysConfig { } 
                }.AsQueryable());
            
            stubHttpContextAccessor.HttpContext.Request.Path ="/api/SysConfig/query";
            
            SysConfigService SysConfigService = new 
                SysConfigService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await SysConfigService.QuerySysConfig(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
          
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QuerySysConfigTestFailed()
        {  //arrange
            var queryReq = new SysConfigQueryRequest {  
						sysConfigName = "文書-警示值",
            };
            string resCode = "U999";
            
            stubDataAccessService.LoadData<TbSysConfig, object>("agdSp.uspSysConfigQuery", Arg.Any<object>())
                .ReturnsNull();
            
            stubHttpContextAccessor.HttpContext.Request.Path = "/api/SysConfig/query";
            SysConfigService SysConfigService = new SysConfigService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await SysConfigService.QuerySysConfig(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspSysConfigGet", Arg.Any<object>());
            Assert.AreEqual(result.total, 0);

        }

        [Test]
        public async ValueTask InsertSysConfigTest()
        {  //arrange
            var insertReq = new SysConfigInsertRequest
            {
				sysConfigType = "CtiStatus",
				sysConfigID = "ACWAlert",
				sysConfigName = "文書-警示值",
				dataType = "time",
				content = "00:01:25",
				isVisible = True,
				displayOrder = 1,
				creator = "sys",
				creatorName = "系統",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspSysConfigInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result:new SqlResponse() { ErrorMsg="",Msg="" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            SysConfigService SysConfigService = new
                SysConfigService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await SysConfigService.InsertSysConfig(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask InsertSysConfigTestFailed()
        {  //arrange
            var insertReq = new SysConfigInsertRequest
            {
				sysConfigType = "CtiStatus",
				sysConfigID = "ACWAlert",
				sysConfigName = "文書-警示值",
				dataType = "time",
				content = "00:01:25",
				isVisible = True,
				displayOrder = 1,
				creator = "sys",
				creatorName = "系統",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspSysConfigInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            SysConfigService SysConfigService = new
                SysConfigService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await SysConfigService.InsertSysConfig(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }


        [Test]
        public async ValueTask UpdateSysConfigTest()
        {  //arrange
            var updateReq = new SysConfigUpdateRequest
            {
				sysConfigType = "SysConfig",
				sysConfigID = "WordPadWordMax",
				sysConfigName = "記事本最大字數限制(字元)",
				dataType = "int",
				content = "2000",
				isVisible = True,
				displayOrder = 9,
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspSysConfigUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            SysConfigService SysConfigService = new
                SysConfigService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await SysConfigService.UpdateSysConfig(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask UpdateSysConfigTestFailed()
        {  //arrange
            var updateReq = new SysConfigUpdateRequest
            {
				sysConfigType = "SysConfig",
				sysConfigID = "WordPadWordMax",
				sysConfigName = "記事本最大字數限制(字元)",
				dataType = "int",
				content = "2000",
				isVisible = True,
				displayOrder = 9,
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspSysConfigUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            SysConfigService SysConfigService = new
                SysConfigService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await SysConfigService.UpdateSysConfig(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }




    }
}
