using System;
using System.Collections.Generic;
using System.Text;

namespace FiscalFlow.Application.DTOs.Cfdi;

public class CfdiErrorResponseDto : CfdiResponseDto
{
    public object Errors { get; init; }
}
