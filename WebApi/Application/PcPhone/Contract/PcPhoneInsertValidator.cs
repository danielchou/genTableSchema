using FluentValidation;
using FluentValidation.Results;

namespace ESUN.AGD.WebApi.Application.PcPhone.Contract
{
     public class  PcPhoneInsertValidator : AbstractValidator<PcPhoneInsertRequest>
    {
        public  PcPhoneInsertValidator()
        {
			RuleFor(x => x.computerIP).NotEmpty().WithMessage("電腦IP為必填");
			RuleFor(x => x.computerIP).MaximumLength(20).WithMessage("電腦IP長度超過系統限制");
			RuleFor(x => x.computerName).NotEmpty().WithMessage("電腦名稱為必填");
			RuleFor(x => x.computerName).MaximumLength(50).WithMessage("電腦名稱長度超過系統限制");
			RuleFor(x => x.extCode).NotEmpty().WithMessage("分機號碼為必填");
			RuleFor(x => x.extCode).MaximumLength(20).WithMessage("分機號碼長度超過系統限制");
			RuleFor(x => x.memo).MaximumLength(200).WithMessage("備註長度超過系統限制");
        }
    }
}
