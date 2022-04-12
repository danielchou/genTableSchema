using FluentValidation;
namespace ESUN.AGD.WebApi.Application.ReasonIvr.Contract
{
    public class ReasonIvrUpdateValidator:AbstractValidator<ReasonIvrUpdateRequest>
    {
        public ReasonIvrUpdateValidator()
        {
			RuleFor(x => x.ivrID).NotEmpty().WithMessage("Ivr代碼不能為空!!");
			RuleFor(x => x.reasonID).NotEmpty().WithMessage("原因代碼不能為空!!");
			RuleFor(x => x.ivrID).NotEmpty().WithMessage("Ivr代碼為必填");
			RuleFor(x => x.ivrID).MaximumLength(20).WithMessage("Ivr代碼長度超過系統限制");
			RuleFor(x => x.reasonID).NotEmpty().WithMessage("原因代碼為必填");
			RuleFor(x => x.reasonID).MaximumLength(20).WithMessage("原因代碼長度超過系統限制");
        }
    }
}
