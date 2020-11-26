using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;

namespace BitmapsComaprer
{
    public class MainWidowViewModel : BindableBase
    {
        public MainWidowViewModel()
        {
            LoadFirstBitampCommand = new DelegateCommand(LoadFirstBitamp);
            LoadSecondBitampCommand = new DelegateCommand(LoadSecondBitamp);
            CompareBitampsCommand = new DelegateCommand(CompareBitamps);
        }

        private string _firstBitmap;
        public string FirstBitmap
        {
            get => _firstBitmap;
            set => SetProperty(ref _firstBitmap, value);
        }

        private string _secondBitmap;
        public string SecondBitmap
        {
            get => _secondBitmap;
            set => SetProperty(ref _secondBitmap, value);
        }

        private string _difference;
        public string Difference
        {
            get => _difference;
            set => SetProperty(ref _difference, value);
        }

        public ICommand LoadFirstBitampCommand { get; }
        public ICommand LoadSecondBitampCommand { get; }
        public ICommand CompareBitampsCommand { get; }

        private void LoadFirstBitamp()
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();

                dlg.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";

                var result = dlg.ShowDialog();

                if (result == true)
                {

                    string filename = dlg.FileName;
                    string path = dlg.InitialDirectory;

                    FirstBitmap = path + filename;
                }
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
                OpenFileDialog dlg = new OpenFileDialog();

                dlg.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";

                var result = dlg.ShowDialog();

                if (result == true)
                {

                    string filename = dlg.FileName;
                    string path = dlg.InitialDirectory;

                    SecondBitmap = path + filename;
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
            }
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


    }
}
