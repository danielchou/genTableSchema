using AutoMapper;
using ESUN.AGD.DataAccess.DataService.DataAccess;
using ESUN.AGD.DataAccess.Type;
using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.Common;
using ESUN.AGD.WebApi.Application.Recording;
using ESUN.AGD.WebApi.Application.Recording.Contract;
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


namespace ESUN.AGD.WebApi.Test.RecordingTest
{
    [TestFixture]
    public class TestRecordingService
    {
        private static IDataAccessService stubDataAccessService = Substitute.For<IDataAccessService>();
        private static IGetTokenService stubGetTokenService = Substitute.For<IGetTokenService>();
        private static IHttpContextAccessor stubHttpContextAccessor = Substitute.For<IHttpContextAccessor>();
        private static IMapper stubMapper = Substitute.For<IMapper>();
        private static IUserService stubUserService = Substitute.For<IUserService>();
        private static IAutoNextNumService autoNextNumService = Substitute.For<IAutoNextNumService>();

        [Test]
        public async ValueTask GetRecordingTest()
        {  //arrange
            int seqNo = 1;
            string resCode = "U200";
           
            stubDataAccessService
                .LoadSingleData<TbRecording, object>("agdSp.uspRecordingGet", Arg.Any<int>())
                .ReturnsForAnyArgs(new TbRecording { SeqNo = 1 });

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            RecordingService RecordingService = 
                new RecordingService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await RecordingService.GetRecording(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspRecordingGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode,resCode);

        }

        [Test]
        public async ValueTask GetRecordingTestFailed()
        {  //arrange
            int seqNo = 0;
            string resCode = "U999";
          
            stubDataAccessService.LoadSingleData<TbRecording, object>("agdSp.uspRecordingGet", Arg.Any<int>())
                .ReturnsNull();          

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            RecordingService RecordingService = new RecordingService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await RecordingService.GetRecording(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<TbRecording, object>("agdSp.uspRecordingGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryRecordingTest()
        {  //arrange
            var queryReq = new RecordingQueryRequest { 						uUID = "002LO75ULK9C531I734VM2LAES0001E5",
						connID = "2222",
						protocol = "voice",
						callType = "Inbound",
						phoneNumber = "99999999",
						agentLoginID = "1111",
						skill = "Skill_VVIP_Card_DEV",
						direction = "1",
						duration = 0,
						recordingID = "None",
						memoUserID = "None",
						memoUserName = "None",
						creator = "admin",
						creatorName = "林管理", };
            string resCode = "U200";
            
            stubDataAccessService.LoadData<TbRecording, object>
                (storeProcedure: "agdSp.uspRecordingQuery", Arg.Any<object>())
               .Returns(new TbRecording[] { 
                    new TbRecording { 
						uUID = "002LO75ULK9C531I734VM2LAES0001E5",
						connID = "2222",
						protocol = "voice",
						callType = "Inbound",
						phoneNumber = "99999999",
						agentLoginID = "1111",
						skill = "Skill_VVIP_Card_DEV",
						direction = "1",
						duration = 0,
						recordingID = "None",
						memoUserID = "None",
						memoUserName = "None",
						creator = "admin",
						creatorName = "林管理",
                        Total = 2
                    },
                    new TbRecording { } 
                }.AsQueryable());
            
            stubHttpContextAccessor.HttpContext.Request.Path ="/api/Recording/query";
            
            RecordingService RecordingService = new 
                RecordingService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await RecordingService.QueryRecording(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
          
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryRecordingTestFailed()
        {  //arrange
            var queryReq = new RecordingQueryRequest {  
						uUID = "002LO75ULK9C531I734VM2LAES0001E5",
						connID = "2222",
						protocol = "voice",
						callType = "Inbound",
						phoneNumber = "99999999",
						agentLoginID = "1111",
						skill = "Skill_VVIP_Card_DEV",
						direction = "1",
						duration = 0,
						recordingID = "None",
						memoUserID = "None",
						memoUserName = "None",
						creator = "admin",
						creatorName = "林管理",
            };
            string resCode = "U999";
            
            stubDataAccessService.LoadData<TbRecording, object>("agdSp.uspRecordingQuery", Arg.Any<object>())
                .ReturnsNull();
            
            stubHttpContextAccessor.HttpContext.Request.Path = "/api/Recording/query";
            RecordingService RecordingService = new RecordingService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await RecordingService.QueryRecording(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspRecordingGet", Arg.Any<object>());
            Assert.AreEqual(result.total, 0);

        }

        [Test]
        public async ValueTask InsertRecordingTest()
        {  //arrange
            var insertReq = new RecordingInsertRequest
            {
				contactID = 10,
				uUID = "002LO75ULK9C531I734VM2LAES0001E5",
				connID = "2222",
				protocol = "voice",
				callType = "Inbound",
				phoneNumber = "99999999",
				agentLoginID = "1111",
				skill = "Skill_VVIP_Card_DEV",
				direction = "1",
				ivrStartDT = "None",
				ivrTransferDT = "None",
				queueTime = "15:12:26",
				beginDT = "2022-08-03 15:12:26",
				beginTime = "15:12:26",
				endDT = "None",
				duration = 0,
				recordingID = "None",
				memo = "None",
				memoDT = "None",
				memoUserID = "None",
				memoUserName = "None",
				creator = "admin",
				creatorName = "林管理",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspRecordingInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result:new SqlResponse() { ErrorMsg="",Msg="" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            RecordingService RecordingService = new
                RecordingService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await RecordingService.InsertRecording(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask InsertRecordingTestFailed()
        {  //arrange
            var insertReq = new RecordingInsertRequest
            {
				contactID = 10,
				uUID = "002LO75ULK9C531I734VM2LAES0001E5",
				connID = "2222",
				protocol = "voice",
				callType = "Inbound",
				phoneNumber = "99999999",
				agentLoginID = "1111",
				skill = "Skill_VVIP_Card_DEV",
				direction = "1",
				ivrStartDT = "None",
				ivrTransferDT = "None",
				queueTime = "15:12:26",
				beginDT = "2022-08-03 15:12:26",
				beginTime = "15:12:26",
				endDT = "None",
				duration = 0,
				recordingID = "None",
				memo = "None",
				memoDT = "None",
				memoUserID = "None",
				memoUserName = "None",
				creator = "admin",
				creatorName = "林管理",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspRecordingInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            RecordingService RecordingService = new
                RecordingService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await RecordingService.InsertRecording(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }


        [Test]
        public async ValueTask UpdateRecordingTest()
        {  //arrange
            var updateReq = new RecordingUpdateRequest
            {
				contactID = 63,
				uUID = "00BOL97QPSES12438TK1TB5AES0000D4",
				connID = "006F02EBA99E9246",
				protocol = "Voice",
				callType = "Inbound",
				phoneNumber = "0932111111",
				agentLoginID = "3641",
				skill = "",
				direction = "1",
				ivrStartDT = "None",
				ivrTransferDT = "None",
				queueTime = "None",
				beginDT = "2022-10-22 00:21:17",
				beginTime = "00:21:17",
				endDT = "2022-10-22 00:24:35",
				duration = 198,
				recordingID = "None",
				memo = "None",
				memoDT = "None",
				memoUserID = "None",
				memoUserName = "None",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspRecordingUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            RecordingService RecordingService = new
                RecordingService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await RecordingService.UpdateRecording(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask UpdateRecordingTestFailed()
        {  //arrange
            var updateReq = new RecordingUpdateRequest
            {
				contactID = 63,
				uUID = "00BOL97QPSES12438TK1TB5AES0000D4",
				connID = "006F02EBA99E9246",
				protocol = "Voice",
				callType = "Inbound",
				phoneNumber = "0932111111",
				agentLoginID = "3641",
				skill = "",
				direction = "1",
				ivrStartDT = "None",
				ivrTransferDT = "None",
				queueTime = "None",
				beginDT = "2022-10-22 00:21:17",
				beginTime = "00:21:17",
				endDT = "2022-10-22 00:24:35",
				duration = 198,
				recordingID = "None",
				memo = "None",
				memoDT = "None",
				memoUserID = "None",
				memoUserName = "None",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspRecordingUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            RecordingService RecordingService = new
                RecordingService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await RecordingService.UpdateRecording(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }




    }
}
