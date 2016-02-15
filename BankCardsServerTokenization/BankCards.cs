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
using System.Collections;


// Bank Cards application for tokenization
public partial class BankCards : Form
{
    #region Form Fields
    private TextBox txt_inputBankCard;
    private TextBox txt_inputToken;
    private Label lbl_inputBankCard;
    private TextBox txt_TokenOutput;
    private TextBox txt_OutputBankCard;
    private Button btn_Exit;
    private Label lbl_ErrorMSGCard;
    private Label lbl_ErrorMSGToken;
    private Label lbl_inputToken;
    #endregion

    #region Form Methods
    //InitializeComponent method for initialize all components in this form
    private void InitializeComponent()
    {
            this.txt_inputBankCard = new System.Windows.Forms.TextBox();
            this.txt_inputToken = new System.Windows.Forms.TextBox();
            this.lbl_inputBankCard = new System.Windows.Forms.Label();
            this.lbl_inputToken = new System.Windows.Forms.Label();
            this.txt_TokenOutput = new System.Windows.Forms.TextBox();
            this.txt_OutputBankCard = new System.Windows.Forms.TextBox();
            this.btn_Exit = new System.Windows.Forms.Button();
            this.lbl_ErrorMSGCard = new System.Windows.Forms.Label();
            this.lbl_ErrorMSGToken = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txt_inputBankCard
            // 
            this.txt_inputBankCard.Location = new System.Drawing.Point(46, 71);
            this.txt_inputBankCard.MaxLength = 16;
            this.txt_inputBankCard.Name = "txt_inputBankCard";
            this.txt_inputBankCard.Size = new System.Drawing.Size(327, 20);
            this.txt_inputBankCard.TabIndex = 0;
            this.txt_inputBankCard.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_inputBankCard_KeyDown);
            // 
            // txt_inputToken
            // 
            this.txt_inputToken.Location = new System.Drawing.Point(400, 71);
            this.txt_inputToken.MaxLength = 16;
            this.txt_inputToken.Name = "txt_inputToken";
            this.txt_inputToken.Size = new System.Drawing.Size(329, 20);
            this.txt_inputToken.TabIndex = 1;
            this.txt_inputToken.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_inputToken_KeyDown);
            // 
            // lbl_inputBankCard
            // 
            this.lbl_inputBankCard.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.lbl_inputBankCard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lbl_inputBankCard.Location = new System.Drawing.Point(47, 46);
            this.lbl_inputBankCard.Name = "lbl_inputBankCard";
            this.lbl_inputBankCard.Size = new System.Drawing.Size(326, 22);
            this.lbl_inputBankCard.TabIndex = 2;
            this.lbl_inputBankCard.Text = "Input bank card:";
            // 
            // lbl_inputToken
            // 
            this.lbl_inputToken.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.lbl_inputToken.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lbl_inputToken.Location = new System.Drawing.Point(400, 47);
            this.lbl_inputToken.Name = "lbl_inputToken";
            this.lbl_inputToken.Size = new System.Drawing.Size(329, 21);
            this.lbl_inputToken.TabIndex = 3;
            this.lbl_inputToken.Text = "Input token:";
            // 
            // txt_TokenOutput
            // 
            this.txt_TokenOutput.Location = new System.Drawing.Point(46, 139);
            this.txt_TokenOutput.Multiline = true;
            this.txt_TokenOutput.Name = "txt_TokenOutput";
            this.txt_TokenOutput.ReadOnly = true;
            this.txt_TokenOutput.Size = new System.Drawing.Size(327, 89);
            this.txt_TokenOutput.TabIndex = 4;
            // 
            // txt_OutputBankCard
            // 
            this.txt_OutputBankCard.Location = new System.Drawing.Point(400, 140);
            this.txt_OutputBankCard.Multiline = true;
            this.txt_OutputBankCard.Name = "txt_OutputBankCard";
            this.txt_OutputBankCard.ReadOnly = true;
            this.txt_OutputBankCard.Size = new System.Drawing.Size(327, 88);
            this.txt_OutputBankCard.TabIndex = 5;
            // 
            // btn_Exit
            // 
            this.btn_Exit.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btn_Exit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btn_Exit.Location = new System.Drawing.Point(304, 268);
            this.btn_Exit.Name = "btn_Exit";
            this.btn_Exit.Size = new System.Drawing.Size(166, 25);
            this.btn_Exit.TabIndex = 6;
            this.btn_Exit.Text = "Exit";
            this.btn_Exit.UseVisualStyleBackColor = false;
            this.btn_Exit.Click += new System.EventHandler(this.btn_Exit_Click);
            // 
            // lbl_ErrorMSGCard
            // 
            this.lbl_ErrorMSGCard.ForeColor = System.Drawing.Color.Red;
            this.lbl_ErrorMSGCard.Location = new System.Drawing.Point(48, 95);
            this.lbl_ErrorMSGCard.Name = "lbl_ErrorMSGCard";
            this.lbl_ErrorMSGCard.Size = new System.Drawing.Size(325, 23);
            this.lbl_ErrorMSGCard.TabIndex = 7;
            this.lbl_ErrorMSGCard.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_ErrorMSGToken
            // 
            this.lbl_ErrorMSGToken.ForeColor = System.Drawing.Color.Red;
            this.lbl_ErrorMSGToken.Location = new System.Drawing.Point(400, 94);
            this.lbl_ErrorMSGToken.Name = "lbl_ErrorMSGToken";
            this.lbl_ErrorMSGToken.Size = new System.Drawing.Size(327, 24);
            this.lbl_ErrorMSGToken.TabIndex = 8;
            this.lbl_ErrorMSGToken.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BankCards
            // 
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(760, 343);
            this.Controls.Add(this.lbl_ErrorMSGToken);
            this.Controls.Add(this.lbl_ErrorMSGCard);
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
    #endregion

    #region Server Fields
    private NetworkStream output; // stream for receiving data           
    private BinaryWriter writer; // facilitates writing to the stream    
    private BinaryReader reader; // facilitates reading from the stream  
    private Thread readThread; // Thread for processing incoming messages
    private TcpClient client;   
    private static ArrayList bankCards;  // arraylist of bank cards , to save them in txt or xml files
    #endregion

    #region Constructors

    //default constructor
    public BankCards()
    {
        bankCards = new ArrayList();
        InitializeComponent();
        writeToFile();
    }
    #endregion

    #region Methods
    // initialize thread for reading
    private void BankCards_Load(object sender, EventArgs e)
    {
        readThread = new Thread(new ThreadStart(RunServer));
        readThread.Start();
    } // end method BankCards_Load

    //when form is closing - exit environment
    private void BankCards_FormClosing(object sender, FormClosingEventArgs e)
    {
        System.Environment.Exit(System.Environment.ExitCode);
    }

    //delegate for display string
    private delegate void DisplayDelegate(string info);

    // method DisplayBankCard sets DisplayBankCard's Text property
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
        } 
        else
        {
            // OK to modify txt_OutputBankCard in current thread
            txt_OutputBankCard.Text += bankCard;
        }
    } // end method DisplayBankCard

    // method DisplayToken sets DisplayToken's Text property
    // in a thread-safe manner
    private void DisplayToken(string token)
    {
        // if modifying txt_TokenOutput is not thread safe
        if (txt_TokenOutput.InvokeRequired)
        {
            // use inherited method Invoke to execute DisplayToken
            // via a delegate                                       
            Invoke(new DisplayDelegate(DisplayToken),
                new object[] { token });
        } 
        else
        {
            // OK to modify txt_TokenOutput in current thread
            txt_TokenOutput.Text += token;
        }
    } // end method DisplayToken

    //when is pressed a button in txt_inputBankCard invoke txt_inputBankCard_KeyDown() method
    private void txt_inputBankCard_KeyDown(object sender, KeyEventArgs e)
    {
        try
        {
            // check if pressed button is enter and txt_inputBankCard is not readonly
            if (e.KeyCode == Keys.Enter && txt_inputBankCard.ReadOnly == false)
            {  
                //check if bank card is valid
                if (ValidateBankCard(txt_inputBankCard.Text) == true)
                {
                    lbl_ErrorMSGCard.Text = "";         //clear error message 
                    txt_TokenOutput.Text = ReturnToken(txt_inputBankCard.Text);     // calculate token and print in txt_TokenOutput field

                    //add new bank card with token to arraylist and save in bankCards.xml
                    bankCards.Add(new BankCard(txt_inputBankCard.Text, txt_TokenOutput.Text));
                    String filename = "bankCards.xml";
                    Easy.save(bankCards, filename);
                    txt_inputBankCard.Clear();  //clear txt_inputBankCard field
                }
                else
                {       //if client enter wrong bank card, print this message in error field
                    lbl_ErrorMSGCard.Text = "Sorry, but you enter wrong bank card!";
                }
              
            } 
        } 
        catch (SocketException)
        {
            txt_TokenOutput.Text = "Error writing object";
        } 
    } // end method txt_inputBankCard_KeyDown

    //when is pressed a button in txt_inputToken invoke txt_inputToken_KeyDown() method
    private void txt_inputToken_KeyDown(object sender, KeyEventArgs e)
    {
        try
        {
            // check if pressed button is enter and txt_inputToken is not readonly
            if (e.KeyCode == Keys.Enter && txt_inputToken.ReadOnly == false)
            {
                bool isFind = false;       // bool variable for check if token is save in bankCards.xml
                ArrayList cards = (ArrayList)Easy.load("bankCards.xml");

                //traverse arraylist and search for card with this token if has print in txt_OutputBankCard field
                foreach (BankCard item in cards)
                {
                    if (item.Token == txt_inputToken.Text)
                    {
                        txt_OutputBankCard.Text = item.Card;
                        isFind = true;
                        break;
                    }
                }
                // if hasn't print error in error field
                if (!isFind)
                {
                    lbl_ErrorMSGToken.Text = "Sorry, but " + txt_inputToken.Text + " token is not save!";
                }
                
                txt_inputToken.Clear();     // clear txt_inputToken field
            } 
        } 
        catch (SocketException)
        {
            txt_OutputBankCard.Text = "Error writing object";
        } 
    } // end method txt_inputToken_KeyDown

    // start server
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

    // when click on exit button, shut down application
    private void btn_Exit_Click(object sender, EventArgs e)
    {
    //    writer.Close();
    //    reader.Close();
    //    output.Close();
    //    client.Close();

        Application.Exit();
    }

    // Method for validation bank card
    private bool ValidateBankCard(string card)
    {
        if (card.Length != 16 && ((int)Char.GetNumericValue(card[0]) < 3 && (int)Char.GetNumericValue(card[0]) > 6))
        {
            return false;
        }
        int sumOfDigits = 0;
        int checkCard = 0;
        //Luth algorithm
        for (int i = 0; i < card.Length; i++)
        {
            if (i % 2 == 0)
            {
                int digit = (int)Char.GetNumericValue(card[i]) * 2;
                if (digit > 9)
                {
                    sumOfDigits = digit % 10;
                    digit /= 10;
                    sumOfDigits += digit;

                    checkCard += sumOfDigits;
                    sumOfDigits = 0;
                }
                else
                {
                    checkCard += digit;
                }
            }
            else
            {
                checkCard += (int)Char.GetNumericValue(card[i]);
            }
        }
        if (checkCard % 10 != 0)
        {
            return false;
        }
        return true;
    } // end method ValidateBankCard

    // Method which calculate token by bank card
    private string ReturnToken(string card)
    {
        Random rnd = new Random();
        string token = "";
        int digit = 5;
        // first digit
        while(digit >= 3 && digit <= 6){
            digit = rnd.Next(0, 10);
        }
        token += digit;
        // other digit without last four
        for (int i = 1; i < card.Length - 4; i++)
        {
            digit = (int)Char.GetNumericValue(card[i]);
            while (digit == (int)Char.GetNumericValue(card[i]))
            {
                digit = rnd.Next(0, 10);
            }
            token += digit;
        }
        // last four digit
        token += card.Substring(12, 4);

        int sum = 0;
        for (int i = 0; i < token.Length; i++)
        {
            sum += (int)Char.GetNumericValue(token[i]);
        }
        // if token % 10 == 0, calculate again token while token % 10 != 0
        if (sum % 10 == 0)
        {
            ReturnToken(card);
        }
        return token;
    } // end method ReturnToken

    // Method which write information in file
    private void writeToFile()
    {
        ArrayList cards = (ArrayList)Easy.load("bankCards.xml");
        var sortedList = cards.Cast<BankCard>().OrderBy(item => item.Token); // sort arraylist by token

        sbyte counter = 0;
        //write to file sortedTokens.txt
        using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@"C:\Users\User\Desktop\CSharpProject\Project\BankCardsServerTokenization\BankCardsServerTokenization\bin\Debug\sortedTokens.txt"))
        {
            file.WriteLine("ID        Card            Token");
            
            foreach (BankCard card in sortedList)
            {
                file.WriteLine(counter++ + ": " + card.Card + " " + card.Token);
            }
        }

        sortedList = cards.Cast<BankCard>().OrderBy(item => item.Card); // sort arraylist by card
        counter = 0;
        //write to file sortedBankCards.txt
        using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@"C:\Users\User\Desktop\CSharpProject\Project\BankCardsServerTokenization\BankCardsServerTokenization\bin\Debug\sortedBankCards.txt"))
        {
            file.WriteLine("ID        Card            Token");

            foreach (BankCard card in sortedList)
            {
                file.WriteLine(counter++ + ": " + card.Card + " " + card.Token);
            }
        }
    }
    #endregion
}