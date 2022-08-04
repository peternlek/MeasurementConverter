using MeasurementConverter.Converters.Interfaces;

namespace MeasurementConverter.Factories.Interfaces
{
    /// <summary>
    /// Defines the interface implemented by all converter factories
    /// </summary>
    /// <typeparam name="T">The conversion type</typeparam>
    /// <typeparam name="TType">The conversion value type</typeparam>
    /// <typeparam name="TUnit">The conversion unit of measurement</typeparam>
    internal interface IBaseConverterFactory<T, TType, TUnit> where T: IBaseConverter<TType, TUnit>
    {
        /// <summary>
        /// Get an instance of the converter based on supplied measurement unit argument
        /// </summary>
        /// <param name="measurementUnit">The converters unit of measurement</param>
        /// <returns>An instance of a converter able to convert values to the supplied measurement unit</returns>
        public T Get(TUnit measurementUnit);
    }
}
