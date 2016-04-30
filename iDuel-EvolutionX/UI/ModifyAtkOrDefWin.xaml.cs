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
                try
                {
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
                            {
                                if (card.curDef.Equals("0"))
                                {
                                    return;
                                }
                                double decrease = Convert.ToDouble(tb_atk.Text.Remove(0, 1));
                                if (decrease == 0)
                                {
                                    card.CurAtk = "0";
                                }
                                else
                                {
                                    Double result = Convert.ToDouble(card.curAtk) - decrease;
                                    if (result < 0)
                                    {
                                        return;
                                    }
                                    card.CurAtk = result.ToString();
                                }
                            } 
                            break;
                        default:
                            {
                                double decrease = Convert.ToDouble(tb_atk.Text);
                                if (decrease == 0)
                                {
                                    card.CurAtk = "0";
                                }
                                else
                                {
                                    card.CurAtk = (Convert.ToDouble(card.curAtk) + decrease).ToString();
                                }
                            }
                            break;
                    }
                    tb_atk.Clear();
                }
                catch (Exception)
                {
                       
                }

            }
        }

        private void tb_def_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
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
                            {
                                if (card.curDef.Equals("0"))
                                {
                                    return;
                                }
                                double decrease = Convert.ToDouble(tb_def.Text.Remove(0, 1));
                                if (decrease == 0)
                                {
                                    card.CurDef = "0";
                                }
                                else
                                {
                                    double result = Convert.ToDouble(card.curDef) - decrease;
                                    if (result < 0)
                                    {
                                        return;
                                    }
                                    card.CurDef = result.ToString();
                                }
                            }
                            break;
                        default:
                            {
                                double decrease = Convert.ToDouble(tb_def.Text);
                                if (decrease == 0)
                                {
                                    card.CurDef = "0";
                                }
                                else
                                {
                                    card.CurDef = (Convert.ToDouble(card.curDef) + decrease).ToString();
                                }
                            }
                            break;
                    }
                    tb_atk.Clear();
                }
                catch (Exception)
                {

                }

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
