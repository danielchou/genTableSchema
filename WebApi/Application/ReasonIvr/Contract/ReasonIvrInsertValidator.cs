using FluentValidation;
using FluentValidation.Results;

namespace ESUN.AGD.WebApi.Application.ReasonIvr.Contract
{
     public class  ReasonIvrInsertValidator : AbstractValidator<ReasonIvrInsertRequest>
    {
        public  ReasonIvrInsertValidator()
        {
			RuleFor(x => x.ivrID).NotEmpty().WithMessage("Ivr代碼為必填");
			RuleFor(x => x.ivrID).MaximumLength(20).WithMessage("Ivr代碼長度超過系統限制");
			RuleFor(x => x.reasonID).NotEmpty().WithMessage("原因代碼為必填");
			RuleFor(x => x.reasonID).MaximumLength(20).WithMessage("原因代碼長度超過系統限制");
        }
    }
}
