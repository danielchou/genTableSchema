using AutoMapper;
using ESUN.AGD.DataAccess.DataService.DataAccess;
using ESUN.AGD.DataAccess.Type;
using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.Common;
using ESUN.AGD.WebApi.Application.RoleFuncHist;
using ESUN.AGD.WebApi.Application.RoleFuncHist.Contract;
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


namespace ESUN.AGD.WebApi.Test.RoleFuncHistTest
{
    [TestFixture]
    public class TestRoleFuncHistService
    {
        private static IDataAccessService stubDataAccessService = Substitute.For<IDataAccessService>();
        private static IGetTokenService stubGetTokenService = Substitute.For<IGetTokenService>();
        private static IHttpContextAccessor stubHttpContextAccessor = Substitute.For<IHttpContextAccessor>();
        private static IMapper stubMapper = Substitute.For<IMapper>();
        private static IUserService stubUserService = Substitute.For<IUserService>();
        private static IAutoNextNumService autoNextNumService = Substitute.For<IAutoNextNumService>();

        [Test]
        public async ValueTask GetRoleFuncHistTest()
        {  //arrange
            int seqNo = 1;
            string resCode = "U200";
           
            stubDataAccessService
                .LoadSingleData<TbRoleFuncHist, object>("agdSp.uspRoleFuncHistGet", Arg.Any<int>())
                .ReturnsForAnyArgs(new TbRoleFuncHist { SeqNo = 1 });

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            RoleFuncHistService RoleFuncHistService = 
                new RoleFuncHistService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await RoleFuncHistService.GetRoleFuncHist(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspRoleFuncHistGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode,resCode);

        }

        [Test]
        public async ValueTask GetRoleFuncHistTestFailed()
        {  //arrange
            int seqNo = 0;
            string resCode = "U999";
          
            stubDataAccessService.LoadSingleData<TbRoleFuncHist, object>("agdSp.uspRoleFuncHistGet", Arg.Any<int>())
                .ReturnsNull();          

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            RoleFuncHistService RoleFuncHistService = new RoleFuncHistService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await RoleFuncHistService.GetRoleFuncHist(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<TbRoleFuncHist, object>("agdSp.uspRoleFuncHistGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryRoleFuncHistTest()
        {  //arrange
            var queryReq = new RoleFuncHistQueryRequest { 						roleID = "R001",
						funcID = "A0101",
						actionDT = "2022-06-14 13:00:58",
						actor = "admin", };
            string resCode = "U200";
            
            stubDataAccessService.LoadData<TbRoleFuncHist, object>
                (storeProcedure: "agdSp.uspRoleFuncHistQuery", Arg.Any<object>())
               .Returns(new TbRoleFuncHist[] { 
                    new TbRoleFuncHist { 
						roleID = "R001",
						funcID = "A0101",
						actionDT = "2022-06-14 13:00:58",
						actor = "admin",
                        Total = 2
                    },
                    new TbRoleFuncHist { } 
                }.AsQueryable());
            
            stubHttpContextAccessor.HttpContext.Request.Path ="/api/RoleFuncHist/query";
            
            RoleFuncHistService RoleFuncHistService = new 
                RoleFuncHistService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await RoleFuncHistService.QueryRoleFuncHist(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
          
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryRoleFuncHistTestFailed()
        {  //arrange
            var queryReq = new RoleFuncHistQueryRequest {  
						roleID = "R001",
						funcID = "A0101",
						actionDT = "2022-06-14 13:00:58",
						actor = "admin",
            };
            string resCode = "U999";
            
            stubDataAccessService.LoadData<TbRoleFuncHist, object>("agdSp.uspRoleFuncHistQuery", Arg.Any<object>())
                .ReturnsNull();
            
            stubHttpContextAccessor.HttpContext.Request.Path = "/api/RoleFuncHist/query";
            RoleFuncHistService RoleFuncHistService = new RoleFuncHistService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await RoleFuncHistService.QueryRoleFuncHist(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspRoleFuncHistGet", Arg.Any<object>());
            Assert.AreEqual(result.total, 0);

        }

        [Test]
        public async ValueTask InsertRoleFuncHistTest()
        {  //arrange
            var insertReq = new RoleFuncHistInsertRequest
            {
				roleID = "R001",
				funcID = "A0101",
				creator = "admin",
				creatorName = "林管理",
				actionType = "I",
				actionDT = "2022-06-14 13:00:58",
				actor = "admin",
				actorName = "林管理",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspRoleFuncHistInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result:new SqlResponse() { ErrorMsg="",Msg="" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            RoleFuncHistService RoleFuncHistService = new
                RoleFuncHistService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await RoleFuncHistService.InsertRoleFuncHist(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask InsertRoleFuncHistTestFailed()
        {  //arrange
            var insertReq = new RoleFuncHistInsertRequest
            {
				roleID = "R001",
				funcID = "A0101",
				creator = "admin",
				creatorName = "林管理",
				actionType = "I",
				actionDT = "2022-06-14 13:00:58",
				actor = "admin",
				actorName = "林管理",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspRoleFuncHistInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            RoleFuncHistService RoleFuncHistService = new
                RoleFuncHistService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await RoleFuncHistService.InsertRoleFuncHist(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }


        [Test]
        public async ValueTask UpdateRoleFuncHistTest()
        {  //arrange
            var updateReq = new RoleFuncHistUpdateRequest
            {
				roleID = "r11",
				funcID = "M0101",
				actionType = "D",
				actionDT = "2022-08-30 17:15:54",
				actor = "admin",
				actorName = "林管理",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspRoleFuncHistUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            RoleFuncHistService RoleFuncHistService = new
                RoleFuncHistService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await RoleFuncHistService.UpdateRoleFuncHist(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask UpdateRoleFuncHistTestFailed()
        {  //arrange
            var updateReq = new RoleFuncHistUpdateRequest
            {
				roleID = "r11",
				funcID = "M0101",
				actionType = "D",
				actionDT = "2022-08-30 17:15:54",
				actor = "admin",
				actorName = "林管理",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspRoleFuncHistUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            RoleFuncHistService RoleFuncHistService = new
                RoleFuncHistService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await RoleFuncHistService.UpdateRoleFuncHist(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }




    }
}
