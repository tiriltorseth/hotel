namespace Arbeidskrav2;

public interface IPayable
{

    public bool ProcessPayment(decimal amount);

    public string GetPaymentInfo();
}

