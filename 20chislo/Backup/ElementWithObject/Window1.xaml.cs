using System;
using System.Collections.Generic;
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

// Дополнительные пространства имен
using System.Windows.Markup;// Для класса XamlReader
using System.IO;            // Для класса FileStream, 
                            // перечислений FileMode и FileAccess
    
namespace ElementWithObject
{
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
    
            //********************************************************
            // Привязка RelativeSource для элементов вкладки Page12
            //********************************************************
            //
            // button12level1
            //
            Binding binding = new Binding();
            binding.Path = new PropertyPath("Tag");
            binding.Mode = BindingMode.OneWay;
            binding.RelativeSource = new RelativeSource(
                RelativeSourceMode.FindAncestor, typeof(StackPanel), 1);
            BindingOperations.SetBinding(button12level1, Button.ContentProperty, binding);
            //
            // button12level2
            //
            binding = new Binding("Tag");
            binding.Mode = BindingMode.OneWay;
            binding.RelativeSource = new RelativeSource(
                RelativeSourceMode.FindAncestor, typeof(StackPanel), 2);
            BindingOperations.SetBinding(button12level2, Button.ContentProperty, binding);
            //
            // textBlock12
            //
            binding = new Binding();
            binding.Path = new PropertyPath("Title");
            binding.Mode = BindingMode.OneWay;
            binding.RelativeSource = new RelativeSource(
                RelativeSourceMode.FindAncestor, typeof(Window), 1);
            BindingOperations.SetBinding(textBlock12, TextBlock.TextProperty, binding);
        }
    
        bool bindingFlag = false;
        private void button8_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if (bindingFlag == false)
            {
                stackPanel8.DataContext = slider8;
                button.Content = "Отключить источник в DataContext";
            }
            else
            {
                stackPanel8.DataContext = null;
                button.Content = "Подключить источник в DataContext";
            }
    
            bindingFlag = !bindingFlag;
        }

        private void documentReader_Loaded(object sender, RoutedEventArgs e)
        {
            String fileName = "Instruct.xaml";
            FileStream xamlFile = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            FlowDocument content = XamlReader.Load(xamlFile) as FlowDocument;
            documentReader.Document = content;
    
            xamlFile.Close();
        }
    }
    
    class Pictures
    {
        // Поле
        public static ImageBrush picture = new ImageBrush();
        public static ImageBrush picture1 = new ImageBrush();
    
        static Pictures()
        {
            picture.ImageSource = new BitmapImage(
                new Uri(@"Images\flower2.jpg", UriKind.Relative));
            picture.Stretch = Stretch.UniformToFill;
            picture.Opacity = 1.0D;
    
            picture1.ImageSource = new BitmapImage(
                new Uri(@"Images\wood.jpg", UriKind.Relative));
            picture1.Stretch = Stretch.Uniform;
            picture1.Opacity = 1.0D;
        }
    
        // Свойство
        public static ImageBrush Picture { get { return picture; } }
        public static ImageBrush Picture1 { get { return picture1; } }
    }

    // Простой пользовательский элемент управления
    class SimpleElement : FrameworkElement
    {
        // Статическое поле зависимости, базовое для свойства
        public static DependencyProperty NumberProperty;
    
        // Инициализация поля в статическом конструкторе
        static SimpleElement()
        {
            NumberProperty = DependencyProperty.Register(
                "Number",
                typeof(double),
                typeof(SimpleElement),
                new FrameworkPropertyMetadata(0.0,
                    FrameworkPropertyMetadataOptions.AffectsRender));
        }
    
        // Свойство зависимости для доступа к полю
        public double Number
        {
            get { return (double)this.GetValue(NumberProperty); }
            set { this.SetValue(NumberProperty, value); }
        }
    
        // Жесткое кодирование размера области вывода
        protected override Size MeasureOverride(Size availableSize)
        {
            return new Size(200, 20);
        } 
    
        // Вывод значения свойства Number
        protected override void OnRender(DrawingContext drawingContext)
        {
            drawingContext.DrawRectangle(Brushes.Yellow, 
                new Pen(Brushes.Red, 1.0), 
                new Rect(new Size(200, 20)));
            drawingContext.DrawText(
                new FormattedText(
                    Convert.ToInt32(Number).ToString(),
                    System.Globalization.CultureInfo.CurrentCulture,
                    FlowDirection.LeftToRight,
                    new Typeface("Arial"),
                    16.0D,
                    SystemColors.WindowTextBrush),
                new Point(0, 0));
        }
    }
}
