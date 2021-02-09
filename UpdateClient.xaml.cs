using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using JDGrooming.Classes;

namespace JDGrooming
{
    /// <summary>
    /// Interaction logic for UpdateClient.xaml
    /// </summary>
    public partial class UpdateClient : UserControl
    {
        public UpdateClient()
        {
            InitializeComponent();
        }

        private void VerifyText(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            CheckBox chkBox = new CheckBox();
            bool pass = false;
            switch ((textBox.Name))
            {
                case "txt_Postcode":
                    chkBox = chk_Postcode;
                    pass = Verification.VerifyPostcode(textBox.Text);
                    break;
                case "txt_Email":
                    chkBox = chk_Email;
                    pass = Verification.VerifyEmail(textBox.Text);
                    break;
                case "txt_Mobile":
                    chkBox = chk_Mobile;
                    pass = Verification.VerifyPhoneNumber(textBox.Text);
                    break;
                case "txt_HomePhone":
                    pass = Verification.VerifyPhoneNumber(textBox.Text);
                    chkBox = chk_HomePhone;
                    break;
            }
            chkBox.IsChecked = pass;
        }
    }
}
