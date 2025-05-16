using System;
using System.Drawing;
using System.Windows.Forms;

namespace PiratesVisualization
{
    public partial class ResultsForm : Form
    {
        // Public property to set the text in the results TextBox
        public string ResultsText
        {
            get { return textBoxResults.Text; }
            set { textBoxResults.Text = value; }
        }

        public ResultsForm()
        {
            InitializeComponent(); // This will set up the form and its controls

            // --- Configure the form and its controls ---
            this.Text = "Simulation Complete"; // Set the title bar text
            this.StartPosition = FormStartPosition.CenterParent; // Center the form on its parent
            this.MinimizeBox = false; // Disable minimize button
            this.MaximizeBox = false; // Disable maximize button
            this.FormBorderStyle = FormBorderStyle.FixedDialog; // Fixed size dialog

            // Create and configure the TextBox
            textBoxResults = new TextBox();
            textBoxResults.Multiline = true; // Allow multiple lines of text
            textBoxResults.ReadOnly = true; // Make the text read-only
            textBoxResults.ScrollBars = ScrollBars.Both; // Add horizontal and vertical scrollbars
            textBoxResults.WordWrap = false; // Disable word wrap so horizontal scrollbar is useful for long lines
            textBoxResults.Dock = DockStyle.Fill; // Make the TextBox fill the form

            // --- Improve text appearance and add padding ---
            textBoxResults.Font = new Font("Consolas", 10, FontStyle.Regular); // Use a monospaced font for potentially better alignment of data
            textBoxResults.Padding = new Padding(5); // Add 5 pixels of padding on all sides


            // Create a Panel to hold the TextBox and provide padding
            Panel textPanel = new Panel();
            textPanel.Dock = DockStyle.Fill;
            textPanel.Padding = new Padding(10); // Add padding around the TextBox

            // Add the TextBox to the text Panel
            textPanel.Controls.Add(textBoxResults);


            // Create and configure the OK button
            buttonOK = new Button();
            buttonOK.Text = "OK";
            buttonOK.DialogResult = DialogResult.OK; // Set the dialog result

            // Create a Panel to hold the OK button and dock it at the bottom
            Panel buttonPanel = new Panel();
            buttonPanel.Height = 40; // Set a height for the button panel
            buttonPanel.Dock = DockStyle.Bottom;

            // Center the OK button within the button panel
            buttonOK.AutoSize = true; // Allow button to size based on text
                                      // Recalculate location in Resize handler
            buttonOK.Anchor = AnchorStyles.None; // Disable anchoring to center it

            // Add the OK button to the panel
            buttonPanel.Controls.Add(buttonOK);

            // Add the text Panel (containing the TextBox) and the button Panel to the form
            this.Controls.Add(textPanel); // Add textPanel first so it fills the remaining space above the button panel
            this.Controls.Add(buttonPanel); // Add button panel last so it's at the bottom


            // Set the AcceptButton and CancelButton to the OK button
            this.AcceptButton = buttonOK;
            this.CancelButton = buttonOK;

            // Hook up the button click event to close the form
            buttonOK.Click += (sender, e) => { this.Close(); };

            // --- Adjust layout on resize (for centering the button) ---
            this.Resize += (sender, e) =>
            {
                // Recalculate button location when the form is resized
                buttonOK.Location = new Point((buttonPanel.ClientSize.Width - buttonOK.Width) / 2, (buttonPanel.ClientSize.Height - buttonOK.Height) / 2);
            };

            // --- Set a default size for the form ---
            this.Size = new Size(500, 400); // Adjusted default size for better viewing
        }

        // --- Designer generated code (usually in .Designer.cs) ---
        // You'll need to manually add these control declarations
        private System.Windows.Forms.TextBox textBoxResults;
        private System.Windows.Forms.Button buttonOK;

        // The InitializeComponent method would typically be in ResultsForm.Designer.cs
        // Since we are adding controls manually in the constructor, we can have a minimal one here.
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ResultsForm
            // 
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Name = "ResultsForm";
            this.Load += new System.EventHandler(this.ResultsForm_Load);
            this.ResumeLayout(false);

        }

        private void ResultsForm_Load(object sender, EventArgs e)
        {

        }
    }
}

