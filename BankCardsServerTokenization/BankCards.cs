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


public partial class BankCards : Form
{
    private TextBox txt_inputBankCard;
    private TextBox txt_inputToken;
    private Label lbl_inputBankCard;
    private TextBox txt_TokenOutput;
    private TextBox txt_OutputBankCard;
    private Button btn_Exit;
    private Label lbl_inputToken;

    private void InitializeComponent()
    {
        this.txt_inputBankCard = new System.Windows.Forms.TextBox();
        this.txt_inputToken = new System.Windows.Forms.TextBox();
        this.lbl_inputBankCard = new System.Windows.Forms.Label();
        this.lbl_inputToken = new System.Windows.Forms.Label();
        this.txt_TokenOutput = new System.Windows.Forms.TextBox();
        this.txt_OutputBankCard = new System.Windows.Forms.TextBox();
        this.btn_Exit = new System.Windows.Forms.Button();
        this.SuspendLayout();
        // 
        // txt_inputBankCard
        // 
        this.txt_inputBankCard.Location = new System.Drawing.Point(50, 119);
        this.txt_inputBankCard.Name = "txt_inputBankCard";
        this.txt_inputBankCard.Size = new System.Drawing.Size(327, 20);
        this.txt_inputBankCard.TabIndex = 0;
        // 
        // txt_inputToken
        // 
        this.txt_inputToken.Location = new System.Drawing.Point(399, 119);
        this.txt_inputToken.Name = "txt_inputToken";
        this.txt_inputToken.Size = new System.Drawing.Size(329, 20);
        this.txt_inputToken.TabIndex = 1;
        // 
        // lbl_inputBankCard
        // 
        this.lbl_inputBankCard.Location = new System.Drawing.Point(50, 97);
        this.lbl_inputBankCard.Name = "lbl_inputBankCard";
        this.lbl_inputBankCard.Size = new System.Drawing.Size(326, 22);
        this.lbl_inputBankCard.TabIndex = 2;
        this.lbl_inputBankCard.Text = "Input bank card:";
        // 
        // lbl_inputToken
        // 
        this.lbl_inputToken.Location = new System.Drawing.Point(399, 95);
        this.lbl_inputToken.Name = "lbl_inputToken";
        this.lbl_inputToken.Size = new System.Drawing.Size(329, 21);
        this.lbl_inputToken.TabIndex = 3;
        this.lbl_inputToken.Text = "Input token:";
        // 
        // txt_TokenOutput
        // 
        this.txt_TokenOutput.Location = new System.Drawing.Point(50, 165);
        this.txt_TokenOutput.Multiline = true;
        this.txt_TokenOutput.Name = "txt_TokenOutput";
        this.txt_TokenOutput.ReadOnly = true;
        this.txt_TokenOutput.Size = new System.Drawing.Size(327, 89);
        this.txt_TokenOutput.TabIndex = 4;
        // 
        // txt_OutputBankCard
        // 
        this.txt_OutputBankCard.Location = new System.Drawing.Point(400, 166);
        this.txt_OutputBankCard.Multiline = true;
        this.txt_OutputBankCard.Name = "txt_OutputBankCard";
        this.txt_OutputBankCard.ReadOnly = true;
        this.txt_OutputBankCard.Size = new System.Drawing.Size(327, 88);
        this.txt_OutputBankCard.TabIndex = 5;
        // 
        // btn_Exit
        // 
        this.btn_Exit.Location = new System.Drawing.Point(306, 322);
        this.btn_Exit.Name = "btn_Exit";
        this.btn_Exit.Size = new System.Drawing.Size(166, 47);
        this.btn_Exit.TabIndex = 6;
        this.btn_Exit.Text = "Exit";
        this.btn_Exit.UseVisualStyleBackColor = true;
        this.btn_Exit.Click += new System.EventHandler(this.btn_Exit_Click);
        // 
        // BankCards
        // 
        this.ClientSize = new System.Drawing.Size(760, 509);
        this.Controls.Add(this.btn_Exit);
        this.Controls.Add(this.txt_OutputBankCard);
        this.Controls.Add(this.txt_TokenOutput);
        this.Controls.Add(this.lbl_inputToken);
        this.Controls.Add(this.lbl_inputBankCard);
        this.Controls.Add(this.txt_inputToken);
        this.Controls.Add(this.txt_inputBankCard);
        this.Name = "BankCards";
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    private NetworkStream output; // stream for receiving data           
    private BinaryWriter writer; // facilitates writing to the stream    
    private BinaryReader reader; // facilitates reading from the stream  
    private Thread readThread; // Thread for processing incoming messages
    private TcpClient client;
    //private string bankCard;
    //private string token;

    public BankCards()
    {
        InitializeComponent();
    }

    // initialize thread for reading
    private void BankCards_Load(object sender, EventArgs e)
    {
        readThread = new Thread(new ThreadStart(RunServer));
        readThread.Start();
    } // end method ChatClientForm_Load

    private void BankCards_FormClosing(object sender, FormClosingEventArgs e)
    {
        System.Environment.Exit(System.Environment.ExitCode);
    }

    private delegate void DisplayDelegate(string info);

    // method DisplayMessage sets displayTextBox's Text property
    // in a thread-safe manner
    private void DisplayBankCard(string bankCard)
    {
        // if modifying txt_OutputBankCard is not thread safe
        if (txt_OutputBankCard.InvokeRequired)
        {
            // use inherited method Invoke to execute DisplayBankCard
            // via a delegate                                       
            Invoke(new DisplayDelegate(DisplayBankCard),
                new object[] { bankCard });
        } // end if
        else
        {
            // OK to modify txt_OutputBankCard in current thread
            txt_OutputBankCard.Text += bankCard;
        }
    } // end method DisplayBankCard

    private void DisplayToken(string token)
    {
        // if modifying txt_TokenOutput is not thread safe
        if (txt_TokenOutput.InvokeRequired)
        {
            // use inherited method Invoke to execute DisplayToken
            // via a delegate                                       
            Invoke(new DisplayDelegate(DisplayBankCard),
                new object[] { token });
        } // end if
        else
        {
            // OK to modify txt_TokenOutput in current thread
            txt_TokenOutput.Text += token;
        }
    } // end method DisplayToken

    private void txt_inputBankCard_KeyDown(object sender, KeyEventArgs e)
    {
        //TODO when enter value, make all checks 
        try
        {
            if (e.KeyCode == Keys.Enter && txt_inputBankCard.ReadOnly == false)
            {
                writer.Write("CLIENT>>> " + txt_inputBankCard.Text);
                txt_TokenOutput.Text += "\r\nCLIENT>>> " + txt_TokenOutput.Text;
                txt_inputBankCard.Clear();
            } // end if
        } // end try
        catch (SocketException)
        {
            txt_TokenOutput.Text += "\nError writing object";
        } // end catch
    } // end method txt_inputBankCard_KeyDown

    private void txt_inputToken_KeyDown(object sender, KeyEventArgs e)
    {
        //TODO when enter value, make all checks 
        try
        {
            if (e.KeyCode == Keys.Enter && txt_inputToken.ReadOnly == false)
            {
                writer.Write("CLIENT>>> " + txt_inputBankCard.Text);
                txt_OutputBankCard.Text += "\r\nCLIENT>>> " + txt_OutputBankCard.Text;
                txt_inputToken.Clear();
            } // end if
        } // end try
        catch (SocketException)
        {
            txt_OutputBankCard.Text += "\nError writing object";
        } // end catch
    } // end method txt_inputToken_KeyDown


    public void RunServer()
    {
        try
        {
            // create TcpClient and connect to server
            client = new TcpClient();                        
            client.Connect( "127.0.0.1", 42422 );

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
    }

    private void btn_Exit_Click(object sender, EventArgs e)
    {
        writer.Close();
        reader.Close();
        output.Close();
        client.Close();

        Application.Exit();
    }

}