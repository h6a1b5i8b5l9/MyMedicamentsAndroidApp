using System.Globalization;
using MyMedicamentsApp.Core.Models;

namespace MyMedicamentsApp.UI.Converters
{
    public class ExpirationTextConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is Medicament medicament)
            {
                if (IsExpired(medicament))
                    return Colors.Red; // Red text for expired

                var days = DaysUntilExpiration(medicament);
                if (days <= 7)
                    return Colors.DarkOrange; // Orange text for urgent
                if (days <= 30)
                    return Colors.Orange; // Orange text for warning

                return Colors.Black; // Normal text color
            }
            return Colors.Black;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
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