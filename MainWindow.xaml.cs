using HomeWorkBankWorkersInterface.BankSystem;
using HomeWorkBankWorkersInterface.BankSystem.BankData;
using HomeWorkBankWorkersInterface.BankSystem.BankWorkers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HomeWorkBankWorkersInterface
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal ObservableCollection<BankClient> BankClients { get; set; }
        internal IAddBankClient Addfunctional;
        internal ISetDataClient Setfunctional;
        internal IGetDataClient Getfunctional;
        internal MainWindow(Worker employee)
        {
            InitializeComponent();
            BankClients = new ObservableCollection<BankClient>();
            ListBoxDataClients.ItemsSource = BankClients;
            InitSettingFunctional(employee);

        }
        private void InitSettingFunctional(Worker employee)
        {
            if (employee is Consultant consultant)
            {
                Setfunctional = consultant;
                Getfunctional = consultant;
                Addfunctional = null;
                TextBoxNameClient.IsEnabled = false;
                TextBoxSurNameClient.IsEnabled = false;
                TextBoxPatronymicClient.IsEnabled = false;
                TextBoxPhoneNumberClient.IsEnabled = true;
                TextBoxPussportSeriesNumberClient.IsEnabled = false;
                ButtonAddClient.IsEnabled = false;
            }
            if (employee is Manager manager)
            {
                Setfunctional = manager;
                Getfunctional = manager;
                Addfunctional = manager;
                TextBoxNameClient.IsEnabled = true;
                TextBoxSurNameClient.IsEnabled = true;
                TextBoxPatronymicClient.IsEnabled = true;
                TextBoxPhoneNumberClient.IsEnabled = true;
                TextBoxPussportSeriesNumberClient.IsEnabled = true;
                ButtonAddClient.IsEnabled = true;
            }
        }

        private void ListBoxDataClients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBoxDataClients.SelectedItem is BankClient client)
            {
                TextBoxNameClient.Text = Getfunctional.GetName(client);
                TextBoxSurNameClient.Text = Getfunctional.GetSurName(client);
                TextBoxPatronymicClient.Text = Getfunctional.GetPatronymic(client);
                TextBoxPhoneNumberClient.Text = Getfunctional.GetPhoneNumber(client);
                TextBoxPussportSeriesNumberClient.Text = Getfunctional.GetPassportSeriesNumber(client);
            }
            else
            {
                TextBoxNameClient.Text = string.Empty;
                TextBoxSurNameClient.Text = string.Empty;
                TextBoxPatronymicClient.Text = string.Empty;
                TextBoxPhoneNumberClient.Text = string.Empty;
                TextBoxPussportSeriesNumberClient.Text = string.Empty;
            }
        }

        private void MenuItemSaveClick(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.FileName = "BankClients";
            dialog.DefaultExt = ".json";
            dialog.Filter = "JSON file (.json)|*.json";

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                string filename = dialog.FileName;
                Debug.WriteLine(filename);
                File.WriteAllText(filename, JsonConvert.SerializeObject(BankClients));
            }
        }
        private void MenuItemOpenClick(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "BankClients";
            dialog.DefaultExt = ".json";
            dialog.Filter = "JSON file (.json)|*.json";

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                string filename = dialog.FileName;
                BankClients = JsonConvert.DeserializeObject<ObservableCollection<BankClient>>(File.ReadAllText(filename));
                ListBoxDataClients.ItemsSource = BankClients;
            }
        }
        private void MenuItemChangeUser_Click(object sender, RoutedEventArgs e)
        {
            WorkerSelection workerSelection = new WorkerSelection();
            workerSelection.Show();
            this.Close();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            if (Addfunctional != null)
            {
                Addfunctional.AddBankClient(BankClients);
                ListBoxDataClients.SelectedIndex = ListBoxDataClients.Items.Count - 1;
            }
        }
        private void ButtonHistory_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxDataClients.SelectedItem is BankClient client)
                foreach (var item in client.InfoChangeDataClients)
                {
                    item.PrintAll();
                    Debug.WriteLine("--------------------------------");
                }
        }
        private void ButtonSaveChange_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxDataClients.SelectedItem is BankClient client)
            {
                if (!Setfunctional.SetName(client, TextBoxNameClient.Text))
                    TextBoxNameClient.Text = Getfunctional.GetName(client);
                if (!Setfunctional.SetSurName(client, TextBoxSurNameClient.Text))
                    TextBoxSurNameClient.Text = Getfunctional.GetSurName(client);
                if (!Setfunctional.SetPatronymic(client, TextBoxPatronymicClient.Text))
                    TextBoxPatronymicClient.Text = Getfunctional.GetPatronymic(client);
                if (!Setfunctional.SetPhoneNumber(client, TextBoxPhoneNumberClient.Text))
                    TextBoxPhoneNumberClient.Text = Getfunctional.GetPhoneNumber(client);
                if (!Setfunctional.SetPassportSeriesNumber(client, TextBoxPussportSeriesNumberClient.Text))
                    TextBoxPussportSeriesNumberClient.Text = Getfunctional.GetPassportSeriesNumber(client);
            }
        }
    }

}
