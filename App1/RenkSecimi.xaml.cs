namespace App1;

public partial class RenkSecimi : ContentPage
{
    int count = 0;
    public RenkSecimi()
	{
		InitializeComponent();
	}
    private void CopyButton_Clicked(object sender, EventArgs e)
    {
        Clipboard.SetTextAsync(resultLabel.Text);
        DisplayAlert("Kopyalandý", "Renk kodu kopyalandý.", "Tamam");
    }

    private void RandomButton_Clicked(object sender, EventArgs e)
    {
        Random random = new Random();
        int red = random.Next(256);
        int green = random.Next(256);
        int blue = random.Next(256);

        redSlider.Value = red;
        greenSlider.Value = green;
        blueSlider.Value = blue;

      
        UpdateLabels(red, green, blue);
        resultLabel.Text = $"#{red:X2}{green:X2}{blue:X2}"; 

        UpdateLabels(red, green, blue);
        colorBox.Color = Color.FromRgb(red, green, blue);
    }

    private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        int red = (int)redSlider.Value;
        int green = (int)greenSlider.Value;
        int blue = (int)blueSlider.Value;

        UpdateLabels(red, green, blue);
        colorBox.Color = Color.FromRgb(red, green, blue);
    }

    private void UpdateLabels(int red, int green, int blue)
    {
        redLabel.Text = $"Red: {red:F0}";
        greenLabel.Text = $"Green: {green:F0}";
        blueLabel.Text = $"Blue: {blue:F0}";

        resultLabel.Text = $"#{red:X2}{green:X2}{blue:X2}";
        colorBox.Color = Color.FromRgb(red, green, blue);
    }
}

