using FluentValidation;
namespace ESUN.AGD.WebApi.Application.NotificationTemplate.Contract
{
    public class NotificationTemplateUpdateValidator:AbstractValidator<NotificationTemplateUpdateRequest>
    {
        public NotificationTemplateUpdateValidator()
        {
			RuleFor(x => x.notificationID).NotEmpty().WithMessage("通知公告代碼不能為空!!");
			RuleFor(x => x.notificationType).NotEmpty().WithMessage("通知公告類別為必填");
			RuleFor(x => x.notificationType).MaximumLength(3).WithMessage("通知公告類別長度超過系統限制");
			RuleFor(x => x.notificationID).NotEmpty().WithMessage("通知公告代碼為必填");
			RuleFor(x => x.notificationID).MaximumLength(50).WithMessage("通知公告代碼長度超過系統限制");
			RuleFor(x => x.notificationName).NotEmpty().WithMessage("通知公告名稱為必填");
			RuleFor(x => x.notificationName).MaximumLength(50).WithMessage("通知公告名稱長度超過系統限制");
			RuleFor(x => x.content).MaximumLength(2000).WithMessage("通知公告範本長度超過系統限制");
			RuleFor(x => x.isEnable).NotEmpty().WithMessage("是否啟用?為必填");
        }
    }
}
