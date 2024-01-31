using AutoMapper;
using ESUN.AGD.DataAccess.DataService.DataAccess;
using ESUN.AGD.DataAccess.Type;
using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.Common;
using ESUN.AGD.WebApi.Application.MessageTemplate;
using ESUN.AGD.WebApi.Application.MessageTemplate.Contract;
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


namespace ESUN.AGD.WebApi.Test.MessageTemplateTest
{
    [TestFixture]
    public class TestMessageTemplateService
    {
        private static IDataAccessService stubDataAccessService = Substitute.For<IDataAccessService>();
        private static IGetTokenService stubGetTokenService = Substitute.For<IGetTokenService>();
        private static IHttpContextAccessor stubHttpContextAccessor = Substitute.For<IHttpContextAccessor>();
        private static IMapper stubMapper = Substitute.For<IMapper>();
        private static IUserService stubUserService = Substitute.For<IUserService>();
        private static IAutoNextNumService autoNextNumService = Substitute.For<IAutoNextNumService>();

        [Test]
        public async ValueTask GetMessageTemplateTest()
        {  //arrange
            int seqNo = 1;
            string resCode = "U200";
           
            stubDataAccessService
                .LoadSingleData<TbMessageTemplate, object>("agdSp.uspMessageTemplateGet", Arg.Any<int>())
                .ReturnsForAnyArgs(new TbMessageTemplate { SeqNo = 1 });

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            MessageTemplateService MessageTemplateService = 
                new MessageTemplateService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await MessageTemplateService.GetMessageTemplate(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspMessageTemplateGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode,resCode);

        }

        [Test]
        public async ValueTask GetMessageTemplateTestFailed()
        {  //arrange
            int seqNo = 0;
            string resCode = "U999";
          
            stubDataAccessService.LoadSingleData<TbMessageTemplate, object>("agdSp.uspMessageTemplateGet", Arg.Any<int>())
                .ReturnsNull();          

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            MessageTemplateService MessageTemplateService = new MessageTemplateService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await MessageTemplateService.GetMessageTemplate(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<TbMessageTemplate, object>("agdSp.uspMessageTemplateGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryMessageTemplateTest()
        {  //arrange
            var queryReq = new MessageTemplateQueryRequest { 						messageTemplateID = 16,
						messageTemplateName = "XXXX",
						eventCode = "asdf",
						eventName = "敬邀申辦大江聯名卡",
						channel = "01",
						templateStatus = "on",
						templateBeginDT = "2022-08-01",
						templateEndDT = "2023-08-31",
						isEnable = True, };
            string resCode = "U200";
            
            stubDataAccessService.LoadData<TbMessageTemplate, object>
                (storeProcedure: "agdSp.uspMessageTemplateQuery", Arg.Any<object>())
               .Returns(new TbMessageTemplate[] { 
                    new TbMessageTemplate { 
						messageTemplateID = 16,
						messageTemplateName = "XXXX",
						eventCode = "asdf",
						eventName = "敬邀申辦大江聯名卡",
						channel = "01",
						templateStatus = "on",
						templateBeginDT = "2022-08-01",
						templateEndDT = "2023-08-31",
						isEnable = True,
                        Total = 2
                    },
                    new TbMessageTemplate { } 
                }.AsQueryable());
            
            stubHttpContextAccessor.HttpContext.Request.Path ="/api/MessageTemplate/query";
            
            MessageTemplateService MessageTemplateService = new 
                MessageTemplateService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await MessageTemplateService.QueryMessageTemplate(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
          
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryMessageTemplateTestFailed()
        {  //arrange
            var queryReq = new MessageTemplateQueryRequest {  
						messageTemplateID = 16,
						messageTemplateName = "XXXX",
						eventCode = "asdf",
						eventName = "敬邀申辦大江聯名卡",
						channel = "01",
						templateStatus = "on",
						templateBeginDT = "2022-08-01",
						templateEndDT = "2023-08-31",
						isEnable = True,
            };
            string resCode = "U999";
            
            stubDataAccessService.LoadData<TbMessageTemplate, object>("agdSp.uspMessageTemplateQuery", Arg.Any<object>())
                .ReturnsNull();
            
            stubHttpContextAccessor.HttpContext.Request.Path = "/api/MessageTemplate/query";
            MessageTemplateService MessageTemplateService = new MessageTemplateService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await MessageTemplateService.QueryMessageTemplate(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspMessageTemplateGet", Arg.Any<object>());
            Assert.AreEqual(result.total, 0);

        }

        [Test]
        public async ValueTask InsertMessageTemplateTest()
        {  //arrange
            var insertReq = new MessageTemplateInsertRequest
            {
				messageTemplateID = 16,
				messageTemplateName = "XXXX",
				eventCode = "asdf",
				eventName = "敬邀申辦大江聯名卡",
				channel = "01",
				templateStatus = "on",
				subject = "敬邀申辦大江聯名卡",
				content = "<!DOCTYPE html>
<html>
<head>
</head>
<body>
<p>親愛的顧客您好：<br />誠摯邀請您申辦 玉山大江聯名卡，提供相關活動資訊給您，請透過下面連結線上申辦即可，謝謝您。<br /><p>https://www.esunbank.com.tw/
</p>
</body>
</html>",
				templateBeginDT = "2022-08-01",
				templateEndDT = "2023-08-31",
				isEnable = True,
				creator = "admin",
				creatorName = "林管理",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspMessageTemplateInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result:new SqlResponse() { ErrorMsg="",Msg="" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            MessageTemplateService MessageTemplateService = new
                MessageTemplateService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await MessageTemplateService.InsertMessageTemplate(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask InsertMessageTemplateTestFailed()
        {  //arrange
            var insertReq = new MessageTemplateInsertRequest
            {
				messageTemplateID = 16,
				messageTemplateName = "XXXX",
				eventCode = "asdf",
				eventName = "敬邀申辦大江聯名卡",
				channel = "01",
				templateStatus = "on",
				subject = "敬邀申辦大江聯名卡",
				content = "<!DOCTYPE html>
<html>
<head>
</head>
<body>
<p>親愛的顧客您好：<br />誠摯邀請您申辦 玉山大江聯名卡，提供相關活動資訊給您，請透過下面連結線上申辦即可，謝謝您。<br /><p>https://www.esunbank.com.tw/
</p>
</body>
</html>",
				templateBeginDT = "2022-08-01",
				templateEndDT = "2023-08-31",
				isEnable = True,
				creator = "admin",
				creatorName = "林管理",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspMessageTemplateInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            MessageTemplateService MessageTemplateService = new
                MessageTemplateService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await MessageTemplateService.InsertMessageTemplate(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }


        [Test]
        public async ValueTask UpdateMessageTemplateTest()
        {  //arrange
            var updateReq = new MessageTemplateUpdateRequest
            {
				messageTemplateID = 16,
				messageTemplateName = "XXXX",
				eventCode = "asdf",
				eventName = "敬邀申辦大江聯名卡",
				channel = "01",
				templateStatus = "on",
				subject = "敬邀申辦大江聯名卡",
				content = "<!DOCTYPE html>
<html>
<head>
</head>
<body>
<p>親愛的顧客您好：<br />誠摯邀請您申辦 玉山大江聯名卡，提供相關活動資訊給您，請透過下面連結線上申辦即可，謝謝您。<br /><p>https://www.esunbank.com.tw/
</p>
</body>
</html>",
				templateBeginDT = "2022-08-01",
				templateEndDT = "2023-08-31",
				isEnable = True,
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspMessageTemplateUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            MessageTemplateService MessageTemplateService = new
                MessageTemplateService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await MessageTemplateService.UpdateMessageTemplate(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask UpdateMessageTemplateTestFailed()
        {  //arrange
            var updateReq = new MessageTemplateUpdateRequest
            {
				messageTemplateID = 16,
				messageTemplateName = "XXXX",
				eventCode = "asdf",
				eventName = "敬邀申辦大江聯名卡",
				channel = "01",
				templateStatus = "on",
				subject = "敬邀申辦大江聯名卡",
				content = "<!DOCTYPE html>
<html>
<head>
</head>
<body>
<p>親愛的顧客您好：<br />誠摯邀請您申辦 玉山大江聯名卡，提供相關活動資訊給您，請透過下面連結線上申辦即可，謝謝您。<br /><p>https://www.esunbank.com.tw/
</p>
</body>
</html>",
				templateBeginDT = "2022-08-01",
				templateEndDT = "2023-08-31",
				isEnable = True,
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspMessageTemplateUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            MessageTemplateService MessageTemplateService = new
                MessageTemplateService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await MessageTemplateService.UpdateMessageTemplate(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }




    }
}
