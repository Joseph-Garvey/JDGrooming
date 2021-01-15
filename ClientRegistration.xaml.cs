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
    /// Interaction logic for ClientRegistration.xaml
    /// </summary>
    public partial class ClientRegistration : UserControl
    {
        public ClientRegistration()
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

        private void Register(object sender, RoutedEventArgs e)
        {
            // add letter validation for names etc
            // try something similar to last year's system but more efficient.
            // this time get something that works, then improve on it.
            // also auto format the strings in future.
            const String failedNameFormat = "\u2022 Names must consist of letters.";
            const String failedForenameLength = "\u2022 Forename must be less than 35 characters.";
            const String failedSurnameLength = "\u2022 Surname must be less than 50 characters.";
            const String failedAddressLength = "\u2022 Address line must be less than 35 characters.";
            const String failedTownLength = "\u2022 Town/City name must be less than 64 characters.";
            const String failedPostCode = "\u2022 Postcode must be of valid format and length eg. XX0 XX0.";
            const String failedEmail = "\u2022 Email must be of valid format and length (<255 chars) eg. JoeBloggs@mail.com";
            const String failedPhone = "\u2022 PhoneNo must be of valid format and length (<32 chars)";
            const String failedContactDetail = "\u2022 Customer must provide at least one method of contact.";
            const String failedMissingInfo = "\u2022 All fields marked * must be completed.";
            String Errors = "";
            if ((txt_Forename.Text == "")
                || (txt_Surname.Text == "")
                || (txtAddress1.Text == "")
                || (txt_Postcode.Text == "")
                || (txt_Town.Text == "")
                ) { AddToErrors(ref Errors, failedMissingInfo); }
            if ((txt_Email.Text == "")
                && (txt_HomePhone.Text == "")
                && (txt_Mobile.Text == "")
                ) { AddToErrors(ref Errors, failedContactDetail); }
            if (txt_Forename.Text.Length > 35)  AddToErrors(ref Errors,failedForenameLength);
            if (txt_Surname.Text.Length > 50)  AddToErrors(ref Errors, failedSurnameLength);
            if(!CheckCharacters(txt_Forename.Text) || !CheckCharacters(txt_Surname.Text))  AddToErrors(ref Errors,failedNameFormat);
            if ((txtAddress1.Text.Length > 35) || (txtAddress2.Text.Length > 35))  AddToErrors(ref Errors, failedAddressLength);
            if (txt_Town.Text.Length > 64)  AddToErrors(ref Errors, failedTownLength);
            if ((txt_Postcode.Text.Length > 8) || (!Verification.VerifyPostcode(txt_Postcode.Text)))  AddToErrors(ref Errors, failedPostCode);
            bool ContactDetailsPresent = false;
            if(txt_Email.Text != "")
            {
                ContactDetailsPresent = true;
                if((txt_Email.Text.Length > 255) || (!Verification.VerifyEmail(txt_Email.Text))) AddToErrors(ref Errors, failedEmail);
            }
            if (txt_Mobile.Text != "")
            {
                ContactDetailsPresent = true;
                if ((txt_Mobile.Text.Length > 32) || (!Verification.VerifyPhoneNumber(txt_Mobile.Text))) AddToErrors(ref Errors, failedPhone);
            }
            if (txt_HomePhone.Text != "")
            {
                ContactDetailsPresent = true;
                if ((txt_HomePhone.Text.Length > 32) || (!Verification.VerifyPhoneNumber(txt_HomePhone.Text))) AddToErrors(ref Errors, failedPhone);
            }
            if (!ContactDetailsPresent) { AddToErrors(ref Errors,failedContactDetail); }
            if(Errors != "") { MessageBox.Show(Errors, "Error", MessageBoxButton.OK ,MessageBoxImage.Error); }
            else
            {
                String Start = "INSERT INTO [Client] ([Surname], [Forename], [FirstLine], [Town], [Postcode]";
                String End = String.Format("VALUES('{0}', '{1}', '{2}', '{3}', '{4}'", txt_Surname.Text, txt_Forename.Text, txtAddress1.Text, txt_Town.Text, txt_Postcode.Text);
                if (txt_Email.Text != "") { Start += ", [Email]"; End += ", '" + txt_Email.Text + "'"; }
                if (txt_HomePhone.Text != "") { Start += ", [HomePhone]"; End += ", '" + txt_HomePhone.Text + "'"; } // fix
                if (txt_Mobile.Text != "") { Start += ", [Mobile]"; End += ", '" + txt_Mobile.Text + "'"; } // fix
                if (txtAddress2.Text != "") { Start += ", [SecondLine]"; End += ", '" + txtAddress2.Text + "'"; }
                ((App)Application.Current).query.QueryDatabase(Start + ") " + End + ");");
            }
        }
        private void AddToErrors(ref String ErrorList, String Error)
        {
            Error += Environment.NewLine;
            ErrorList += Error;
        }
        private bool CheckCharacters(String test)
        {
            foreach(Char c in test)
            {
                if (!Char.IsLetter(c)) { return false; }
            }
            return true;
        }
    }
}
