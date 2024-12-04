using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using RSACipher.Logic;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace RSACipher
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private RSAEncryption _rsaEncryption;
        private byte[] _encryptedData;

        public MainWindow()
        {
            this.InitializeComponent();
            _rsaEncryption = new RSAEncryption();
        }

        private void GenerateKeysButton_Click(object sender, RoutedEventArgs e)
        {
            var privateKey = _rsaEncryption.GenerateKeys(true);
            var publicKey = _rsaEncryption.GenerateKeys(false);

            PrivateKeyTextBox.Text = privateKey;
            PublicKeyTextBox.Text = publicKey;
        }

        private void EncryptButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var plainText = PlainTextTextBox.Text;
                var publicKey = PublicKeyTextBox.Text;

                _encryptedData = _rsaEncryption.Encrypt(plainText, publicKey);
                EncryptedTextBox.Text = Convert.ToBase64String(_encryptedData);
            }
            catch(Exception ex)
            {
                ShowError(ex.Message);
            }
        }

        private void DecryptButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(_encryptedData != null)
                {
                    var decryptedText = _rsaEncryption.Decrypt(_encryptedData);
                    DecryptedTextBox.Text = decryptedText;
                }
                else
                {
                    ShowError("No encrypted data found.");
                }
            }
            catch(Exception ex)
            {
                ShowError(ex.Message);
            }
        }

        private void ShowError(string message)
        {
            var dialog = new ContentDialog
            {
                Title = "Error",
                Content = message,
                CloseButtonText = "OK"
            };
            _ = dialog.ShowAsync();
        }
    }
}
