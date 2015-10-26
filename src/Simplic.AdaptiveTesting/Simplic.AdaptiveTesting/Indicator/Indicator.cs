using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.AdaptiveTesting
{
    public abstract class Indicator
    {
        public abstract void Start();

        public abstract void Stop();

        public abstract object GetResult();
    }
}
