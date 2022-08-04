using MeasurementConverter.Enums;

namespace MeasurementConverter.Converters.Interfaces
{
    /// <summary>
    /// Defines the interface implemented by all temperature converters
    /// </summary>
    public interface ITemperatureConverter : IBaseConverter<double, TemperatureUnit>
    {
    }
}   