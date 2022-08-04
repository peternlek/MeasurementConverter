using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using MeasurementConverter.Converters.Interfaces;
using MeasurementConverter.Enums;
using MeasurementConverter.Factories.Interfaces;

namespace MeasurementConverter.ViewModels
{
    /// <summary>
    /// The main view interaction model
    /// </summary>
    public class MainViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        #region Fields

        private const double MaxConversionValue = 20000;
        private const double MinConversionValue = -20000;
        private const int ConvertedValuePrecision = 10;

        private readonly ITemperatureConverterFactory _temperatureConverterFactory;
        private readonly IDictionary<string, IList<string>> _errors = new Dictionary<string, IList<string>>();

        private string _conversionValue = "0";
        private double _convertedValue;
        private TemperatureUnit _selectedFromTemperatureUnit;
        private TemperatureUnit _selectedToTemperatureUnit;
        private ITemperatureConverter _activeTemperatureConverter;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets a flag indicating whether state of view model is valid
        /// </summary>
        public bool IsValid => string.IsNullOrEmpty(this[nameof(ConversionValue)]);

        /// <summary>
        /// Gets the validation error string
        /// </summary>
        public string Error 
        {
            get
            {
                return _errors.Any()
                    ? String.Join(Environment.NewLine, _errors.Select(err => err.Value))
                    : null;
            }
        }

        /// <summary>
        /// Apply validation to input values
        /// </summary>
        /// <param name="columnName">The property name changing</param>
        /// <returns>Error string or null if validation passes</returns>
        public string this[string columnName]
        {
            get
            {
                string error = null;
                var parsedValue = 0.0;
                switch (columnName)
                {
                    case nameof(ConversionValue):
                        if (!double.TryParse(ConversionValue, out parsedValue))
                        {
                            error = Resources.InvalidConversionValueError;
                        }

                        if (double.TryParse(ConversionValue, out parsedValue) &&
                            (parsedValue > MaxConversionValue ||
                            parsedValue < MinConversionValue))
                        {
                            error = Resources.InvalidConversionValueRangeError;
                        }

                        break;
                }

                if (!string.IsNullOrEmpty(error))
                {
                    StoreError(error, columnName);
                }
                else
                {
                    if (_errors.TryGetValue(columnName, out _))
                    {
                        _errors.Remove(columnName);
                    }
                }

                OnPropertyChanged(nameof(Error));
                OnPropertyChanged(nameof(IsValid));

                return error;
            }
        }

        /// <summary>
        /// Notifies subscribers when property changed
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Gets or sets the value to convert
        /// </summary>
        public string ConversionValue
        {
            get => _conversionValue;
            set
            {
                if (value != _conversionValue)
                {
                    _conversionValue = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the converted value
        /// </summary>
        public double ConvertedValue
        {
            get => _convertedValue;
            set
            {
                if (value.CompareTo(_convertedValue) != 0)
                {
                    _convertedValue = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets the available temperature unit of measurements to convert from/to
        /// </summary>
        public IDictionary<string, TemperatureUnit> TemperatureUnits { get; } = 
            Enum.GetNames(typeof(TemperatureUnit)).
                ToDictionary(unit => unit, unit => (TemperatureUnit)Enum.Parse(typeof(TemperatureUnit), unit));

        /// <summary>
        /// Gets or sets the selected temperature unit to convert from
        /// </summary>
        public TemperatureUnit SelectedFromTemperatureUnit
        {
            get => _selectedFromTemperatureUnit;
            set
            {
                if (_selectedFromTemperatureUnit != value)
                {
                    _selectedFromTemperatureUnit = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the selected temperature unit to convert to
        /// </summary>
        public TemperatureUnit SelectedToTemperatureUnit
        {
            get => _selectedToTemperatureUnit;
            set
            {
                if (_selectedToTemperatureUnit != value)
                {
                    _selectedToTemperatureUnit = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/>
        /// </summary>
        /// <param name="temperatureConverterFactory">The temperature converter factory</param>
        public MainViewModel(ITemperatureConverterFactory temperatureConverterFactory)
        {
            _temperatureConverterFactory = temperatureConverterFactory;

            PropertyChanged += MainViewModelPropertyChanged;

            SelectedToTemperatureUnit = TemperatureUnit.Fahrenheit;
            SelectedFromTemperatureUnit = TemperatureUnit.Celsius;
        }

        #endregion Constructors

        #region Protexcted methods

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event
        /// </summary>
        /// <param name="propertyName">Name of property with new value</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion Protected methods

        #region Private methods

        /// <summary>
        /// Maintains the dictionary of errors by property
        /// </summary>
        /// <param name="error">The error string</param>
        /// <param name="propertyName">The associated property name</param>
        private void StoreError(string? error, string propertyName)
        {
            if (!string.IsNullOrEmpty(error))
            {
                if (_errors.TryGetValue(propertyName, out var propertyErrors))
                {
                    if (propertyErrors.All(err => err != error))
                    {
                        propertyErrors.Add(error);
                    }
                }
                else
                {
                    _errors[propertyName] = new List<string> { error };
                }
            }
        }

        /// <summary>
        /// Property changed event handler
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The arguments</param>
        private void MainViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(SelectedFromTemperatureUnit):
                case nameof(ConversionValue):
                    Convert();
                    break;
                case nameof(SelectedToTemperatureUnit):
                    _activeTemperatureConverter = _temperatureConverterFactory.Get(SelectedToTemperatureUnit);
                    Convert();
                    break;
            }
        }

        /// <summary>
        /// Convert value using the active converter
        /// </summary>
        private void Convert()
        {
            if (IsValid &&
                double.TryParse(ConversionValue, out var parsedValue))
            {
                // Arbitrary precision applied
                ConvertedValue =  Math.Round(_activeTemperatureConverter.Convert(parsedValue, SelectedFromTemperatureUnit), ConvertedValuePrecision);
            }
        }

        #endregion Private methods
        
    }
}
