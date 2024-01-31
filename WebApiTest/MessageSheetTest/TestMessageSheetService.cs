using AutoMapper;
using ESUN.AGD.DataAccess.DataService.DataAccess;
using ESUN.AGD.DataAccess.Type;
using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.Common;
using ESUN.AGD.WebApi.Application.MessageSheet;
using ESUN.AGD.WebApi.Application.MessageSheet.Contract;
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


namespace ESUN.AGD.WebApi.Test.MessageSheetTest
{
    [TestFixture]
    public class TestMessageSheetService
    {
        private static IDataAccessService stubDataAccessService = Substitute.For<IDataAccessService>();
        private static IGetTokenService stubGetTokenService = Substitute.For<IGetTokenService>();
        private static IHttpContextAccessor stubHttpContextAccessor = Substitute.For<IHttpContextAccessor>();
        private static IMapper stubMapper = Substitute.For<IMapper>();
        private static IUserService stubUserService = Substitute.For<IUserService>();
        private static IAutoNextNumService autoNextNumService = Substitute.For<IAutoNextNumService>();

        [Test]
        public async ValueTask GetMessageSheetTest()
        {  //arrange
            int seqNo = 1;
            string resCode = "U200";
           
            stubDataAccessService
                .LoadSingleData<TbMessageSheet, object>("agdSp.uspMessageSheetGet", Arg.Any<int>())
                .ReturnsForAnyArgs(new TbMessageSheet { SeqNo = 1 });

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            MessageSheetService MessageSheetService = 
                new MessageSheetService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await MessageSheetService.GetMessageSheet(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspMessageSheetGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode,resCode);

        }

        [Test]
        public async ValueTask GetMessageSheetTestFailed()
        {  //arrange
            int seqNo = 0;
            string resCode = "U999";
          
            stubDataAccessService.LoadSingleData<TbMessageSheet, object>("agdSp.uspMessageSheetGet", Arg.Any<int>())
                .ReturnsNull();          

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            MessageSheetService MessageSheetService = new MessageSheetService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await MessageSheetService.GetMessageSheet(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<TbMessageSheet, object>("agdSp.uspMessageSheetGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryMessageSheetTest()
        {  //arrange
            var queryReq = new MessageSheetQueryRequest { 						messageSheetID = "E01",
						messageSheetName = "信用卡",
						isEnable = True, };
            string resCode = "U200";
            
            stubDataAccessService.LoadData<TbMessageSheet, object>
                (storeProcedure: "agdSp.uspMessageSheetQuery", Arg.Any<object>())
               .Returns(new TbMessageSheet[] { 
                    new TbMessageSheet { 
						messageSheetID = "E01",
						messageSheetName = "信用卡",
						isEnable = True,
                        Total = 2
                    },
                    new TbMessageSheet { } 
                }.AsQueryable());
            
            stubHttpContextAccessor.HttpContext.Request.Path ="/api/MessageSheet/query";
            
            MessageSheetService MessageSheetService = new 
                MessageSheetService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await MessageSheetService.QueryMessageSheet(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
          
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryMessageSheetTestFailed()
        {  //arrange
            var queryReq = new MessageSheetQueryRequest {  
						messageSheetID = "E01",
						messageSheetName = "信用卡",
						isEnable = True,
            };
            string resCode = "U999";
            
            stubDataAccessService.LoadData<TbMessageSheet, object>("agdSp.uspMessageSheetQuery", Arg.Any<object>())
                .ReturnsNull();
            
            stubHttpContextAccessor.HttpContext.Request.Path = "/api/MessageSheet/query";
            MessageSheetService MessageSheetService = new MessageSheetService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await MessageSheetService.QueryMessageSheet(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspMessageSheetGet", Arg.Any<object>());
            Assert.AreEqual(result.total, 0);

        }

        [Test]
        public async ValueTask InsertMessageSheetTest()
        {  //arrange
            var insertReq = new MessageSheetInsertRequest
            {
				channel = "01",
				messageSheetID = "E01",
				messageSheetName = "信用卡",
				displayOrder = 1,
				isEnable = True,
				creator = "admin",
				creatorName = "林管理",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspMessageSheetInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result:new SqlResponse() { ErrorMsg="",Msg="" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            MessageSheetService MessageSheetService = new
                MessageSheetService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await MessageSheetService.InsertMessageSheet(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask InsertMessageSheetTestFailed()
        {  //arrange
            var insertReq = new MessageSheetInsertRequest
            {
				channel = "01",
				messageSheetID = "E01",
				messageSheetName = "信用卡",
				displayOrder = 1,
				isEnable = True,
				creator = "admin",
				creatorName = "林管理",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspMessageSheetInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            MessageSheetService MessageSheetService = new
                MessageSheetService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await MessageSheetService.InsertMessageSheet(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }


        [Test]
        public async ValueTask UpdateMessageSheetTest()
        {  //arrange
            var updateReq = new MessageSheetUpdateRequest
            {
				channel = "01",
				messageSheetID = "E01",
				messageSheetName = "信用卡",
				displayOrder = 1,
				isEnable = True,
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspMessageSheetUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            MessageSheetService MessageSheetService = new
                MessageSheetService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await MessageSheetService.UpdateMessageSheet(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask UpdateMessageSheetTestFailed()
        {  //arrange
            var updateReq = new MessageSheetUpdateRequest
            {
				channel = "01",
				messageSheetID = "E01",
				messageSheetName = "信用卡",
				displayOrder = 1,
				isEnable = True,
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspMessageSheetUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            MessageSheetService MessageSheetService = new
                MessageSheetService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await MessageSheetService.UpdateMessageSheet(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }




    }
}
