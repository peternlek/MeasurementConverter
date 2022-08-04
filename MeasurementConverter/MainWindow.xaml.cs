using System.Windows;
using MeasurementConverter.Factories;
using MeasurementConverter.ViewModels;

namespace MeasurementConverter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Properties

        /// <summary>
        /// Gets the data context for this view
        /// </summary>
        public MainViewModel MainViewModel { get; } = new MainViewModel(new TemperatureConverterFactory());

        #endregion Properties

        #region Constructors

        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion Constructors
    }
}
