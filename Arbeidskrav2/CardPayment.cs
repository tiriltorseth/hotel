namespace Arbeidskrav2;

public class CardPayment : IPayable
{
    private string cardNumber;
    private string cardType;

    /// <summary>
    /// Kort nummer lagret i property
    /// Sjekker at kortnummer er mer enn 4 tall langt
    /// </summary>
    public string CardNumber
    {
        get { return cardNumber; }
        private set
        {
            if (value == null || value.Length < 4)
               Console.WriteLine("Card Number must be at least 4 characters long!");
            cardNumber = value;
        }
    }
    
    
    /// <summary>
    /// Kort type lagret i property, der lengden blir sjekket
    /// </summary>
    public string CardType
    {
        get { return cardType; }
        private set
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length < 3)
                Console.WriteLine("Card Type must be at least 3 characters long!");
            
            if (value.Length > 30)
                Console.WriteLine("Card Type must be shorter than 30 characters!");
            
            cardType = value;
        }
    }

    /// <summary>
    /// Oppretter ny kort betaling som tar inn kortnummer og type
    /// </summary>
    public CardPayment(string cardNumber, string cardType)
    {
        this.CardNumber = cardNumber;
        this.CardType = cardType;
    }

    /// <summary>
    /// Simulering av betaling som alltid er true
    /// </summary>
    public bool ProcessPayment(decimal amount)
    {
        return true;
    }
    
    /// <summary>
    /// Skriver ut informasjon om betalingen
    /// </summary>
    public string GetPaymentInfo()
    {
        var showCardNumber = CardNumber.Substring(CardNumber.Length - 4, 4);
        return CardType + " **** " + showCardNumber;
    }
}