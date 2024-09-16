using OllamaSharp;
using System.Windows;
using System.Windows.Input;

namespace OllamaConversation
{
    public partial class MainWindow : Window
    {
        private readonly Uri uri = new("http://localhost:11434");
        private OllamaApiClient apiClient;
        internal MyDataViewModel DataViewModel { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            apiClient = new OllamaApiClient(uri);
            DataViewModel = new MyDataViewModel();
            DataContext = DataViewModel;
        }

        private async Task GetOllamaModels()
        {
            var models = await apiClient.ListLocalModels();
            DataViewModel.Conversations.Clear();
            foreach (var model in models)
            {
                DataViewModel.Conversations.Add(new MyConversation()
                {
                    ModelName = model.Name,
                    Chat = new Chat(apiClient) { Model = model.Name }
                });
            }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await GetOllamaModels();
        }

        private async Task SendPrompt()
        {
            if (DataViewModel.SelectedConversation == null) { return; }
            if (DataViewModel.SelectedConversation.Chat == null) { return; }
            if (string.IsNullOrWhiteSpace(tbPrompt.Text)) { return; }
            DataViewModel.SelectedConversation.IsWaitingPrompt = false;
            apiClient.SelectedModel = DataViewModel.SelectedConversation.ModelName;
            string prompt = tbPrompt.Text;
            var message = new MyConversationMessage()
            {
                Message = prompt,
                DateTime = DateTime.Now,
                IsSentByUser = true
            };
            DataViewModel.SelectedConversation.Messages.Add(message);
            tbPrompt.Text = "";
            var request = new OllamaSharp.Models.GenerateRequest()
            {
                Prompt = tbPrompt.Text,
                Context = null
            };
            var answer = new MyConversationMessage()
            {
                DateTime = DateTime.Now,
                IsSentByUser = false
            };
            DataViewModel.SelectedConversation.Messages.Add(answer);
            await foreach (var answerToken in DataViewModel.SelectedConversation.Chat.Send(prompt))
            {
                await Task.Delay(20);
                answer.Message += answerToken; answer.DateTime = DateTime.Now;
            }
            DataViewModel.SelectedConversation.IsWaitingPrompt = true;
        }

        private async void SendMessage_Clicked(object sender, RoutedEventArgs e)
        {
            await SendPrompt();
        }

        private async void tbPrompt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
                {
                    tbPrompt.Text += "\n";
                }
                else
                {
                    await SendPrompt();
                }
            }
        }
    }
}