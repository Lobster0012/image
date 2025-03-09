using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace project_image
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            using (var _context = DbImageEntities.GetContext())
            {
                var data = _context.primer.ToList();

                var primerList = data.Select(p => new
                {
                    Id = p.id,
                    Opisanie = p.opisanie,
                    PhotoFail = p.photo_fail
                }).ToList();

                DataContext = primerList;
            }
        }
    }

    /// <summary>
    /// Конвертер для преобразования byte[] в изображение
    /// </summary>
    public class ByteArrayToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is byte[] byteArray && byteArray.Length > 0)
            {
                var image = new BitmapImage();
                using (var stream = new MemoryStream(byteArray))
                {
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.StreamSource = stream;
                    image.EndInit();
                }
                return image;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}