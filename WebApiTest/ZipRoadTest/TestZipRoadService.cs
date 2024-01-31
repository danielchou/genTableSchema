using AutoMapper;
using ESUN.AGD.DataAccess.DataService.DataAccess;
using ESUN.AGD.DataAccess.Type;
using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.Common;
using ESUN.AGD.WebApi.Application.ZipRoad;
using ESUN.AGD.WebApi.Application.ZipRoad.Contract;
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


namespace ESUN.AGD.WebApi.Test.ZipRoadTest
{
    [TestFixture]
    public class TestZipRoadService
    {
        private static IDataAccessService stubDataAccessService = Substitute.For<IDataAccessService>();
        private static IGetTokenService stubGetTokenService = Substitute.For<IGetTokenService>();
        private static IHttpContextAccessor stubHttpContextAccessor = Substitute.For<IHttpContextAccessor>();
        private static IMapper stubMapper = Substitute.For<IMapper>();
        private static IUserService stubUserService = Substitute.For<IUserService>();
        private static IAutoNextNumService autoNextNumService = Substitute.For<IAutoNextNumService>();

        [Test]
        public async ValueTask GetZipRoadTest()
        {  //arrange
            int seqNo = 1;
            string resCode = "U200";
           
            stubDataAccessService
                .LoadSingleData<TbZipRoad, object>("agdSp.uspZipRoadGet", Arg.Any<int>())
                .ReturnsForAnyArgs(new TbZipRoad { SeqNo = 1 });

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            ZipRoadService ZipRoadService = 
                new ZipRoadService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ZipRoadService.GetZipRoad(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspZipRoadGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode,resCode);

        }

        [Test]
        public async ValueTask GetZipRoadTestFailed()
        {  //arrange
            int seqNo = 0;
            string resCode = "U999";
          
            stubDataAccessService.LoadSingleData<TbZipRoad, object>("agdSp.uspZipRoadGet", Arg.Any<int>())
                .ReturnsNull();          

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            ZipRoadService ZipRoadService = new ZipRoadService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ZipRoadService.GetZipRoad(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<TbZipRoad, object>("agdSp.uspZipRoadGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryZipRoadTest()
        {  //arrange
            var queryReq = new ZipRoadQueryRequest {  };
            string resCode = "U200";
            
            stubDataAccessService.LoadData<TbZipRoad, object>
                (storeProcedure: "agdSp.uspZipRoadQuery", Arg.Any<object>())
               .Returns(new TbZipRoad[] { 
                    new TbZipRoad { 

                        Total = 2
                    },
                    new TbZipRoad { } 
                }.AsQueryable());
            
            stubHttpContextAccessor.HttpContext.Request.Path ="/api/ZipRoad/query";
            
            ZipRoadService ZipRoadService = new 
                ZipRoadService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ZipRoadService.QueryZipRoad(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
          
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryZipRoadTestFailed()
        {  //arrange
            var queryReq = new ZipRoadQueryRequest {  

            };
            string resCode = "U999";
            
            stubDataAccessService.LoadData<TbZipRoad, object>("agdSp.uspZipRoadQuery", Arg.Any<object>())
                .ReturnsNull();
            
            stubHttpContextAccessor.HttpContext.Request.Path = "/api/ZipRoad/query";
            ZipRoadService ZipRoadService = new ZipRoadService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ZipRoadService.QueryZipRoad(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspZipRoadGet", Arg.Any<object>());
            Assert.AreEqual(result.total, 0);

        }

        [Test]
        public async ValueTask InsertZipRoadTest()
        {  //arrange
            var insertReq = new ZipRoadInsertRequest
            {
				zipCode = "001",
				road = "一條街",
				creator = "10046",
				creatorName = "陳麗杰鰻魚飯",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspZipRoadInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result:new SqlResponse() { ErrorMsg="",Msg="" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            ZipRoadService ZipRoadService = new
                ZipRoadService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ZipRoadService.InsertZipRoad(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask InsertZipRoadTestFailed()
        {  //arrange
            var insertReq = new ZipRoadInsertRequest
            {
				zipCode = "001",
				road = "一條街",
				creator = "10046",
				creatorName = "陳麗杰鰻魚飯",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspZipRoadInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            ZipRoadService ZipRoadService = new
                ZipRoadService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ZipRoadService.InsertZipRoad(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }


        [Test]
        public async ValueTask UpdateZipRoadTest()
        {  //arrange
            var updateReq = new ZipRoadUpdateRequest
            {
				zipCode = "983",
				road = "羅山",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspZipRoadUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            ZipRoadService ZipRoadService = new
                ZipRoadService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ZipRoadService.UpdateZipRoad(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask UpdateZipRoadTestFailed()
        {  //arrange
            var updateReq = new ZipRoadUpdateRequest
            {
				zipCode = "983",
				road = "羅山",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspZipRoadUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            ZipRoadService ZipRoadService = new
                ZipRoadService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await ZipRoadService.UpdateZipRoad(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }




    }
}
