using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DataBaseClient
{
    /// <summary>
    /// Логика взаимодействия для ChandgeTableDeleteColumnsView.xaml
    /// </summary>
    public partial class ChandgeTableDeleteColumnsView : Window
    {
        public ChandgeTableDeleteColumnsView()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as ChandgeTableDeleteColumnsVM;
            if (viewModel != null)
            {
                if (viewModel.Table.IsValid())
                {
                    DialogResult = true;
                }
                else
                {
                    ShowMessageError.ShowError("Введене некоректные данные! Заполните поля и попробуйте снова");
                }
            }
        }
    }
}
