using FluentValidation;
using FluentValidation.Results;

namespace ESUN.AGD.WebApi.Application.Calendar.Contract
{
     public class  CalendarInsertValidator : AbstractValidator<CalendarInsertRequest>
    {
        public  CalendarInsertValidator()
        {
			RuleFor(x => x.userID).NotEmpty().WithMessage("使用者帳號為必填");
			RuleFor(x => x.userID).MaximumLength(20).WithMessage("使用者帳號長度超過系統限制");
			RuleFor(x => x.content).NotEmpty().WithMessage("班表內容為必填");
			RuleFor(x => x.content).MaximumLength(50).WithMessage("班表內容長度超過系統限制");
        }
    }
}
