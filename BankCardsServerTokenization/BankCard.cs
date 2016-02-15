using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankCardsServerTokenization
{

    // BankCard class that represent bank card with properties Card and Token and 3 constructors
    public class BankCard
    {
        #region Fields
        private string card;
        private string token;
        #endregion

        #region Properties
        public string Card
        {
            get
            {
                return card;
            }
            set
            {
                if (value != null)
                {
                    card = value;
                }
            }
        }
        public string Token
        {
            get
            {
                return token;
            }
            set
            {
                if (value != null)
                {
                    token = value;
                }
            }
        }
        #endregion

        #region Constructors
        //constructor with two parameters
        public BankCard(string card, string token)
        {
            Card = card;
            Token = token;
        }

        //default constructor
        public BankCard():this("error", "error") { }
        
        //copy constructor
        public BankCard(BankCard bc) : this(bc.Card, bc.Token) { }
        #endregion
    }
}
