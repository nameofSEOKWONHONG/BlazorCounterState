using Fluxor;

namespace CounterStateServer.Data
{
    public class CounterState
    {
        public int ClickCount { get; }

        public CounterState(int clickCount)
        {
            this.ClickCount = clickCount;
        }
    }

    public class Feature : Feature<CounterState>
    {
        public override string GetName() => "Counter";

        protected override CounterState GetInitialState() => new CounterState(clickCount: 0);
    }
}