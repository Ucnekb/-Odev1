

using System.ComponentModel;

namespace App1;

public partial class kredi_hesaplama : ContentPage, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    private double IaylikTaksit;
    private double ItoplamBorc;
    private double ItoplamFaiz;

    public double aylikTaksit
    {
        get { return IaylikTaksit; }
        set
        {
            if (IaylikTaksit != value)
            {
                IaylikTaksit = value;
                OnPropertyChanged(nameof(aylikTaksit));
            }
        }
    }

    public double toplamBorc
    {
        get { return ItoplamBorc; }
        set
        {
            if (ItoplamBorc != value)
            {
                ItoplamBorc = value;
                OnPropertyChanged(nameof(toplamBorc));
            }
        }
    }

    public double toplamFaiz
    {
        get { return ItoplamFaiz; }
        set
        {
            if (ItoplamFaiz != value)
            {
                ItoplamFaiz = value;
                OnPropertyChanged(nameof(toplamFaiz));
            }
        }
    }


    public kredi_hesaplama()
    {
        InitializeComponent();
        Picker picker = new Picker { Title = "Kredi Türü" };
        picker.Items.Add("Ýhtiyaç Kredisi");
        picker.Items.Add("Konut Kredisi");
        picker.Items.Add("Taþýt Kredisi");
        picker.Items.Add("Ticari Kredisi");
        BindingContext = this;




    }

    private void HesaplaButton_Clicked(object sender, EventArgs e)
    {
        if (KrediPicker.SelectedItem != null)
        {
            var kredi = KrediPicker.SelectedItem.ToString();

            var oran = Convert.ToDouble(EntryOran.Text);
            var vade = Convert.ToInt32(EntryVade.Text);
            var tutar = Convert.ToDouble(EntryTutar.Text);
            double brutFaiz;
            if (kredi == "Ýhtiyaç Kredisi")
            {
                brutFaiz = ((oran + (oran * 0.10) + (oran * 0.15)) / 100);
            }
            else if (kredi == "Konut Kredisi")
            {
                brutFaiz = ((oran + (oran * 0) + (oran * 0)) / 100);
            }
            else if (kredi == "Taþýt Kredisi")
            {
                brutFaiz = ((oran + (oran * 0.05) + (oran * 0.15)) / 100);
            }
            else if (kredi == "Ticari Kredisi")
            {
                brutFaiz = ((oran + (oran * 0.05) + (oran * 0)) / 100);
            }
            else
            {
                brutFaiz = 0;
            }
            double taksit = ((Math.Pow(1 + brutFaiz, vade) * brutFaiz) / (Math.Pow(1 + brutFaiz, vade) - 1)) * tutar;
            double toplam = taksit * vade;
            double toplamFaiz = toplam - tutar;
            aylikTaksit = taksit;
            toplamBorc = toplam;
            this.toplamFaiz = toplamFaiz;
        }
        else
        {
            DisplayAlert("Hata", "Bir kredi türü seçin", "Tamam");
        }
    }

    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }


}


