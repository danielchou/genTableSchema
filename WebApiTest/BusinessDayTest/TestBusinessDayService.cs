using AutoMapper;
using ESUN.AGD.DataAccess.DataService.DataAccess;
using ESUN.AGD.DataAccess.Type;
using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.Common;
using ESUN.AGD.WebApi.Application.BusinessDay;
using ESUN.AGD.WebApi.Application.BusinessDay.Contract;
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


namespace ESUN.AGD.WebApi.Test.BusinessDayTest
{
    [TestFixture]
    public class TestBusinessDayService
    {
        private static IDataAccessService stubDataAccessService = Substitute.For<IDataAccessService>();
        private static IGetTokenService stubGetTokenService = Substitute.For<IGetTokenService>();
        private static IHttpContextAccessor stubHttpContextAccessor = Substitute.For<IHttpContextAccessor>();
        private static IMapper stubMapper = Substitute.For<IMapper>();
        private static IUserService stubUserService = Substitute.For<IUserService>();
        private static IAutoNextNumService autoNextNumService = Substitute.For<IAutoNextNumService>();

        [Test]
        public async ValueTask GetBusinessDayTest()
        {  //arrange
            int seqNo = 1;
            string resCode = "U200";
           
            stubDataAccessService
                .LoadSingleData<TbBusinessDay, object>("agdSp.uspBusinessDayGet", Arg.Any<int>())
                .ReturnsForAnyArgs(new TbBusinessDay { SeqNo = 1 });

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            BusinessDayService BusinessDayService = 
                new BusinessDayService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await BusinessDayService.GetBusinessDay(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspBusinessDayGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode,resCode);

        }

        [Test]
        public async ValueTask GetBusinessDayTestFailed()
        {  //arrange
            int seqNo = 0;
            string resCode = "U999";
          
            stubDataAccessService.LoadSingleData<TbBusinessDay, object>("agdSp.uspBusinessDayGet", Arg.Any<int>())
                .ReturnsNull();          

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            BusinessDayService BusinessDayService = new BusinessDayService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await BusinessDayService.GetBusinessDay(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<TbBusinessDay, object>("agdSp.uspBusinessDayGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryBusinessDayTest()
        {  //arrange
            var queryReq = new BusinessDayQueryRequest {  };
            string resCode = "U200";
            
            stubDataAccessService.LoadData<TbBusinessDay, object>
                (storeProcedure: "agdSp.uspBusinessDayQuery", Arg.Any<object>())
               .Returns(new TbBusinessDay[] { 
                    new TbBusinessDay { 

                        Total = 2
                    },
                    new TbBusinessDay { } 
                }.AsQueryable());
            
            stubHttpContextAccessor.HttpContext.Request.Path ="/api/BusinessDay/query";
            
            BusinessDayService BusinessDayService = new 
                BusinessDayService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await BusinessDayService.QueryBusinessDay(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
          
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryBusinessDayTestFailed()
        {  //arrange
            var queryReq = new BusinessDayQueryRequest {  

            };
            string resCode = "U999";
            
            stubDataAccessService.LoadData<TbBusinessDay, object>("agdSp.uspBusinessDayQuery", Arg.Any<object>())
                .ReturnsNull();
            
            stubHttpContextAccessor.HttpContext.Request.Path = "/api/BusinessDay/query";
            BusinessDayService BusinessDayService = new BusinessDayService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await BusinessDayService.QueryBusinessDay(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspBusinessDayGet", Arg.Any<object>());
            Assert.AreEqual(result.total, 0);

        }

        [Test]
        public async ValueTask InsertBusinessDayTest()
        {  //arrange
            var insertReq = new BusinessDayInsertRequest
            {
				businessDayDate = "2022-01-01",
				businessDayCheck = "N",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspBusinessDayInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result:new SqlResponse() { ErrorMsg="",Msg="" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            BusinessDayService BusinessDayService = new
                BusinessDayService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await BusinessDayService.InsertBusinessDay(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask InsertBusinessDayTestFailed()
        {  //arrange
            var insertReq = new BusinessDayInsertRequest
            {
				businessDayDate = "2022-01-01",
				businessDayCheck = "N",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspBusinessDayInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            BusinessDayService BusinessDayService = new
                BusinessDayService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await BusinessDayService.InsertBusinessDay(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }


        [Test]
        public async ValueTask UpdateBusinessDayTest()
        {  //arrange
            var updateReq = new BusinessDayUpdateRequest
            {
				businessDayDate = "2022-12-31",
				businessDayCheck = "N",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspBusinessDayUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            BusinessDayService BusinessDayService = new
                BusinessDayService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await BusinessDayService.UpdateBusinessDay(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask UpdateBusinessDayTestFailed()
        {  //arrange
            var updateReq = new BusinessDayUpdateRequest
            {
				businessDayDate = "2022-12-31",
				businessDayCheck = "N",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspBusinessDayUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            BusinessDayService BusinessDayService = new
                BusinessDayService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await BusinessDayService.UpdateBusinessDay(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }




    }
}
