using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LockCommons.Models
{
    public delegate Task<bool> ProcessEventDelegate(object lockEvent);
}
