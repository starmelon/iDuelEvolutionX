using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iDuel_EvolutionX.Model
{
    class Deck
    {
        string path;

        public string Path
        {
            get { return path; }
            set { path = value; }
        }

        string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        List<CardControl> main;

        public List<CardControl> Main
        {
            get { return main; }
            set { main = value; }
        }
        List<CardControl> extra;

        public List<CardControl> Extra
        {
            get { return extra; }
            set { extra = value; }
        }
        List<CardControl> side;

        public List<CardControl> Side
        {
            get { return side; }
            set { side = value; }
        }


        public Deck()
        {
            main = new List<CardControl>();
            extra = new List<CardControl>();
            side = new List<CardControl>();
        }

        
    }
}
