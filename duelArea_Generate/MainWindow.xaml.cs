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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace duelArea_Generate
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            result.Clear();

            double card_Margin = Convert.ToDouble(card_margin.Text.ToString());
            double card_Height = Convert.ToDouble(card_height.Text.ToString());
            double card_Width = Convert.ToDouble(card_width.Text.ToString());
            double card_Hand_Height   = Convert.ToDouble(card_hand_height.Text.ToString());
            double canvas_Range = Convert.ToDouble(canvas_range.Text.ToString());

            double monster_row = 4;
            double monster_col = 5;
            double other_row = 6;
            double other_col = 2;
            double hand_row = 2;
            double graveyard_row = 1;

            double window_height = ((card_Height + card_Margin * 2) * monster_row) + ((card_Width + card_Margin * 2) * graveyard_row) + (card_Hand_Height * hand_row) + canvas_Range * 6;
            double window_width = ((card_Height + card_Margin * 2) * monster_col) + ((card_Width + card_Margin * 2) * other_col) + canvas_Range * 6;

            double other_Cav2Top = ( window_height - (((card_Height + card_Margin * 2 ) * other_row) + canvas_Range * 7 ) )* 0.5;

            result.AppendText("Height = \"" + window_height + "\" " + "Width = \"" + window_width + "\"" + Environment.NewLine + Environment.NewLine);

            result.AppendText("<!-- 左边Canvas -->" + Environment.NewLine);

            for (int i = 0; i < 6; i++)
            {
                //左边
                switch(i)
                {
                    case 0:
                        result.AppendText("<Canvas AllowDrop = \"False\" Canvas.Left = \"0\" Canvas.Top = \"" + (other_Cav2Top + ((card_Height + card_Margin * 2) * i) + canvas_Range * (i+1)) + "\" Width = \"" + (card_Width + card_Margin * 2) + "\" Height = \"" + (card_Height + card_Margin * 2) + "\" Background=\"Transparent\" Name = \"card_2_Deck\" />\r\n"); 
                        break;
                    case 1:
                        result.AppendText("<Canvas AllowDrop = \"False\" Canvas.Left = \"0\" Canvas.Top = \"" + (other_Cav2Top + ((card_Height + card_Margin * 2) * i) + canvas_Range * (i + 1)) + "\" Width = \"" + (card_Width + card_Margin * 2) + "\" Height = \"" + (card_Height + card_Margin * 2) + "\" Background=\"Transparent\" Name = \"card_2_Right\" />\r\n"); 
                        break;
                    case 2:
                        result.AppendText("<Canvas AllowDrop = \"True\" Canvas.Left = \"0\" Canvas.Top = \"" + (other_Cav2Top + ((card_Height + card_Margin * 2) * i) + canvas_Range * (i + 1)) + "\" Width = \"" + (card_Width + card_Margin * 2) + "\" Height = \"" + (card_Height + card_Margin * 2) + "\" Background=\"Transparent\" Name = \"card_2_Graveyard\" />\r\n"); 
                        break;
                    case 3:
                        result.AppendText("<Canvas AllowDrop = \"True\" Canvas.Left = \"0\" Canvas.Top = \"" + (other_Cav2Top + ((card_Height + card_Margin * 2) * i) + canvas_Range * (i + 1)) + "\" Width = \"" + (card_Width + card_Margin * 2) + "\" Height = \"" + (card_Height + card_Margin * 2) + "\" Background=\"Transparent\" Name = \"card_1_Area\" />\r\n");
                        break;
                    case 4:
                        result.AppendText("<Canvas AllowDrop = \"True\" Canvas.Left = \"0\" Canvas.Top = \"" + (other_Cav2Top + ((card_Height + card_Margin * 2) * i) + canvas_Range * (i + 1)) + "\" Width = \"" + (card_Width + card_Margin * 2) + "\" Height = \"" + (card_Height + card_Margin * 2) + "\" Background=\"Transparent\" Name = \"card_1_Left\" />\r\n");
                        break;
                    case 5:
                        result.AppendText("<Canvas AllowDrop = \"True\" Canvas.Left = \"0\" Canvas.Top = \"" + (other_Cav2Top + ((card_Height + card_Margin * 2) * i) + canvas_Range * (i + 1)) + "\" Width = \"" + (card_Width + card_Margin * 2) + "\" Height = \"" + (card_Height + card_Margin * 2) + "\" Background=\"Transparent\" Name = \"card_1_Extra\" />\r\n");
                        break;
                }
                //result.AppendText("<Canvas AllowDrop = \"True\" Canvas.Left = \"0\" Canvas.Top = \"" + (other_Cav2Top + ((card_Height + card_Margin * 2) * i)) + "\" Width = \"" + (card_Width + card_Margin * 2) + "\" Height = \"" + (card_Height + card_Margin * 2) + "\" />\r\n" );
            }

            result.AppendText(Environment.NewLine + "<!-- 右边Canvas -->" + Environment.NewLine);

            for (int i = 0; i < 6; i++)
            {
                //右边
                switch (i)
                {
                    case 0:
                        result.AppendText("<Canvas AllowDrop = \"False\" Canvas.Right =\"0\" Canvas.Top =\"" + (other_Cav2Top + ((card_Height + card_Margin * 2) * i) + canvas_Range * (i + 1)) + "\" Width = \"" + (card_Width + card_Margin * 2) + "\" Height = \"" + (card_Height + card_Margin * 2) + "\" Background=\"Transparent\" Name = \"card_2_Extra\" />\r\n");
                        break;
                    case 1:
                        result.AppendText("<Canvas AllowDrop = \"False\" Canvas.Right =\"0\" Canvas.Top =\"" + (other_Cav2Top + ((card_Height + card_Margin * 2) * i) + canvas_Range * (i + 1)) + "\" Width = \"" + (card_Width + card_Margin * 2) + "\" Height = \"" + (card_Height + card_Margin * 2) + "\" Background=\"Transparent\" Name = \"card_2_Left\" />\r\n");
                        break;
                    case 2:
                        result.AppendText("<Canvas AllowDrop = \"False\" Canvas.Right =\"0\" Canvas.Top =\"" + (other_Cav2Top + ((card_Height + card_Margin * 2) * i) + canvas_Range * (i + 1)) + "\" Width = \"" + (card_Width + card_Margin * 2) + "\" Height = \"" + (card_Height + card_Margin * 2) + "\" Background=\"Transparent\" Name = \"card_2_Area\" />\r\n");
                        break;
                    case 3:
                        result.AppendText("<Canvas AllowDrop = \"True\" Canvas.Right =\"0\" Canvas.Top =\"" + (other_Cav2Top + ((card_Height + card_Margin * 2) * i) + canvas_Range * (i + 1)) + "\" Width = \"" + (card_Width + card_Margin * 2) + "\" Height = \"" + (card_Height + card_Margin * 2) + "\" Background=\"Transparent\" Name = \"card_1_Graveyard\" />\r\n");
                        break;
                    case 4:
                        result.AppendText("<Canvas AllowDrop = \"True\" Canvas.Right =\"0\" Canvas.Top =\"" + (other_Cav2Top + ((card_Height + card_Margin * 2) * i) + canvas_Range * (i + 1)) + "\" Width = \"" + (card_Width + card_Margin * 2) + "\" Height = \"" + (card_Height + card_Margin * 2) + "\" Background=\"Transparent\" Name = \"card_1_Right\" />\r\n");
                        break;
                    case 5:
                        result.AppendText("<Canvas AllowDrop = \"True\" Canvas.Right =\"0\" Canvas.Top =\"" + (other_Cav2Top + ((card_Height + card_Margin * 2) * i) + canvas_Range * (i + 1)) + "\" Width = \"" + (card_Width + card_Margin * 2) + "\" Height = \"" + (card_Height + card_Margin * 2) + "\" Background=\"Transparent\" Name = \"card_1_Deck\" />\r\n");
                        break;
                }
                //result.AppendText("<Canvas AllowDrop = \"True\" Canvas.Right =\"0\" Canvas.Top =\"" + (other_Cav2Top + ((card_Height + card_Margin * 2) * i)) + "\" Width = \"" + (card_Width + card_Margin * 2) + "\" Height = \"" + (card_Height + card_Margin * 2) + "\" />\r\n");
            }

            result.AppendText(Environment.NewLine + "<!-- 敌方魔陷&怪物区Canvas -->" + Environment.NewLine);

            for (int i = 0; i < 10; i++)
            {
                if (i<5)
                {
                    result.AppendText("<Canvas AllowDrop = \"True\" Canvas.Top = \"" + (card_Hand_Height + canvas_Range) + "\" Canvas.Right = \"" + ((card_Width + card_Margin * 2) + ((card_Height + card_Margin * 2) * i) + canvas_Range * (i+1)) + "\" Width = \"87\" Height = \"87\" Background=\"LightBlue\" Name = \"card_2_" + (i + 1) + "\" + RenderTransformOrigin=\"0.5,0.5\" >\r\n");
                    result.AppendText("    <Canvas.RenderTransform>\r\n");
                    result.AppendText("        <TransformGroup>\r\n");
                    result.AppendText("            <ScaleTransform/>\r\n");
                    result.AppendText("            <SkewTransform/>\r\n");
                    result.AppendText("            <RotateTransform Angle = \"180\"/>\r\n");
                    result.AppendText("            <TranslateTransform/>\r\n");
                    result.AppendText("        </TransformGroup>\r\n");
                    result.AppendText("    </Canvas.RenderTransform>\r\n");
                    result.AppendText("</Canvas>\r\n");
                }
                else
                {
                    result.AppendText("<Canvas AllowDrop = \"True\" Canvas.Top = \"" + (card_Hand_Height + (card_Height + card_Margin * 2) + canvas_Range * 2) + "\" Canvas.Right = \"" + ((card_Width + card_Margin * 2) + ((card_Height + card_Margin * 2) * (i - 5)) + canvas_Range * (i + 1 - 5)) + "\" Width = \"87\" Height = \"87\" Background=\"LightBlue\" Name = \"card_2_" + (i + 1) + "\" RenderTransformOrigin=\"0.5,0.5\" >\r\n");
                    result.AppendText("    <Canvas.RenderTransform>\r\n");
                    result.AppendText("        <TransformGroup>\r\n");
                    result.AppendText("            <ScaleTransform/>\r\n");
                    result.AppendText("            <SkewTransform/>\r\n");
                    result.AppendText("            <RotateTransform Angle = \"180\"/>\r\n");
                    result.AppendText("            <TranslateTransform/>\r\n");
                    result.AppendText("        </TransformGroup>\r\n");
                    result.AppendText("    </Canvas.RenderTransform>\r\n");
                    result.AppendText("</Canvas>\r\n");
                }
                
            }

            result.AppendText(Environment.NewLine + Environment.NewLine + "<!-- 己方除外Canvas -->" + Environment.NewLine);
            result.AppendText("<Canvas AllowDrop = \"True\" Canvas.Bottom = \"" + (card_Hand_Height + ((card_Height + (card_Margin * 2)) * 2) + canvas_Range * 3) + "\" Canvas.Right = \"" + canvas_Range +  "\" Width = \"" + (card_Width + card_Margin * 2) + "\" Height = \"" + (card_Height + card_Margin * 2) + "\" Background = \"Black\" Name = \"card_1_Outside\" RenderTransformOrigin=\"0,1\" >\r\n");
            result.AppendText("    <Canvas.RenderTransform>\r\n");
            result.AppendText("        <TransformGroup>\r\n");
            result.AppendText("            <ScaleTransform/>\r\n");
            result.AppendText("            <SkewTransform/>\r\n");
            result.AppendText("            <RotateTransform Angle = \"-90\"/>\r\n");
            result.AppendText("            <TranslateTransform/>\r\n");
            result.AppendText("        </TransformGroup>\r\n");
            result.AppendText("    </Canvas.RenderTransform>\r\n");
            result.AppendText("</Canvas>\r\n");
                
            result.AppendText(Environment.NewLine + Environment.NewLine + "<!-- 对方除外区Canvas -->" + Environment.NewLine);
            result.AppendText("<Canvas AllowDrop = \"False\" Canvas.Bottom = \"" + (card_Hand_Height + ((card_Height + (card_Margin * 2)) * 2) + canvas_Range * 3) + "\" Canvas.Left = \"" + canvas_Range + "\" Width = \"" + (card_Width + card_Margin * 2) + "\" Height = \"" + (card_Height + card_Margin * 2) + "\" Background = \"Black\" Name = \"card_2_Outside\" RenderTransformOrigin=\"1,1\" >\r\n");
            result.AppendText("    <Canvas.RenderTransform>\r\n");
            result.AppendText("        <TransformGroup>\r\n");
            result.AppendText("            <ScaleTransform/>\r\n");
            result.AppendText("            <SkewTransform/>\r\n");
            result.AppendText("            <RotateTransform Angle = \"90\"/>\r\n");
            result.AppendText("            <TranslateTransform/>\r\n");
            result.AppendText("        </TransformGroup>\r\n");
            result.AppendText("    </Canvas.RenderTransform>\r\n");
            result.AppendText("</Canvas>\r\n");


            result.AppendText(Environment.NewLine + Environment.NewLine + "<!-- 我方魔陷&怪物区Canvas -->" + Environment.NewLine);

            for (int i = 0; i < 10; i++)
            {
                if (i < 5)
                {

                    result.AppendText("<Canvas AllowDrop = \"True\" Canvas.Bottom = \"" + (card_Hand_Height + canvas_Range) + "\" Canvas.Left = \"" + ((card_Width + card_Margin * 2) + ((card_Height + card_Margin * 2) * i) + canvas_Range * (i+1)) + "\" Width = \"87\" Height = \"87\" Background=\"LightBlue\" Name = \"card_1_" + (i + 1) + "\" />\r\n");
                    //result.AppendText("<Canvas AllowDrop = \"True\" Canvas.Buttom = \"" + card_Hand_Height + "\" Canvas.Left = \"" + ((card_Width + card_Margin * 2) + ((card_Height + card_Margin * 2) * i)) + "\" Width = \"87\" Height = \"87\" Background=\"LightBlue\" />");
                }
                else
                {
                    result.AppendText("<Canvas AllowDrop = \"True\" Canvas.Bottom = \"" + (card_Hand_Height + (card_Height + card_Margin * 2) + canvas_Range * 2) + "\" Canvas.Left = \"" + ((card_Width + card_Margin * 2) + ((card_Height + card_Margin * 2) * (i - 5)) + canvas_Range * (i+1 - 5)) + "\" Width = \"87\" Height = \"87\" Background=\"LightBlue\" Name = \"card_1_" + (i + 1) + "\" />\r\n");
                    //result.AppendText("<Canvas AllowDrop = \"True\" Canvas.Buttom = \"" + (card_Hand_Height + (card_Height + card_Margin * 2)) + "\" Canvas.Left = \"" + ((card_Width + card_Margin * 2) + ((card_Height + card_Margin * 2) * i)) + "\" Width = \"87\" Height = \"87\" Background=\"LightBlue\" />");

                }

            }
 
        }
    }
}
