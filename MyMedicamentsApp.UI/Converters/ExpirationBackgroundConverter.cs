using System.Globalization;
using MyMedicamentsApp.Core.Models;

namespace MyMedicamentsApp.UI.Converters
{
    public class ExpirationBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Medicament medicament)
            {
                if (IsExpired(medicament))
                    return Color.FromArgb("#FFEBEE"); // Light red background

                var days = DaysUntilExpiration(medicament);
                if (days <= 7)
                    return Color.FromArgb("#FFF3E0"); // Light orange background
                if (days <= 30)
                    return Color.FromArgb("#FFF8E1"); // Light yellow background

                return Colors.Transparent; // Normal background
            }
            return Colors.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private bool IsExpired(Medicament medicament)
        {
            return medicament.ExpirationDate.Date < DateTime.Today;
        }

        private int DaysUntilExpiration(Medicament medicament)
        {
            return (medicament.ExpirationDate.Date - DateTime.Today).Days;
        }
    }
} 