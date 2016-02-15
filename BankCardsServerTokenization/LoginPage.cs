using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;
using BankCardsServerTokenization;
using wox.serial;
using System.Text.RegularExpressions;   

// Login Form 
public partial class LoginPage : Form
{
    #region Form Fields
    private Label lbl_Username;
    private Label lbl_Password;
    private Button btn_Login;
    private TextBox txt_Username;
    private Label lbl_ErrorMSGUsername;
    private Label lbl_ErrorMSGPassword;
    private TextBox txt_Password;
    #endregion

    #region Form Methods
    // Initialize form components
    private void InitializeComponent()
    {
            this.lbl_Username = new System.Windows.Forms.Label();
            this.lbl_Password = new System.Windows.Forms.Label();
            this.btn_Login = new System.Windows.Forms.Button();
            this.txt_Username = new System.Windows.Forms.TextBox();
            this.txt_Password = new System.Windows.Forms.TextBox();
            this.lbl_ErrorMSGUsername = new System.Windows.Forms.Label();
            this.lbl_ErrorMSGPassword = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbl_Username
            // 
            this.lbl_Username.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbl_Username.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lbl_Username.Location = new System.Drawing.Point(31, 41);
            this.lbl_Username.Name = "lbl_Username";
            this.lbl_Username.Size = new System.Drawing.Size(161, 26);
            this.lbl_Username.TabIndex = 0;
            this.lbl_Username.Text = "Username:";
            this.lbl_Username.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_Password
            // 
            this.lbl_Password.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F);
            this.lbl_Password.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lbl_Password.Location = new System.Drawing.Point(30, 90);
            this.lbl_Password.Name = "lbl_Password";
            this.lbl_Password.Size = new System.Drawing.Size(161, 27);
            this.lbl_Password.TabIndex = 1;
            this.lbl_Password.Text = "Password:";
            this.lbl_Password.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btn_Login
            // 
            this.btn_Login.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btn_Login.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.25F);
            this.btn_Login.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btn_Login.Location = new System.Drawing.Point(157, 144);
            this.btn_Login.Name = "btn_Login";
            this.btn_Login.Size = new System.Drawing.Size(182, 44);
            this.btn_Login.TabIndex = 2;
            this.btn_Login.Text = "Login";
            this.btn_Login.UseVisualStyleBackColor = false;
            this.btn_Login.Click += new System.EventHandler(this.btn_Login_Click);
            // 
            // txt_Username
            // 
            this.txt_Username.Location = new System.Drawing.Point(157, 47);
            this.txt_Username.Name = "txt_Username";
            this.txt_Username.Size = new System.Drawing.Size(183, 20);
            this.txt_Username.TabIndex = 3;
            // 
            // txt_Password
            // 
            this.txt_Password.Location = new System.Drawing.Point(157, 96);
            this.txt_Password.Name = "txt_Password";
            this.txt_Password.PasswordChar = '*';
            this.txt_Password.Size = new System.Drawing.Size(182, 20);
            this.txt_Password.TabIndex = 4;
            // 
            // lbl_ErrorMSGUsername
            // 
            this.lbl_ErrorMSGUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lbl_ErrorMSGUsername.ForeColor = System.Drawing.Color.Red;
            this.lbl_ErrorMSGUsername.Location = new System.Drawing.Point(158, 67);
            this.lbl_ErrorMSGUsername.Name = "lbl_ErrorMSGUsername";
            this.lbl_ErrorMSGUsername.Size = new System.Drawing.Size(181, 26);
            this.lbl_ErrorMSGUsername.TabIndex = 5;
            // 
            // lbl_ErrorMSGPassword
            // 
            this.lbl_ErrorMSGPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lbl_ErrorMSGPassword.ForeColor = System.Drawing.Color.Red;
            this.lbl_ErrorMSGPassword.Location = new System.Drawing.Point(157, 117);
            this.lbl_ErrorMSGPassword.Name = "lbl_ErrorMSGPassword";
            this.lbl_ErrorMSGPassword.Size = new System.Drawing.Size(182, 27);
            this.lbl_ErrorMSGPassword.TabIndex = 6;
            // 
            // LoginPage
            // 
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(370, 220);
            this.Controls.Add(this.lbl_ErrorMSGPassword);
            this.Controls.Add(this.lbl_ErrorMSGUsername);
            this.Controls.Add(this.txt_Password);
            this.Controls.Add(this.txt_Username);
            this.Controls.Add(this.btn_Login);
            this.Controls.Add(this.lbl_Password);
            this.Controls.Add(this.lbl_Username);
            this.Name = "LoginPage";
            this.ResumeLayout(false);
            this.PerformLayout();

    }
    #endregion

    #region Fields
    private NetworkStream output; // stream for receiving data           
    private BinaryWriter writer; // facilitates writing to the stream    
    private BinaryReader reader; // facilitates reading from the stream  
    private Thread readThread; // Thread for processing incoming messages
    private TcpClient client;
    #endregion

    #region Constructors
    // Default constructors
    public LoginPage()
    {
        InitializeComponent();
    }
    #endregion

    #region Methods
    // initialize thread for reading
    private void LoginPage_Load(object sender, EventArgs e)
    {
        readThread = new Thread(new ThreadStart(RunServer));
        readThread.Start();
    } // end method ChatClientForm_Load

    // When close form, exit app
    private void LoginPage_FormClosing(object sender, FormClosingEventArgs e)
    {
        System.Environment.Exit(System.Environment.ExitCode);
    } // end method LoginPage_FormClosing

    //Method to start server
    public void RunServer()
    {
        try
        {
            // create TcpClient and connect to server
            client = new TcpClient();
            client.Connect("127.0.0.1", 42422);

            // get NetworkStream associated with TcpClient
            output = client.GetStream();

            // create objects for writing and reading across stream
            writer = new BinaryWriter(output);
            reader = new BinaryReader(output);
        }
        catch (Exception error)
        {
            // handle exception if error in establishing connection
            MessageBox.Show(error.ToString(), "Connection Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            System.Environment.Exit(System.Environment.ExitCode);
        }
    }//end method RunServer
    
    // When click on login button
    private void btn_Login_Click(object sender, EventArgs e)
    {
        //Save in variables text values on fields
        string username = txt_Username.Text;
        string password = txt_Password.Text;
        
        //regex for validation text fields
        string regFormat = @"^[a-zA-Z0-9.]{5,20}$";
        Regex reg = new Regex(regFormat);
        
        Match matchUsername = reg.Match(username);
        Match matchPassword = reg.Match(password);

        //Is validate successfully username
        if (!matchUsername.Success)
        {
            lbl_ErrorMSGUsername.Text = "Your username is too short!";
            lbl_ErrorMSGPassword.Text = "";
        }
        //Is validate successfully password
        else if (!matchPassword.Success)
        {
            lbl_ErrorMSGPassword.Text = "Your password is too short!";
            lbl_ErrorMSGUsername.Text = "";
        }
        else
        {
            // clear error messages
            lbl_ErrorMSGPassword.Text = "";   
            lbl_ErrorMSGUsername.Text = "";
            Users users = (Users)Easy.load("users.xml");

            if (users.Username == username && users.Password == password)
            {
                var win = new BankCards();
                win.Show();
            }
            else
            {
                Users user = new Users(username, password);
                String filename = "users.xml";
                Easy.save(user, filename);
                var win = new BankCards();
                win.Show();
            }
        }
    } // end method btn_Login_Click
    #endregion
}

