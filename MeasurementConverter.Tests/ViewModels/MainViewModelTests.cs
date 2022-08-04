using MeasurementConverter.Converters.Interfaces;
using MeasurementConverter.Enums;
using MeasurementConverter.Factories.Interfaces;
using MeasurementConverter.ViewModels;
using Moq;

namespace MeasurementConverter.Tests.ViewModels
{
    /// <summary>
    /// Unit tests for the <see cref="MainViewModel"/>
    /// </summary>
    [TestClass]
    public class MainViewModelTests
    {
        #region Fields

        private Mock<ITemperatureConverterFactory> _mockTemperatureConverterFactory;
        private Mock<ITemperatureConverter> _mockTemperatureConverter;

        #endregion 

        #region Test methods

        /// <summary>
        /// Tests the state following creation is correct
        /// </summary>
        [TestMethod]
        public void MainViewModelTest_state_is_correct_when_sut_created()
        {
            // Act
            var sut = CreateSut();

            // Assert
            Assert.AreEqual(sut.SelectedFromTemperatureUnit, TemperatureUnit.Celsius);
            Assert.AreEqual(sut.SelectedToTemperatureUnit, TemperatureUnit.Fahrenheit);
            Assert.AreEqual(double.Parse(sut.ConversionValue), 0);
        }

        /// <summary>
        /// Tests active converter is called with correct arguments when conversion value changes
        /// </summary>
        [TestMethod]
        public void MainViewModelTest_entering_valid_value_issues_convert_request_to_active_converter()
        {
            // Arrange
            var sut = CreateSut();

            // Act
            sut.ConversionValue = "1";

            // Assert
            _mockTemperatureConverter.Verify(tc => tc.Convert(1.0, TemperatureUnit.Celsius), Times.Once);
        }

        /// <summary>
        /// Tests active converter is called with correct arguments when From unit of measurement changes
        /// </summary>
        [TestMethod]
        public void MainViewModelTest_altering_conversion_from_unit_type_issues_convert_request_to_active_converter()
        {
            // Arrange
            var sut = CreateSut();

            // Act
            sut.SelectedFromTemperatureUnit = TemperatureUnit.Fahrenheit;

            // Assert
            _mockTemperatureConverter.Verify(tc => tc.Convert(0, TemperatureUnit.Fahrenheit), Times.Once);
        }

        /// <summary>
        /// Tests active converter is updated with call to converter factory and this is called with correct
        /// arguments when To unit of measurement changes
        /// </summary>
        [TestMethod]
        public void MainViewModelTest_altering_conversion_to_unit_type_issues_convert_request_to_active_converter()
        {
            // Arrange
            var sut = CreateSut();

            // Act
            sut.SelectedToTemperatureUnit = TemperatureUnit.Celsius;

            // Assert
            _mockTemperatureConverter.Verify(tc => tc.Convert(0, TemperatureUnit.Celsius), Times.Exactly(2));
            _mockTemperatureConverterFactory.Verify(tc => tc.Get(TemperatureUnit.Celsius), Times.Once);
        }

        /// <summary>
        /// Tests validation results in error when invalid characters contained in conversion value
        /// </summary>
        [TestMethod]
        public void MainViewModelTest_ensure_error_raised_when_invalid_characters_in_conversion_value()
        {
            // Arrange
            var sut = CreateSut();
            sut.ConversionValue = "123a";

            // Act
            var actualError = sut[nameof(MainViewModel.ConversionValue)];

            // Assert
            Assert.AreEqual(Resources.InvalidConversionValueError, actualError);
        }

        /// <summary>
        /// Tests validation results in error when conversion value is outside of the valid range   
        /// </summary>
        [TestMethod]
        public void MainViewModelTest_ensure_error_raised_when_invalid_range_for_conversion_value()
        {
            // Arrange
            var sut = CreateSut();

            // Act
            sut.ConversionValue = "20001";
            var actualErrorHigh = sut[nameof(MainViewModel.ConversionValue)];
            sut.ConversionValue = "-20001";
            var actualErrorLow = sut[nameof(MainViewModel.ConversionValue)];

            // Assert
            Assert.AreEqual(Resources.InvalidConversionValueRangeError, actualErrorHigh);
            Assert.AreEqual(Resources.InvalidConversionValueRangeError, actualErrorLow);
        }

        #endregion Test methods

        #region Helper methods

        /// <summary>
        /// Creates the system under test
        /// </summary>
        /// <returns>An instance of type <see cref="MainViewModel"/></returns>
        private MainViewModel CreateSut()
        {
            _mockTemperatureConverterFactory = new Mock<ITemperatureConverterFactory>();
            _mockTemperatureConverter = new Mock<ITemperatureConverter>();
            _mockTemperatureConverterFactory.Setup(tcf => tcf.Get(It.IsAny<TemperatureUnit>()))
                .Returns(_mockTemperatureConverter.Object);

            return new MainViewModel(
                _mockTemperatureConverterFactory.Object);
        }

        #endregion Helper methods
    }
}