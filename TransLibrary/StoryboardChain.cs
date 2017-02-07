using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.Animation;

namespace TransLibrary
{
    [System.Windows.Markup.ContentProperty("Animates")]
    public class StoryboardChain
    {
        private FrameworkElement _obj;
        private List<FrameworkElement> _objs;
        //private Dictionary<FrameworkElement,>
        private List<Storyboard> _Animates = new List<Storyboard>();
        public List<Storyboard> Animates
        {
            get
            {
                return _Animates;
            }
            set
            {
                _Animates = value;
            }
        }

        public void Begin()
        {
           // _obj = containingObject;

            if (Animates.Count == 0)
            {
                return;
            }

            for (int i = 0; i < Animates.Count; ++i)
            {
                Storyboard refBoard = Animates[i];
                int h = (i + 1 == Animates.Count) ? -1 : i + 1;
                ElementIndexer.SetPos(refBoard, h);
                refBoard.Completed -= OnCurrentFinished;
                refBoard.Completed += new EventHandler(OnCurrentFinished);
            }
            Animates[0].Begin();
        }

        

        protected void OnCurrentFinished(object sender, EventArgs e)
        {
            ClockGroup sender_cg = (ClockGroup)sender;
            Storyboard sender_sb = (Storyboard)sender_cg.Timeline;
            if (sender_sb == null)
            {
                return;
            }
            int nextPos = ElementIndexer.GetPos(sender_sb);
            if ((nextPos == -1) || (Animates.Count <= nextPos))
            {
                return;
            }
            Animates[nextPos].Begin();
        }

        public void Begin(FrameworkElement containingObject)
        {
            _obj = containingObject;

            if (Animates.Count == 0)
            {
                return;
            }

            for (int i = 0; i < Animates.Count; ++i)
            {
                Storyboard refBoard = Animates[i];
                int h = (i + 1 == Animates.Count) ? -1 : i + 1;

                ElementIndexer.SetPos(refBoard, h);
                refBoard.Completed -= OnCurrentFinished2;
                refBoard.Completed += new EventHandler(OnCurrentFinished2); 
            }
            Animates[0].Begin(containingObject);
        }

        protected void OnCurrentFinished2(object sender, EventArgs e)
        {
            ClockGroup sender_cg = (ClockGroup)sender;
            Storyboard sender_sb = (Storyboard)sender_cg.Timeline;
            
            if (sender_sb == null)
            {
                return;
            }
            int nextPos = ElementIndexer.GetPos(sender_sb);
            if ((nextPos == -1) || (Animates.Count <= nextPos))
            {
                return;
            }
            Animates[nextPos].Begin(_obj);
        }

        public void Begin(List<FrameworkElement> containingObjects)
        {
            _objs = containingObjects;

            if (Animates.Count == 0)
            {
                return;
            }

            for (int i = 0; i < Animates.Count; ++i)
            {
                Storyboard refBoard = Animates[i];
                int h = (i + 1 == Animates.Count) ? -1 : i + 1;
                ElementIndexer.SetPos(refBoard, h);
                refBoard.Completed -= OnCurrentFinished3;
                refBoard.Completed += new EventHandler(OnCurrentFinished3);
            }
            Animates[0].Begin(containingObjects[0]);
        }

        protected void OnCurrentFinished3(object sender, EventArgs e)
        {
            ClockGroup sender_cg = (ClockGroup)sender;
            Storyboard sender_sb = (Storyboard)sender_cg.Timeline;
            if (sender_sb == null)
            {
                return;
            }
            int nextPos = ElementIndexer.GetPos(sender_sb);
            if ((nextPos == -1) || (Animates.Count <= nextPos))
            {
                return;
            }
            Animates[nextPos].Begin(_objs[nextPos]);
        }

        public StoryboardChain addAnime(Storyboard sb)
        {
            this.Animates.Add(sb);
            return this;
        }

        public StoryboardChain addComplete(EventHandler complete)
        {
            int animeCount = _Animates.Count;
            if (animeCount > 0)
            {
                _Animates[animeCount - 1].Completed += complete;
            }
            return this;
        }

    }
}
