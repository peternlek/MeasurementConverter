using MeasurementConverter.Converters;
using MeasurementConverter.Enums;

namespace MeasurementConverter.Tests.Converters
{
    /// <summary>
    /// Unit tests for the <see cref="CelsiusConverter"/>
    /// </summary>
    [TestClass]
    public class CelsiusConverterTests
    {
        #region Test methods

        /// <summary>
        /// Tests a value for each <see cref="TemperatureUnit"/> unit of measurement type is converted correctly
        /// </summary>
        /// <param name="temperatureUnit">The value unit of measurement</param>
        /// <param name="conversionValue">The value to convert</param>
        /// <param name="expectedValue">The expected converted value</param>
        [DataTestMethod]
        [DataRow(TemperatureUnit.Fahrenheit, 100, 37.7778)]
        [DataRow(TemperatureUnit.Celsius, 100, 100)]
        public void CelsiusConverterTests_test_that_value_is_converted_correctly_for_each_temperature_unit_type(
            TemperatureUnit temperatureUnit, 
            double conversionValue,
            double expectedValue)
        {
            // Arrange
            var sut = CreateSut();

            // Act
            var calculatedValue = Math.Round(sut.Convert(conversionValue, temperatureUnit), 4);

            // Assert
            Assert.AreEqual(0, calculatedValue.CompareTo(expectedValue), message:$"Error: expected {expectedValue}, calculated {calculatedValue}");
        }

        #endregion Test methods

        #region Helper methods

        /// <summary>
        /// Creates the system under test
        /// </summary>
        /// <returns>An instance of type <see cref="FahrenheitConverter"/></returns>
        private CelsiusConverter CreateSut()
        {
            return new CelsiusConverter();
        }

        #endregion Helper methods
    }
}