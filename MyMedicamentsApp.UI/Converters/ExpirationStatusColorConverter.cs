using System.Globalization;
using MyMedicamentsApp.Core.Models;

namespace MyMedicamentsApp.UI.Converters
{
    public class ExpirationStatusColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is Medicament medicament)
            {
                if (IsExpired(medicament))
                    return Colors.Red;

                var days = DaysUntilExpiration(medicament);
                if (days <= 7)
                    return Colors.Orange;
                if (days <= 30)
                    return Colors.DarkOrange;

                return Colors.Green;
            }
            return Colors.Gray;
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