using AutoMapper;
using ESUN.AGD.DataAccess.DataService.DataAccess;
using ESUN.AGD.DataAccess.Type;
using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.Common;
using ESUN.AGD.WebApi.Application.PcPhone;
using ESUN.AGD.WebApi.Application.PcPhone.Contract;
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


namespace ESUN.AGD.WebApi.Test.PcPhoneTest
{
    [TestFixture]
    public class TestPcPhoneService
    {
        private static IDataAccessService stubDataAccessService = Substitute.For<IDataAccessService>();
        private static IGetTokenService stubGetTokenService = Substitute.For<IGetTokenService>();
        private static IHttpContextAccessor stubHttpContextAccessor = Substitute.For<IHttpContextAccessor>();
        private static IMapper stubMapper = Substitute.For<IMapper>();
        private static IUserService stubUserService = Substitute.For<IUserService>();
        private static IAutoNextNumService autoNextNumService = Substitute.For<IAutoNextNumService>();

        [Test]
        public async ValueTask GetPcPhoneTest()
        {  //arrange
            int seqNo = 1;
            string resCode = "U200";
           
            stubDataAccessService
                .LoadSingleData<TbPcPhone, object>("agdSp.uspPcPhoneGet", Arg.Any<int>())
                .ReturnsForAnyArgs(new TbPcPhone { SeqNo = 1 });

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            PcPhoneService PcPhoneService = 
                new PcPhoneService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await PcPhoneService.GetPcPhone(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspPcPhoneGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode,resCode);

        }

        [Test]
        public async ValueTask GetPcPhoneTestFailed()
        {  //arrange
            int seqNo = 0;
            string resCode = "U999";
          
            stubDataAccessService.LoadSingleData<TbPcPhone, object>("agdSp.uspPcPhoneGet", Arg.Any<int>())
                .ReturnsNull();          

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            PcPhoneService PcPhoneService = new PcPhoneService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await PcPhoneService.GetPcPhone(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<TbPcPhone, object>("agdSp.uspPcPhoneGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryPcPhoneTest()
        {  //arrange
            var queryReq = new PcPhoneQueryRequest { 						computerName = "AA1",
						extCode = "1100", };
            string resCode = "U200";
            
            stubDataAccessService.LoadData<TbPcPhone, object>
                (storeProcedure: "agdSp.uspPcPhoneQuery", Arg.Any<object>())
               .Returns(new TbPcPhone[] { 
                    new TbPcPhone { 
						computerName = "AA1",
						extCode = "1100",
                        Total = 2
                    },
                    new TbPcPhone { } 
                }.AsQueryable());
            
            stubHttpContextAccessor.HttpContext.Request.Path ="/api/PcPhone/query";
            
            PcPhoneService PcPhoneService = new 
                PcPhoneService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await PcPhoneService.QueryPcPhone(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
          
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryPcPhoneTestFailed()
        {  //arrange
            var queryReq = new PcPhoneQueryRequest {  
						computerName = "AA1",
						extCode = "1100",
            };
            string resCode = "U999";
            
            stubDataAccessService.LoadData<TbPcPhone, object>("agdSp.uspPcPhoneQuery", Arg.Any<object>())
                .ReturnsNull();
            
            stubHttpContextAccessor.HttpContext.Request.Path = "/api/PcPhone/query";
            PcPhoneService PcPhoneService = new PcPhoneService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await PcPhoneService.QueryPcPhone(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspPcPhoneGet", Arg.Any<object>());
            Assert.AreEqual(result.total, 0);

        }

        [Test]
        public async ValueTask InsertPcPhoneTest()
        {  //arrange
            var insertReq = new PcPhoneInsertRequest
            {
				computerIP = "1.1.1.1",
				computerName = "AA1",
				extCode = "1100",
				memo = "1234",
				creator = "sys",
				creatorName = "系統",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspPcPhoneInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result:new SqlResponse() { ErrorMsg="",Msg="" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            PcPhoneService PcPhoneService = new
                PcPhoneService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await PcPhoneService.InsertPcPhone(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask InsertPcPhoneTestFailed()
        {  //arrange
            var insertReq = new PcPhoneInsertRequest
            {
				computerIP = "1.1.1.1",
				computerName = "AA1",
				extCode = "1100",
				memo = "1234",
				creator = "sys",
				creatorName = "系統",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspPcPhoneInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            PcPhoneService PcPhoneService = new
                PcPhoneService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await PcPhoneService.InsertPcPhone(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }


        [Test]
        public async ValueTask UpdatePcPhoneTest()
        {  //arrange
            var updateReq = new PcPhoneUpdateRequest
            {
				computerIP = "2.2.2.2",
				computerName = "QQ",
				extCode = "1234",
				memo = "",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspPcPhoneUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            PcPhoneService PcPhoneService = new
                PcPhoneService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await PcPhoneService.UpdatePcPhone(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask UpdatePcPhoneTestFailed()
        {  //arrange
            var updateReq = new PcPhoneUpdateRequest
            {
				computerIP = "2.2.2.2",
				computerName = "QQ",
				extCode = "1234",
				memo = "",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspPcPhoneUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            PcPhoneService PcPhoneService = new
                PcPhoneService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await PcPhoneService.UpdatePcPhone(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }




    }
}
