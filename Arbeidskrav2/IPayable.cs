namespace Arbeidskrav2;

/// <summary>
/// Interface for simulering av betaling
/// </summary>
public interface IPayable
{

    /// <summary>
    /// Bool som tar inn amount og viser betalingsprosess
    /// </summary>
    public bool ProcessPayment(decimal amount);

    /// <summary>
    /// Metode som som henter betalingsinformasjon i en string
    /// </summary>
    public string GetPaymentInfo();
}

