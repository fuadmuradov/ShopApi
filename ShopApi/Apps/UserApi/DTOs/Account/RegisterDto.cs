using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApi.Apps.UserApi.DTOs.Account
{
    public class RegisterDto
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class RegisterDtoValidation : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidation()
        {
            RuleFor(x => x.FullName).NotNull().WithMessage("Fullname mast be Filled")
            .NotEmpty().MaximumLength(20).MinimumLength(4);

            RuleFor(x => x.UserName).NotEmpty().WithMessage("Username Must be Filled")
                .MaximumLength(25).MinimumLength(4);

            RuleFor(x => x.Password).MinimumLength(8).MaximumLength(30).NotEmpty().NotNull();

            RuleFor(x => x.ConfirmPassword).MinimumLength(8).MaximumLength(30).NotEmpty().NotNull();

            RuleFor(x => x).Custom((x, context) => { 
                if(x.Password != x.ConfirmPassword)
                {
                    context.AddFailure("ConfirmPassword", "Password and COnfirm Password must be Equal");
                }
            });
        }
    }


}
