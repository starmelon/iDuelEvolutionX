using iDuel_EvolutionX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace iDuel_EvolutionX.UI
{
    /// <summary>
    /// ModifyAtkOrDef.xaml 的交互逻辑
    /// </summary>
    public partial class ModifyAtkOrDefWin : Window
    {
        CardUI card;
        public ModifyAtkOrDefWin(CardUI card)
        {
            InitializeComponent();
            this.card = card;
            tb_atk.Focus();


        }

        private void btn_ok_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void btn_reset_atk_Click(object sender, RoutedEventArgs e)
        {
            card.CurAtk = card.info.atk;
        }

      

        private void btn_reset_def_Click(object sender, RoutedEventArgs e)
        {
            card.CurDef = card.info.def;
        }

        private void tb_atk_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (string.IsNullOrEmpty(tb_atk.Text))
                {
                    this.Close();
                    return;
                }
                switch (tb_atk.Text[0])
                {
                    case '/':
                        if (card.curDef.Equals("0"))
                        {
                            return;
                        }
                        card.CurAtk = (Convert.ToDouble(card.curAtk) / 2L).ToString();
                        break;
                    case '*':
                        if (card.curDef.Equals("0"))
                        {
                            return;
                        }
                        card.CurAtk = (Convert.ToDouble(card.curAtk) * 2L).ToString();
                        break;
                    case '-':
                        if (card.curDef.Equals("0"))
                        {
                            return;
                        }
                        card.CurAtk = (Convert.ToDouble(card.curAtk) - Convert.ToDouble(tb_atk.Text.Remove(0, 1))).ToString();
                        break;
                    default:
                        card.CurAtk = (Convert.ToDouble(card.curAtk) + Convert.ToDouble(tb_atk.Text)).ToString();
                        break;
                }
                tb_atk.Clear();

            }
        }

        private void tb_def_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                switch (tb_def.Text[0])
                {
                    case '/':
                        if (card.curDef.Equals("0"))
                        {
                            return;
                        }
                        card.CurDef = (Convert.ToDouble(card.curDef) / 2L).ToString();
                        break;
                    case '*':
                        if (card.curDef.Equals("0"))
                        {
                            return;
                        }
                        card.CurDef = (Convert.ToDouble(card.curDef) * 2L).ToString();
                        break;
                    case '-':
                        if (card.curDef.Equals("0"))
                        {
                            return;
                        }
                        card.CurDef = (Convert.ToDouble(card.curDef) - Convert.ToDouble(tb_def.Text.Remove(0, 1))).ToString();
                        break;
                    default:
                        card.CurDef = (Convert.ToDouble(card.curDef) + Convert.ToDouble(tb_def.Text)).ToString();
                        break;
                }
                tb_atk.Clear();

            }
        }

        private void win_modify_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }

  
        private void tb_atk_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            
            if (e.Delta > 0)
            {
                card.CurAtk = (Convert.ToDouble(card.curAtk) + 100d).ToString();
            }
            else
            {
                if (card.curAtk.Equals("0"))
                {
                    return;
                }
                card.CurAtk = (Convert.ToDouble(card.curAtk) - 100d).ToString();
            }
        }

        private void tb_def_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            
            if (e.Delta > 0)
            {
                card.CurDef = (Convert.ToDouble(card.curDef) + 100d).ToString();
            }
            else
            {
                if (card.curDef.Equals("0"))
                {
                    return;
                }
                card.CurDef = (Convert.ToDouble(card.curDef) - 100d).ToString();
            }
        }
    }
}
