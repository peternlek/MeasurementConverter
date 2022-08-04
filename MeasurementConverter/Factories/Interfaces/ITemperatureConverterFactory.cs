using MeasurementConverter.Converters.Interfaces;
using MeasurementConverter.Enums;

namespace MeasurementConverter.Factories.Interfaces;

/// <summary>
/// Defines the interface implemented by the <see cref="TemperatureConverterFactory"/>
/// </summary>
public interface ITemperatureConverterFactory
{
    /// <summary>
    /// Get an instance of the converter based on supplied unit of measurement argument
    /// </summary>
    /// <param name="measurementUnit">The converters unit of measurement</param>
    /// <returns>An instance of a converter able to convert values to the supplied unit of measurement</returns>
    ITemperatureConverter Get(TemperatureUnit measurementUnit);
}