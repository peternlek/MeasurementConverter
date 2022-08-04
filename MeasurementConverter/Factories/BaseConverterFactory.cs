using MeasurementConverter.Converters.Interfaces;
using MeasurementConverter.Factories.Interfaces;

namespace MeasurementConverter.Factories
{
    /// <summary>
    /// Abstract generic base class for all converter factories
    /// </summary>
    /// <typeparam name="T">The conversion type</typeparam>
    /// <typeparam name="TType">The conversion value type</typeparam>
    /// <typeparam name="TUnit">The conversion unit of measurement</typeparam>
    public abstract class BaseConverterFactory<T, TType, TUnit> : IBaseConverterFactory<T, TType, TUnit> where T : IBaseConverter<TType, TUnit>
    {
        #region Public methods

        /// <summary>
        /// Get an instance of the converter based on supplied unit of measurement argument
        /// </summary>
        /// <param name="measurementUnit">The converters unit of measurement</param>
        /// <returns>An instance of a converter able to convert values to the supplied unit of measurement</returns>
        public abstract T Get(TUnit measurementUnit);

        #endregion Public methods
    }
}
