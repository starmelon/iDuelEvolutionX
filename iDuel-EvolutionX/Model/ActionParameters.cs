using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace iDuel_EvolutionX.Model
{
    class ActionParameters
    {
        private MainWindow mainwindow;

        private int cv_num;

        public int Cv_num
        {
            get { return cv_num; }
            set 
            { 
                cv_num = value; 
            }
        }
        private int cv_children_num;

        public int Cv_children_num
        {
            get { return cv_children_num; }
            set { cv_children_num = value; }
        }
        private int cv_aim_num;

        public int Cv_aim_num
        {
            get { return cv_aim_num; }
            set { cv_aim_num = value; }
        }


        public ActionParameters()
        {
 
        }

        public ActionParameters(MainWindow mainwindow_)
        {
            mainwindow = mainwindow_;
        }

        public ActionParameters(MainWindow mainwindow_, Card card,Canvas cv_aim)
        {
            mainwindow = mainwindow_;
            Canvas cv = card.Parent as Canvas;
            cv_num = mainwindow.MySpace.Children.IndexOf(cv);
            cv_children_num = cv.Children.IndexOf(card);
            cv_aim_num = mainwindow.MySpace.Children.IndexOf(cv_aim);
 
        }

        public void card2num(Card card)
        {
            Canvas cv = card.Parent as Canvas;
            cv_children_num = cv.Children.IndexOf(card);
        }

        public void cv_aim2num(Canvas cv_aim)
        {
            Cv_aim_num = mainwindow.MySpace.Children.IndexOf(cv_aim);
        }

        public void cv2num (Card card)
        {
            Canvas cv = card.Parent as Canvas;
            Cv_num = mainwindow.MySpace.Children.IndexOf(cv);
        }

    }
}
