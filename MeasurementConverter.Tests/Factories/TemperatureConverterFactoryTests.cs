using MeasurementConverter.Converters;
using MeasurementConverter.Enums;
using MeasurementConverter.Factories;

namespace MeasurementConverter.Tests.Factories
{
    /// <summary>
    /// Unit tests for the <see cref="TemperatureConverterFactory"/>
    /// </summary>
    [TestClass]
    public class TemperatureConverterFactoryTests
    {
        #region Test methods

        /// <summary>
        /// Test each <see cref="TemperatureUnit"/> unit of measurement argument results in the return of the correct converter 
        /// </summary>
        /// <param name="temperatureUnit">The target unit of measurement</param>
        [DataTestMethod]
        [DataRow(TemperatureUnit.Fahrenheit)]
        [DataRow(TemperatureUnit.Celsius)]
        public void TemperatureConverterFactoryTests_test_that_correct_converter_is_created_for_given_measurement_unit(
            TemperatureUnit temperatureUnit)
        {
            // Arrange
            var sut = CreateSut();

            // Act
            var createdConverter = sut.Get(temperatureUnit);

            // Assert
            switch (temperatureUnit)
            {
                case TemperatureUnit.Celsius:
                    Assert.IsTrue(createdConverter is CelsiusConverter, "Error: expected converter to be of type CelsiusConverter");
                    break;
                case TemperatureUnit.Fahrenheit:
                    Assert.IsTrue(createdConverter is FahrenheitConverter, "Error: expected converter to be of type FahrenheitConverter");
                    break;
            }
        }

        /// <summary>
        /// Tests that only one instance of a converter for a given <see cref="TemperatureUnit"/> unit of measurement is created and re-used
        /// </summary>
        [TestMethod]
        public void TemperatureConverterFactoryTests_test_that_same_instance_is_returned_for_multiple_requests_of_the_same_measurement_unit()
        {
            // Arrange
            var sut = CreateSut();
            var firstInstanceRequested = sut.Get(TemperatureUnit.Fahrenheit);

            // Act
            var secondInstanceRequested = sut.Get(TemperatureUnit.Fahrenheit);

            // Assert
            Assert.AreSame(firstInstanceRequested, secondInstanceRequested);
        }

        #endregion Test methods

        #region Helper methods

        /// <summary>
        /// Creates the system under test
        /// </summary>
        /// <returns>An instance of type <see cref="TemperatureConverterFactory"/></returns>
        private TemperatureConverterFactory CreateSut()
        {
            return new TemperatureConverterFactory();
        }

        #endregion Helper methods
    }
}