using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Input;

namespace BitmapsComaprer
{
    public class MainWidowViewModel : BindableBase
    {
        public MainWidowViewModel()
        {
            LoadInitialFirstBitampCommand = new DelegateCommand(LoadInitialFirstBitamp);
            LoadInitialSecondBitampCommand = new DelegateCommand(LoadInitialSecondBitamp);
            LoadFirstBitampCommand = new DelegateCommand(LoadFirstBitamp);
            LoadSecondBitampCommand = new DelegateCommand(LoadSecondBitamp);
            CompareBitampsCommand = new DelegateCommand(CompareBitamps);
        }
        private string _initialFirstBitmap;
        public string InitialFirstBitmap
        {
            get => _initialFirstBitmap;
            set => SetProperty(ref _initialFirstBitmap, value);
        }

        private string _firstBitmap;
        public string FirstBitmap
        {
            get => _firstBitmap;
            set => SetProperty(ref _firstBitmap, value);
        }

        private string _initialSecondBitmap;
        public string InitialSecondBitmap
        {
            get => _initialSecondBitmap;
            set => SetProperty(ref _initialSecondBitmap, value);
        }
        private string _secondBitmap;
        public string SecondBitmap
        {
            get => _secondBitmap;
            set => SetProperty(ref _secondBitmap, value);
        }

        private double _averageValueFirstBitmap;
        public double AverageValueFirstBitmap
        {
            get => _averageValueFirstBitmap;
            set => SetProperty(ref _averageValueFirstBitmap, value);
        }

        private double _averageValueSecondBitmap;
        public double AverageValueSecondBitmap
        {
            get => _averageValueSecondBitmap;
            set => SetProperty(ref _averageValueSecondBitmap, value);
        }

        private string _difference;
        public string Difference
        {
            get => _difference;
            set => SetProperty(ref _difference, value);
        }

        public ICommand LoadFirstBitampCommand { get; }
        public ICommand LoadSecondBitampCommand { get; }
        public ICommand LoadInitialFirstBitampCommand { get; }
        public ICommand LoadInitialSecondBitampCommand { get; }
        public ICommand CompareBitampsCommand { get; }

        private void LoadInitialFirstBitamp()
        {
            try
            {
                InitialFirstBitmap = GetDirectory();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
            }
        }
        private void LoadInitialSecondBitamp()
        {
            try
            {
                InitialSecondBitmap = GetDirectory();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
            }
        }
        private string GetDirectory()
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = "C:\\Users";
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                return dialog.FileName;
            }
            return "C:\\Users";
        }
        private void LoadFirstBitamp()
        {
            try
            {
                var initial = "C:\\Users";
                if (!string.IsNullOrEmpty(InitialFirstBitmap))
                {
                    initial = InitialFirstBitmap;
                }

                var bitmapString = GetBitamp(initial);

                if (string.IsNullOrEmpty(bitmapString))
                {
                    return;
                }

                FirstBitmap = bitmapString;
                AverageValueFirstBitmap = AverageBitmap(FirstBitmap);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
            }
        }
        private void LoadSecondBitamp()
        {
            try
            {
                var initial = "C:\\Users";
                if (!string.IsNullOrEmpty(InitialSecondBitmap))
                {
                    initial = InitialSecondBitmap;
                }

                var bitmapString = GetBitamp(initial);

                if (string.IsNullOrEmpty(bitmapString))
                {
                    return;
                }

                SecondBitmap = bitmapString;
                AverageValueSecondBitmap = AverageBitmap(SecondBitmap);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
            }
        }
        private string GetBitamp(string initialDirectory)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png, *.bmp) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png; *.bmp";
            dlg.InitialDirectory = initialDirectory;

            var result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;
                return  filename;
            }

            return string.Empty;
        }
        private void CompareBitamps()
        {
            try
            {
                if (FirstBitmap == null)
                {
                    return;
                }

                if (SecondBitmap == null)
                {
                    return;
                }

                var firstArray = new Bitmap(FirstBitmap).ToArray();
                var secondArray = new Bitmap(SecondBitmap).ToArray();

                if(firstArray.Length != secondArray.Length)
                {
                    Difference = $"Choose bitmap with the same size.";
                    return;
                }

                List<double> differenceValues = new List<double>();

                for (int i = 0; i < firstArray.Length; i++)
                {
                    var difference = Math.Abs(firstArray[i] - secondArray[i]);

                    differenceValues.Add(difference);
                }

                var sumOfDifferences = differenceValues.Sum();
                var countOfElements = differenceValues.Count;

                var averageDifference = sumOfDifferences / countOfElements;

                Difference = $"Average Difference between bitmaps: {averageDifference}";
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
            }
        }

        private double AverageBitmap(string path)
        {
            Bitmap bitmap = new Bitmap(path);

            var array = bitmap.ToArray();

            long sumPixelValue = 0;

            foreach (byte pixel in array)
            {
                sumPixelValue += pixel;
            }

            double averageValue = (double)sumPixelValue / (double)array.Length;

            return averageValue;
        }
    }
}
