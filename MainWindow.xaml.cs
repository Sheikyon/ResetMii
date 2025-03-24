using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ResetMii
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            string confirmationNumber = inputTextBox.Text;

            int delta = timeZoneComboBox.SelectedIndex - 1;
            string date = CalculateDate(delta);

            if (confirmationNumber.Length != 8 || !confirmationNumber.All(char.IsDigit))
            {
                MessageBox.Show("Please provide a valid 8-digit confirmation number.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrEmpty(date))
            {
                MessageBox.Show("Please select a date.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            GenerateUnlockCode(date, confirmationNumber);
        }

        private void GenerateUnlockCode(string date, string confirmationNumber)
        {
            string fullNum = date + confirmationNumber.Substring(4, 4);
            MessageBox.Show($"Full Number: {fullNum}", "Debug", MessageBoxButton.OK, MessageBoxImage.Information);

            uint crc = ComputeCrc32(fullNum);
            MessageBox.Show($"CRC32: {crc:X8}", "Debug", MessageBoxButton.OK, MessageBoxImage.Information);

            uint xorResult = crc ^ 0xAAAA;
            uint sumResult = xorResult + 0x14C1;
            int unlockCode = (int)(sumResult % 100000);
            MessageBox.Show($"XOR: {xorResult:X8}, Sum: {sumResult:X8}, Unlock Code: {unlockCode:D5}", "Debug", MessageBoxButton.OK, MessageBoxImage.Information);

            MessageBox.Show($"Your unlock code: {unlockCode:D5}", "Unlock Code", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private uint ComputeCrc32(string input)
        {
            uint[] table = GenerateCrc32Table();
            uint crc = 0xFFFFFFFF;

            foreach (char c in input)
            {
                byte index = (byte)((crc & 0xFF) ^ (byte)c);
                crc = (crc >> 8) ^ table[index];
            }

            return crc;
        }

        private uint[] GenerateCrc32Table()
        {
            uint[] table = new uint[256];
            for (uint i = 0; i < 256; i++)
            {
                uint crc = i;
                for (int j = 0; j < 8; j++)
                {
                    if ((crc & 1) == 1)
                        crc = (crc >> 1) ^ 0xEDB88320;
                    else
                        crc >>= 1;
                }
                table[i] = crc;
            }
            return table;
        }

        private string CalculateDate(int delta)
        {
            long ctime = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds();
            DateTime targetDate = DateTimeOffset.FromUnixTimeSeconds(ctime + delta * 86400).DateTime;
            return targetDate.ToString("MMdd");
        }
    }
}