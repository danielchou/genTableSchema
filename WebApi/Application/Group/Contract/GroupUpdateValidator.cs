using FluentValidation;
namespace ESUN.AGD.WebApi.Application.Group.Contract
{
    public class GroupUpdateValidator:AbstractValidator<GroupUpdateRequest>
    {
        public GroupUpdateValidator()
        {
			RuleFor(x => x.groupID).NotEmpty().WithMessage("群組代碼不能為空!!");
			RuleFor(x => x.groupID).NotEmpty().WithMessage("群組代碼為必填");
			RuleFor(x => x.groupID).MaximumLength(20).WithMessage("群組代碼長度超過系統限制");
			RuleFor(x => x.groupName).NotEmpty().WithMessage("群組名稱為必填");
			RuleFor(x => x.groupName).MaximumLength(50).WithMessage("群組名稱長度超過系統限制");
        }
    }
}
