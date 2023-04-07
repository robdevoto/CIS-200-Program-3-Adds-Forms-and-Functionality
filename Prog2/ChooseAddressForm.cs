// Program 3
// CIS 200-75
// Fall 2022
// Due: 11/23/2022
// By: 1001001

// File: ChooseAddressForm.cs
// This class creates the GUI for Program 3 for editing existing addresses.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UPVApp
{
    public partial class ChooseAddressForm : Form
    {
        private List<Address> addressList;  // List of addresses used to fill combo boxes

        // Precondition:  None
        // Postcondition: The forms GUI is prepared for display
        public ChooseAddressForm(List<Address> addresses)
        {
            InitializeComponent();
            addressList = addresses;
        }

        //Property
        public int AddressIndex
        {
            // Precondition:  An item from the combo box has been selected
            // Postcondition: The index of the selected value is returned
            get
            {
                return addressListCbo.SelectedIndex;
            }

            // Precondition:  value is between -1 (inclusive) and the addressList.Count
            // Postcondition: The specified index is set
            set
            {
                if ((value >= -1) && (value < addressList.Count))
                    addressListCbo.SelectedIndex = value;
                else
                    throw new ArgumentOutOfRangeException("AddressIndex", value,
                        "Index must be valid");
            }
        }

        // Precondition:  None
        // Postcondition: The address list is used to populate the name in the combo box
        private void ChooseAddressForm_Load(object sender, EventArgs e)
        {
            foreach (Address a in addressList)
            {
                addressListCbo.Items.Add(a.Name);
            }

            addressListCbo.SelectedIndex = 0;
        }

        // Precondition:  The cancel button is selected
        // Postcondition: The form is closed
        private void cancelBtn_MouseDown(object sender, MouseEventArgs e)
        {
            DialogResult= DialogResult.Cancel;
        }

        // Precondition:  Focus is shifting from the combo box
        // Postcondition: An error provider shows that no address is selected
        private void addressListCbo_Validating(object sender, CancelEventArgs e)
        {
            if (addressListCbo.SelectedIndex == -1)
            {
                e.Cancel = true;
                errorProvider1.SetError(addressListCbo, "Please select an address.");
            }
        }

        // Precondition:  The data is okay and has passed validating
        // Postcondition: Error provider cleared and focus allowed to change
        private void addressListCbo_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(addressListCbo, "");
        }

        // Precondition:  The ok button is clicked
        // Postcondition: If everthing validated then form will close,
        //                otherwise form stays open for correction
        private void okBtn_Click(object sender, EventArgs e)
        {
            if (ValidateChildren())
                DialogResult= DialogResult.OK;
        }
    }
}
