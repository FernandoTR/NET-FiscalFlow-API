using System;
using System.Collections.Generic;
using System.Text;

namespace FiscalFlow.Application.Interfaces.Auth;

public interface ILoginService
{
    Task<LoginResultDto> LoginAsync(string email, string password);
}
