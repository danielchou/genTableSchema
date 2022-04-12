using FluentValidation;
using FluentValidation.Results;

namespace ESUN.AGD.WebApi.Application.PhoneBook.Contract
{
     public class  PhoneBookInsertValidator : AbstractValidator<PhoneBookInsertRequest>
    {
        public  PhoneBookInsertValidator()
        {
			RuleFor(x => x.phoneBookID).NotEmpty().WithMessage("電話簿代碼為必填");
			RuleFor(x => x.phoneBookID).MaximumLength(20).WithMessage("電話簿代碼長度超過系統限制");
			RuleFor(x => x.phoneBookName).NotEmpty().WithMessage("電話簿名稱為必填");
			RuleFor(x => x.phoneBookName).MaximumLength(50).WithMessage("電話簿名稱長度超過系統限制");
			RuleFor(x => x.parentPhoneBookID).NotEmpty().WithMessage("上層電話簿代碼為必填");
			RuleFor(x => x.parentPhoneBookID).MaximumLength(20).WithMessage("上層電話簿代碼長度超過系統限制");
			RuleFor(x => x.phoneBookNumber).NotEmpty().WithMessage("電話號碼為必填");
			RuleFor(x => x.phoneBookNumber).MaximumLength(20).WithMessage("電話號碼長度超過系統限制");
			RuleFor(x => x.memo).MaximumLength(200).WithMessage("備註長度超過系統限制");
        }
    }
}
