using AutoMapper;
using ESUN.AGD.DataAccess.DataService.DataAccess;
using ESUN.AGD.DataAccess.Type;
using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.Common;
using ESUN.AGD.WebApi.Application.RoleHist;
using ESUN.AGD.WebApi.Application.RoleHist.Contract;
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


namespace ESUN.AGD.WebApi.Test.RoleHistTest
{
    [TestFixture]
    public class TestRoleHistService
    {
        private static IDataAccessService stubDataAccessService = Substitute.For<IDataAccessService>();
        private static IGetTokenService stubGetTokenService = Substitute.For<IGetTokenService>();
        private static IHttpContextAccessor stubHttpContextAccessor = Substitute.For<IHttpContextAccessor>();
        private static IMapper stubMapper = Substitute.For<IMapper>();
        private static IUserService stubUserService = Substitute.For<IUserService>();
        private static IAutoNextNumService autoNextNumService = Substitute.For<IAutoNextNumService>();

        [Test]
        public async ValueTask GetRoleHistTest()
        {  //arrange
            int seqNo = 1;
            string resCode = "U200";
           
            stubDataAccessService
                .LoadSingleData<TbRoleHist, object>("agdSp.uspRoleHistGet", Arg.Any<int>())
                .ReturnsForAnyArgs(new TbRoleHist { SeqNo = 1 });

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            RoleHistService RoleHistService = 
                new RoleHistService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await RoleHistService.GetRoleHist(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspRoleHistGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode,resCode);

        }

        [Test]
        public async ValueTask GetRoleHistTestFailed()
        {  //arrange
            int seqNo = 0;
            string resCode = "U999";
          
            stubDataAccessService.LoadSingleData<TbRoleHist, object>("agdSp.uspRoleHistGet", Arg.Any<int>())
                .ReturnsNull();          

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            RoleHistService RoleHistService = new RoleHistService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await RoleHistService.GetRoleHist(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<TbRoleHist, object>("agdSp.uspRoleHistGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryRoleHistTest()
        {  //arrange
            var queryReq = new RoleHistQueryRequest { 						roleID = "666",
						actionDT = "2022-10-25 16:11:55",
						actor = "admin", };
            string resCode = "U200";
            
            stubDataAccessService.LoadData<TbRoleHist, object>
                (storeProcedure: "agdSp.uspRoleHistQuery", Arg.Any<object>())
               .Returns(new TbRoleHist[] { 
                    new TbRoleHist { 
						roleID = "666",
						actionDT = "2022-10-25 16:11:55",
						actor = "admin",
                        Total = 2
                    },
                    new TbRoleHist { } 
                }.AsQueryable());
            
            stubHttpContextAccessor.HttpContext.Request.Path ="/api/RoleHist/query";
            
            RoleHistService RoleHistService = new 
                RoleHistService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await RoleHistService.QueryRoleHist(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
          
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryRoleHistTestFailed()
        {  //arrange
            var queryReq = new RoleHistQueryRequest {  
						roleID = "666",
						actionDT = "2022-10-25 16:11:55",
						actor = "admin",
            };
            string resCode = "U999";
            
            stubDataAccessService.LoadData<TbRoleHist, object>("agdSp.uspRoleHistQuery", Arg.Any<object>())
                .ReturnsNull();
            
            stubHttpContextAccessor.HttpContext.Request.Path = "/api/RoleHist/query";
            RoleHistService RoleHistService = new RoleHistService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await RoleHistService.QueryRoleHist(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspRoleHistGet", Arg.Any<object>());
            Assert.AreEqual(result.total, 0);

        }

        [Test]
        public async ValueTask InsertRoleHistTest()
        {  //arrange
            var insertReq = new RoleHistInsertRequest
            {
				roleID = "666",
				roleName = "六六六角色",
				creator = "admin",
				creatorName = "林管理",
				actionType = "I",
				actionDT = "2022-10-25 16:11:55",
				actor = "admin",
				actorName = "林管理",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspRoleHistInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result:new SqlResponse() { ErrorMsg="",Msg="" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            RoleHistService RoleHistService = new
                RoleHistService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await RoleHistService.InsertRoleHist(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask InsertRoleHistTestFailed()
        {  //arrange
            var insertReq = new RoleHistInsertRequest
            {
				roleID = "666",
				roleName = "六六六角色",
				creator = "admin",
				creatorName = "林管理",
				actionType = "I",
				actionDT = "2022-10-25 16:11:55",
				actor = "admin",
				actorName = "林管理",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspRoleHistInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            RoleHistService RoleHistService = new
                RoleHistService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await RoleHistService.InsertRoleHist(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }


        [Test]
        public async ValueTask UpdateRoleHistTest()
        {  //arrange
            var updateReq = new RoleHistUpdateRequest
            {
				roleID = "R888",
				roleName = "發發發角色",
				actionType = "I",
				actionDT = "2022-10-25 16:11:37",
				actor = "admin",
				actorName = "林管理",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspRoleHistUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            RoleHistService RoleHistService = new
                RoleHistService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await RoleHistService.UpdateRoleHist(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask UpdateRoleHistTestFailed()
        {  //arrange
            var updateReq = new RoleHistUpdateRequest
            {
				roleID = "R888",
				roleName = "發發發角色",
				actionType = "I",
				actionDT = "2022-10-25 16:11:37",
				actor = "admin",
				actorName = "林管理",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspRoleHistUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            RoleHistService RoleHistService = new
                RoleHistService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await RoleHistService.UpdateRoleHist(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }




    }
}
