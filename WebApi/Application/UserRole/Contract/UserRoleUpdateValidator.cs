using FluentValidation;
namespace ESUN.AGD.WebApi.Application.UserRole.Contract
{
    public class UserRoleUpdateValidator:AbstractValidator<UserRoleUpdateRequest>
    {
        public UserRoleUpdateValidator()
        {
			RuleFor(x => x.userID).NotEmpty().WithMessage("使用者帳號不能為空!!");
			RuleFor(x => x.roleID).NotEmpty().WithMessage("角色代碼不能為空!!");
			RuleFor(x => x.userID).NotEmpty().WithMessage("使用者帳號為必填");
			RuleFor(x => x.userID).MaximumLength(20).WithMessage("使用者帳號長度超過系統限制");
			RuleFor(x => x.roleID).NotEmpty().WithMessage("角色代碼為必填");
			RuleFor(x => x.roleID).MaximumLength(20).WithMessage("角色代碼長度超過系統限制");
        }
    }
}
