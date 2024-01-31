using AutoMapper;
using ESUN.AGD.DataAccess.DataService.DataAccess;
using ESUN.AGD.DataAccess.Type;
using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.Common;
using ESUN.AGD.WebApi.Application.MealScript;
using ESUN.AGD.WebApi.Application.MealScript.Contract;
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


namespace ESUN.AGD.WebApi.Test.MealScriptTest
{
    [TestFixture]
    public class TestMealScriptService
    {
        private static IDataAccessService stubDataAccessService = Substitute.For<IDataAccessService>();
        private static IGetTokenService stubGetTokenService = Substitute.For<IGetTokenService>();
        private static IHttpContextAccessor stubHttpContextAccessor = Substitute.For<IHttpContextAccessor>();
        private static IMapper stubMapper = Substitute.For<IMapper>();
        private static IUserService stubUserService = Substitute.For<IUserService>();
        private static IAutoNextNumService autoNextNumService = Substitute.For<IAutoNextNumService>();

        [Test]
        public async ValueTask GetMealScriptTest()
        {  //arrange
            int seqNo = 1;
            string resCode = "U200";
           
            stubDataAccessService
                .LoadSingleData<TbMealScript, object>("agdSp.uspMealScriptGet", Arg.Any<int>())
                .ReturnsForAnyArgs(new TbMealScript { SeqNo = 1 });

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            MealScriptService MealScriptService = 
                new MealScriptService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await MealScriptService.GetMealScript(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspMealScriptGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode,resCode);

        }

        [Test]
        public async ValueTask GetMealScriptTestFailed()
        {  //arrange
            int seqNo = 0;
            string resCode = "U999";
          
            stubDataAccessService.LoadSingleData<TbMealScript, object>("agdSp.uspMealScriptGet", Arg.Any<int>())
                .ReturnsNull();          

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            MealScriptService MealScriptService = new MealScriptService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await MealScriptService.GetMealScript(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<TbMealScript, object>("agdSp.uspMealScriptGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryMealScriptTest()
        {  //arrange
            var queryReq = new MealScriptQueryRequest { 						scriptID = "MS01", };
            string resCode = "U200";
            
            stubDataAccessService.LoadData<TbMealScript, object>
                (storeProcedure: "agdSp.uspMealScriptQuery", Arg.Any<object>())
               .Returns(new TbMealScript[] { 
                    new TbMealScript { 
						scriptID = "MS01",
                        Total = 2
                    },
                    new TbMealScript { } 
                }.AsQueryable());
            
            stubHttpContextAccessor.HttpContext.Request.Path ="/api/MealScript/query";
            
            MealScriptService MealScriptService = new 
                MealScriptService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await MealScriptService.QueryMealScript(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
          
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryMealScriptTestFailed()
        {  //arrange
            var queryReq = new MealScriptQueryRequest {  
						scriptID = "MS01",
            };
            string resCode = "U999";
            
            stubDataAccessService.LoadData<TbMealScript, object>("agdSp.uspMealScriptQuery", Arg.Any<object>())
                .ReturnsNull();
            
            stubHttpContextAccessor.HttpContext.Request.Path = "/api/MealScript/query";
            MealScriptService MealScriptService = new MealScriptService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await MealScriptService.QueryMealScript(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspMealScriptGet", Arg.Any<object>());
            Assert.AreEqual(result.total, 0);

        }

        [Test]
        public async ValueTask InsertMealScriptTest()
        {  //arrange
            var insertReq = new MealScriptInsertRequest
            {
				scriptID = "MS01",
				content = "<div align="center">

<table class="MsoTableGrid" border="1" cellspacing="0" cellpadding="0" width="90%" style="width:90.24%;border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;
 mso-yfti-tbllook:1184;mso-padding-alt:0cm 5.4pt 0cm 5.4pt">
 <tbody><tr>
  <td width="6%" valign="top" style="width:6.16%;border:solid windowtext 1.0pt;
  mso-border-alt:solid windowtext .5pt;background:#F2F2F2;mso-background-themecolor:
  background1;mso-background-themeshade:242;padding:0cm 5.4pt 0cm 5.4pt">
  <p class="2" align="center" style="margin-top:2.4pt;margin-right:0cm;margin-bottom:
  0cm;margin-left:0cm;margin-bottom:.0001pt;text-align:center;text-indent:0cm"><b><span style="font-family:標楷體;mso-ascii-font-family:
  &quot;Times New Roman&quot;;mso-hansi-font-family:&quot;Times New Roman&quot;;mso-bidi-font-family:
  &quot;Times New Roman&quot;">編號</span></b><b><span lang="EN-US"><o:p></o:p></span></b></p>
  </td>
  <td width="20%" valign="top" style="width:20.0%;border:solid windowtext 1.0pt;
  border-left:none;mso-border-left-alt:solid windowtext .5pt;mso-border-alt:
  solid windowtext .5pt;background:#F2F2F2;mso-background-themecolor:background1;
  mso-background-themeshade:242;padding:0cm 5.4pt 0cm 5.4pt">
  <p class="2" align="center" style="margin-top:2.4pt;margin-right:0cm;margin-bottom:
  0cm;margin-left:0cm;margin-bottom:.0001pt;text-align:center;text-indent:0cm"><b><span style="font-family: 標楷體;">欄位名稱</span></b><b><span lang="EN-US"><o:p></o:p></span></b></p>
  </td>
  <td width="15%" valign="top" style="width:15.42%;border:solid windowtext 1.0pt;
  border-left:none;mso-border-left-alt:solid windowtext .5pt;mso-border-alt:
  solid windowtext .5pt;background:#F2F2F2;mso-background-themecolor:background1;
  mso-background-themeshade:242;padding:0cm 5.4pt 0cm 5.4pt">
  <p class="2" align="center" style="margin-top:2.4pt;margin-right:0cm;margin-bottom:
  0cm;margin-left:0cm;margin-bottom:.0001pt;text-align:center;text-indent:0cm"><b><span style="font-family: 標楷體;">欄位種類</span></b><b><span lang="EN-US"><o:p></o:p></span></b></p>
  </td>
  <td width="10%" valign="top" style="width:10.8%;border:solid windowtext 1.0pt;
  border-left:none;mso-border-left-alt:solid windowtext .5pt;mso-border-alt:
  solid windowtext .5pt;background:#F2F2F2;mso-background-themecolor:background1;
  mso-background-themeshade:242;padding:0cm 5.4pt 0cm 5.4pt">
  <p class="2" align="center" style="margin-top:2.4pt;margin-right:0cm;margin-bottom:
  0cm;margin-left:0cm;margin-bottom:.0001pt;text-align:center;text-indent:0cm"><b><span style="font-family: 標楷體;">輸入必要欄位</span></b><b><span lang="EN-US"><o:p></o:p></span></b></p>
  </td>
  <td width="47%" valign="top" style="width:47.64%;border:solid windowtext 1.0pt;
  border-left:none;mso-border-left-alt:solid windowtext .5pt;mso-border-alt:
  solid windowtext .5pt;background:#F2F2F2;mso-background-themecolor:background1;
  mso-background-themeshade:242;padding:0cm 5.4pt 0cm 5.4pt">
  <p class="2" align="center" style="margin-top:2.4pt;margin-right:0cm;margin-bottom:
  0cm;margin-left:0cm;margin-bottom:.0001pt;text-align:center;text-indent:0cm"><b><span style="font-family: 標楷體;">說明</span></b><b><span lang="EN-US"><o:p></o:p></span></b></p>
  </td>
 </tr>
 <tr>
  <td width="6%" valign="top" style="width:6.16%;border:solid windowtext 1.0pt;
  border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0cm 5.4pt 0cm 5.4pt">
  <p class="2" align="center" style="text-align: right; margin: 2.4pt 0cm 0.0001pt; text-indent: 0cm;"></p><ol><li><span lang="EN-US"><br></span></li><li><span lang="EN-US">&nbsp;</span><!--[endif]--><span lang="EN-US">&nbsp;</span></li></ol><!--[if !supportLists]--><p></p>
  </td>
  <td width="20%" valign="top" style="width:20.0%;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
  <h4 style="margin-top:2.4pt;margin-right:0cm;margin-bottom:0cm;
  margin-left:0cm;margin-bottom:.0001pt;text-indent:0cm"><span style="font-family:標楷體;mso-ascii-font-family:&quot;Times New Roman&quot;;mso-hansi-font-family:
  &quot;Times New Roman&quot;">班表資</span><font face="Times New Roman">訊<span lang="EN-US">-</span>用餐時段</font><span style="font-family:標楷體;mso-ascii-font-family:&quot;Times New Roman&quot;;mso-hansi-font-family:
  &quot;Times New Roman&quot;"><font size="1">說明</font>文案</span></h4>
  </td>
  <td width="15%" valign="top" style="width:15.42%;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt"><ul><li><span style="font-family:標楷體;mso-ascii-font-family:&quot;Times New Roman&quot;;mso-hansi-font-family:
  &quot;Times New Roman&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">文<b><u>字</u></b></span></li><li><font color="#ff0000" style="letter-spacing: 0.06em;">輸<span style="background-color: rgba(0, 255, 0, 0.67);">入</span></font><span style="letter-spacing: 0.06em; background-color: rgba(0, 255, 0, 0.67);">框</span></li></ul>
  </td>
  <td width="10%" valign="top" style="width:10.8%;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
  <p class="2" align="center" style="margin-top:2.4pt;margin-right:0cm;margin-bottom:
  0cm;margin-left:0cm;margin-bottom:.0001pt;text-align:center;text-indent:0cm"><span lang="EN-US"><b>Y</b><o:p></o:p></span></p>
  </td>
  <td width="47%" valign="top" style="width:47.64%;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
  <p class="2" style="margin-top:2.4pt;margin-right:0cm;margin-bottom:0cm;
  margin-left:0cm;margin-bottom:.0001pt;text-indent:0cm"><span style="font-family:標楷體;mso-ascii-font-family:&quot;Times New Roman&quot;;mso-hansi-font-family:
  &quot;Times New Roman&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;"><i><u>自由</u>輸入</i>，採用<strike>記事本相同工具</strike></span><span lang="EN-US">(</span><span style="font-family:標楷體;mso-ascii-font-family:&quot;Times New Roman&quot;;mso-hansi-font-family:
  &quot;Times New Roman&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">如字體格式</span><span lang="EN-US">/</span><span style="font-family:標楷體;mso-ascii-font-family:&quot;Times New Roman&quot;;mso-hansi-font-family:
  &quot;Times New Roman&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">顏色等</span><span lang="EN-US">) </span><span style="font-family:標楷體;mso-ascii-font-family:&quot;Times New Roman&quot;;mso-hansi-font-family:
  &quot;Times New Roman&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">編輯</span></p><hr><p class="2" style="margin-top:2.4pt;margin-right:0cm;margin-bottom:0cm;
  margin-left:0cm;margin-bottom:.0001pt;text-indent:0cm"><span style="font-family:標楷體;mso-ascii-font-family:&quot;Times New Roman&quot;;mso-hansi-font-family:
  &quot;Times New Roman&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">，前台<span style="background-color: rgb(0, 0, 0);"><b><font color="#ffffff">顯示相</font></b></span>同格式內容。</span><span lang="EN-US"><o:p></o:p></span></p>
  </td>
 </tr>
</tbody></table><table class="MsoTableGrid" border="1" cellspacing="0" cellpadding="0" width="90%" style="letter-spacing: 0.96px; width: 1369.83px; border-collapse: collapse; border: none;"><tbody><tr><td width="6%" valign="top" style="width: 84.2969px; border: 1pt solid windowtext; background: rgb(242, 242, 242); padding: 0cm 5.4pt;"><p class="2" align="center" style="margin: 2.4pt 0cm 0.0001pt; text-align: center; text-indent: 0cm;"><b><span style="font-family: 標楷體;">編號</span></b><b><span lang="EN-US"><o:p></o:p></span></b></p></td><td width="20%" valign="top" style="width: 273.703px; border-top: 1pt solid windowtext; border-right: 1pt solid windowtext; border-bottom: 1pt solid windowtext; border-image: initial; border-left: none; background: rgb(242, 242, 242); padding: 0cm 5.4pt;"><p class="2" align="center" style="margin: 2.4pt 0cm 0.0001pt; text-align: center; text-indent: 0cm;"><b><span style="font-family: 標楷體;">欄位名稱</span></b><b><span lang="EN-US"><o:p></o:p></span></b></p></td><td width="15%" valign="top" style="width: 211.016px; border-top: 1pt solid windowtext; border-right: 1pt solid windowtext; border-bottom: 1pt solid windowtext; border-image: initial; border-left: none; background: rgb(242, 242, 242); padding: 0cm 5.4pt;"><p class="2" align="center" style="margin: 2.4pt 0cm 0.0001pt; text-align: center; text-indent: 0cm;"><b><span style="font-family: 標楷體;">欄位種類</span></b><b><span lang="EN-US"><o:p></o:p></span></b></p></td><td width="10%" valign="top" style="width: 147.797px; border-top: 1pt solid windowtext; border-right: 1pt solid windowtext; border-bottom: 1pt solid windowtext; border-image: initial; border-left: none; background: rgb(242, 242, 242); padding: 0cm 5.4pt;"><p class="2" align="center" style="margin: 2.4pt 0cm 0.0001pt; text-align: center; text-indent: 0cm;"><b><span style="font-family: 標楷體;">輸入必要欄位</span></b><b><span lang="EN-US"><o:p></o:p></span></b></p></td><td width="47%" valign="top" style="width: 651.703px; border-top: 1pt solid windowtext; border-right: 1pt solid windowtext; border-bottom: 1pt solid windowtext; border-image: initial; border-left: none; background: rgb(242, 242, 242); padding: 0cm 5.4pt;"><p class="2" align="center" style="margin: 2.4pt 0cm 0.0001pt; text-align: center; text-indent: 0cm;"><b><span style="font-family: 標楷體;">說明</span></b><b><span lang="EN-US"><o:p></o:p></span></b></p></td></tr><tr><td width="6%" valign="top" style="width: 84.2969px; border-right: 1pt solid windowtext; border-bottom: 1pt solid windowtext; border-left: 1pt solid windowtext; border-image: initial; border-top: none; padding: 0cm 5.4pt;"><p class="2" align="center" style="margin: 2.4pt 0cm 0.0001pt; text-align: right; text-indent: 0cm;"></p><ol><li><span lang="EN-US">.&nbsp;</span><span lang="EN-US">&nbsp;</span></li></ol><p></p></td><td width="20%" valign="top" style="width: 273.703px; border-top: none; border-left: none; border-bottom: 1pt solid windowtext; border-right: 1pt solid windowtext; padding: 0cm 5.4pt;"><h4 style="margin: 2.4pt 0cm 0.0001pt; text-indent: 0cm;"><span style="font-family: 標楷體;">班表資</span><font face="Times New Roman">訊<span lang="EN-US">-</span>用餐時段</font><span style="font-family: 標楷體;"><font size="1">說明</font>文案</span></h4></td><td width="15%" valign="top" style="width: 211.016px; border-top: none; border-left: none; border-bottom: 1pt solid windowtext; border-right: 1pt solid windowtext; padding: 0cm 5.4pt;"><ul><li><span style="font-family: 標楷體;">文字</span></li></ul><ol><li><font color="#ff0000" style="background-color: rgba(255, 128, 0, 0.67);">輸入</font><span style="background-color: rgba(0, 255, 0, 0.67);">框</span></li></ol></td><td width="10%" valign="top" style="width: 147.797px; border-top: none; border-left: none; border-bottom: 1pt solid windowtext; border-right: 1pt solid windowtext; padding: 0cm 5.4pt;"><p class="2" align="center" style="margin: 2.4pt 0cm 0.0001pt; text-align: center; text-indent: 0cm;"><span lang="EN-US"><b>Y</b><o:p></o:p></span></p></td><td width="47%" valign="top" style="width: 651.703px; border-top: none; border-left: none; border-bottom: 1pt solid windowtext; border-right: 1pt solid windowtext; padding: 0cm 5.4pt;"><p class="2" style="margin: 2.4pt 0cm 0.0001pt; text-indent: 0cm;"><span style="font-family: 標楷體;"><i><u>自由</u>輸入</i>，採用<strike>記事本相同工具</strike></span><span lang="EN-US">(</span><span style="font-family: 標楷體;">如字體格式</span><span lang="EN-US">/</span><span style="font-family: 標楷體;">顏色等</span><span lang="EN-US">)&nbsp;</span><span style="font-family: 標楷體;">編輯</span></p><hr><p class="2" style="margin: 2.4pt 0cm 0.0001pt; text-indent: 0cm;"><span style="font-family: 標楷體;">，前台顯示相同格式內容。</span><span lang="EN-US"><o:p></o:p></span></p><div><span style="font-family: 標楷體;"><br></span></div></td></tr></tbody></table>

</div>",
				creator = "admin",
				creatorName = "林管理",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspMealScriptInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result:new SqlResponse() { ErrorMsg="",Msg="" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            MealScriptService MealScriptService = new
                MealScriptService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await MealScriptService.InsertMealScript(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask InsertMealScriptTestFailed()
        {  //arrange
            var insertReq = new MealScriptInsertRequest
            {
				scriptID = "MS01",
				content = "<div align="center">

<table class="MsoTableGrid" border="1" cellspacing="0" cellpadding="0" width="90%" style="width:90.24%;border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;
 mso-yfti-tbllook:1184;mso-padding-alt:0cm 5.4pt 0cm 5.4pt">
 <tbody><tr>
  <td width="6%" valign="top" style="width:6.16%;border:solid windowtext 1.0pt;
  mso-border-alt:solid windowtext .5pt;background:#F2F2F2;mso-background-themecolor:
  background1;mso-background-themeshade:242;padding:0cm 5.4pt 0cm 5.4pt">
  <p class="2" align="center" style="margin-top:2.4pt;margin-right:0cm;margin-bottom:
  0cm;margin-left:0cm;margin-bottom:.0001pt;text-align:center;text-indent:0cm"><b><span style="font-family:標楷體;mso-ascii-font-family:
  &quot;Times New Roman&quot;;mso-hansi-font-family:&quot;Times New Roman&quot;;mso-bidi-font-family:
  &quot;Times New Roman&quot;">編號</span></b><b><span lang="EN-US"><o:p></o:p></span></b></p>
  </td>
  <td width="20%" valign="top" style="width:20.0%;border:solid windowtext 1.0pt;
  border-left:none;mso-border-left-alt:solid windowtext .5pt;mso-border-alt:
  solid windowtext .5pt;background:#F2F2F2;mso-background-themecolor:background1;
  mso-background-themeshade:242;padding:0cm 5.4pt 0cm 5.4pt">
  <p class="2" align="center" style="margin-top:2.4pt;margin-right:0cm;margin-bottom:
  0cm;margin-left:0cm;margin-bottom:.0001pt;text-align:center;text-indent:0cm"><b><span style="font-family: 標楷體;">欄位名稱</span></b><b><span lang="EN-US"><o:p></o:p></span></b></p>
  </td>
  <td width="15%" valign="top" style="width:15.42%;border:solid windowtext 1.0pt;
  border-left:none;mso-border-left-alt:solid windowtext .5pt;mso-border-alt:
  solid windowtext .5pt;background:#F2F2F2;mso-background-themecolor:background1;
  mso-background-themeshade:242;padding:0cm 5.4pt 0cm 5.4pt">
  <p class="2" align="center" style="margin-top:2.4pt;margin-right:0cm;margin-bottom:
  0cm;margin-left:0cm;margin-bottom:.0001pt;text-align:center;text-indent:0cm"><b><span style="font-family: 標楷體;">欄位種類</span></b><b><span lang="EN-US"><o:p></o:p></span></b></p>
  </td>
  <td width="10%" valign="top" style="width:10.8%;border:solid windowtext 1.0pt;
  border-left:none;mso-border-left-alt:solid windowtext .5pt;mso-border-alt:
  solid windowtext .5pt;background:#F2F2F2;mso-background-themecolor:background1;
  mso-background-themeshade:242;padding:0cm 5.4pt 0cm 5.4pt">
  <p class="2" align="center" style="margin-top:2.4pt;margin-right:0cm;margin-bottom:
  0cm;margin-left:0cm;margin-bottom:.0001pt;text-align:center;text-indent:0cm"><b><span style="font-family: 標楷體;">輸入必要欄位</span></b><b><span lang="EN-US"><o:p></o:p></span></b></p>
  </td>
  <td width="47%" valign="top" style="width:47.64%;border:solid windowtext 1.0pt;
  border-left:none;mso-border-left-alt:solid windowtext .5pt;mso-border-alt:
  solid windowtext .5pt;background:#F2F2F2;mso-background-themecolor:background1;
  mso-background-themeshade:242;padding:0cm 5.4pt 0cm 5.4pt">
  <p class="2" align="center" style="margin-top:2.4pt;margin-right:0cm;margin-bottom:
  0cm;margin-left:0cm;margin-bottom:.0001pt;text-align:center;text-indent:0cm"><b><span style="font-family: 標楷體;">說明</span></b><b><span lang="EN-US"><o:p></o:p></span></b></p>
  </td>
 </tr>
 <tr>
  <td width="6%" valign="top" style="width:6.16%;border:solid windowtext 1.0pt;
  border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0cm 5.4pt 0cm 5.4pt">
  <p class="2" align="center" style="text-align: right; margin: 2.4pt 0cm 0.0001pt; text-indent: 0cm;"></p><ol><li><span lang="EN-US"><br></span></li><li><span lang="EN-US">&nbsp;</span><!--[endif]--><span lang="EN-US">&nbsp;</span></li></ol><!--[if !supportLists]--><p></p>
  </td>
  <td width="20%" valign="top" style="width:20.0%;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
  <h4 style="margin-top:2.4pt;margin-right:0cm;margin-bottom:0cm;
  margin-left:0cm;margin-bottom:.0001pt;text-indent:0cm"><span style="font-family:標楷體;mso-ascii-font-family:&quot;Times New Roman&quot;;mso-hansi-font-family:
  &quot;Times New Roman&quot;">班表資</span><font face="Times New Roman">訊<span lang="EN-US">-</span>用餐時段</font><span style="font-family:標楷體;mso-ascii-font-family:&quot;Times New Roman&quot;;mso-hansi-font-family:
  &quot;Times New Roman&quot;"><font size="1">說明</font>文案</span></h4>
  </td>
  <td width="15%" valign="top" style="width:15.42%;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt"><ul><li><span style="font-family:標楷體;mso-ascii-font-family:&quot;Times New Roman&quot;;mso-hansi-font-family:
  &quot;Times New Roman&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">文<b><u>字</u></b></span></li><li><font color="#ff0000" style="letter-spacing: 0.06em;">輸<span style="background-color: rgba(0, 255, 0, 0.67);">入</span></font><span style="letter-spacing: 0.06em; background-color: rgba(0, 255, 0, 0.67);">框</span></li></ul>
  </td>
  <td width="10%" valign="top" style="width:10.8%;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
  <p class="2" align="center" style="margin-top:2.4pt;margin-right:0cm;margin-bottom:
  0cm;margin-left:0cm;margin-bottom:.0001pt;text-align:center;text-indent:0cm"><span lang="EN-US"><b>Y</b><o:p></o:p></span></p>
  </td>
  <td width="47%" valign="top" style="width:47.64%;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
  <p class="2" style="margin-top:2.4pt;margin-right:0cm;margin-bottom:0cm;
  margin-left:0cm;margin-bottom:.0001pt;text-indent:0cm"><span style="font-family:標楷體;mso-ascii-font-family:&quot;Times New Roman&quot;;mso-hansi-font-family:
  &quot;Times New Roman&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;"><i><u>自由</u>輸入</i>，採用<strike>記事本相同工具</strike></span><span lang="EN-US">(</span><span style="font-family:標楷體;mso-ascii-font-family:&quot;Times New Roman&quot;;mso-hansi-font-family:
  &quot;Times New Roman&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">如字體格式</span><span lang="EN-US">/</span><span style="font-family:標楷體;mso-ascii-font-family:&quot;Times New Roman&quot;;mso-hansi-font-family:
  &quot;Times New Roman&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">顏色等</span><span lang="EN-US">) </span><span style="font-family:標楷體;mso-ascii-font-family:&quot;Times New Roman&quot;;mso-hansi-font-family:
  &quot;Times New Roman&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">編輯</span></p><hr><p class="2" style="margin-top:2.4pt;margin-right:0cm;margin-bottom:0cm;
  margin-left:0cm;margin-bottom:.0001pt;text-indent:0cm"><span style="font-family:標楷體;mso-ascii-font-family:&quot;Times New Roman&quot;;mso-hansi-font-family:
  &quot;Times New Roman&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">，前台<span style="background-color: rgb(0, 0, 0);"><b><font color="#ffffff">顯示相</font></b></span>同格式內容。</span><span lang="EN-US"><o:p></o:p></span></p>
  </td>
 </tr>
</tbody></table><table class="MsoTableGrid" border="1" cellspacing="0" cellpadding="0" width="90%" style="letter-spacing: 0.96px; width: 1369.83px; border-collapse: collapse; border: none;"><tbody><tr><td width="6%" valign="top" style="width: 84.2969px; border: 1pt solid windowtext; background: rgb(242, 242, 242); padding: 0cm 5.4pt;"><p class="2" align="center" style="margin: 2.4pt 0cm 0.0001pt; text-align: center; text-indent: 0cm;"><b><span style="font-family: 標楷體;">編號</span></b><b><span lang="EN-US"><o:p></o:p></span></b></p></td><td width="20%" valign="top" style="width: 273.703px; border-top: 1pt solid windowtext; border-right: 1pt solid windowtext; border-bottom: 1pt solid windowtext; border-image: initial; border-left: none; background: rgb(242, 242, 242); padding: 0cm 5.4pt;"><p class="2" align="center" style="margin: 2.4pt 0cm 0.0001pt; text-align: center; text-indent: 0cm;"><b><span style="font-family: 標楷體;">欄位名稱</span></b><b><span lang="EN-US"><o:p></o:p></span></b></p></td><td width="15%" valign="top" style="width: 211.016px; border-top: 1pt solid windowtext; border-right: 1pt solid windowtext; border-bottom: 1pt solid windowtext; border-image: initial; border-left: none; background: rgb(242, 242, 242); padding: 0cm 5.4pt;"><p class="2" align="center" style="margin: 2.4pt 0cm 0.0001pt; text-align: center; text-indent: 0cm;"><b><span style="font-family: 標楷體;">欄位種類</span></b><b><span lang="EN-US"><o:p></o:p></span></b></p></td><td width="10%" valign="top" style="width: 147.797px; border-top: 1pt solid windowtext; border-right: 1pt solid windowtext; border-bottom: 1pt solid windowtext; border-image: initial; border-left: none; background: rgb(242, 242, 242); padding: 0cm 5.4pt;"><p class="2" align="center" style="margin: 2.4pt 0cm 0.0001pt; text-align: center; text-indent: 0cm;"><b><span style="font-family: 標楷體;">輸入必要欄位</span></b><b><span lang="EN-US"><o:p></o:p></span></b></p></td><td width="47%" valign="top" style="width: 651.703px; border-top: 1pt solid windowtext; border-right: 1pt solid windowtext; border-bottom: 1pt solid windowtext; border-image: initial; border-left: none; background: rgb(242, 242, 242); padding: 0cm 5.4pt;"><p class="2" align="center" style="margin: 2.4pt 0cm 0.0001pt; text-align: center; text-indent: 0cm;"><b><span style="font-family: 標楷體;">說明</span></b><b><span lang="EN-US"><o:p></o:p></span></b></p></td></tr><tr><td width="6%" valign="top" style="width: 84.2969px; border-right: 1pt solid windowtext; border-bottom: 1pt solid windowtext; border-left: 1pt solid windowtext; border-image: initial; border-top: none; padding: 0cm 5.4pt;"><p class="2" align="center" style="margin: 2.4pt 0cm 0.0001pt; text-align: right; text-indent: 0cm;"></p><ol><li><span lang="EN-US">.&nbsp;</span><span lang="EN-US">&nbsp;</span></li></ol><p></p></td><td width="20%" valign="top" style="width: 273.703px; border-top: none; border-left: none; border-bottom: 1pt solid windowtext; border-right: 1pt solid windowtext; padding: 0cm 5.4pt;"><h4 style="margin: 2.4pt 0cm 0.0001pt; text-indent: 0cm;"><span style="font-family: 標楷體;">班表資</span><font face="Times New Roman">訊<span lang="EN-US">-</span>用餐時段</font><span style="font-family: 標楷體;"><font size="1">說明</font>文案</span></h4></td><td width="15%" valign="top" style="width: 211.016px; border-top: none; border-left: none; border-bottom: 1pt solid windowtext; border-right: 1pt solid windowtext; padding: 0cm 5.4pt;"><ul><li><span style="font-family: 標楷體;">文字</span></li></ul><ol><li><font color="#ff0000" style="background-color: rgba(255, 128, 0, 0.67);">輸入</font><span style="background-color: rgba(0, 255, 0, 0.67);">框</span></li></ol></td><td width="10%" valign="top" style="width: 147.797px; border-top: none; border-left: none; border-bottom: 1pt solid windowtext; border-right: 1pt solid windowtext; padding: 0cm 5.4pt;"><p class="2" align="center" style="margin: 2.4pt 0cm 0.0001pt; text-align: center; text-indent: 0cm;"><span lang="EN-US"><b>Y</b><o:p></o:p></span></p></td><td width="47%" valign="top" style="width: 651.703px; border-top: none; border-left: none; border-bottom: 1pt solid windowtext; border-right: 1pt solid windowtext; padding: 0cm 5.4pt;"><p class="2" style="margin: 2.4pt 0cm 0.0001pt; text-indent: 0cm;"><span style="font-family: 標楷體;"><i><u>自由</u>輸入</i>，採用<strike>記事本相同工具</strike></span><span lang="EN-US">(</span><span style="font-family: 標楷體;">如字體格式</span><span lang="EN-US">/</span><span style="font-family: 標楷體;">顏色等</span><span lang="EN-US">)&nbsp;</span><span style="font-family: 標楷體;">編輯</span></p><hr><p class="2" style="margin: 2.4pt 0cm 0.0001pt; text-indent: 0cm;"><span style="font-family: 標楷體;">，前台顯示相同格式內容。</span><span lang="EN-US"><o:p></o:p></span></p><div><span style="font-family: 標楷體;"><br></span></div></td></tr></tbody></table>

</div>",
				creator = "admin",
				creatorName = "林管理",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspMealScriptInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            MealScriptService MealScriptService = new
                MealScriptService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await MealScriptService.InsertMealScript(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }


        [Test]
        public async ValueTask UpdateMealScriptTest()
        {  //arrange
            var updateReq = new MealScriptUpdateRequest
            {
				scriptID = "MS01",
				content = "<div align="center">

<table class="MsoTableGrid" border="1" cellspacing="0" cellpadding="0" width="90%" style="width:90.24%;border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;
 mso-yfti-tbllook:1184;mso-padding-alt:0cm 5.4pt 0cm 5.4pt">
 <tbody><tr>
  <td width="6%" valign="top" style="width:6.16%;border:solid windowtext 1.0pt;
  mso-border-alt:solid windowtext .5pt;background:#F2F2F2;mso-background-themecolor:
  background1;mso-background-themeshade:242;padding:0cm 5.4pt 0cm 5.4pt">
  <p class="2" align="center" style="margin-top:2.4pt;margin-right:0cm;margin-bottom:
  0cm;margin-left:0cm;margin-bottom:.0001pt;text-align:center;text-indent:0cm"><b><span style="font-family:標楷體;mso-ascii-font-family:
  &quot;Times New Roman&quot;;mso-hansi-font-family:&quot;Times New Roman&quot;;mso-bidi-font-family:
  &quot;Times New Roman&quot;">編號</span></b><b><span lang="EN-US"><o:p></o:p></span></b></p>
  </td>
  <td width="20%" valign="top" style="width:20.0%;border:solid windowtext 1.0pt;
  border-left:none;mso-border-left-alt:solid windowtext .5pt;mso-border-alt:
  solid windowtext .5pt;background:#F2F2F2;mso-background-themecolor:background1;
  mso-background-themeshade:242;padding:0cm 5.4pt 0cm 5.4pt">
  <p class="2" align="center" style="margin-top:2.4pt;margin-right:0cm;margin-bottom:
  0cm;margin-left:0cm;margin-bottom:.0001pt;text-align:center;text-indent:0cm"><b><span style="font-family: 標楷體;">欄位名稱</span></b><b><span lang="EN-US"><o:p></o:p></span></b></p>
  </td>
  <td width="15%" valign="top" style="width:15.42%;border:solid windowtext 1.0pt;
  border-left:none;mso-border-left-alt:solid windowtext .5pt;mso-border-alt:
  solid windowtext .5pt;background:#F2F2F2;mso-background-themecolor:background1;
  mso-background-themeshade:242;padding:0cm 5.4pt 0cm 5.4pt">
  <p class="2" align="center" style="margin-top:2.4pt;margin-right:0cm;margin-bottom:
  0cm;margin-left:0cm;margin-bottom:.0001pt;text-align:center;text-indent:0cm"><b><span style="font-family: 標楷體;">欄位種類</span></b><b><span lang="EN-US"><o:p></o:p></span></b></p>
  </td>
  <td width="10%" valign="top" style="width:10.8%;border:solid windowtext 1.0pt;
  border-left:none;mso-border-left-alt:solid windowtext .5pt;mso-border-alt:
  solid windowtext .5pt;background:#F2F2F2;mso-background-themecolor:background1;
  mso-background-themeshade:242;padding:0cm 5.4pt 0cm 5.4pt">
  <p class="2" align="center" style="margin-top:2.4pt;margin-right:0cm;margin-bottom:
  0cm;margin-left:0cm;margin-bottom:.0001pt;text-align:center;text-indent:0cm"><b><span style="font-family: 標楷體;">輸入必要欄位</span></b><b><span lang="EN-US"><o:p></o:p></span></b></p>
  </td>
  <td width="47%" valign="top" style="width:47.64%;border:solid windowtext 1.0pt;
  border-left:none;mso-border-left-alt:solid windowtext .5pt;mso-border-alt:
  solid windowtext .5pt;background:#F2F2F2;mso-background-themecolor:background1;
  mso-background-themeshade:242;padding:0cm 5.4pt 0cm 5.4pt">
  <p class="2" align="center" style="margin-top:2.4pt;margin-right:0cm;margin-bottom:
  0cm;margin-left:0cm;margin-bottom:.0001pt;text-align:center;text-indent:0cm"><b><span style="font-family: 標楷體;">說明</span></b><b><span lang="EN-US"><o:p></o:p></span></b></p>
  </td>
 </tr>
 <tr>
  <td width="6%" valign="top" style="width:6.16%;border:solid windowtext 1.0pt;
  border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0cm 5.4pt 0cm 5.4pt">
  <p class="2" align="center" style="text-align: right; margin: 2.4pt 0cm 0.0001pt; text-indent: 0cm;"></p><ol><li><span lang="EN-US"><br></span></li><li><span lang="EN-US">&nbsp;</span><!--[endif]--><span lang="EN-US">&nbsp;</span></li></ol><!--[if !supportLists]--><p></p>
  </td>
  <td width="20%" valign="top" style="width:20.0%;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
  <h4 style="margin-top:2.4pt;margin-right:0cm;margin-bottom:0cm;
  margin-left:0cm;margin-bottom:.0001pt;text-indent:0cm"><span style="font-family:標楷體;mso-ascii-font-family:&quot;Times New Roman&quot;;mso-hansi-font-family:
  &quot;Times New Roman&quot;">班表資</span><font face="Times New Roman">訊<span lang="EN-US">-</span>用餐時段</font><span style="font-family:標楷體;mso-ascii-font-family:&quot;Times New Roman&quot;;mso-hansi-font-family:
  &quot;Times New Roman&quot;"><font size="1">說明</font>文案</span></h4>
  </td>
  <td width="15%" valign="top" style="width:15.42%;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt"><ul><li><span style="font-family:標楷體;mso-ascii-font-family:&quot;Times New Roman&quot;;mso-hansi-font-family:
  &quot;Times New Roman&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">文<b><u>字</u></b></span></li><li><font color="#ff0000" style="letter-spacing: 0.06em;">輸<span style="background-color: rgba(0, 255, 0, 0.67);">入</span></font><span style="letter-spacing: 0.06em; background-color: rgba(0, 255, 0, 0.67);">框</span></li></ul>
  </td>
  <td width="10%" valign="top" style="width:10.8%;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
  <p class="2" align="center" style="margin-top:2.4pt;margin-right:0cm;margin-bottom:
  0cm;margin-left:0cm;margin-bottom:.0001pt;text-align:center;text-indent:0cm"><span lang="EN-US"><b>Y</b><o:p></o:p></span></p>
  </td>
  <td width="47%" valign="top" style="width:47.64%;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
  <p class="2" style="margin-top:2.4pt;margin-right:0cm;margin-bottom:0cm;
  margin-left:0cm;margin-bottom:.0001pt;text-indent:0cm"><span style="font-family:標楷體;mso-ascii-font-family:&quot;Times New Roman&quot;;mso-hansi-font-family:
  &quot;Times New Roman&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;"><i><u>自由</u>輸入</i>，採用<strike>記事本相同工具</strike></span><span lang="EN-US">(</span><span style="font-family:標楷體;mso-ascii-font-family:&quot;Times New Roman&quot;;mso-hansi-font-family:
  &quot;Times New Roman&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">如字體格式</span><span lang="EN-US">/</span><span style="font-family:標楷體;mso-ascii-font-family:&quot;Times New Roman&quot;;mso-hansi-font-family:
  &quot;Times New Roman&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">顏色等</span><span lang="EN-US">) </span><span style="font-family:標楷體;mso-ascii-font-family:&quot;Times New Roman&quot;;mso-hansi-font-family:
  &quot;Times New Roman&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">編輯</span></p><hr><p class="2" style="margin-top:2.4pt;margin-right:0cm;margin-bottom:0cm;
  margin-left:0cm;margin-bottom:.0001pt;text-indent:0cm"><span style="font-family:標楷體;mso-ascii-font-family:&quot;Times New Roman&quot;;mso-hansi-font-family:
  &quot;Times New Roman&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">，前台<span style="background-color: rgb(0, 0, 0);"><b><font color="#ffffff">顯示相</font></b></span>同格式內容。</span><span lang="EN-US"><o:p></o:p></span></p>
  </td>
 </tr>
</tbody></table><table class="MsoTableGrid" border="1" cellspacing="0" cellpadding="0" width="90%" style="letter-spacing: 0.96px; width: 1369.83px; border-collapse: collapse; border: none;"><tbody><tr><td width="6%" valign="top" style="width: 84.2969px; border: 1pt solid windowtext; background: rgb(242, 242, 242); padding: 0cm 5.4pt;"><p class="2" align="center" style="margin: 2.4pt 0cm 0.0001pt; text-align: center; text-indent: 0cm;"><b><span style="font-family: 標楷體;">編號</span></b><b><span lang="EN-US"><o:p></o:p></span></b></p></td><td width="20%" valign="top" style="width: 273.703px; border-top: 1pt solid windowtext; border-right: 1pt solid windowtext; border-bottom: 1pt solid windowtext; border-image: initial; border-left: none; background: rgb(242, 242, 242); padding: 0cm 5.4pt;"><p class="2" align="center" style="margin: 2.4pt 0cm 0.0001pt; text-align: center; text-indent: 0cm;"><b><span style="font-family: 標楷體;">欄位名稱</span></b><b><span lang="EN-US"><o:p></o:p></span></b></p></td><td width="15%" valign="top" style="width: 211.016px; border-top: 1pt solid windowtext; border-right: 1pt solid windowtext; border-bottom: 1pt solid windowtext; border-image: initial; border-left: none; background: rgb(242, 242, 242); padding: 0cm 5.4pt;"><p class="2" align="center" style="margin: 2.4pt 0cm 0.0001pt; text-align: center; text-indent: 0cm;"><b><span style="font-family: 標楷體;">欄位種類</span></b><b><span lang="EN-US"><o:p></o:p></span></b></p></td><td width="10%" valign="top" style="width: 147.797px; border-top: 1pt solid windowtext; border-right: 1pt solid windowtext; border-bottom: 1pt solid windowtext; border-image: initial; border-left: none; background: rgb(242, 242, 242); padding: 0cm 5.4pt;"><p class="2" align="center" style="margin: 2.4pt 0cm 0.0001pt; text-align: center; text-indent: 0cm;"><b><span style="font-family: 標楷體;">輸入必要欄位</span></b><b><span lang="EN-US"><o:p></o:p></span></b></p></td><td width="47%" valign="top" style="width: 651.703px; border-top: 1pt solid windowtext; border-right: 1pt solid windowtext; border-bottom: 1pt solid windowtext; border-image: initial; border-left: none; background: rgb(242, 242, 242); padding: 0cm 5.4pt;"><p class="2" align="center" style="margin: 2.4pt 0cm 0.0001pt; text-align: center; text-indent: 0cm;"><b><span style="font-family: 標楷體;">說明</span></b><b><span lang="EN-US"><o:p></o:p></span></b></p></td></tr><tr><td width="6%" valign="top" style="width: 84.2969px; border-right: 1pt solid windowtext; border-bottom: 1pt solid windowtext; border-left: 1pt solid windowtext; border-image: initial; border-top: none; padding: 0cm 5.4pt;"><p class="2" align="center" style="margin: 2.4pt 0cm 0.0001pt; text-align: right; text-indent: 0cm;"></p><ol><li><span lang="EN-US">.&nbsp;</span><span lang="EN-US">&nbsp;</span></li></ol><p></p></td><td width="20%" valign="top" style="width: 273.703px; border-top: none; border-left: none; border-bottom: 1pt solid windowtext; border-right: 1pt solid windowtext; padding: 0cm 5.4pt;"><h4 style="margin: 2.4pt 0cm 0.0001pt; text-indent: 0cm;"><span style="font-family: 標楷體;">班表資</span><font face="Times New Roman">訊<span lang="EN-US">-</span>用餐時段</font><span style="font-family: 標楷體;"><font size="1">說明</font>文案</span></h4></td><td width="15%" valign="top" style="width: 211.016px; border-top: none; border-left: none; border-bottom: 1pt solid windowtext; border-right: 1pt solid windowtext; padding: 0cm 5.4pt;"><ul><li><span style="font-family: 標楷體;">文字</span></li></ul><ol><li><font color="#ff0000" style="background-color: rgba(255, 128, 0, 0.67);">輸入</font><span style="background-color: rgba(0, 255, 0, 0.67);">框</span></li></ol></td><td width="10%" valign="top" style="width: 147.797px; border-top: none; border-left: none; border-bottom: 1pt solid windowtext; border-right: 1pt solid windowtext; padding: 0cm 5.4pt;"><p class="2" align="center" style="margin: 2.4pt 0cm 0.0001pt; text-align: center; text-indent: 0cm;"><span lang="EN-US"><b>Y</b><o:p></o:p></span></p></td><td width="47%" valign="top" style="width: 651.703px; border-top: none; border-left: none; border-bottom: 1pt solid windowtext; border-right: 1pt solid windowtext; padding: 0cm 5.4pt;"><p class="2" style="margin: 2.4pt 0cm 0.0001pt; text-indent: 0cm;"><span style="font-family: 標楷體;"><i><u>自由</u>輸入</i>，採用<strike>記事本相同工具</strike></span><span lang="EN-US">(</span><span style="font-family: 標楷體;">如字體格式</span><span lang="EN-US">/</span><span style="font-family: 標楷體;">顏色等</span><span lang="EN-US">)&nbsp;</span><span style="font-family: 標楷體;">編輯</span></p><hr><p class="2" style="margin: 2.4pt 0cm 0.0001pt; text-indent: 0cm;"><span style="font-family: 標楷體;">，前台顯示相同格式內容。</span><span lang="EN-US"><o:p></o:p></span></p><div><span style="font-family: 標楷體;"><br></span></div></td></tr></tbody></table>

</div>",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspMealScriptUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            MealScriptService MealScriptService = new
                MealScriptService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await MealScriptService.UpdateMealScript(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask UpdateMealScriptTestFailed()
        {  //arrange
            var updateReq = new MealScriptUpdateRequest
            {
				scriptID = "MS01",
				content = "<div align="center">

<table class="MsoTableGrid" border="1" cellspacing="0" cellpadding="0" width="90%" style="width:90.24%;border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;
 mso-yfti-tbllook:1184;mso-padding-alt:0cm 5.4pt 0cm 5.4pt">
 <tbody><tr>
  <td width="6%" valign="top" style="width:6.16%;border:solid windowtext 1.0pt;
  mso-border-alt:solid windowtext .5pt;background:#F2F2F2;mso-background-themecolor:
  background1;mso-background-themeshade:242;padding:0cm 5.4pt 0cm 5.4pt">
  <p class="2" align="center" style="margin-top:2.4pt;margin-right:0cm;margin-bottom:
  0cm;margin-left:0cm;margin-bottom:.0001pt;text-align:center;text-indent:0cm"><b><span style="font-family:標楷體;mso-ascii-font-family:
  &quot;Times New Roman&quot;;mso-hansi-font-family:&quot;Times New Roman&quot;;mso-bidi-font-family:
  &quot;Times New Roman&quot;">編號</span></b><b><span lang="EN-US"><o:p></o:p></span></b></p>
  </td>
  <td width="20%" valign="top" style="width:20.0%;border:solid windowtext 1.0pt;
  border-left:none;mso-border-left-alt:solid windowtext .5pt;mso-border-alt:
  solid windowtext .5pt;background:#F2F2F2;mso-background-themecolor:background1;
  mso-background-themeshade:242;padding:0cm 5.4pt 0cm 5.4pt">
  <p class="2" align="center" style="margin-top:2.4pt;margin-right:0cm;margin-bottom:
  0cm;margin-left:0cm;margin-bottom:.0001pt;text-align:center;text-indent:0cm"><b><span style="font-family: 標楷體;">欄位名稱</span></b><b><span lang="EN-US"><o:p></o:p></span></b></p>
  </td>
  <td width="15%" valign="top" style="width:15.42%;border:solid windowtext 1.0pt;
  border-left:none;mso-border-left-alt:solid windowtext .5pt;mso-border-alt:
  solid windowtext .5pt;background:#F2F2F2;mso-background-themecolor:background1;
  mso-background-themeshade:242;padding:0cm 5.4pt 0cm 5.4pt">
  <p class="2" align="center" style="margin-top:2.4pt;margin-right:0cm;margin-bottom:
  0cm;margin-left:0cm;margin-bottom:.0001pt;text-align:center;text-indent:0cm"><b><span style="font-family: 標楷體;">欄位種類</span></b><b><span lang="EN-US"><o:p></o:p></span></b></p>
  </td>
  <td width="10%" valign="top" style="width:10.8%;border:solid windowtext 1.0pt;
  border-left:none;mso-border-left-alt:solid windowtext .5pt;mso-border-alt:
  solid windowtext .5pt;background:#F2F2F2;mso-background-themecolor:background1;
  mso-background-themeshade:242;padding:0cm 5.4pt 0cm 5.4pt">
  <p class="2" align="center" style="margin-top:2.4pt;margin-right:0cm;margin-bottom:
  0cm;margin-left:0cm;margin-bottom:.0001pt;text-align:center;text-indent:0cm"><b><span style="font-family: 標楷體;">輸入必要欄位</span></b><b><span lang="EN-US"><o:p></o:p></span></b></p>
  </td>
  <td width="47%" valign="top" style="width:47.64%;border:solid windowtext 1.0pt;
  border-left:none;mso-border-left-alt:solid windowtext .5pt;mso-border-alt:
  solid windowtext .5pt;background:#F2F2F2;mso-background-themecolor:background1;
  mso-background-themeshade:242;padding:0cm 5.4pt 0cm 5.4pt">
  <p class="2" align="center" style="margin-top:2.4pt;margin-right:0cm;margin-bottom:
  0cm;margin-left:0cm;margin-bottom:.0001pt;text-align:center;text-indent:0cm"><b><span style="font-family: 標楷體;">說明</span></b><b><span lang="EN-US"><o:p></o:p></span></b></p>
  </td>
 </tr>
 <tr>
  <td width="6%" valign="top" style="width:6.16%;border:solid windowtext 1.0pt;
  border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0cm 5.4pt 0cm 5.4pt">
  <p class="2" align="center" style="text-align: right; margin: 2.4pt 0cm 0.0001pt; text-indent: 0cm;"></p><ol><li><span lang="EN-US"><br></span></li><li><span lang="EN-US">&nbsp;</span><!--[endif]--><span lang="EN-US">&nbsp;</span></li></ol><!--[if !supportLists]--><p></p>
  </td>
  <td width="20%" valign="top" style="width:20.0%;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
  <h4 style="margin-top:2.4pt;margin-right:0cm;margin-bottom:0cm;
  margin-left:0cm;margin-bottom:.0001pt;text-indent:0cm"><span style="font-family:標楷體;mso-ascii-font-family:&quot;Times New Roman&quot;;mso-hansi-font-family:
  &quot;Times New Roman&quot;">班表資</span><font face="Times New Roman">訊<span lang="EN-US">-</span>用餐時段</font><span style="font-family:標楷體;mso-ascii-font-family:&quot;Times New Roman&quot;;mso-hansi-font-family:
  &quot;Times New Roman&quot;"><font size="1">說明</font>文案</span></h4>
  </td>
  <td width="15%" valign="top" style="width:15.42%;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt"><ul><li><span style="font-family:標楷體;mso-ascii-font-family:&quot;Times New Roman&quot;;mso-hansi-font-family:
  &quot;Times New Roman&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">文<b><u>字</u></b></span></li><li><font color="#ff0000" style="letter-spacing: 0.06em;">輸<span style="background-color: rgba(0, 255, 0, 0.67);">入</span></font><span style="letter-spacing: 0.06em; background-color: rgba(0, 255, 0, 0.67);">框</span></li></ul>
  </td>
  <td width="10%" valign="top" style="width:10.8%;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
  <p class="2" align="center" style="margin-top:2.4pt;margin-right:0cm;margin-bottom:
  0cm;margin-left:0cm;margin-bottom:.0001pt;text-align:center;text-indent:0cm"><span lang="EN-US"><b>Y</b><o:p></o:p></span></p>
  </td>
  <td width="47%" valign="top" style="width:47.64%;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt">
  <p class="2" style="margin-top:2.4pt;margin-right:0cm;margin-bottom:0cm;
  margin-left:0cm;margin-bottom:.0001pt;text-indent:0cm"><span style="font-family:標楷體;mso-ascii-font-family:&quot;Times New Roman&quot;;mso-hansi-font-family:
  &quot;Times New Roman&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;"><i><u>自由</u>輸入</i>，採用<strike>記事本相同工具</strike></span><span lang="EN-US">(</span><span style="font-family:標楷體;mso-ascii-font-family:&quot;Times New Roman&quot;;mso-hansi-font-family:
  &quot;Times New Roman&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">如字體格式</span><span lang="EN-US">/</span><span style="font-family:標楷體;mso-ascii-font-family:&quot;Times New Roman&quot;;mso-hansi-font-family:
  &quot;Times New Roman&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">顏色等</span><span lang="EN-US">) </span><span style="font-family:標楷體;mso-ascii-font-family:&quot;Times New Roman&quot;;mso-hansi-font-family:
  &quot;Times New Roman&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">編輯</span></p><hr><p class="2" style="margin-top:2.4pt;margin-right:0cm;margin-bottom:0cm;
  margin-left:0cm;margin-bottom:.0001pt;text-indent:0cm"><span style="font-family:標楷體;mso-ascii-font-family:&quot;Times New Roman&quot;;mso-hansi-font-family:
  &quot;Times New Roman&quot;;mso-bidi-font-family:&quot;Times New Roman&quot;">，前台<span style="background-color: rgb(0, 0, 0);"><b><font color="#ffffff">顯示相</font></b></span>同格式內容。</span><span lang="EN-US"><o:p></o:p></span></p>
  </td>
 </tr>
</tbody></table><table class="MsoTableGrid" border="1" cellspacing="0" cellpadding="0" width="90%" style="letter-spacing: 0.96px; width: 1369.83px; border-collapse: collapse; border: none;"><tbody><tr><td width="6%" valign="top" style="width: 84.2969px; border: 1pt solid windowtext; background: rgb(242, 242, 242); padding: 0cm 5.4pt;"><p class="2" align="center" style="margin: 2.4pt 0cm 0.0001pt; text-align: center; text-indent: 0cm;"><b><span style="font-family: 標楷體;">編號</span></b><b><span lang="EN-US"><o:p></o:p></span></b></p></td><td width="20%" valign="top" style="width: 273.703px; border-top: 1pt solid windowtext; border-right: 1pt solid windowtext; border-bottom: 1pt solid windowtext; border-image: initial; border-left: none; background: rgb(242, 242, 242); padding: 0cm 5.4pt;"><p class="2" align="center" style="margin: 2.4pt 0cm 0.0001pt; text-align: center; text-indent: 0cm;"><b><span style="font-family: 標楷體;">欄位名稱</span></b><b><span lang="EN-US"><o:p></o:p></span></b></p></td><td width="15%" valign="top" style="width: 211.016px; border-top: 1pt solid windowtext; border-right: 1pt solid windowtext; border-bottom: 1pt solid windowtext; border-image: initial; border-left: none; background: rgb(242, 242, 242); padding: 0cm 5.4pt;"><p class="2" align="center" style="margin: 2.4pt 0cm 0.0001pt; text-align: center; text-indent: 0cm;"><b><span style="font-family: 標楷體;">欄位種類</span></b><b><span lang="EN-US"><o:p></o:p></span></b></p></td><td width="10%" valign="top" style="width: 147.797px; border-top: 1pt solid windowtext; border-right: 1pt solid windowtext; border-bottom: 1pt solid windowtext; border-image: initial; border-left: none; background: rgb(242, 242, 242); padding: 0cm 5.4pt;"><p class="2" align="center" style="margin: 2.4pt 0cm 0.0001pt; text-align: center; text-indent: 0cm;"><b><span style="font-family: 標楷體;">輸入必要欄位</span></b><b><span lang="EN-US"><o:p></o:p></span></b></p></td><td width="47%" valign="top" style="width: 651.703px; border-top: 1pt solid windowtext; border-right: 1pt solid windowtext; border-bottom: 1pt solid windowtext; border-image: initial; border-left: none; background: rgb(242, 242, 242); padding: 0cm 5.4pt;"><p class="2" align="center" style="margin: 2.4pt 0cm 0.0001pt; text-align: center; text-indent: 0cm;"><b><span style="font-family: 標楷體;">說明</span></b><b><span lang="EN-US"><o:p></o:p></span></b></p></td></tr><tr><td width="6%" valign="top" style="width: 84.2969px; border-right: 1pt solid windowtext; border-bottom: 1pt solid windowtext; border-left: 1pt solid windowtext; border-image: initial; border-top: none; padding: 0cm 5.4pt;"><p class="2" align="center" style="margin: 2.4pt 0cm 0.0001pt; text-align: right; text-indent: 0cm;"></p><ol><li><span lang="EN-US">.&nbsp;</span><span lang="EN-US">&nbsp;</span></li></ol><p></p></td><td width="20%" valign="top" style="width: 273.703px; border-top: none; border-left: none; border-bottom: 1pt solid windowtext; border-right: 1pt solid windowtext; padding: 0cm 5.4pt;"><h4 style="margin: 2.4pt 0cm 0.0001pt; text-indent: 0cm;"><span style="font-family: 標楷體;">班表資</span><font face="Times New Roman">訊<span lang="EN-US">-</span>用餐時段</font><span style="font-family: 標楷體;"><font size="1">說明</font>文案</span></h4></td><td width="15%" valign="top" style="width: 211.016px; border-top: none; border-left: none; border-bottom: 1pt solid windowtext; border-right: 1pt solid windowtext; padding: 0cm 5.4pt;"><ul><li><span style="font-family: 標楷體;">文字</span></li></ul><ol><li><font color="#ff0000" style="background-color: rgba(255, 128, 0, 0.67);">輸入</font><span style="background-color: rgba(0, 255, 0, 0.67);">框</span></li></ol></td><td width="10%" valign="top" style="width: 147.797px; border-top: none; border-left: none; border-bottom: 1pt solid windowtext; border-right: 1pt solid windowtext; padding: 0cm 5.4pt;"><p class="2" align="center" style="margin: 2.4pt 0cm 0.0001pt; text-align: center; text-indent: 0cm;"><span lang="EN-US"><b>Y</b><o:p></o:p></span></p></td><td width="47%" valign="top" style="width: 651.703px; border-top: none; border-left: none; border-bottom: 1pt solid windowtext; border-right: 1pt solid windowtext; padding: 0cm 5.4pt;"><p class="2" style="margin: 2.4pt 0cm 0.0001pt; text-indent: 0cm;"><span style="font-family: 標楷體;"><i><u>自由</u>輸入</i>，採用<strike>記事本相同工具</strike></span><span lang="EN-US">(</span><span style="font-family: 標楷體;">如字體格式</span><span lang="EN-US">/</span><span style="font-family: 標楷體;">顏色等</span><span lang="EN-US">)&nbsp;</span><span style="font-family: 標楷體;">編輯</span></p><hr><p class="2" style="margin: 2.4pt 0cm 0.0001pt; text-indent: 0cm;"><span style="font-family: 標楷體;">，前台顯示相同格式內容。</span><span lang="EN-US"><o:p></o:p></span></p><div><span style="font-family: 標楷體;"><br></span></div></td></tr></tbody></table>

</div>",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspMealScriptUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            MealScriptService MealScriptService = new
                MealScriptService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await MealScriptService.UpdateMealScript(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }




    }
}
