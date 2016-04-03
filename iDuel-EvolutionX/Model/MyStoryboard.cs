using System;
using System.Collections.Generic;
using iDuel_EvolutionX.UI;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace iDuel_EvolutionX.Model
{
    class MyStoryboard : Storyboard
    {
        public UIElement sword { get; set; }
        public CardUI card { get; set; }
        public List<CardUI> cards;
    }
}
