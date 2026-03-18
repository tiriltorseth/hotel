using System.Text.RegularExpressions;

namespace Arbeidskrav2;

public class VippsPayment : IPayable
{
    private string phoneNumber;

    /// <summary>
    /// Telefonnummer lagret i property, sjekker at det er 8 tall med et visst mønster
    /// </summary>
    public string PhoneNumber
    {
        get { return phoneNumber; }
        private set
        {
            var pattern = @"^\d{2}\s\d{2}\s\d{2}\s\d{2}$";
            
            if (string.IsNullOrWhiteSpace(value) || !(Regex.IsMatch(value, pattern)))
                Console.WriteLine("Invalid phone number. Please insert in this format ## ## ## ##");
            phoneNumber = value;
        }
    }

    /// <summary>
    /// Oppretter ny vipps betaling som tar inn telefonnummer
    /// </summary>
    public VippsPayment(string phoneNumber)
    {
        this.PhoneNumber = phoneNumber;
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
        return $"Vipps {PhoneNumber}";
    }
}