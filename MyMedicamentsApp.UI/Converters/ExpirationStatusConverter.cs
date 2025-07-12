using System.Globalization;
using MyMedicamentsApp.Core.Models;

namespace MyMedicamentsApp.UI.Converters
{
    public class ExpirationStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Medicament medicament)
            {
                return GetExpirationStatus(medicament);
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private string GetExpirationStatus(Medicament medicament)
        {
            if (IsExpired(medicament))
                return "EXPIRED";

            var days = DaysUntilExpiration(medicament);
            if (days <= 7)
                return $"Expires in {days} days";
            if (days <= 30)
                return $"Expires in {days} days";

            return $"Expires in {days} days";
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