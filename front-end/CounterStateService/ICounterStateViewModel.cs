using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Components;

namespace CounterState
{
    public interface IStoreBase<T> where T : class
    {
        IEnumerable<T> Gets();
        T Get();
        bool Save(IEnumerable<T> items);
        bool Save(T item);
        bool Remove(T item);
    }
    
    public interface IViewModelBase : INotifyPropertyChanged, IDisposable
    {
        
        
    }

    public abstract class StoreViewModelBase<TModel> : ViewModelBase, IStoreBase<TModel>
        where TModel : class
    {
        public abstract IEnumerable<TModel> Gets();

        public abstract TModel Get();

        public abstract bool Save(IEnumerable<TModel> items);

        public abstract bool Save(TModel item);

        public abstract bool Remove(TModel item);
    }

    public abstract class ViewModelBase : IViewModelBase
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public abstract void Dispose();
    }

    public interface ICounterStateViewModel
    {
        int Count { get; set; }
        string Message { get; set; }
        void Increment();
        void Decrement();
    }
}