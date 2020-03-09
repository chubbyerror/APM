using App.Metrics;
using App.Metrics.Counter;

namespace APM.Example
{
    public static class MetricsRegistry
    {
        public static CounterOptions SampleCounter => new CounterOptions()
        {
            Name = "Sample Counter",
            MeasurementUnit = Unit.Calls
        };
    }
}
