using System.Text.RegularExpressions;

namespace Arbeidskrav2;

public class VippsPayment : IPayable
{
    private string phoneNumber;

    public string PhoneNumber
    {
        get { return phoneNumber; }
        private set
        {
            var pattern = @"^\d{2}\s\d{2}\s\d{2}\s\d{2}$";
            
            if (string.IsNullOrWhiteSpace(value) || !(Regex.IsMatch(value, pattern)))
                throw new ArgumentException("Invalid phone number. Please insert in this format ## ## ## ##");
            phoneNumber = value;
        }
    }

    public VippsPayment(string phoneNumber)
    {
        this.PhoneNumber = phoneNumber;
    }
    
    public bool ProcessPayment(decimal amount)
    {
        return true;
    }
    
    public string GetPaymentInfo()
    {
        return $"Vipps {PhoneNumber}";
    }
}