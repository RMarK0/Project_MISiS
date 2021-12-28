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

namespace Project_MISiS
{
    /// <summary>
    /// Логика взаимодействия для HelpPage.xaml
    /// </summary>
    public partial class HelpPage : Page
    {
        private NavigationService _navigation;
        public HelpPage()
        {
            InitializeComponent();
            MainTextBlock.Text = "Разделение мусора (также раздельный сбор мусора, селективный сбор мусора) — практика сбора и сортировки мусора с учётом" +
                                 " его происхождения и пригодности к переработке или вторичному использованию. Раздельный сбор мусора позволяет отделить " +
                                 "перерабатываемые отходы от неперерабатываемых, а также выделить отдельные типы отходов, пригодные для вторичного использования. " +
                                 "Эти действия позволяют не только вернуть в промышленный оборот максимум материалов, но и сократить расходы на вывоз мусора, " +
                                 "его промышленное сепарирование, а также снизить углеродный след, общее загрязнение окружающей среды, в том числе сократить площадь " +
                                 "мусорных полигонов.\n\n" +
                                 "В основе системы разделения мусора лежит идея поддержки устойчивого природопользования и минимизации потерь ценных материалов." +
                                 " Раздельный сбор мусора предполагает его самостоятельное разделение каждым человеком, и эффективность разделения требует сознательности" +
                                 " и понимания процесса всеми его участниками. Таким образом, проводимое по правилам бытовое разделение мусора позволяет избежать затрат" +
                                 " на его промышленную сепарацию на сортировочных комплексах, а население становится полноценным участником в процессе переработки отходов.";
        }

        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            _navigation = NavigationService.GetNavigationService(this);
            MainMenuPage page = new MainMenuPage();
            if (_navigation != null)
                _navigation.Navigate(page);
        }
    }
}
