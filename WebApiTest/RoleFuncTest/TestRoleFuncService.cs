using AutoMapper;
using ESUN.AGD.DataAccess.DataService.DataAccess;
using ESUN.AGD.DataAccess.Type;
using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.Common;
using ESUN.AGD.WebApi.Application.RoleFunc;
using ESUN.AGD.WebApi.Application.RoleFunc.Contract;
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


namespace ESUN.AGD.WebApi.Test.RoleFuncTest
{
    [TestFixture]
    public class TestRoleFuncService
    {
        private static IDataAccessService stubDataAccessService = Substitute.For<IDataAccessService>();
        private static IGetTokenService stubGetTokenService = Substitute.For<IGetTokenService>();
        private static IHttpContextAccessor stubHttpContextAccessor = Substitute.For<IHttpContextAccessor>();
        private static IMapper stubMapper = Substitute.For<IMapper>();
        private static IUserService stubUserService = Substitute.For<IUserService>();
        private static IAutoNextNumService autoNextNumService = Substitute.For<IAutoNextNumService>();

        [Test]
        public async ValueTask GetRoleFuncTest()
        {  //arrange
            int seqNo = 1;
            string resCode = "U200";
           
            stubDataAccessService
                .LoadSingleData<TbRoleFunc, object>("agdSp.uspRoleFuncGet", Arg.Any<int>())
                .ReturnsForAnyArgs(new TbRoleFunc { SeqNo = 1 });

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            RoleFuncService RoleFuncService = 
                new RoleFuncService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await RoleFuncService.GetRoleFunc(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspRoleFuncGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode,resCode);

        }

        [Test]
        public async ValueTask GetRoleFuncTestFailed()
        {  //arrange
            int seqNo = 0;
            string resCode = "U999";
          
            stubDataAccessService.LoadSingleData<TbRoleFunc, object>("agdSp.uspRoleFuncGet", Arg.Any<int>())
                .ReturnsNull();          

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            RoleFuncService RoleFuncService = new RoleFuncService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await RoleFuncService.GetRoleFunc(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<TbRoleFunc, object>("agdSp.uspRoleFuncGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryRoleFuncTest()
        {  //arrange
            var queryReq = new RoleFuncQueryRequest { 						funcID = "M0100", };
            string resCode = "U200";
            
            stubDataAccessService.LoadData<TbRoleFunc, object>
                (storeProcedure: "agdSp.uspRoleFuncQuery", Arg.Any<object>())
               .Returns(new TbRoleFunc[] { 
                    new TbRoleFunc { 
						funcID = "M0100",
                        Total = 2
                    },
                    new TbRoleFunc { } 
                }.AsQueryable());
            
            stubHttpContextAccessor.HttpContext.Request.Path ="/api/RoleFunc/query";
            
            RoleFuncService RoleFuncService = new 
                RoleFuncService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await RoleFuncService.QueryRoleFunc(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
          
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryRoleFuncTestFailed()
        {  //arrange
            var queryReq = new RoleFuncQueryRequest {  
						funcID = "M0100",
            };
            string resCode = "U999";
            
            stubDataAccessService.LoadData<TbRoleFunc, object>("agdSp.uspRoleFuncQuery", Arg.Any<object>())
                .ReturnsNull();
            
            stubHttpContextAccessor.HttpContext.Request.Path = "/api/RoleFunc/query";
            RoleFuncService RoleFuncService = new RoleFuncService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await RoleFuncService.QueryRoleFunc(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspRoleFuncGet", Arg.Any<object>());
            Assert.AreEqual(result.total, 0);

        }

        [Test]
        public async ValueTask InsertRoleFuncTest()
        {  //arrange
            var insertReq = new RoleFuncInsertRequest
            {
				roleID = "R000",
				funcID = "M0100",
				creator = "sys",
				creatorName = "系統",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspRoleFuncInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result:new SqlResponse() { ErrorMsg="",Msg="" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            RoleFuncService RoleFuncService = new
                RoleFuncService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await RoleFuncService.InsertRoleFunc(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask InsertRoleFuncTestFailed()
        {  //arrange
            var insertReq = new RoleFuncInsertRequest
            {
				roleID = "R000",
				funcID = "M0100",
				creator = "sys",
				creatorName = "系統",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspRoleFuncInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            RoleFuncService RoleFuncService = new
                RoleFuncService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await RoleFuncService.InsertRoleFunc(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }


        [Test]
        public async ValueTask UpdateRoleFuncTest()
        {  //arrange
            var updateReq = new RoleFuncUpdateRequest
            {
				roleID = "R009",
				funcID = "R0101",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspRoleFuncUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            RoleFuncService RoleFuncService = new
                RoleFuncService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await RoleFuncService.UpdateRoleFunc(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask UpdateRoleFuncTestFailed()
        {  //arrange
            var updateReq = new RoleFuncUpdateRequest
            {
				roleID = "R009",
				funcID = "R0101",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspRoleFuncUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            RoleFuncService RoleFuncService = new
                RoleFuncService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await RoleFuncService.UpdateRoleFunc(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }




    }
}
