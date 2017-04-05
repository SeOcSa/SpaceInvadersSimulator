using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvadersSimulator
{
    class Ybeing
    {
        private int lifeTime;
        public Ybeing()
        {
            Random rand = new Random();
            lifeTime = rand.Next(120);
        }

        public int growing()
        {
            if (lifeTime <= 0)
				return 0;
            else
            {
                Random rand = new Random();
                var val = rand.Next(5,10);
                lifeTime -= val;
				
				return val;
            }
        }
    }
}
