using MeasurementConverter.Converters.Interfaces;
using MeasurementConverter.Enums;

namespace MeasurementConverter.Converters
{
    /// <summary>
    /// The Fahrenheit temperature converter
    /// </summary>
    public class FahrenheitConverter : BaseConverter<double, TemperatureUnit>, ITemperatureConverter
    {
        #region Public methods

        /// <summary>
        /// Converts provided value to Fahrenheit from the type determined by the <see cref="TemperatureUnit"/> argument
        /// </summary>
        /// <param name="value">The value to be converted</param>
        /// <param name="temperatureUnit">The conversion value unit of measurement</param>
        /// <returns>The converted value</returns>
        public override double Convert(double value, TemperatureUnit temperatureUnit)
        {
            double returnVal;

            switch (temperatureUnit)
            {
                case TemperatureUnit.Celsius:
                    returnVal = (value * 9 / 5) + 32;
                    break;
                default:
                    returnVal = value;
                    break;
            }

            return returnVal;
        }

        #endregion Public methods
    }
}
