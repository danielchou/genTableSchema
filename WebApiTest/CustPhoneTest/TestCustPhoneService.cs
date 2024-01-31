using AutoMapper;
using ESUN.AGD.DataAccess.DataService.DataAccess;
using ESUN.AGD.DataAccess.Type;
using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.Common;
using ESUN.AGD.WebApi.Application.CustPhone;
using ESUN.AGD.WebApi.Application.CustPhone.Contract;
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


namespace ESUN.AGD.WebApi.Test.CustPhoneTest
{
    [TestFixture]
    public class TestCustPhoneService
    {
        private static IDataAccessService stubDataAccessService = Substitute.For<IDataAccessService>();
        private static IGetTokenService stubGetTokenService = Substitute.For<IGetTokenService>();
        private static IHttpContextAccessor stubHttpContextAccessor = Substitute.For<IHttpContextAccessor>();
        private static IMapper stubMapper = Substitute.For<IMapper>();
        private static IUserService stubUserService = Substitute.For<IUserService>();
        private static IAutoNextNumService autoNextNumService = Substitute.For<IAutoNextNumService>();

        [Test]
        public async ValueTask GetCustPhoneTest()
        {  //arrange
            int seqNo = 1;
            string resCode = "U200";
           
            stubDataAccessService
                .LoadSingleData<TbCustPhone, object>("agdSp.uspCustPhoneGet", Arg.Any<int>())
                .ReturnsForAnyArgs(new TbCustPhone { SeqNo = 1 });

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            CustPhoneService CustPhoneService = 
                new CustPhoneService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await CustPhoneService.GetCustPhone(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspCustPhoneGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode,resCode);

        }

        [Test]
        public async ValueTask GetCustPhoneTestFailed()
        {  //arrange
            int seqNo = 0;
            string resCode = "U999";
          
            stubDataAccessService.LoadSingleData<TbCustPhone, object>("agdSp.uspCustPhoneGet", Arg.Any<int>())
                .ReturnsNull();          

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            CustPhoneService CustPhoneService = new CustPhoneService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await CustPhoneService.GetCustPhone(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<TbCustPhone, object>("agdSp.uspCustPhoneGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryCustPhoneTest()
        {  //arrange
            var queryReq = new CustPhoneQueryRequest { 						phoneType = "C",
						phone = "0287121298", };
            string resCode = "U200";
            
            stubDataAccessService.LoadData<TbCustPhone, object>
                (storeProcedure: "agdSp.uspCustPhoneQuery", Arg.Any<object>())
               .Returns(new TbCustPhone[] { 
                    new TbCustPhone { 
						phoneType = "C",
						phone = "0287121298",
                        Total = 2
                    },
                    new TbCustPhone { } 
                }.AsQueryable());
            
            stubHttpContextAccessor.HttpContext.Request.Path ="/api/CustPhone/query";
            
            CustPhoneService CustPhoneService = new 
                CustPhoneService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await CustPhoneService.QueryCustPhone(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
          
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryCustPhoneTestFailed()
        {  //arrange
            var queryReq = new CustPhoneQueryRequest {  
						phoneType = "C",
						phone = "0287121298",
            };
            string resCode = "U999";
            
            stubDataAccessService.LoadData<TbCustPhone, object>("agdSp.uspCustPhoneQuery", Arg.Any<object>())
                .ReturnsNull();
            
            stubHttpContextAccessor.HttpContext.Request.Path = "/api/CustPhone/query";
            CustPhoneService CustPhoneService = new CustPhoneService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await CustPhoneService.QueryCustPhone(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspCustPhoneGet", Arg.Any<object>());
            Assert.AreEqual(result.total, 0);

        }

        [Test]
        public async ValueTask InsertCustPhoneTest()
        {  //arrange
            var insertReq = new CustPhoneInsertRequest
            {
				custKey = 1,
				phoneType = "C",
				phone = "0287121298",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspCustPhoneInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result:new SqlResponse() { ErrorMsg="",Msg="" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            CustPhoneService CustPhoneService = new
                CustPhoneService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await CustPhoneService.InsertCustPhone(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask InsertCustPhoneTestFailed()
        {  //arrange
            var insertReq = new CustPhoneInsertRequest
            {
				custKey = 1,
				phoneType = "C",
				phone = "0287121298",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspCustPhoneInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            CustPhoneService CustPhoneService = new
                CustPhoneService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await CustPhoneService.InsertCustPhone(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }


        [Test]
        public async ValueTask UpdateCustPhoneTest()
        {  //arrange
            var updateReq = new CustPhoneUpdateRequest
            {
				custKey = 8,
				phoneType = "R",
				phone = "0221236789",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspCustPhoneUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            CustPhoneService CustPhoneService = new
                CustPhoneService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await CustPhoneService.UpdateCustPhone(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask UpdateCustPhoneTestFailed()
        {  //arrange
            var updateReq = new CustPhoneUpdateRequest
            {
				custKey = 8,
				phoneType = "R",
				phone = "0221236789",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspCustPhoneUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            CustPhoneService CustPhoneService = new
                CustPhoneService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await CustPhoneService.UpdateCustPhone(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }




    }
}
