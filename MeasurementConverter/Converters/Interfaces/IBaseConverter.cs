namespace MeasurementConverter.Converters.Interfaces;

/// <summary>
/// Defines the interface implemented by all converters
/// </summary>
/// <typeparam name="T">The conversion type</typeparam>
/// <typeparam name="TUnit">The conversion unit of measurement</typeparam>
public interface IBaseConverter<T, TUnit>
{
    /// <summary>
    /// Generic conversion method
    /// </summary>
    /// <param name="value">The value to be converted</param>
    /// <param name="unitOfMeasurement">The conversion value unit of measurement</param>
    /// <returns>The converted value</returns>
    T Convert(T value, TUnit unitOfMeasurement);
}