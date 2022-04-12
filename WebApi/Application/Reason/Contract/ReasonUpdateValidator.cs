using FluentValidation;
namespace ESUN.AGD.WebApi.Application.Reason.Contract
{
    public class ReasonUpdateValidator:AbstractValidator<ReasonUpdateRequest>
    {
        public ReasonUpdateValidator()
        {
			RuleFor(x => x.reasonID).NotEmpty().WithMessage("聯繫原因碼代碼不能為空!!");
			RuleFor(x => x.reasonID).NotEmpty().WithMessage("聯繫原因碼代碼為必填");
			RuleFor(x => x.reasonID).MaximumLength(20).WithMessage("聯繫原因碼代碼長度超過系統限制");
			RuleFor(x => x.reasonName).NotEmpty().WithMessage("聯繫原因碼名稱為必填");
			RuleFor(x => x.reasonName).MaximumLength(50).WithMessage("聯繫原因碼名稱長度超過系統限制");
			RuleFor(x => x.parentReasonID).NotEmpty().WithMessage("上層聯繫原因碼代碼為必填");
			RuleFor(x => x.parentReasonID).MaximumLength(20).WithMessage("上層聯繫原因碼代碼長度超過系統限制");
			RuleFor(x => x.bussinessUnit).MaximumLength(3).WithMessage("事業處長度超過系統限制");
			RuleFor(x => x.bussinessB03Type).MaximumLength(3).WithMessage("B03業務別長度超過系統限制");
			RuleFor(x => x.reviewType).MaximumLength(3).WithMessage("覆核類別長度超過系統限制");
			RuleFor(x => x.memo).MaximumLength(20).WithMessage("備註長度超過系統限制");
			RuleFor(x => x.webUrl).MaximumLength(200).WithMessage("網頁連結長度超過系統限制");
			RuleFor(x => x.kMUrl).MaximumLength(200).WithMessage("KM連結長度超過系統限制");
			RuleFor(x => x.isUsually).NotEmpty().WithMessage("是否常用為必填");
			RuleFor(x => x.usuallyReasonName).MaximumLength(50).WithMessage("常用名稱長度超過系統限制");
			RuleFor(x => x.isEnable).NotEmpty().WithMessage("是否啟用?為必填");
        }
    }
}
