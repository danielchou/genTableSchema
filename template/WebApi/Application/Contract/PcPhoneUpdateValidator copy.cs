using FluentValidation;
namespace ESUN.AGD.WebApi.Application.PcPhone.Contract
{
    public class PcPhoneUpdateValidator:AbstractValidator<PcPhoneUpdateRequest>
    {
        public PcPhoneUpdateValidator()
        {
            RuleFor(x => x.seqNo).NotEmpty().WithMessage("資料代碼不能為空!!");
            RuleFor(x => x.extCode).NotEmpty().WithMessage("分機號碼為必填");
            RuleFor(x => x.extCode).MaximumLength(10).WithMessage("分機號碼長度超過系統限制");
            RuleFor(x => x.computerName).NotEmpty().WithMessage("電腦名稱為必填");
            RuleFor(x => x.computerName).MaximumLength(25).WithMessage("電腦名稱長度超過系統限制");
            RuleFor(x => x.computerIp).NotEmpty().WithMessage("電腦IP為必填");
            RuleFor(x => x.computerIp).MaximumLength(23).WithMessage("電腦IP長度超過系統限制");
            RuleFor(x => x.isEnable).NotEmpty().WithMessage("是否啟用?為必填");
        }
    }
}
