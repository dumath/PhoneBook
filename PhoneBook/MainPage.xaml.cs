using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace PhoneBook
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private const string NUMS = "0123456789"; //Допустимые символы в phone.Text;
        ViewModel vm = null;
        public MainPage()
        {
            this.InitializeComponent();
            vm = new ViewModel();
        }

        /// <summary>
        /// Обработчик события. Каждое изменение символа в phone.Text(TextBox) обрабатывается этим методом.
        /// </summary>
        /// <param name="sender"> </param>
        /// <param name="e"></param>
        public void FindByPhone(object sender, TextChangedEventArgs e)
        {
            name.TextChanged -= FindByName;
            if(checkSymbols(phone.Text))
            {
                long num = long.Parse(phone.Text);
                List<Contact> contacts = vm[num];
                if (contacts != null && contacts.Count != 0)
                {
                    name.Text = contacts[0].Name;
                    if (contacts.Count > 1)
                    {
                        StackPanel matchedByPhoneNames = new StackPanel();
                        foreach (Contact c in contacts)
                            matchedByPhoneNames.Children.Add(new ListViewItem() { Content = c.Name });
                        Flyout matchedNamesFlyout = new Flyout();
                        matchedNamesFlyout.Content = matchedByPhoneNames;
                        name.ContextFlyout = matchedNamesFlyout;
                        //name.ContextFlyout.ShowMode = FlyoutShowMode.Auto;
                        name.ContextFlyout.Placement = FlyoutPlacementMode.BottomEdgeAlignedLeft;
                        name.ContextFlyout.ShowAt(name);
                    }
                }
            }
            name.TextChanged += FindByName;
        }

        /// <summary>
        /// Обработчик события. Каждое изменение символа в name.Text(TextBox) обрабатывается этим методом.
        /// </summary>
        /// <param name="sender"> </param>
        /// <param name="e"></param>
        public void FindByName(object sender, TextChangedEventArgs e)
        {
            phone.TextChanged -= FindByPhone;

            List<Contact> contacts = vm[name.Text];
            if (contacts != null && contacts.Count != 0)
            {
                phone.Text = contacts[0].Number.ToString();
                if (contacts.Count > 1)
                {
                    StackPanel matchedByNamePhone = new StackPanel();
                    foreach (Contact c in contacts)
                    {
                        matchedByNamePhone.Children.Add(new ListViewItem() { Content = c.Number });
                    }
                    Flyout matchedNumbersFlyout = new Flyout();
                    matchedNumbersFlyout.Content = matchedByNamePhone;
                    name.ContextFlyout = matchedNumbersFlyout;
                    //name.ContextFlyout.ShowMode = FlyoutShowMode.Auto;
                    name.ContextFlyout.Placement = FlyoutPlacementMode.BottomEdgeAlignedRight;
                    name.ContextFlyout.ShowAt(phone);
                }
            }
            phone.TextChanged += FindByPhone;
        }

        //TODO: ?
        private void flyoutPhoneClick(object sender, TappedRoutedEventArgs e)
        {

        }

        /// <summary>
        /// Метод тестирует входящую строку на предмет наличия букв в числах.
        /// </summary>
        /// <param name="text"> Тестируемая строка </param>
        /// <returns> False, если буквы присутствуют, строка пустая(Empty), либо ссылается на Null значение. </returns>
        private bool checkSymbols(string text)
        {
            if (text == null || text == string.Empty)
                return false;
            foreach (char c in text)
            {
                if (!NUMS.Contains(c))
                    return false;
            }
            return true;
        }
    }
}
