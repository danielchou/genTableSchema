using AutoMapper;
using ESUN.AGD.DataAccess.DataService.DataAccess;
using ESUN.AGD.DataAccess.Type;
using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.Common;
using ESUN.AGD.WebApi.Application.Txn;
using ESUN.AGD.WebApi.Application.Txn.Contract;
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


namespace ESUN.AGD.WebApi.Test.TxnTest
{
    [TestFixture]
    public class TestTxnService
    {
        private static IDataAccessService stubDataAccessService = Substitute.For<IDataAccessService>();
        private static IGetTokenService stubGetTokenService = Substitute.For<IGetTokenService>();
        private static IHttpContextAccessor stubHttpContextAccessor = Substitute.For<IHttpContextAccessor>();
        private static IMapper stubMapper = Substitute.For<IMapper>();
        private static IUserService stubUserService = Substitute.For<IUserService>();
        private static IAutoNextNumService autoNextNumService = Substitute.For<IAutoNextNumService>();

        [Test]
        public async ValueTask GetTxnTest()
        {  //arrange
            int seqNo = 1;
            string resCode = "U200";
           
            stubDataAccessService
                .LoadSingleData<TbTxn, object>("agdSp.uspTxnGet", Arg.Any<int>())
                .ReturnsForAnyArgs(new TbTxn { SeqNo = 1 });

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            TxnService TxnService = 
                new TxnService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await TxnService.GetTxn(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspTxnGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode,resCode);

        }

        [Test]
        public async ValueTask GetTxnTestFailed()
        {  //arrange
            int seqNo = 0;
            string resCode = "U999";
          
            stubDataAccessService.LoadSingleData<TbTxn, object>("agdSp.uspTxnGet", Arg.Any<int>())
                .ReturnsNull();          

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            TxnService TxnService = new TxnService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await TxnService.GetTxn(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<TbTxn, object>("agdSp.uspTxnGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryTxnTest()
        {  //arrange
            var queryReq = new TxnQueryRequest { 						txnName = "警示帳戶註記",
						isEnable = False, };
            string resCode = "U200";
            
            stubDataAccessService.LoadData<TbTxn, object>
                (storeProcedure: "agdSp.uspTxnQuery", Arg.Any<object>())
               .Returns(new TbTxn[] { 
                    new TbTxn { 
						txnName = "警示帳戶註記",
						isEnable = False,
                        Total = 2
                    },
                    new TbTxn { } 
                }.AsQueryable());
            
            stubHttpContextAccessor.HttpContext.Request.Path ="/api/Txn/query";
            
            TxnService TxnService = new 
                TxnService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await TxnService.QueryTxn(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
          
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryTxnTestFailed()
        {  //arrange
            var queryReq = new TxnQueryRequest {  
						txnName = "警示帳戶註記",
						isEnable = False,
            };
            string resCode = "U999";
            
            stubDataAccessService.LoadData<TbTxn, object>("agdSp.uspTxnQuery", Arg.Any<object>())
                .ReturnsNull();
            
            stubHttpContextAccessor.HttpContext.Request.Path = "/api/Txn/query";
            TxnService TxnService = new TxnService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await TxnService.QueryTxn(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspTxnGet", Arg.Any<object>());
            Assert.AreEqual(result.total, 0);

        }

        [Test]
        public async ValueTask InsertTxnTest()
        {  //arrange
            var insertReq = new TxnInsertRequest
            {
				txnID = "TxnAccident",
				txnName = "警示帳戶註記",
				txnScript = "<span style="background-color: rgb(0, 0, 0);"><font color="#ffffff" size="7">asf一二三</font></span>",
				displayOrder = 6,
				isEnable = False,
				creator = "admin",
				creatorName = "林管理",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspTxnInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result:new SqlResponse() { ErrorMsg="",Msg="" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            TxnService TxnService = new
                TxnService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await TxnService.InsertTxn(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask InsertTxnTestFailed()
        {  //arrange
            var insertReq = new TxnInsertRequest
            {
				txnID = "TxnAccident",
				txnName = "警示帳戶註記",
				txnScript = "<span style="background-color: rgb(0, 0, 0);"><font color="#ffffff" size="7">asf一二三</font></span>",
				displayOrder = 6,
				isEnable = False,
				creator = "admin",
				creatorName = "林管理",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspTxnInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            TxnService TxnService = new
                TxnService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await TxnService.InsertTxn(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }


        [Test]
        public async ValueTask UpdateTxnTest()
        {  //arrange
            var updateReq = new TxnUpdateRequest
            {
				txnID = "TxnNoSale",
				txnName = "不行銷註記",
				txnScript = "<p class="MsoNormal" align="center" style="text-align:center;mso-pagination:widow-orphan"><b><span style="font-size:14.0pt;font-family:&quot;新細明體&quot;,serif;mso-ascii-font-family:Verdana;
mso-hansi-font-family:Verdana;mso-bidi-font-family:新細明體;color:#4A598C;
mso-font-kerning:0pt">不行銷註記</span></b></p>

<p class="MsoNormal"><span style="font-size:
16.0pt;mso-bidi-font-size:12.0pt;font-family:標楷體;mso-bidi-font-family:新細明體;
color:#0000CC;mso-font-kerning:0pt">不行銷註記注意事項：<span lang="EN-US"><o:p></o:p></span></span></p><p class="MsoNormal"><ol><li><font color="#0000cc" face="標楷體" size="4">aaaaa</font></li><li><span style="color: rgb(0, 0, 204); font-family: 標楷體; font-size: large;">bbbbb</span><br></li><li><span style="color: rgb(0, 0, 204); font-family: 標楷體; font-size: large;">ccccccc</span><br></li></ol></p>",
				displayOrder = 2,
				isEnable = True,
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspTxnUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            TxnService TxnService = new
                TxnService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await TxnService.UpdateTxn(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask UpdateTxnTestFailed()
        {  //arrange
            var updateReq = new TxnUpdateRequest
            {
				txnID = "TxnNoSale",
				txnName = "不行銷註記",
				txnScript = "<p class="MsoNormal" align="center" style="text-align:center;mso-pagination:widow-orphan"><b><span style="font-size:14.0pt;font-family:&quot;新細明體&quot;,serif;mso-ascii-font-family:Verdana;
mso-hansi-font-family:Verdana;mso-bidi-font-family:新細明體;color:#4A598C;
mso-font-kerning:0pt">不行銷註記</span></b></p>

<p class="MsoNormal"><span style="font-size:
16.0pt;mso-bidi-font-size:12.0pt;font-family:標楷體;mso-bidi-font-family:新細明體;
color:#0000CC;mso-font-kerning:0pt">不行銷註記注意事項：<span lang="EN-US"><o:p></o:p></span></span></p><p class="MsoNormal"><ol><li><font color="#0000cc" face="標楷體" size="4">aaaaa</font></li><li><span style="color: rgb(0, 0, 204); font-family: 標楷體; font-size: large;">bbbbb</span><br></li><li><span style="color: rgb(0, 0, 204); font-family: 標楷體; font-size: large;">ccccccc</span><br></li></ol></p>",
				displayOrder = 2,
				isEnable = True,
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspTxnUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            TxnService TxnService = new
                TxnService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await TxnService.UpdateTxn(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }




    }
}
