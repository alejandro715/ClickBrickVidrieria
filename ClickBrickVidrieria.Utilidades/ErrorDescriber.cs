using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickBrickVidrieria.Utilidades
{
    public class ErrorDescriber: IdentityErrorDescriber
    {
        public override IdentityError PasswordRequiresLower()
        {
            return new IdentityError()
            {

                Code = nameof(PasswordRequiresLower),
                Description = "El Password debe tener al menos una letra minuscula"
            };
        }

    }
}
