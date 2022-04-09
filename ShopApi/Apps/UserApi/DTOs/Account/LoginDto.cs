using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApi.Apps.UserApi.DTOs.Account
{
    public class LoginDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class LoginDtoValidation : AbstractValidator<LoginDto>
    {
        public LoginDtoValidation()
        {
            RuleFor(x => x.UserName).NotEmpty().MinimumLength(4).MaximumLength(25);
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8).MaximumLength(35);
        }
    }
}
