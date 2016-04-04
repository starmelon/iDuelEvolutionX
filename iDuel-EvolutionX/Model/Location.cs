using iDuel_EvolutionX.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iDuel_EvolutionX.Model
{
    public class Location
    {
        private int z;
        private Area x;

        public int Z
        {
            get
            {
                return z;
            }

            set
            {
                z = value;
            }
        }

        public Area X
        {
            get
            {
                return x;
            }

            set
            {
                x = value;
            }
        }

        public Location()
        {
            Z = -1;
            X = Area.NON_VALUE;
        }

        public Location(Area area,int z)
        {
            Z = z;
            X = area;
        }
    }
}
