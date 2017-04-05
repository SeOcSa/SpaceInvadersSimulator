using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvadersSimulator
{
    class Wbeing
    {
        private int lifeTime;
        public Wbeing()
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
                var val = rand.Next(1,3);
                lifeTime -= val;
				
				return val;
            }
        }
    }
}
