using iDuel_EvolutionX.Model;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
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

namespace iDuel_EvolutionX.UI
{
    //定义一个当控件拥有元素发生变化时所接受的委托
    public delegate void CollectionChangeDelegate(MyCanvas mcv, CardUI card);

    /// <summary>
    /// MyCanvas.xaml 的交互逻辑
    /// </summary>
    public class MyCanvas : Canvas
    {
        public Area area = Area.NON_VALUE;
        public Position x;
        public TextBlock tb_atkDef;
        public StackPanel signs;
        private event CollectionChangeDelegate whenRemoveChildren;
        public CollectionChangeDelegate WhenRemoveChildren
        {
            get
            {
                return whenRemoveChildren;
            }

            set
            {
                whenRemoveChildren = value;

            }
        }
        private event CollectionChangeDelegate whenAddChildren;
        public CollectionChangeDelegate WhenAddChildren
        {
            get
            {
                return whenAddChildren;
            }

            set
            {
                whenAddChildren = value;
            }
        }
        private event CollectionChangeDelegate whenInsertChildren;
        public CollectionChangeDelegate WhenInsertChildren
        {
            get
            {
                return whenInsertChildren;
            }

            set
            {
                whenInsertChildren = value;
            }
        }


        public MyCanvas()
        {
            //InitializeComponent();
        }



        protected override UIElementCollection CreateUIElementCollection(FrameworkElement logicalParent)
        {
            return new ObservableUIElementCollection(this, logicalParent);
        }
    }

    /// <summary>
    ///自定义的转换器
    /// </summary>
    [ValueConversion(typeof(int), typeof(string))]
    public class CountToDimensionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int re = (int)value;

            return re.ToString();


        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string strValue = value as string;
            return Int32.Parse(strValue);
            //return DependencyProperty.UnsetValue;
        }
    }



    /// <summary>
    ///自定义的转换器
    /// </summary>
    [ValueConversion(typeof(int), typeof(string))]
    public class CountToDimensionConverter2 : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            //value.ToString();
            //MainWindow mw = Application.Current.MainWindow as MainWindow;
            //MyCanvas mcv = mw.FindName(parameter.ToString()) as MyCanvas;

            MyCanvas mcv = parameter as MyCanvas;
            //UIElementCollection vc = value as UIElementCollection;
            if (mcv.Children.Count > 0)
            {
                CardUI card = mcv.Children[mcv.Children.Count - 1] as CardUI;
                if (card.info.sCardType.Contains("魔法") || card.info.sCardType.Contains("陷阱"))
                {
                    return null;
                }
                return card.CurAtk + "/" + card.CurDef;
            }
            else if (mcv.Children.Count < 1)
            {
                return null;
            }

            return null;
            //int re = (int)value;




        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string strValue = value as string;
            return null;
            //return DependencyProperty.UnsetValue;
        }
    }

    /// <summary>
    ///自定义的转换器
    /// </summary>
    [ValueConversion(typeof(int), typeof(string))]
    public class CountToDimensionConverter3 : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            //value.ToString();
            //MainWindow mw = Application.Current.MainWindow as MainWindow;
            //MyCanvas mcv = mw.FindName(parameter.ToString()) as MyCanvas;

            MyCanvas mcv = parameter as MyCanvas;
            //UIElementCollection vc = value as UIElementCollection;
            if (mcv.Children.Count > 0)
            {
                Card card = mcv.Children[mcv.Children.Count - 1] as Card;
                if (card.isBack == true)
                {
                    return null;
                }
                if (card.sCardType.Contains("魔法") || card.sCardType.Contains("陷阱"))
                {
                    return null;
                }
                return card.atk + "/" + card.def;
            }

            return null;
            //int re = (int)value;




        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string strValue = value as string;
            return null;
            //return DependencyProperty.UnsetValue;
        }
    }


    public class ObservableUIElementCollection : UIElementCollection, INotifyCollectionChanged, INotifyPropertyChanged
    {
        private MyCanvas owner;

        public ObservableUIElementCollection(UIElement visualParent, FrameworkElement logicalParent)
            : base(visualParent, logicalParent)
        {
            owner = visualParent as MyCanvas;
        }

        public override int Add(UIElement element)
        {
            int index = base.Add(element);
            var args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, element, index);
            OnCollectionChanged(args);
            OnPropertyChanged("Count");
            OnPropertyChanged("Item[]");
            if (owner.WhenAddChildren != null)
            {
                owner.WhenAddChildren(owner, element as CardUI);
            }

            return index;
        }

        public override void Clear()
        {
            base.Clear();
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            OnPropertyChanged("Count");
            OnPropertyChanged("Item[]");
        }

        public override void Insert(int index, UIElement element)
        {
            base.Insert(index, element);
            var args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, element, index);
            OnCollectionChanged(args);
            OnPropertyChanged("Count");
            OnPropertyChanged("Item[]");
            if (owner.WhenInsertChildren != null)
            {
                owner.WhenInsertChildren(owner, element as CardUI);
            }
        }

        public override void Remove(UIElement element)
        {
            int index = IndexOf(element);
            if (index >= 0)
            {
                RemoveAt(index);
                var args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, element, index);
                OnCollectionChanged(args);
                OnPropertyChanged("Count");
                OnPropertyChanged("Item[]");
                if (owner.WhenRemoveChildren != null)
                {
                    owner.WhenRemoveChildren(owner, element as CardUI);
                }

            }

        }

        public override UIElement this[int index]
        {
            get
            {
                return base[index];
            }
            set
            {
                base[index] = value;
                var args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, value, index);
                OnCollectionChanged(args);
                OnPropertyChanged("Item[]");
            }
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            var handler = CollectionChanged;
            if (handler != null)
                handler(this, e);
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

