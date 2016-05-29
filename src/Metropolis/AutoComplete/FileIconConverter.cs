using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Data;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Metropolis.AutoComplete
{
    public class FileIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var iconFile = SystemIcons.WinLogo;
            var fsInfo = value as FileSystemInfo;

            if (fsInfo == null) return ConvertToImageSource(iconFile);
            try
            {
                if (fsInfo is DirectoryInfo)
                {
                    iconFile = Properties.Resources.folder_2_32;
                }
                else
                {
                    var filePath = fsInfo.FullName;

                    if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
                        iconFile = Icon.ExtractAssociatedIcon(filePath);
                }
            }
            catch (Exception)
            {

            }
            return ConvertToImageSource(iconFile);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }

        private static ImageSource ConvertToImageSource(Icon icon)
        {
            using (var i = Icon.FromHandle(icon.Handle))
            {

                ImageSource img = Imaging.CreateBitmapSourceFromHIcon(
                                        i.Handle,
                                        new Int32Rect(0, 0, i.Width, i.Height),
                                        BitmapSizeOptions.FromEmptyOptions());
                return img;
            }
        }
    }
}
