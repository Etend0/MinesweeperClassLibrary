namespace FrmGetUser
{
    public partial class FrmGetName : Form
    {
        // Variable to hold the name of the user
        string _name = " ";
        // Variable to hold the score of the user
        public int score;

        // Constructor for the form
        public FrmGetName()
        {
            InitializeComponent();
        }

        // Override the OnShown method to display the score when the form is shown
        protected override void OnShown(EventArgs e)
        {
            // Call the base method
            base.OnShown(e);
            // Set the text of the score label to the score variable
            lblScore.Text = score.ToString();
        }

        /// <summary>
        /// Click Event Handler to grab the users name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void BtnSubmit_Click(object sender, EventArgs e)
        {
            // Set the name variable to the text of the text box
            _name = txtName.Text;

            // Variable to hold the integer value of the name if it is an integer
            int value;

            // Bool to check if the name is an integer
            bool isInteger = int.TryParse(_name, out value);

            // Check if the name is empty or an integer, if so, show a message box to enter a name and return
            if (string.IsNullOrWhiteSpace(_name) || isInteger == true)
            {
                // Prompt the user to enter a name
                System.Windows.Forms.MessageBox.Show("Please enter your name.");
                return;
            }
            else
            {
                // Close the form
                this.Close();
            }
        }

        /// <summary>
        /// Method to return the name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public String returnString(string name)
        {
            // Set the name variable to the value of the name parameter
            name = _name;
            // Return the name variable
            return name;
        }
    }
}
