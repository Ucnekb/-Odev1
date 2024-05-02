namespace App1;

public partial class VucutKitleEndeksi : ContentPage
{
	public VucutKitleEndeksi()
	{
		InitializeComponent();
	}

    private void CalculateBMI_Clicked(object sender, EventArgs e)
    {
        if (double.TryParse(weightEntry.Text, out double weight) && double.TryParse(heightEntry.Text, out double height))
        {
            double heightInMeters = height / 100;
            double bmi = weight / (heightInMeters * heightInMeters);

            string result = $"V�cut Kitle �ndeksi (VKI): {bmi:F2}";

            if (bmi < 16)
                result += " - �leri D�zey Zay�f";
            else if (bmi >= 16 && bmi < 17)
                result += " - Orta D�zey Zay�f";
            else if (bmi >= 17 && bmi < 18.49)
                result += " - hafif D�zey Zay�f";
            else if (bmi >= 18.50 && bmi < 24.9)
                result += " - normal kilolu";
            else if (bmi >= 25 && bmi < 29.99)
                result += " - hafif �i�man";
            else if (bmi >= 30 && bmi < 34.99)
                result += " - 1.derece obez";
            else if (bmi >= 35 && bmi < 39.99)
                result += " - 2.dereceden obez";
            else if (bmi >= 40)
                result += " -morbid obez";
            bmiLabel.Text = result;
        }
        else
        {
            bmiLabel.Text = "L�tfen ge�erli a��rl�k ve boy de�erleri girin.";
        }
    }

    private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        int weight=(int)weightSlider.Value;
        int height=(int)heightSlider.Value;
        updateEntry(weight, height);

    }

    private void updateEntry(int weight, int height)
    {
        weightEntry.Text = weight.ToString();
        heightEntry.Text = height.ToString();

    }
}
