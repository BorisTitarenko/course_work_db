using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace cource_work.Data
{
    public enum DeportingStatus
    {
       
        [Description("PLANNED")]
        PLANNED,
        [Description("PLATFORM")]
        PLATFORM,
        [Description("RUNNING")]
        RUNNING,
        [Description("CLOSED")]
        CLOSED
    }
}
