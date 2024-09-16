using OllamaSharp;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OllamaConversation
{
    internal class MyConversation : INotifyPropertyChanged
    {
        private string _modelName;
        private bool _isWaitingPrompt;

        // Seçili Ollama modelinin adı
        public string ModelName
        {
            get { return _modelName; }
            set { _modelName = value; NotifyPropertyChanged(); }
        }

        // Yapay zekanın cevap verirken false, verdikten sonra true olur
        // Böylece penceredeki tuşların IsEnabled özelliği Binding ile belirlenir
        public bool IsWaitingPrompt
        {
            get { return _isWaitingPrompt; }
            set { _isWaitingPrompt = value; NotifyPropertyChanged(); }
        }

        // Konuşma içerisindeki bütün mesajların listesi
        public ObservableCollection<MyConversationMessage> Messages { get; set; }

        // OllamaSharp kütüphanesindeki Chat nesnesi
        public Chat? Chat { get; set; }

        public MyConversation()
        {
            _modelName = string.Empty;
            _isWaitingPrompt = true;
            Messages = [];
            Chat = null;
        }
        
        public event PropertyChangedEventHandler? PropertyChanged;

        // Bu sınıfın herhangi bir özelliği değiştiğinde WPF arayüzüne otomatik yansıtacaktır
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    internal class MyConversationMessage : INotifyPropertyChanged
    {
        private string _message;
        private DateTime _dateTime;
        private bool _isSentByUser;

        // Mesajın içeriği string olacak
        public string Message
        {
            get { return _message; }
            set { _message = value; NotifyPropertyChanged(); }
        }
        // Mesajın zamanı DateTime olacak
        public DateTime DateTime
        {
            get { return _dateTime; }
            set { _dateTime = value; NotifyPropertyChanged(); }
        }
        // Mesaj kullanıcı tarafından gönderildiyse true
        // Yapay zekaya ait ise false olacak
        public bool IsSentByUser
        {
            get { return _isSentByUser; }
            set { _isSentByUser = value; NotifyPropertyChanged(); }
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        public MyConversationMessage()
        {
            _message = string.Empty;
            _dateTime = DateTime.Now;
            _isSentByUser = false;
        }

        // Bu sınıfın herhangi bir özelliği değiştiğinde WPF arayüzüne otomatik yansıtacaktır
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
