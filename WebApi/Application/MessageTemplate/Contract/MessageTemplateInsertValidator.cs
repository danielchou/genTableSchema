using FluentValidation;
using FluentValidation.Results;

namespace ESUN.AGD.WebApi.Application.MessageTemplate.Contract
{
     public class  MessageTemplateInsertValidator : AbstractValidator<MessageTemplateInsertRequest>
    {
        public  MessageTemplateInsertValidator()
        {
			RuleFor(x => x.messageTemplateID).NotEmpty().WithMessage("訊息傳送範本代碼為必填");
			RuleFor(x => x.messageTemplateID).MaximumLength(20).WithMessage("訊息傳送範本代碼長度超過系統限制");
			RuleFor(x => x.messageTemplateName).NotEmpty().WithMessage("訊息傳送範本名稱為必填");
			RuleFor(x => x.messageTemplateName).MaximumLength(50).WithMessage("訊息傳送範本名稱長度超過系統限制");
			RuleFor(x => x.messageB08Code).NotEmpty().WithMessage("訊息傳送B08代碼為必填");
			RuleFor(x => x.messageB08Code).MaximumLength(20).WithMessage("訊息傳送B08代碼長度超過系統限制");
			RuleFor(x => x.content).MaximumLength(2000).WithMessage("訊息傳送範本長度超過系統限制");
			RuleFor(x => x.isEnable).NotEmpty().WithMessage("是否啟用?為必填");
        }
    }
}
