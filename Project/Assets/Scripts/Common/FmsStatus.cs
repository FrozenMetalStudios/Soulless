using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Common
{
    public enum eFmsStatus : int
    {
        FMS_SUCCESS = 0,

        FMS_ERROR_GENERAL   = -500,
        FMS_INVALID_ARG     = -501,

        FMS_NOT_IMPLEMENTED = -800,
    }
}
