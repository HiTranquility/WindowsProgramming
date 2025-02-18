using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace LoginForm
{
    public partial class LoginForm : Form
    {
        private string connectionString = "server=localhost;database=mydb;user=root;password=CaVN2004;";
        public LoginForm()
        {
            InitializeComponent();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            string username = usernameTxt.Text.Trim();
            string password = passwordTxt.Text.Trim();  // Use Trim() to avoid extra spaces

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string query = "SELECT COUNT(*) FROM log_in WHERE username = @user AND password = @pass";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@user", MySqlDbType.VarChar).Value = username;
                        cmd.Parameters.Add("@pass", MySqlDbType.VarChar).Value = password;

                        int count = Convert.ToInt32(cmd.ExecuteScalar());  // Gets number of matching rows

                        if (count > 0)
                        {
                            MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = DialogResult.OK;  // Proceed to the main menu
                        }
                        else
                        {
                            MessageBox.Show("Invalid username or password.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btShowImage_Click(object sender, EventArgs e)
        {
            // Use a verbatim string literal for the file path to avoid escape characters
            string filePath = @"C:\Users\Admin\Pictures\Yuta Dragon\yuttari_dragon_01.png";

            // Check if the file exists before trying to load it
            if (!File.Exists(filePath))
            {
                MessageBox.Show("Image file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Load the image and set it to the PictureBox
                profilePic.Image = Image.FromFile(filePath);
                profilePic.SizeMode = PictureBoxSizeMode.Zoom;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading image: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btChangeImage_Click(object sender, EventArgs e)
        {
            // Let the user select an image file using an OpenFileDialog
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                openFileDialog.Title = "Select an Image";

                // Show the dialog and check if the user clicked OK
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Load the selected image and display it in the PictureBox
                        profilePic.Image = Image.FromFile(openFileDialog.FileName);
                        profilePic.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error loading image: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
