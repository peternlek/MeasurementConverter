using System;
using System.Collections.Generic;
using MeasurementConverter.Converters;
using MeasurementConverter.Converters.Interfaces;
using MeasurementConverter.Enums;
using MeasurementConverter.Factories.Interfaces;

namespace MeasurementConverter.Factories
{
    /// <summary>
    /// The <see cref="TemperatureUnit"/> converter factory. This maintains a registry of converters for re-use.
    /// </summary>
    public class TemperatureConverterFactory : BaseConverterFactory<ITemperatureConverter, double, TemperatureUnit>, ITemperatureConverterFactory
    {
        #region Fields

        /// <summary>
        /// The registry of <see cref="ITemperatureConverter"/> converters
        /// </summary>
        private readonly IDictionary<TemperatureUnit, ITemperatureConverter> _temperatureConverters = new Dictionary<TemperatureUnit, ITemperatureConverter>();

        #endregion Fields

        #region Public methods

        /// <summary>
        /// Get an instance of the converter based on supplied unit of measurement argument
        /// </summary>
        /// <param name="measurementUnit">The converters unit of measurement</param>
        /// <returns>An instance of a converter able to convert values to the supplied unit of measurement</returns>
        public override ITemperatureConverter Get(TemperatureUnit measurementUnit)
        {
            if (!_temperatureConverters.TryGetValue(measurementUnit, out var returnVal))
            {
                switch (measurementUnit)
                {
                    case TemperatureUnit.Celsius:
                        _temperatureConverters[measurementUnit] = returnVal = new CelsiusConverter();
                        break;
                    case TemperatureUnit.Fahrenheit:
                        _temperatureConverters[measurementUnit] = returnVal = new FahrenheitConverter();
                        break;
                    default:
                        throw new InvalidOperationException($"Unexpected measurement unit: {measurementUnit}");
                }
            }

            return returnVal;
        }

        #endregion Public methods
    }
}
