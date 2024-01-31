using AutoMapper;
using ESUN.AGD.DataAccess.DataService.DataAccess;
using ESUN.AGD.DataAccess.Type;
using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.Common;
using ESUN.AGD.WebApi.Application.UserPhoto;
using ESUN.AGD.WebApi.Application.UserPhoto.Contract;
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


namespace ESUN.AGD.WebApi.Test.UserPhotoTest
{
    [TestFixture]
    public class TestUserPhotoService
    {
        private static IDataAccessService stubDataAccessService = Substitute.For<IDataAccessService>();
        private static IGetTokenService stubGetTokenService = Substitute.For<IGetTokenService>();
        private static IHttpContextAccessor stubHttpContextAccessor = Substitute.For<IHttpContextAccessor>();
        private static IMapper stubMapper = Substitute.For<IMapper>();
        private static IUserService stubUserService = Substitute.For<IUserService>();
        private static IAutoNextNumService autoNextNumService = Substitute.For<IAutoNextNumService>();

        [Test]
        public async ValueTask GetUserPhotoTest()
        {  //arrange
            int seqNo = 1;
            string resCode = "U200";
           
            stubDataAccessService
                .LoadSingleData<TbUserPhoto, object>("agdSp.uspUserPhotoGet", Arg.Any<int>())
                .ReturnsForAnyArgs(new TbUserPhoto { SeqNo = 1 });

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            UserPhotoService UserPhotoService = 
                new UserPhotoService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await UserPhotoService.GetUserPhoto(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspUserPhotoGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode,resCode);

        }

        [Test]
        public async ValueTask GetUserPhotoTestFailed()
        {  //arrange
            int seqNo = 0;
            string resCode = "U999";
          
            stubDataAccessService.LoadSingleData<TbUserPhoto, object>("agdSp.uspUserPhotoGet", Arg.Any<int>())
                .ReturnsNull();          

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            UserPhotoService UserPhotoService = new UserPhotoService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await UserPhotoService.GetUserPhoto(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<TbUserPhoto, object>("agdSp.uspUserPhotoGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryUserPhotoTest()
        {  //arrange
            var queryReq = new UserPhotoQueryRequest {  };
            string resCode = "U200";
            
            stubDataAccessService.LoadData<TbUserPhoto, object>
                (storeProcedure: "agdSp.uspUserPhotoQuery", Arg.Any<object>())
               .Returns(new TbUserPhoto[] { 
                    new TbUserPhoto { 

                        Total = 2
                    },
                    new TbUserPhoto { } 
                }.AsQueryable());
            
            stubHttpContextAccessor.HttpContext.Request.Path ="/api/UserPhoto/query";
            
            UserPhotoService UserPhotoService = new 
                UserPhotoService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await UserPhotoService.QueryUserPhoto(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
          
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryUserPhotoTestFailed()
        {  //arrange
            var queryReq = new UserPhotoQueryRequest {  

            };
            string resCode = "U999";
            
            stubDataAccessService.LoadData<TbUserPhoto, object>("agdSp.uspUserPhotoQuery", Arg.Any<object>())
                .ReturnsNull();
            
            stubHttpContextAccessor.HttpContext.Request.Path = "/api/UserPhoto/query";
            UserPhotoService UserPhotoService = new UserPhotoService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await UserPhotoService.QueryUserPhoto(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspUserPhotoGet", Arg.Any<object>());
            Assert.AreEqual(result.total, 0);

        }

        [Test]
        public async ValueTask InsertUserPhotoTest()
        {  //arrange
            var insertReq = new UserPhotoInsertRequest
            {
				userID = "10001",
				photoFile = "",
				creator = "admin",
				creatorName = "林管理",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspUserPhotoInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result:new SqlResponse() { ErrorMsg="",Msg="" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            UserPhotoService UserPhotoService = new
                UserPhotoService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await UserPhotoService.InsertUserPhoto(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask InsertUserPhotoTestFailed()
        {  //arrange
            var insertReq = new UserPhotoInsertRequest
            {
				userID = "10001",
				photoFile = "",
				creator = "admin",
				creatorName = "林管理",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspUserPhotoInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            UserPhotoService UserPhotoService = new
                UserPhotoService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await UserPhotoService.InsertUserPhoto(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }


        [Test]
        public async ValueTask UpdateUserPhotoTest()
        {  //arrange
            var updateReq = new UserPhotoUpdateRequest
            {
				userID = "sys",
				photoFile = "",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspUserPhotoUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            UserPhotoService UserPhotoService = new
                UserPhotoService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await UserPhotoService.UpdateUserPhoto(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask UpdateUserPhotoTestFailed()
        {  //arrange
            var updateReq = new UserPhotoUpdateRequest
            {
				userID = "sys",
				photoFile = "",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspUserPhotoUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            UserPhotoService UserPhotoService = new
                UserPhotoService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await UserPhotoService.UpdateUserPhoto(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }




    }
}
