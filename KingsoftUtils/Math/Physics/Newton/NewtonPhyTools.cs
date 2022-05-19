using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingsoft.Utils.Math.Physics.Newton
{
    public static class NewtonPhyTools
    {
        /// <summary>
        /// Principle of action formula by Isaac Newton
        /// <see cref="NewtonPhyTools"/>
        /// </summary>
        public static PhyMath PrincipleOfAction(this PhyMath self, float mass, float acceleration, out float result)
        {
            result = mass * acceleration;
            return self;
        }
        /// <summary>
        /// Principle of action (shortcut)
        /// <see cref="NewtonPhyTools"/>
        /// </summary>
        public static PhyMath POA(this PhyMath self, float mass, float acceleration, out float result) 
            => self.PrincipleOfAction(mass, acceleration, out result);
    }
}
