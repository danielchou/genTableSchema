using AutoMapper;
using ESUN.AGD.DataAccess.DataService.DataAccess;
using ESUN.AGD.DataAccess.Type;
using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.Common;
using ESUN.AGD.WebApi.Application.AutoNextNum;
using ESUN.AGD.WebApi.Application.AutoNextNum.Contract;
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


namespace ESUN.AGD.WebApi.Test.AutoNextNumTest
{
    [TestFixture]
    public class TestAutoNextNumService
    {
        private static IDataAccessService stubDataAccessService = Substitute.For<IDataAccessService>();
        private static IGetTokenService stubGetTokenService = Substitute.For<IGetTokenService>();
        private static IHttpContextAccessor stubHttpContextAccessor = Substitute.For<IHttpContextAccessor>();
        private static IMapper stubMapper = Substitute.For<IMapper>();
        private static IUserService stubUserService = Substitute.For<IUserService>();
        private static IAutoNextNumService autoNextNumService = Substitute.For<IAutoNextNumService>();

        [Test]
        public async ValueTask GetAutoNextNumTest()
        {  //arrange
            int seqNo = 1;
            string resCode = "U200";
           
            stubDataAccessService
                .LoadSingleData<TbAutoNextNum, object>("agdSp.uspAutoNextNumGet", Arg.Any<int>())
                .ReturnsForAnyArgs(new TbAutoNextNum { SeqNo = 1 });

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            AutoNextNumService AutoNextNumService = 
                new AutoNextNumService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await AutoNextNumService.GetAutoNextNum(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspAutoNextNumGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode,resCode);

        }

        [Test]
        public async ValueTask GetAutoNextNumTestFailed()
        {  //arrange
            int seqNo = 0;
            string resCode = "U999";
          
            stubDataAccessService.LoadSingleData<TbAutoNextNum, object>("agdSp.uspAutoNextNumGet", Arg.Any<int>())
                .ReturnsNull();          

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            AutoNextNumService AutoNextNumService = new AutoNextNumService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await AutoNextNumService.GetAutoNextNum(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<TbAutoNextNum, object>("agdSp.uspAutoNextNumGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryAutoNextNumTest()
        {  //arrange
            var queryReq = new AutoNextNumQueryRequest { 						tableType = "AuthCallReason", };
            string resCode = "U200";
            
            stubDataAccessService.LoadData<TbAutoNextNum, object>
                (storeProcedure: "agdSp.uspAutoNextNumQuery", Arg.Any<object>())
               .Returns(new TbAutoNextNum[] { 
                    new TbAutoNextNum { 
						tableType = "AuthCallReason",
                        Total = 2
                    },
                    new TbAutoNextNum { } 
                }.AsQueryable());
            
            stubHttpContextAccessor.HttpContext.Request.Path ="/api/AutoNextNum/query";
            
            AutoNextNumService AutoNextNumService = new 
                AutoNextNumService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await AutoNextNumService.QueryAutoNextNum(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
          
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryAutoNextNumTestFailed()
        {  //arrange
            var queryReq = new AutoNextNumQueryRequest {  
						tableType = "AuthCallReason",
            };
            string resCode = "U999";
            
            stubDataAccessService.LoadData<TbAutoNextNum, object>("agdSp.uspAutoNextNumQuery", Arg.Any<object>())
                .ReturnsNull();
            
            stubHttpContextAccessor.HttpContext.Request.Path = "/api/AutoNextNum/query";
            AutoNextNumService AutoNextNumService = new AutoNextNumService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await AutoNextNumService.QueryAutoNextNum(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspAutoNextNumGet", Arg.Any<object>());
            Assert.AreEqual(result.total, 0);

        }

        [Test]
        public async ValueTask InsertAutoNextNumTest()
        {  //arrange
            var insertReq = new AutoNextNumInsertRequest
            {
				tableType = "AuthCallReason",
				tableDescr = "國際電話撥號原因代碼",
				lastNum = 4,
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspAutoNextNumInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result:new SqlResponse() { ErrorMsg="",Msg="" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            AutoNextNumService AutoNextNumService = new
                AutoNextNumService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await AutoNextNumService.InsertAutoNextNum(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask InsertAutoNextNumTestFailed()
        {  //arrange
            var insertReq = new AutoNextNumInsertRequest
            {
				tableType = "AuthCallReason",
				tableDescr = "國際電話撥號原因代碼",
				lastNum = 4,
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspAutoNextNumInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            AutoNextNumService AutoNextNumService = new
                AutoNextNumService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await AutoNextNumService.InsertAutoNextNum(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }


        [Test]
        public async ValueTask UpdateAutoNextNumTest()
        {  //arrange
            var updateReq = new AutoNextNumUpdateRequest
            {
				tableType = "TransactionApi",
				tableDescr = "交易執行",
				lastNum = 1871,
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspAutoNextNumUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            AutoNextNumService AutoNextNumService = new
                AutoNextNumService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await AutoNextNumService.UpdateAutoNextNum(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask UpdateAutoNextNumTestFailed()
        {  //arrange
            var updateReq = new AutoNextNumUpdateRequest
            {
				tableType = "TransactionApi",
				tableDescr = "交易執行",
				lastNum = 1871,
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspAutoNextNumUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            AutoNextNumService AutoNextNumService = new
                AutoNextNumService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await AutoNextNumService.UpdateAutoNextNum(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }




    }
}
