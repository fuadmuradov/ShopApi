using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApi.Apps.UserApi.DTOs.Account
{
    public class AccountUserGetDto
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
    }
}
