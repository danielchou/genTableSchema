using AutoMapper;
using ESUN.AGD.DataAccess.DataService.DataAccess;
using ESUN.AGD.DataAccess.Type;
using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.Common;
using ESUN.AGD.WebApi.Application.Code;
using ESUN.AGD.WebApi.Application.Code.Contract;
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


namespace ESUN.AGD.WebApi.Test.CodeTest
{
    [TestFixture]
    public class TestCodeService
    {
        private static IDataAccessService stubDataAccessService = Substitute.For<IDataAccessService>();
        private static IGetTokenService stubGetTokenService = Substitute.For<IGetTokenService>();
        private static IHttpContextAccessor stubHttpContextAccessor = Substitute.For<IHttpContextAccessor>();
        private static IMapper stubMapper = Substitute.For<IMapper>();
        private static IUserService stubUserService = Substitute.For<IUserService>();
        private static IAutoNextNumService autoNextNumService = Substitute.For<IAutoNextNumService>();

        [Test]
        public async ValueTask GetCodeTest()
        {  //arrange
            int seqNo = 1;
            string resCode = "U200";
           
            stubDataAccessService
                .LoadSingleData<TbCode, object>("agdSp.uspCodeGet", Arg.Any<int>())
                .ReturnsForAnyArgs(new TbCode { SeqNo = 1 });

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            CodeService CodeService = 
                new CodeService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await CodeService.GetCode(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspCodeGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode,resCode);

        }

        [Test]
        public async ValueTask GetCodeTestFailed()
        {  //arrange
            int seqNo = 0;
            string resCode = "U999";
          
            stubDataAccessService.LoadSingleData<TbCode, object>("agdSp.uspCodeGet", Arg.Any<int>())
                .ReturnsNull();          

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            CodeService CodeService = new CodeService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await CodeService.GetCode(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<TbCode, object>("agdSp.uspCodeGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryCodeTest()
        {  //arrange
            var queryReq = new CodeQueryRequest { 						codeID = "13",
						isEnable = True, };
            string resCode = "U200";
            
            stubDataAccessService.LoadData<TbCode, object>
                (storeProcedure: "agdSp.uspCodeQuery", Arg.Any<object>())
               .Returns(new TbCode[] { 
                    new TbCode { 
						codeID = "13",
						isEnable = True,
                        Total = 2
                    },
                    new TbCode { } 
                }.AsQueryable());
            
            stubHttpContextAccessor.HttpContext.Request.Path ="/api/Code/query";
            
            CodeService CodeService = new 
                CodeService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await CodeService.QueryCode(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
          
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryCodeTestFailed()
        {  //arrange
            var queryReq = new CodeQueryRequest {  
						codeID = "13",
						isEnable = True,
            };
            string resCode = "U999";
            
            stubDataAccessService.LoadData<TbCode, object>("agdSp.uspCodeQuery", Arg.Any<object>())
                .ReturnsNull();
            
            stubHttpContextAccessor.HttpContext.Request.Path = "/api/Code/query";
            CodeService CodeService = new CodeService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await CodeService.QueryCode(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspCodeGet", Arg.Any<object>());
            Assert.AreEqual(result.total, 0);

        }

        [Test]
        public async ValueTask InsertCodeTest()
        {  //arrange
            var insertReq = new CodeInsertRequest
            {
				codeType = "AccidentCode",
				codeID = "13",
				codeName = "他行通報(聯防機制)",
				content = "",
				memo = "",
				displayOrder = 1,
				isEnable = True,
				creator = "sys",
				creatorName = "system",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspCodeInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result:new SqlResponse() { ErrorMsg="",Msg="" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            CodeService CodeService = new
                CodeService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await CodeService.InsertCode(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask InsertCodeTestFailed()
        {  //arrange
            var insertReq = new CodeInsertRequest
            {
				codeType = "AccidentCode",
				codeID = "13",
				codeName = "他行通報(聯防機制)",
				content = "",
				memo = "",
				displayOrder = 1,
				isEnable = True,
				creator = "sys",
				creatorName = "system",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspCodeInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            CodeService CodeService = new
                CodeService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await CodeService.InsertCode(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }


        [Test]
        public async ValueTask UpdateCodeTest()
        {  //arrange
            var updateReq = new CodeUpdateRequest
            {
				codeType = "TxnItemCode",
				codeID = "TIC10",
				codeName = "存款業務>通報帳戶>聯防通報",
				content = "",
				memo = "",
				displayOrder = 0,
				isEnable = True,
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspCodeUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            CodeService CodeService = new
                CodeService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await CodeService.UpdateCode(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask UpdateCodeTestFailed()
        {  //arrange
            var updateReq = new CodeUpdateRequest
            {
				codeType = "TxnItemCode",
				codeID = "TIC10",
				codeName = "存款業務>通報帳戶>聯防通報",
				content = "",
				memo = "",
				displayOrder = 0,
				isEnable = True,
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspCodeUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            CodeService CodeService = new
                CodeService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await CodeService.UpdateCode(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }




    }
}
