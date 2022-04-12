using FluentValidation;
using FluentValidation.Results;

namespace ESUN.AGD.WebApi.Application.MessageSheet.Contract
{
     public class  MessageSheetInsertValidator : AbstractValidator<MessageSheetInsertRequest>
    {
        public  MessageSheetInsertValidator()
        {
			RuleFor(x => x.messageSheetType).NotEmpty().WithMessage("訊息傳送頁籤類別為必填");
			RuleFor(x => x.messageSheetType).MaximumLength(1).WithMessage("訊息傳送頁籤類別長度超過系統限制");
			RuleFor(x => x.messageSheetID).NotEmpty().WithMessage("訊息傳送頁籤代碼為必填");
			RuleFor(x => x.messageSheetID).MaximumLength(20).WithMessage("訊息傳送頁籤代碼長度超過系統限制");
			RuleFor(x => x.messageSheetName).NotEmpty().WithMessage("訊息傳送頁籤名稱為必填");
			RuleFor(x => x.messageSheetName).MaximumLength(50).WithMessage("訊息傳送頁籤名稱長度超過系統限制");
			RuleFor(x => x.isEnable).NotEmpty().WithMessage("是否啟用?為必填");
        }
    }
}
