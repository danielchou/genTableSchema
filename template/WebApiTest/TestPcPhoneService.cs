using AutoMapper;
using ESUN.AGD.DataAccess.DataService.DataAccess;
using ESUN.AGD.DataAccess.Type;
using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.Common;
using ESUN.AGD.WebApi.Application.$pt_TableName;
using ESUN.AGD.WebApi.Application.$pt_TableName.Contract;
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


namespace ESUN.AGD.WebApi.Test.$pt_TableName$pt_Test
{
    [TestFixture]
    public class Test$pt_TableName$pt_Service
    {
        private static IDataAccessService stubDataAccessService = Substitute.For<IDataAccessService>();
        private static IGetTokenService stubGetTokenService = Substitute.For<IGetTokenService>();
        private static IHttpContextAccessor stubHttpContextAccessor = Substitute.For<IHttpContextAccessor>();
        private static IMapper stubMapper = Substitute.For<IMapper>();
        private static IUserService stubUserService = Substitute.For<IUserService>();
        private static IAutoNextNumService autoNextNumService = Substitute.For<IAutoNextNumService>();

        [Test]
        public async ValueTask Get$pt_TableName$pt_Test()
        {  //arrange
            int seqNo = 1;
            string resCode = "U200";
           
            stubDataAccessService
                .LoadSingleData<Tb$pt_TableName, object>("agdSp.usp$pt_TableName$pt_Get", Arg.Any<int>())
                .ReturnsForAnyArgs(new Tb$pt_TableName { SeqNo = 1 });

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            $pt_TableName$pt_Service $pt_TableName$pt_Service = 
                new $pt_TableName$pt_Service
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await $pt_TableName$pt_Service.Get$pt_TableName(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.usp$pt_TableName$pt_Get", Arg.Any<object>());
            Assert.AreEqual(result.resultCode,resCode);

        }

        [Test]
        public async ValueTask Get$pt_TableName$pt_TestFailed()
        {  //arrange
            int seqNo = 0;
            string resCode = "U999";
          
            stubDataAccessService.LoadSingleData<Tb$pt_TableName, object>("agdSp.usp$pt_TableName$pt_Get", Arg.Any<int>())
                .ReturnsNull();          

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            $pt_TableName$pt_Service $pt_TableName$pt_Service = new $pt_TableName$pt_Service
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await $pt_TableName$pt_Service.Get$pt_TableName(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<Tb$pt_TableName, object>("agdSp.usp$pt_TableName$pt_Get", Arg.Any<object>());
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask Query$pt_TableName$pt_Test()
        {  //arrange
            var queryReq = new $pt_TableName$pt_QueryRequest { $pt_queryParas };
            string resCode = "U200";
            
            stubDataAccessService.LoadData<Tb$pt_TableName, object>
                (storeProcedure: "agdSp.usp$pt_TableName$pt_Query", Arg.Any<object>())
               .Returns(new Tb$pt_TableName[] { 
                    new Tb$pt_TableName { 
$pt_queryParas
                        Total = 2
                    },
                    new Tb$pt_TableName { } 
                }.AsQueryable());
            
            stubHttpContextAccessor.HttpContext.Request.Path ="/api/$pt_TableName/query";
            
            $pt_TableName$pt_Service $pt_TableName$pt_Service = new 
                $pt_TableName$pt_Service(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await $pt_TableName$pt_Service.Query$pt_TableName(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
          
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask Query$pt_TableName$pt_TestFailed()
        {  //arrange
            var queryReq = new $pt_TableName$pt_QueryRequest {  
$pt_queryParas
            };
            string resCode = "U999";
            
            stubDataAccessService.LoadData<Tb$pt_TableName, object>("agdSp.usp$pt_TableName$pt_Query", Arg.Any<object>())
                .ReturnsNull();
            
            stubHttpContextAccessor.HttpContext.Request.Path = "/api/$pt_TableName/query";
            $pt_TableName$pt_Service $pt_TableName$pt_Service = new $pt_TableName$pt_Service(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await $pt_TableName$pt_Service.Query$pt_TableName(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.usp$pt_TableName$pt_Get", Arg.Any<object>());
            Assert.AreEqual(result.total, 0);

        }

        [Test]
        public async ValueTask Insert$pt_TableName$pt_Test()
        {  //arrange
            var insertReq = new $pt_TableName$pt_InsertRequest
            {
$pt_ColumnsInsert
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.usp$pt_TableName$pt_Insert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result:new SqlResponse() { ErrorMsg="",Msg="" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            $pt_TableName$pt_Service $pt_TableName$pt_Service = new
                $pt_TableName$pt_Service(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await $pt_TableName$pt_Service.Insert$pt_TableName(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask Insert$pt_TableName$pt_TestFailed()
        {  //arrange
            var insertReq = new $pt_TableName$pt_InsertRequest
            {
$pt_ColumnsInsert
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.usp$pt_TableName$pt_Insert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            $pt_TableName$pt_Service $pt_TableName$pt_Service = new
                $pt_TableName$pt_Service(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await $pt_TableName$pt_Service.Insert$pt_TableName(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }


        [Test]
        public async ValueTask Update$pt_TableName$pt_Test()
        {  //arrange
            var updateReq = new $pt_TableName$pt_UpdateRequest
            {
$pt_ColumnsUpdate
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.usp$pt_TableName$pt_Update", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            $pt_TableName$pt_Service $pt_TableName$pt_Service = new
                $pt_TableName$pt_Service(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await $pt_TableName$pt_Service.Update$pt_TableName(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask Update$pt_TableName$pt_TestFailed()
        {  //arrange
            var updateReq = new $pt_TableName$pt_UpdateRequest
            {
$pt_ColumnsUpdate
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.usp$pt_TableName$pt_Update", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            $pt_TableName$pt_Service $pt_TableName$pt_Service = new
                $pt_TableName$pt_Service(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await $pt_TableName$pt_Service.Update$pt_TableName(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }




    }
}
