using MeasurementConverter.Converters.Interfaces;

namespace MeasurementConverter.Converters
{
    /// <summary>
    /// Abstract generic base class for all converters
    /// </summary>
    /// <typeparam name="T">The conversion type</typeparam>
    /// <typeparam name="TUnit">The conversion unit of measurement</typeparam>
    public abstract class BaseConverter<T, TUnit> : IBaseConverter<T, TUnit>
    {
        #region Public methods

        /// <summary>
        /// Generic conversion method
        /// </summary>
        /// <param name="value">The value to be converted</param>
        /// <param name="unitOfMeasurement">The conversion value unit of measurement</param>
        /// <returns>The converted value</returns>
        public abstract T Convert(T value, TUnit unitOfMeasurement);

        #endregion Public methods
    }
}
