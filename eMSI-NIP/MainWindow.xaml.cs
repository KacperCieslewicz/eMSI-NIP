using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Xml;
using eMSI_NIP.ServiceReference;

namespace eMSI_NIP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string USER_KEY = "aaaaaaaaaabbbbbbbbbb";
        private const string LENGTH_ERROR = "Numer NIP musi składać się z 10 znaków!";
        private const string CHARACTER_ERROR = "Numer NIP musi składać się tylko z cyfr!";
        private const string NOT_FOUND = "Nie znaleziono podmiotu dla podanych kryteriów wyszukiwania!";

        private string sid;
        private UslugaBIRzewnPublClient client;
        private ParametryWyszukiwania param;
        private XmlDocument document;

        public MainWindow()
        {
            client = new UslugaBIRzewnPublClient();
            param = new ParametryWyszukiwania();
            document = new XmlDocument();
            Login();
            Closing += MainWindow_Closing;
            InitializeComponent();
        }

        #region UI

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Logout();
        }

        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            SearchForEntity();
        }

        private void TextBoxNIP_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                SearchForEntity();
            }
        }

        private void FillUIWithData()
        {
            XmlNode node = document.SelectSingleNode("root/dane");

            textBlockRegon.Text = node["Regon"].InnerText;
            textBlockStatusNIP.Text = node["StatusNip"].InnerText;
            textBlockNazwa.Text = node["Nazwa"].InnerText;
            textBlockWojewodztwo.Text = node["Wojewodztwo"].InnerText;
            textBlockPowiat.Text = node["Powiat"].InnerText;
            textBlockGmina.Text = node["Gmina"].InnerText;
            textBlockMiejscowosc.Text = node["Miejscowosc"].InnerText;
            textBlockKodPocztowy.Text = node["KodPocztowy"].InnerText;
            textBlockUlica.Text = node["Ulica"].InnerText;
            textBlockNrNieruchomosci.Text = node["NrNieruchomosci"].InnerText;
            textBlockNrLokalu.Text = node["NrLokalu"].InnerText;
            textBlockTyp.Text = node["Typ"].InnerText;
        }

        #endregion

        #region Others

        private void Login()
        {
            sid = client.Zaloguj(USER_KEY);
            OperationContextScope scope = new OperationContextScope(client.InnerChannel);
            HttpRequestMessageProperty reqProps = new HttpRequestMessageProperty();
            reqProps.Headers.Add("sid", sid);
            OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = reqProps;
        }

        private void Logout()
        {
            client.Wyloguj(sid);
        }

        private void SearchForEntity()
        {
            string nip = textBoxNIP.Text;
            if (IsNipCorrect(nip))
            {
                param.Nip = nip;
                string result = client.DaneSzukajPodmioty(param);
                document.LoadXml(result);
                XmlNode errorCodeNode = document.SelectSingleNode("root/dane/ErrorCode");

                if (errorCodeNode == null)
                {
                    FillUIWithData();
                }
                else
                {
                    MessageBox.Show(NOT_FOUND);
                }
            }
        }

        private bool IsNipCorrect(string nip)
        {
            if(nip.Length != 10)
            {
                MessageBox.Show(LENGTH_ERROR);
                return false;
            }
            if(!Regex.IsMatch(nip, @"^\d+$"))
            {
                MessageBox.Show(CHARACTER_ERROR);
                return false;
            }
            return true;
        }

        #endregion
    }
}
