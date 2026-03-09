namespace Arbeidskrav2;

public class CardPayment : IPayable
{
    private string cardNumber;
    private string cardType;

    public string CardNumber
    {
        get { return cardNumber; }
        private set
        { cardNumber = value; }
    }

    public string CardType
    {
        get { return cardType; }
        private set { cardType = value; }
    }

    public CardPayment(string CardNumber, string CardType)
    {
        this.CardNumber = CardNumber;
        this.CardType = CardType;
    }

    public bool ProcessPayment(decimal Amount)
    {
        return true;
    }
    
    public string GetPaymentInfo()
    {
        var showCardNumber = cardNumber.Substring(cardNumber.Length - 4, 4);
        return CardType + " **** " + showCardNumber;
    }
}