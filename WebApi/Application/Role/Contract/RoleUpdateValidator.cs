using FluentValidation;
namespace ESUN.AGD.WebApi.Application.Role.Contract
{
    public class RoleUpdateValidator:AbstractValidator<RoleUpdateRequest>
    {
        public RoleUpdateValidator()
        {
			RuleFor(x => x.roleID).NotEmpty().WithMessage("角色代碼不能為空!!");
			RuleFor(x => x.roleID).NotEmpty().WithMessage("角色代碼為必填");
			RuleFor(x => x.roleID).MaximumLength(20).WithMessage("角色代碼長度超過系統限制");
			RuleFor(x => x.roleName).NotEmpty().WithMessage("角色名稱為必填");
			RuleFor(x => x.roleName).MaximumLength(50).WithMessage("角色名稱長度超過系統限制");
        }
    }
}
