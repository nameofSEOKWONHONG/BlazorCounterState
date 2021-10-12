using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Components;

namespace CounterState
{
    public class CounterStateViewModel : ViewModelBase, ICounterStateViewModel
    {
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
        
        public override void Dispose()
        {
            
        }
    }
}

