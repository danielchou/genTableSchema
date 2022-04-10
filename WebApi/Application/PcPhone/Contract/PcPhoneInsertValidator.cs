using FluentValidation;
using FluentValidation.Results;

namespace ESUN.AGD.WebApi.Application.PcPhone.Contract
{
     public class  PcPhoneInsertValidator : AbstractValidator<PcPhoneInsertRequest>
    {
        public  PcPhoneInsertValidator()
        {
			RuleFor(x => x.extCode).NotEmpty().WithMessage("電話分機為必填");
			RuleFor(x => x.extCode).MaximumLength(10).WithMessage("電話分機長度超過系統限制");
			RuleFor(x => x.computerName).NotEmpty().WithMessage("電腦名稱為必填");
			RuleFor(x => x.computerName).MaximumLength(25).WithMessage("電腦名稱長度超過系統限制");
			RuleFor(x => x.computerIp).NotEmpty().WithMessage("電腦IP為必填");
			RuleFor(x => x.computerIp).MaximumLength(23).WithMessage("電腦IP長度超過系統限制");
			RuleFor(x => x.isEnable).NotEmpty().WithMessage("是否啟用?為必填")
        }
    }
}
