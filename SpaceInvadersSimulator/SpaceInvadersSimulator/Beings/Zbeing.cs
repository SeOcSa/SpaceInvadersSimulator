using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvadersSimulator
{
    class Zbeing
    {
        private int lifeTime;
        public Zbeing()
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
                var val = rand.Next(3,5);
                lifeTime -= val;
				
				return val;
            }
        }
    }
}
