using DashboardUI.Validators;
using FluentValidation.Results;
using ModelLibrary;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace DashboardUI
{
    public partial class Dashboard : Form
    {
        readonly BindingList<string> errors = new BindingList<string>();

        public Dashboard()
        {
            InitializeComponent();
            errorListBox.DataSource = errors;
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            errors.Clear();
            PersonModel person = new PersonModel();

            // Validate my data
            var validator = new PersonValidators();
            var results = validator.Validate(person);

            if (!results.IsValid)
            {
                foreach (ValidationFailure failure in results.Errors)
                {
                    errors.Add($"{failure.ErrorMessage}");
                }
            }

            if (!decimal.TryParse(accountBalanceText.Text, out decimal accountBalance))
            {
                errors.Add("Account Balance: Invalid Amount");
                return;
            }

            person.FirstName = firstNameText.Text;
            person.LastName = lastNameText.Text;
            person.AccountBalance = accountBalance;
            person.DateOfBirth = dateOfBirthPicker.Value;

            // Insert into the database

            MessageBox.Show("Operation Complete");
        }
    }
}
