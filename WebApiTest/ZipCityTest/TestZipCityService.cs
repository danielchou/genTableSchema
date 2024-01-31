using AutoMapper;
using ESUN.AGD.DataAccess.DataService.DataAccess;
using ESUN.AGD.DataAccess.Type;
using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.Common;
using ESUN.AGD.WebApi.Application.ZipCity;
using ESUN.AGD.WebApi.Application.ZipCity.Contract;
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


namespace ESUN.AGD.WebApi.Test.ZipCityTest
{
    [TestFixture]
    public class TestZipCityService
    {
        private static IDataAccessService stubDataAccessService = Substitute.For<IDataAccessService>();
        private static IGetTokenService stubGetTokenService = Substitute.For<IGetTokenService>();
        private static IHttpContextAccessor stubHttpContextAccessor = Substitute.For<IHttpContextAccessor>();
        private static IMapper stubMapper = Substitute.For<IMapper>();
        private static IUserService stubUserService = Substitute.For<IUserService>();
        private static IAutoNextNumService autoNextNumService = Substitute.For<IAutoNextNumService>();

        [Test]
        public async ValueTask GetZipCityTest()
        {  //arrange
            int seqNo = 1;
            string resCode = "U200";
           
            stubDataAccessService
                .LoadSingleData<TbZipCity, object>("agdSp.uspZipCityGet", Arg.Any<int>())
                .ReturnsForAnyArgs(new TbZipCity { SeqNo = 1 });

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            ZipCityService ZipCityService = 
                new ZipCityService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ZipCityService.GetZipCity(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspZipCityGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode,resCode);

        }

        [Test]
        public async ValueTask GetZipCityTestFailed()
        {  //arrange
            int seqNo = 0;
            string resCode = "U999";
          
            stubDataAccessService.LoadSingleData<TbZipCity, object>("agdSp.uspZipCityGet", Arg.Any<int>())
                .ReturnsNull();          

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            ZipCityService ZipCityService = new ZipCityService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ZipCityService.GetZipCity(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<TbZipCity, object>("agdSp.uspZipCityGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryZipCityTest()
        {  //arrange
            var queryReq = new ZipCityQueryRequest { 						city = "臺北市",
						district  = "中正區", };
            string resCode = "U200";
            
            stubDataAccessService.LoadData<TbZipCity, object>
                (storeProcedure: "agdSp.uspZipCityQuery", Arg.Any<object>())
               .Returns(new TbZipCity[] { 
                    new TbZipCity { 
						city = "臺北市",
						district  = "中正區",
                        Total = 2
                    },
                    new TbZipCity { } 
                }.AsQueryable());
            
            stubHttpContextAccessor.HttpContext.Request.Path ="/api/ZipCity/query";
            
            ZipCityService ZipCityService = new 
                ZipCityService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ZipCityService.QueryZipCity(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
          
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryZipCityTestFailed()
        {  //arrange
            var queryReq = new ZipCityQueryRequest {  
						city = "臺北市",
						district  = "中正區",
            };
            string resCode = "U999";
            
            stubDataAccessService.LoadData<TbZipCity, object>("agdSp.uspZipCityQuery", Arg.Any<object>())
                .ReturnsNull();
            
            stubHttpContextAccessor.HttpContext.Request.Path = "/api/ZipCity/query";
            ZipCityService ZipCityService = new ZipCityService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ZipCityService.QueryZipCity(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspZipCityGet", Arg.Any<object>());
            Assert.AreEqual(result.total, 0);

        }

        [Test]
        public async ValueTask InsertZipCityTest()
        {  //arrange
            var insertReq = new ZipCityInsertRequest
            {
				zipCode = "100",
				city = "臺北市",
				district  = "中正區",
				creator = "admin",
				creatorName = "林管理",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspZipCityInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result:new SqlResponse() { ErrorMsg="",Msg="" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            ZipCityService ZipCityService = new
                ZipCityService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ZipCityService.InsertZipCity(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask InsertZipCityTestFailed()
        {  //arrange
            var insertReq = new ZipCityInsertRequest
            {
				zipCode = "100",
				city = "臺北市",
				district  = "中正區",
				creator = "admin",
				creatorName = "林管理",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspZipCityInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            ZipCityService ZipCityService = new
                ZipCityService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ZipCityService.InsertZipCity(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }


        [Test]
        public async ValueTask UpdateZipCityTest()
        {  //arrange
            var updateReq = new ZipCityUpdateRequest
            {
				zipCode = "983",
				city = "花蓮縣",
				district  = "富里鄉",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspZipCityUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            ZipCityService ZipCityService = new
                ZipCityService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ZipCityService.UpdateZipCity(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask UpdateZipCityTestFailed()
        {  //arrange
            var updateReq = new ZipCityUpdateRequest
            {
				zipCode = "983",
				city = "花蓮縣",
				district  = "富里鄉",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspZipCityUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            ZipCityService ZipCityService = new
                ZipCityService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ZipCityService.UpdateZipCity(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }




    }
}
