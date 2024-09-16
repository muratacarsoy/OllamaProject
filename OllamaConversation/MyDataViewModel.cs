using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OllamaConversation
{
    internal class MyDataViewModel : INotifyPropertyChanged
    {
        private MyConversation? _selectedConversation;

        public ObservableCollection<MyConversation> Conversations { get; set; }

        public MyConversation? SelectedConversation
        {
            get { return _selectedConversation; }
            set { _selectedConversation = value; NotifyPropertyChanged(); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public MyDataViewModel()
        {
            Conversations = [];
            _selectedConversation = null;
        }


        // Bu sınıfın herhangi bir özelliği değiştiğinde WPF arayüzüne otomatik yansıtacaktır
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
