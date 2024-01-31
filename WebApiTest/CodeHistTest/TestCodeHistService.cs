using AutoMapper;
using ESUN.AGD.DataAccess.DataService.DataAccess;
using ESUN.AGD.DataAccess.Type;
using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.Common;
using ESUN.AGD.WebApi.Application.CodeHist;
using ESUN.AGD.WebApi.Application.CodeHist.Contract;
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


namespace ESUN.AGD.WebApi.Test.CodeHistTest
{
    [TestFixture]
    public class TestCodeHistService
    {
        private static IDataAccessService stubDataAccessService = Substitute.For<IDataAccessService>();
        private static IGetTokenService stubGetTokenService = Substitute.For<IGetTokenService>();
        private static IHttpContextAccessor stubHttpContextAccessor = Substitute.For<IHttpContextAccessor>();
        private static IMapper stubMapper = Substitute.For<IMapper>();
        private static IUserService stubUserService = Substitute.For<IUserService>();
        private static IAutoNextNumService autoNextNumService = Substitute.For<IAutoNextNumService>();

        [Test]
        public async ValueTask GetCodeHistTest()
        {  //arrange
            int seqNo = 1;
            string resCode = "U200";
           
            stubDataAccessService
                .LoadSingleData<TbCodeHist, object>("agdSp.uspCodeHistGet", Arg.Any<int>())
                .ReturnsForAnyArgs(new TbCodeHist { SeqNo = 1 });

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            CodeHistService CodeHistService = 
                new CodeHistService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await CodeHistService.GetCodeHist(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspCodeHistGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode,resCode);

        }

        [Test]
        public async ValueTask GetCodeHistTestFailed()
        {  //arrange
            int seqNo = 0;
            string resCode = "U999";
          
            stubDataAccessService.LoadSingleData<TbCodeHist, object>("agdSp.uspCodeHistGet", Arg.Any<int>())
                .ReturnsNull();          

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            CodeHistService CodeHistService = new CodeHistService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await CodeHistService.GetCodeHist(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<TbCodeHist, object>("agdSp.uspCodeHistGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryCodeHistTest()
        {  //arrange
            var queryReq = new CodeHistQueryRequest { 						codeType = "EnterpriseEmail",
						codeID = "11111",
						actionDT = "2022-06-14 11:39:09",
						actor = "admin", };
            string resCode = "U200";
            
            stubDataAccessService.LoadData<TbCodeHist, object>
                (storeProcedure: "agdSp.uspCodeHistQuery", Arg.Any<object>())
               .Returns(new TbCodeHist[] { 
                    new TbCodeHist { 
						codeType = "EnterpriseEmail",
						codeID = "11111",
						actionDT = "2022-06-14 11:39:09",
						actor = "admin",
                        Total = 2
                    },
                    new TbCodeHist { } 
                }.AsQueryable());
            
            stubHttpContextAccessor.HttpContext.Request.Path ="/api/CodeHist/query";
            
            CodeHistService CodeHistService = new 
                CodeHistService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await CodeHistService.QueryCodeHist(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
          
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryCodeHistTestFailed()
        {  //arrange
            var queryReq = new CodeHistQueryRequest {  
						codeType = "EnterpriseEmail",
						codeID = "11111",
						actionDT = "2022-06-14 11:39:09",
						actor = "admin",
            };
            string resCode = "U999";
            
            stubDataAccessService.LoadData<TbCodeHist, object>("agdSp.uspCodeHistQuery", Arg.Any<object>())
                .ReturnsNull();
            
            stubHttpContextAccessor.HttpContext.Request.Path = "/api/CodeHist/query";
            CodeHistService CodeHistService = new CodeHistService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await CodeHistService.QueryCodeHist(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspCodeHistGet", Arg.Any<object>());
            Assert.AreEqual(result.total, 0);

        }

        [Test]
        public async ValueTask InsertCodeHistTest()
        {  //arrange
            var insertReq = new CodeHistInsertRequest
            {
				codeType = "EnterpriseEmail",
				codeID = "11111",
				codeName = "1111-1@yah.com.tw",
				content = "",
				memo = "11111公司",
				displayOrder = 0,
				isEnable = True,
				creator = "admin",
				creatorName = "林管理",
				actionType = "I",
				actionDT = "2022-06-14 11:39:09",
				actor = "admin",
				actorName = "林管理",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspCodeHistInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result:new SqlResponse() { ErrorMsg="",Msg="" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            CodeHistService CodeHistService = new
                CodeHistService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await CodeHistService.InsertCodeHist(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask InsertCodeHistTestFailed()
        {  //arrange
            var insertReq = new CodeHistInsertRequest
            {
				codeType = "EnterpriseEmail",
				codeID = "11111",
				codeName = "1111-1@yah.com.tw",
				content = "",
				memo = "11111公司",
				displayOrder = 0,
				isEnable = True,
				creator = "admin",
				creatorName = "林管理",
				actionType = "I",
				actionDT = "2022-06-14 11:39:09",
				actor = "admin",
				actorName = "林管理",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspCodeHistInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            CodeHistService CodeHistService = new
                CodeHistService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await CodeHistService.InsertCodeHist(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }


        [Test]
        public async ValueTask UpdateCodeHistTest()
        {  //arrange
            var updateReq = new CodeHistUpdateRequest
            {
				codeType = "EnterpriseEmail",
				codeID = "E2",
				codeName = "bank@esunemail.com",
				content = "測試訊息",
				memo = "",
				displayOrder = 0,
				isEnable = True,
				actionType = "D",
				actionDT = "2022-06-14 11:39:12",
				actor = "admin",
				actorName = "林管理",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspCodeHistUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            CodeHistService CodeHistService = new
                CodeHistService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await CodeHistService.UpdateCodeHist(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask UpdateCodeHistTestFailed()
        {  //arrange
            var updateReq = new CodeHistUpdateRequest
            {
				codeType = "EnterpriseEmail",
				codeID = "E2",
				codeName = "bank@esunemail.com",
				content = "測試訊息",
				memo = "",
				displayOrder = 0,
				isEnable = True,
				actionType = "D",
				actionDT = "2022-06-14 11:39:12",
				actor = "admin",
				actorName = "林管理",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspCodeHistUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            CodeHistService CodeHistService = new
                CodeHistService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await CodeHistService.UpdateCodeHist(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }




    }
}
