using System;
using System.ComponentModel;
using Microsoft.AspNetCore.Components;

namespace CounterState
{
    public interface INotifier
    {
        event EventHandler? OnNotify;
    }

    public interface ICounterStateViewModel : INotifyPropertyChanged, INotifier
    {
        int Count { get; set; }
        void Increment();
        void Decrement();
    }
}