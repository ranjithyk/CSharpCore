using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace CSharpCore.Generics
{
    public class ObservableGroup<K, T> : ObservableCollection<T>
    {
        private K key;
        public K Key
        {
            get => key;

            private set
            {
                key = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Key)));
            }
        }

        public ObservableGroup(K key, IEnumerable<T> items)
        {
            Key = key;
            foreach (var item in items)
                this.Items.Add(item);
        }
    }
}
