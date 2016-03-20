using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Documents;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using iDuel_EvolutionX.Model;
using NBX3.Service;
using System.Windows.Controls;

namespace iDuel_EvolutionX.UI
{
public class DragAdorner : Adorner
{
    #region 变量
    protected UIElement _child;
    protected VisualBrush _brush;
    protected UIElement _owner;
    protected double XCenter;
    protected double YCenter;

    private double _leftOffset;
    private double _topOffset;
    public double Scale = 1.0;
    #endregion

    #region 构造函数
    public DragAdorner(UIElement owner) : base(owner) { }

    public DragAdorner(UIElement owner, UIElement adornElement, double opacity)
        : base(owner)
    {
        this._owner = owner;

        Rectangle r = new Rectangle();
        r.RadiusX = 3;
        r.RadiusY = 3;

        r.Width = adornElement.DesiredSize.Width;
        r.Height = adornElement.DesiredSize.Height; 

        XCenter = adornElement.DesiredSize.Width / 2;
        YCenter = adornElement.DesiredSize.Height / 2;

        VisualBrush _brush;
        Image img = new Image();
            img.Source = DuelOperate.getInstance().myself.cardback;
            _brush = new VisualBrush(img);

        //    if (isback)
        //{
        //    //Image img = new Image();
        //    img.Source = DuelOperate.getInstance().myself.cardback;
        //    _brush = new VisualBrush(img);

            
        //}
        //else
        //{
        //    Card card = adornElement as Card;
        //    img.Source = card.Source;
        //    //if (isdef)
        //    //{
        //    //    r.Height = adornElement.DesiredSize.Width;
        //    //    r.Width = adornElement.DesiredSize.Height; 
        //    //}

        //    _brush = new VisualBrush(img);
        //}
        //if (isdef)
        //{
        //    r.RenderTransformOrigin = new Point(0.5, 0.5);
        //    r.RenderTransform = new RotateTransform(-90);
        //}
     
        _brush.Opacity = opacity;
        

        r.Fill = _brush;
        this._child = r;
    }
    #endregion

    #region 属性
    public double LeftOffset
    {
        get { return this._leftOffset; }
        set
        {
            this._leftOffset = value - XCenter;
            this.UpdatePosition();
        }
    }

    public double TopOffset
    {
        get { return this._topOffset; }
        set
        {
            this._topOffset = value - YCenter;
            this.UpdatePosition();
        }
    }

    protected override int VisualChildrenCount
    {
        get
        {
            return 1;
        }
    }
    #endregion

    #region 方法
    private void UpdatePosition()
    {
        AdornerLayer adorner = (AdornerLayer)this.Parent;
        if (adorner != null)
        {
            adorner.Update(this.AdornedElement);
        }
    }

    protected override Visual GetVisualChild(int index)
    {
        return _child;
    }

    protected override Size MeasureOverride(Size finalSize)
    {
        this._child.Measure(finalSize);
        return this._child.DesiredSize;
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
        this._child.Arrange(new Rect(_child.DesiredSize));
        return finalSize;
    }

    public override GeneralTransform GetDesiredTransform(GeneralTransform transform)
    {
        GeneralTransformGroup result = new GeneralTransformGroup();

        result.Children.Add(base.GetDesiredTransform(transform));
        result.Children.Add(new TranslateTransform(this._leftOffset, this._topOffset));
        return result;
    }
    #endregion
}
}
