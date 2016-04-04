using iDuel_EvolutionX.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iDuel_EvolutionX.Model
{
    public class Deck
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

        List<CardUI> main;

        public List<CardUI> Main
        {
            get { return main; }
            set { main = value; }
        }
        List<CardUI> extra;

        public List<CardUI> Extra
        {
            get { return extra; }
            set { extra = value; }
        }
        List<CardUI> side;

        public List<CardUI> Side
        {
            get { return side; }
            set { side = value; }
        }


        public Deck()
        {
            main = new List<CardUI>();
            extra = new List<CardUI>();
            side = new List<CardUI>();
        }

        
    }
}
