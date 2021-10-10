using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Components;

namespace CounterState
{
    public class CounterStateViewModel : ICounterStateViewModel
    {
        public event EventHandler? OnNotify;

        private int _count;
        public int Count
        {
            get => _count;
            set
            {
                _count = value;
                OnPropertyChanged();
            } 
        }

        private string _message;

        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged();
            }
        }

        public void Increment()
        {
            Count += 1;
        }

        public void Decrement()
        {
            Count -= 1;
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            if(OnNotify != null)
            {
                OnNotify(this, new EventArgs());
            }
        }
    }
}

