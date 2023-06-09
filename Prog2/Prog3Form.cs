﻿// Program 3
// CIS 200-75
// Fall 2022
// Due: 11/22/2022
// By: 1001001

// File: Prog3Form.cs
// This class creates the main GUI for Program 2. It provides a
// File menu with About and Exit items, an Edit menu with an Address item,
// an Insert menu with Address and Letter items, and a Report menu with
// List Addresses and List Parcels items.

using Prog2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;

namespace UPVApp
{
    public partial class Prog3Form : Form
    {
        private UserParcelView upv; // The UserParcelView

        // Precondition:  None
        // Postcondition: The form's GUI is prepared for display. A few test addresses are
        //                added to the list of addresses
        public Prog3Form()
        {
            InitializeComponent();

            upv = new UserParcelView();

            // Test data commented out after adding the "Save As" file menu code
            // Test Data - Magic Numbers OK
            //upv.AddAddress("  John Smith  ", "   123 Any St.   ", "  Apt. 45 ",
            //    "  Louisville   ", "  KY   ", 40202); // Test Address 1
            //upv.AddAddress("Jane Doe", "987 Main St.",
            //    "Beverly Hills", "CA", 90210); // Test Address 2
            //upv.AddAddress("James Kirk", "654 Roddenberry Way", "Suite 321",
            //    "El Paso", "TX", 79901); // Test Address 3
            //upv.AddAddress("John Crichton", "678 Pau Place", "Apt. 7",
            //    "Portland", "ME", 04101); // Test Address 4
            //upv.AddAddress("John Doe", "111 Market St.", "",
            //    "Jeffersonville", "IN", 47130); // Test Address 5
            //upv.AddAddress("Jane Smith", "55 Hollywood Blvd.", "Apt. 9",
            //    "Los Angeles", "CA", 90212); // Test Address 6
            //upv.AddAddress("Captain Robert Crunch", "21 Cereal Rd.", "Room 987",
            //    "Bethesda", "MD", 20810); // Test Address 7
            //upv.AddAddress("Vlad Dracula", "6543 Vampire Way", "Apt. 1",
            //    "Bloodsucker City", "TN", 37210); // Test Address 8

            //upv.AddLetter(upv.AddressAt(0), upv.AddressAt(1), 3.95M);                     // Letter test object
            //upv.AddLetter(upv.AddressAt(2), upv.AddressAt(3), 4.25M);                     // Letter test object
            //upv.AddGroundPackage(upv.AddressAt(4), upv.AddressAt(5), 14, 10, 5, 12.5);    // Ground test object
            //upv.AddGroundPackage(upv.AddressAt(6), upv.AddressAt(7), 8.5, 9.5, 6.5, 2.5); // Ground test object
            //upv.AddNextDayAirPackage(upv.AddressAt(0), upv.AddressAt(2), 25, 15, 15,      // Next Day test object
            //    85, 7.50M);
            //upv.AddNextDayAirPackage(upv.AddressAt(2), upv.AddressAt(4), 9.5, 6.0, 5.5,   // Next Day test object
            //    5.25, 5.25M);
            //upv.AddNextDayAirPackage(upv.AddressAt(1), upv.AddressAt(6), 10.5, 6.5, 9.5,  // Next Day test object
            //    15.5, 5.00M);
            //upv.AddTwoDayAirPackage(upv.AddressAt(4), upv.AddressAt(6), 46.5, 39.5, 28.0, // Two Day test object
            //    80.5, TwoDayAirPackage.Delivery.Saver);
            //upv.AddTwoDayAirPackage(upv.AddressAt(7), upv.AddressAt(0), 15.0, 9.5, 6.5,   // Two Day test object
            //    75.5, TwoDayAirPackage.Delivery.Early);
            //upv.AddTwoDayAirPackage(upv.AddressAt(5), upv.AddressAt(3), 12.0, 12.0, 6.0,  // Two Day test object
            //    5.5, TwoDayAirPackage.Delivery.Saver);
        }

        // Precondition:  File, About menu item activated
        // Postcondition: Information about author displayed in dialog box
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string NL = Environment.NewLine; // Newline shorthand

            MessageBox.Show($"Program 3{NL}By: 1001001{NL}CIS 200{NL}Fall 2022",
                "About Program 3");
        }

        // Precondition:  File, Exit menu item activated
        // Postcondition: The application is exited
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Precondition:  Insert, Address menu item activated
        // Postcondition: The Address dialog box is displayed. If data entered
        //                are OK, an Address is created and added to the list
        //                of addresses
        private void addressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddressForm addressForm = new AddressForm();    // The address dialog box form
            DialogResult result = addressForm.ShowDialog(); // Show form as dialog and store result
            int zip; // Address zip code

            if (result == DialogResult.OK) // Only add if OK
            {
                if (int.TryParse(addressForm.ZipText, out zip))
                {
                    upv.AddAddress(addressForm.AddressName, addressForm.Address1,
                        addressForm.Address2, addressForm.City, addressForm.State,
                        zip); // Use form's properties to create address
                }
                else // This should never happen if form validation works!
                {
                    MessageBox.Show("Problem with Address Validation!", "Validation Error");
                }
            }

            addressForm.Dispose(); // Best practice for dialog boxes
                                   // Alternatively, use with using clause as in Ch. 17
        }

        // Precondition:  Report, List Addresses menu item activated
        // Postcondition: The list of addresses is displayed in the addressResultsTxt
        //                text box
        private void listAddressesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder result = new StringBuilder(); // Holds text as report being built
                                                        // StringBuilder more efficient than String
            string NL = Environment.NewLine;            // Newline shorthand

            result.Append("Addresses:");
            result.Append(NL); // Remember, \n doesn't always work in GUIs
            result.Append(NL);

            foreach (Address a in upv.AddressList)
            {
                result.Append(a.ToString());
                result.Append(NL);
                result.Append("------------------------------");
                result.Append(NL);
            }

            reportTxt.Text = result.ToString();

            // -- OR --
            // Not using StringBuilder, just use TextBox directly

            //reportTxt.Clear();
            //reportTxt.AppendText("Addresses:");
            //reportTxt.AppendText(NL); // Remember, \n doesn't always work in GUIs
            //reportTxt.AppendText(NL);

            //foreach (Address a in upv.AddressList)
            //{
            //    reportTxt.AppendText(a.ToString());
            //    reportTxt.AppendText(NL);
            //    reportTxt.AppendText("------------------------------");
            //    reportTxt.AppendText(NL);
            //}

            // Put cursor at start of report
            reportTxt.Focus();
            reportTxt.SelectionStart = 0;
            reportTxt.SelectionLength = 0;
        }

        // Precondition:  Insert, Letter menu item activated
        // Postcondition: The Letter dialog box is displayed. If data entered
        //                are OK, a Letter is created and added to the list
        //                of parcels
        private void letterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LetterForm letterForm; // The letter dialog box form
            DialogResult result;   // The result of showing form as dialog
            decimal fixedCost;     // The letter's cost

            if (upv.AddressCount < LetterForm.MIN_ADDRESSES) // Make sure we have enough addresses
            {
                MessageBox.Show("Need " + LetterForm.MIN_ADDRESSES + " addresses to create letter!",
                    "Addresses Error");
                return; // Exit now since can't create valid letter
            }

            letterForm = new LetterForm(upv.AddressList); // Send list of addresses
            result = letterForm.ShowDialog();

            if (result == DialogResult.OK) // Only add if OK
            {
                if (decimal.TryParse(letterForm.FixedCostText, out fixedCost))
                {
                    // For this to work, LetterForm's combo boxes need to be in same
                    // order as upv's AddressList
                    upv.AddLetter(upv.AddressAt(letterForm.OriginAddressIndex),
                        upv.AddressAt(letterForm.DestinationAddressIndex),
                        fixedCost); // Letter to be inserted
                }
               else // This should never happen if form validation works!
                {
                    MessageBox.Show("Problem with Letter Validation!", "Validation Error");
                }
            }

            letterForm.Dispose(); // Best practice for dialog boxes
                                  // Alternatively, use with using clause as in Ch. 17
        }

        // Precondition:  Report, List Parcels menu item activated
        // Postcondition: The list of parcels is displayed in the parcelResultsTxt
        //                text box
        private void listParcelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // This report is generated without using a StringBuilder, just to show an
            // alternative approach more like what most students will have done
            // Method AppendText is equivalent to using .Text +=

            decimal totalCost = 0;                      // Running total of parcel shipping costs
            string NL = Environment.NewLine;            // Newline shorthand

            reportTxt.Clear(); // Clear the textbox
            reportTxt.AppendText("Parcels:");
            reportTxt.AppendText(NL); // Remember, \n doesn't always work in GUIs
            reportTxt.AppendText(NL);

            foreach (Parcel p in upv.ParcelList)
            {
                reportTxt.AppendText(p.ToString());
                reportTxt.AppendText(NL);
                reportTxt.AppendText("------------------------------");
                reportTxt.AppendText(NL);
                totalCost += p.CalcCost();
            }

            reportTxt.AppendText(NL);
            reportTxt.AppendText($"Total Cost: {totalCost:C}");

            // Put cursor at start of report
            reportTxt.Focus();
            reportTxt.SelectionStart = 0;
            reportTxt.SelectionLength = 0;
        }

        // Precondition:  File, Save As menu item activated
        // Postcondition: The list of addresses is saved to a file using serialization
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream output = null;
            DialogResult result;
            string fileName;

            using (SaveFileDialog fileChooser = new SaveFileDialog())
            {
                fileChooser.CheckFileExists = false;

                result = fileChooser.ShowDialog();
                fileName = fileChooser.FileName;
            }

            if (result == DialogResult.OK)
            {
                if (fileName == string.Empty)
                    MessageBox.Show("Invalid File Name", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    // try block while saving to catch exceptions
                    try
                    {
                        output = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                        formatter.Serialize(output, upv);
                    }
                    // exception handler
                    catch (IOException)
                    {
                        MessageBox.Show("I/O Error Writing to File", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    // exception handler
                    catch (SerializationException)
                    {
                        MessageBox.Show("Serialization Error Writing to File", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    // finally block which always executes to close the file if it exists
                    finally
                    {
                        output?.Close();
                    }
                }
            }
        }

        // Precondition:  File, Open menu item activated
        // Postcondition: The UserParceView is read from a file using deserialization
        //                and the replacing the existing upv with the current
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BinaryFormatter reader = new BinaryFormatter();
            FileStream input = null;
            DialogResult result;
            string fileName;
            UserParcelView temp;

            using (OpenFileDialog filechooser = new OpenFileDialog())
            {
                result = filechooser.ShowDialog();
                fileName = filechooser.FileName;
            }

            if (result == DialogResult.OK)
            {
                if (fileName == string.Empty)
                    MessageBox.Show("Invalid File Name", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    // try block while reading to catch exceptions
                    try
                    {
                        input = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                        temp = (UserParcelView)reader.Deserialize(input);
                        upv = temp;
                    }
                    // exception handler
                    catch (IOException)
                    {
                        MessageBox.Show("I/O Error Reading from File", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    // exception handler
                    catch (SerializationException)
                    {
                        MessageBox.Show("Serialization Error Reading from File", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    // finally block which always executes to close the file if it exists
                    finally
                    {
                        input?.Close();
                    }
                }
            }
        }

        // Precondition:  Edit, Address menu item activated
        // Postcondition: An address is selected from a list and edited 
        //                replaceing the old information with the new
        //                information via the object's properites
        private void addressToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (upv.AddressList.Count > 0)
            {
                ChooseAddressForm chooseAddForm = new ChooseAddressForm(upv.AddressList);
                DialogResult result = chooseAddForm.ShowDialog();

                if (result == DialogResult.OK)
                {
                    int editIndex;
                    editIndex = chooseAddForm.AddressIndex;

                    if (editIndex >= 0)
                    {
                        Address editAddress = upv.AddressAt(editIndex);
                        AddressForm addressForm = new AddressForm();

                        addressForm.AddressName = editAddress.Name;
                        addressForm.Address1 = editAddress.Address1;
                        addressForm.Address2 = editAddress.Address2;
                        addressForm.City = editAddress.City;
                        addressForm.State = editAddress.State;
                        addressForm.ZipText = $"{editAddress.Zip:D5}";

                        result = addressForm.ShowDialog();

                        if (result == DialogResult.OK)
                        {
                            editAddress.Name = addressForm.AddressName;
                            editAddress.Address1 = addressForm.Address1;
                            editAddress.Address2 = addressForm.Address2;
                            editAddress.City = addressForm.City;
                            editAddress.State = addressForm.State;
                            if (int.TryParse(addressForm.ZipText, out int zip))
                            {
                                editAddress.Zip = zip;
                            }
                            else
                            {
                                MessageBox.Show("Issue with Zip Code Validation", "Validation Error");
                            }
                        }
                        addressForm.Dispose();
                    }
                }
                chooseAddForm.Dispose();
            }
            else MessageBox.Show("No addresses to edit.", "No Addresses");
        }
    }
}