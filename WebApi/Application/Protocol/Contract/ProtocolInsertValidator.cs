using FluentValidation;
using FluentValidation.Results;

namespace ESUN.AGD.WebApi.Application.Protocol.Contract
{
     public class  ProtocolInsertValidator : AbstractValidator<ProtocolInsertRequest>
    {
        public  ProtocolInsertValidator()
        {
			RuleFor(x => x.protocol).NotEmpty().WithMessage("通路碼代碼為必填");
			RuleFor(x => x.protocol).MaximumLength(20).WithMessage("通路碼代碼長度超過系統限制");
			RuleFor(x => x.protocolName).NotEmpty().WithMessage("通路碼名稱為必填");
			RuleFor(x => x.protocolName).MaximumLength(50).WithMessage("通路碼名稱長度超過系統限制");
			RuleFor(x => x.direction).NotEmpty().WithMessage("IN/OUT方向為必填");
			RuleFor(x => x.direction).MaximumLength(1).WithMessage("IN/OUT方向長度超過系統限制");
			RuleFor(x => x.isEnable).NotEmpty().WithMessage("是否啟用?為必填");
        }
    }
}
