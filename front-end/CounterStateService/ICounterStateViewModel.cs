using System;
using System.ComponentModel;
using Microsoft.AspNetCore.Components;

namespace CounterState
{
    public interface ICounterStateViewModel : INotifyPropertyChanged
    {
        int Count { get; set; }
        void Increment();
        void Decrement();
    }
}