using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
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
using System.Xml.Linq;

namespace WpfApp1PO
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        class TableRates
        {
            [JsonPropertyName("table")]
            public string Table { get; set; }

            [JsonPropertyName("no")]
            public string Number { get; set; }            
            
            [JsonPropertyName("tradingDate")]
            public DateTime TradingDate { get; set; }            
            
            [JsonPropertyName("effectiveDate")]
            public DateTime EffectiveDate { get; set; }
            [JsonPropertyName("rates")]
            public List<Rate> Rates { get; set; }

        }
        record Rate 
        {
            [JsonPropertyName("currency")]
            public string Currency { get; set; }

            [JsonPropertyName("code")]
            public string Code { get; set; }

            [JsonPropertyName("ask")]
            public decimal Ask { get; set; }            
            
            [JsonPropertyName("bid")]
            public decimal Bid { get; set; }
            public Rate(string Currency, string Code, decimal Ask, decimal Bid)
            {
                this.Currency = Currency;
                this.Code = Code;
                this.Bid = Bid;
                this.Ask = Ask;
                
            }
            public Rate()
            { 

            
            }
        }
        Dictionary<string, Rate> Rates = new Dictionary<string, Rate>();

        private void DownloadJsonData()
        {
            WebClient client = new WebClient();
            client.Headers.Add("Accept", "application/json");
            string json = client.DownloadString("http://api.nbp.pl/api/exchangerates/tables/C/");
            List<TableRates> tableRates = JsonSerializer.Deserialize<List<TableRates>>(json);
            TableRates table = tableRates[0];
            table.Rates.Add(new Rate() { Currency = "złoty", Code = "PLN", Ask = 1, Bid = 1 });
            foreach(Rate rate in table.Rates)
            {
                Rates.Add(rate.Code, rate);
            }
        }
        private void DownloadData()
        {
            CultureInfo info = CultureInfo.CreateSpecificCulture("en-EN");
            WebClient client = new WebClient();
            client.Headers.Add("Accept", "application/xml");
            string xmlRate = client.DownloadString("http://api.nbp.pl/api/exchangerates/tables/C/");
            XDocument rateDoc = XDocument.Parse(xmlRate);
            IEnumerable<Rate> rates = rateDoc
                .Element("ArrayOfExchangeRatesTable")
                .Elements("ExchangeRatesTable")
                .Elements("Rates")
                .Elements("Rate")
                .Select(x => new Rate
                (
                    x.Element("Currency").Value,
                    x.Element("Code").Value,
                    decimal.Parse(x.Element("Ask").Value, info),
                    decimal.Parse(x.Element("Bid").Value, info)

                ));
            foreach (Rate rate in rates)
            {
                Rates.Add(rate.Code, rate);
            }
            Rates.Add("PLN", new Rate("złoty", "PLN", 1, 1));
        }
        public MainWindow()
        {
            InitializeComponent();
            DownloadJsonData();
            UpdateGui();
        }

        private void UpdateGui()
        {
            OutputCurrencyCode.Items.Clear();
            InputCurrencyCode.Items.Clear();

            foreach (string code in Rates.Keys)
            {
                OutputCurrencyCode.Items.Add(code);
                InputCurrencyCode.Items.Add(code);
            }
            OutputCurrencyCode.SelectedIndex = 0;
            InputCurrencyCode.SelectedIndex = 1;
        }

        private void CalcResult(object sender, RoutedEventArgs e)
        {
            //pobrać kwotę
            //pobrać kod waluty kwoty
            //pobrać kod waluty docelowej
            //obliczyć kwotę w walucie docelowej
            //OutputAmount.Text = "Kliknąłeś";
            Rate inputRate = Rates[InputCurrencyCode.Text];
            Rate outputRate = Rates[OutputCurrencyCode.Text];
            if(decimal.TryParse(InputAmount.Text,out decimal amount))
            {
                decimal result = amount * inputRate.Ask / outputRate.Ask;
                OutputAmount.Text = result.ToString("N2");
            }
            
        }

        private void LoadFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "wybierz plik tekstowy z notowaniami";
            dialog.Filter = "Plik tekstowy(*.txt)|*.txt";
            if(dialog.ShowDialog() == true)
            {
                if(File.Exists(dialog.FileName))
                {                    
                    string[] lines = File.ReadAllLines(dialog.FileName);
                    Rates.Clear();
                    foreach(string line in lines)
                    {
                        string[] tokens = line.Split(";");
                        string code = tokens[0];
                        string currency = tokens[1];
                        string askStr = tokens[2];
                        string bidStr = tokens[3];
                        if(decimal.TryParse(askStr,out decimal ask) && decimal.TryParse(bidStr,out decimal bid))
                        {
                            Rate rate = new Rate() { Code = code, Currency = currency, Ask = ask, Bid = bid };
                            Rates.Add(rate.Code, rate);
                        }
                    }

                    UpdateGui();
                }
            }
        }

        private void SaveFileJson(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Plik JSON(*.json)|*.json";
            if (dialog.ShowDialog() == true)
            {
                string json = JsonSerializer.Serialize(Rates);
                File.WriteAllText(dialog.FileName, json);


                //odczyt z pliku json
                string content = File.ReadAllText(dialog.FileName);
                Rates = JsonSerializer.Deserialize<Dictionary<string, Rate>>(content);
                UpdateGui();

            }
            //utwórz dialog
            //ustaw filtr dla pliku *.json
            //zmien tytul
            //dodaj warunek da ShowDialog
            //na klasie JsonSerializer wywoal metodę serialize przekazując obiekt Rates jako argument
            //wynik przypisz do zmiennej typu string
            //zapisz lancuch przy pomocy klasy File i odszukaj metode pasujaca do zapisu calego lancucha
            //przekaz do metody sciezke z dialog.FileName i lancuch z json
        }

        private void NumberValidation(object sender, TextCompositionEventArgs e)
        {
            string oldText = InputAmount.Text;
            string deltaText = e.Text;
            e.Handled = !decimal.TryParse(oldText + deltaText, out decimal val);
        }
    }
}
