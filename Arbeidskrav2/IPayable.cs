using System.Data.SqlTypes;

namespace Arbeidskrav2;

public interface IPayable
{

    public bool ProcessPayment(decimal Amount)
    {
        return true;
    }

    public string GetPaymentInfo();
}

