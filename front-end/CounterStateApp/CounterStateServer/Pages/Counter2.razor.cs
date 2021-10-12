using Fluxor;
using Microsoft.AspNetCore.Components;
using CounterState = CounterStateServer.Data.CounterState;

namespace CounterStateServer.Pages
{
    public partial class Counter2
    {
        [Inject]
        private IState<Data.CounterState> CounterState { get; set; }
        
        [Inject]
        private IDispatcher Dispatcher { get; set; }

        private void IncrementCount()
        {
            var action = new IncrementCounterAction();
            Dispatcher.Dispatch(action);
        }
    }

    public class IncrementCounterAction
    {
        
    }

    public static class Reducers
    {
        [ReducerMethod]
        public static Data.CounterState ReduceIncrementCounterAction(Data.CounterState state,
            IncrementCounterAction action) =>
            new Data.CounterState(clickCount: state.ClickCount + 1);
    }
}