using iDuel_EvolutionX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public partial class ModifyAtkOrDef : Window
    {
        CardControl card;
        public ModifyAtkOrDef(CardControl card)
        {
            InitializeComponent();
            this.card = card;

          
        }

        private void btn_ok_Click(object sender, RoutedEventArgs e)
        {
            if (!tb_atk.Text.Equals(card.CurAtk))
            {
                card.CurAtk = tb_atk.Text;
            }
            if (!tb_def.Text.Equals(card.CurDef))
            {
                card.CurDef = tb_def.Text;
            }
            
            this.Close();
        }
    }
}
