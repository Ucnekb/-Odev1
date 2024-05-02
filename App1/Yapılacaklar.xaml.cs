using Newtonsoft.Json;

namespace App1;

public partial class Yapılacaklar : ContentPage
{

    private List<string> todoList;
    private string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "todoList.json");
    public Yapılacaklar()
	{
		InitializeComponent();

        LoadTodoList();
    }
    private async void AddTodo_Clicked(object sender, EventArgs e)
    {
        string newTodo = await DisplayPromptAsync("Yeni Görev Ekle", "Görevin adını giriniz:");
        if (!string.IsNullOrWhiteSpace(newTodo))
        {
            todoList.Add(newTodo);
            AddTodoItem(newTodo);
            SaveTodoList();
        }
    }

    private void SaveTodoList()
    {
        string json = JsonConvert.SerializeObject(todoList);
        File.WriteAllText(filePath, json);
    }

    private void LoadTodoList()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            todoList = JsonConvert.DeserializeObject<List<string>>(json);
            foreach (string todo in todoList)
            {
                if (!string.IsNullOrWhiteSpace(todo))
                {
                    AddTodoItem(todo);
                }
            }
        }
        else
        {
            todoList = new List<string>();
        }
    }

    private async void SaveTodo_Clicked(object sender, EventArgs e)
    {
        await DisplayAlert("Kaydet", "Görevler kaydedildi.", "Tamam");
    }

    private async void EditTodo_Clicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        
        var parentLayout = (StackLayout)button.Parent;
        var label = (Label)parentLayout.Children[1];
        string editedTodo = await DisplayPromptAsync("Görevi Düzenle", "Yeni görev adını giriniz:", initialValue: label.Text);
        if (!string.IsNullOrWhiteSpace(editedTodo))
        {
            int index = todoList.IndexOf(label.Text);
            if (index != -1)
            {
                todoList[index] = editedTodo;
                label.Text = editedTodo;
                SaveTodoList();
            }
        }
    }

    private void DeleteTodo_Clicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var parentLayout = (StackLayout)button.Parent;
        var label = (Label)parentLayout.Children[1];
        todoList.Remove(label.Text);
        todoStackLayout.Children.Remove(parentLayout);
        SaveTodoList();
    }

    private void AddTodoItem(string todoText)
    {
        var checkBox = new CheckBox();
        var label = new Label { Text = todoText, VerticalOptions = LayoutOptions.CenterAndExpand };
        var editButton = new Button { Text = "Düzenle" };
        var deleteButton = new Button { Text = "Sil" };

        editButton.Clicked += EditTodo_Clicked;
        deleteButton.Clicked += DeleteTodo_Clicked;

        var stackLayout = new StackLayout
        {
            Orientation = StackOrientation.Horizontal,
            Children = { checkBox, label, editButton, deleteButton },
            Margin = new Thickness(0, 5)
        };

        todoStackLayout.Children.Add(stackLayout);
    }
}

