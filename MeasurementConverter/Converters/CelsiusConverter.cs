using System;
using MeasurementConverter.Converters.Interfaces;
using MeasurementConverter.Enums;

namespace MeasurementConverter.Converters
{
    /// <summary>
    /// The Celsius temperature converter
    /// </summary>
    public class CelsiusConverter : BaseConverter<double, TemperatureUnit>, ITemperatureConverter
    {
        #region Public methods

        /// <summary>
        /// Converts provided value to Celsius from the type determined by the <see cref="TemperatureUnit"/> argument
        /// </summary>
        /// <param name="value">The value to be converted</param>
        /// <param name="temperatureUnit">The conversion value unit of measurement</param>
        /// <returns>The converted value</returns>
        public override double Convert(double value, TemperatureUnit temperatureUnit)
        {
            double returnVal;

            switch (temperatureUnit)
            {
                case TemperatureUnit.Fahrenheit:
                    returnVal = (value - 32) * 5 / 9;
                    break;
                case TemperatureUnit.Celsius:
                    returnVal = value;
                    break;
                default:
                    throw new InvalidOperationException($"Unexpected temperature unit: {temperatureUnit}");
            }

            return returnVal;
        }

        #endregion Public methods
    }
}
