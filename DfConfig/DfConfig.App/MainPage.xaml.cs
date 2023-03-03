namespace DfConfig.App
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnCounterClicked(object sender, EventArgs e)
        {
            var httpclient = new HttpClient();
            var result = await httpclient.PostAsync("http://localhost:5264/api/Admin/GetAppList", null);
            var str = result.Content.ReadAsStringAsync().Result;

            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {str} time";
            else
                CounterBtn.Text = $"Clicked {str} times";

            SemanticScreenReader.Announce(CounterBtn.Text);


        }
    }
}