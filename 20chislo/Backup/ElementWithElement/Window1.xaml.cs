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
    
namespace ElementWithElement
{
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
    
            // Создаем и настраиваем объект привязки
            Binding binding = new Binding();
            binding.Source = slider8;// Источник 
            binding.Path = new PropertyPath("Value"); 
            binding.Mode = BindingMode.OneWay;
    
            // Привязываем элементы-приемники
            label8.SetBinding(Label.ContentProperty, binding);          // Label
            textBlock8.SetBinding(TextBlock.FontSizeProperty, binding); // TextBlock
    
            // Удаление декларативных привязок во вкладке Page9
            BindingOperations.ClearBinding(label9, Label.ContentProperty);
            BindingOperations.ClearAllBindings(textBlock9);
    
            //******************************************************
            // Привязываем элементы во вкладке Page9 по новой схеме
            //******************************************************
            // Создаем и настраиваем узел привязки
            binding = new Binding();
            binding.Source = slider9;// Источник 
            binding.Path = new PropertyPath("Value");
            binding.Mode = BindingMode.OneWay;
    
            // Привязываем label9 к бегунку slider9
            label9.SetBinding(Label.ContentProperty, binding);
    
            // Привязываем textBlock9 не к slider9, а к label9,
            // реализуя схему цепочки привязок (для разнообразия).
            // Для этого надо создать новый узел привязки!!!
            binding = new Binding();
            binding.Source = label9;// Источник 
            binding.Path = new PropertyPath("Content");
            binding.Mode = BindingMode.OneWay;
    
            textBlock9.SetBinding(TextBlock.FontSizeProperty, binding);
            slider9.Value = 21;// Чуть отступим вправо
            //******************************************************
        }
    
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Инициализация для Page1
            int value = Convert.ToInt32(textBlock1.FontSize);// Явное приведение типов
            slider1.Minimum = value;    // Тип int приводится к double по умолчанию
            label1.Content = value.ToString();// Content имеет тип Object и проглотит все
        }
    
        // Обработчик для Page1
        private void slider1_ValueChanged(object sender, 
            RoutedPropertyChangedEventArgs<double> e)
        {
            if (!this.IsLoaded)
                return;// Событие возбуждается, а элементы еще не созданы
    
            Slider slider = (Slider)sender; // Повышаем полномочия ссылки
            int value = (int)slider.Value;  // Явное приведение типов
            label1.Content = value;         // Content имеет тип Object и проглотит все
            textBlock1.FontSize = value;    // Тип int приводится к double неявно
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Получить выражение привязки к свойству зависимости Text, 
            // которая размещена в текстовом элементе TextBox с именем textBox11
            BindingExpression binding = 
                textBox11.GetBindingExpression(TextBox.TextProperty);
            // Обновить связанный элемент FlowDocument
            binding.UpdateSource();
        }
    }
}