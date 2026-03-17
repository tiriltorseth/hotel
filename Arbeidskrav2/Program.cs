using System.Runtime.InteropServices.JavaScript;
using System.Security.Cryptography.X509Certificates;
using System.Transactions;

namespace Arbeidskrav2;

class Program
{
    static void Main(string[] args)
    {
        // Creating hotel objekt
        var hotel = new Hotel();

        // Single Room
        hotel.RoomRegister.Add(new SingleRoom("101", "SingleRoom"));
        hotel.RoomRegister.Add(new SingleRoom("102", "SingleRoom"));
        hotel.RoomRegister.Add(new SingleRoom("103", "SingleRoom"));
        hotel.RoomRegister.Add(new SingleRoom("104", "SingleRoom"));
        hotel.RoomRegister.Add(new SingleRoom("105", "SingleRoom"));

        // Double Room
        hotel.RoomRegister.Add(new DoubleRoom("201", "DoubleRoom"));
        hotel.RoomRegister.Add(new DoubleRoom("202", "DoubleRoom"));
        hotel.RoomRegister.Add(new DoubleRoom("203", "DoubleRoom"));
        hotel.RoomRegister.Add(new DoubleRoom("204", "DoubleRoom"));
        hotel.RoomRegister.Add(new DoubleRoom("205", "DoubleRoom"));

        //Suite
        hotel.RoomRegister.Add(new Suite("301", "Suite"));
        hotel.RoomRegister.Add(new Suite("302", "Suite"));
        hotel.RoomRegister.Add(new Suite("303", "Suite"));

        // Regular guests
        hotel.GuestRegister.Add(new RegularGuest("Oda Romsaas", "oda@gmail.com"));
        hotel.GuestRegister.Add(new RegularGuest("Tiril Tørseth", "tiril@gmail.com"));
        hotel.GuestRegister.Add(new RegularGuest("CharlotteN Nordheim", "charlo@gmail.com"));
        hotel.GuestRegister.Add(new RegularGuest("Veronica Frelsøy", "vero@gmail.com"));
        hotel.GuestRegister.Add(new RegularGuest("Andrine Knain", "andrine@gmail.com"));

        //VIP Guests
        hotel.GuestRegister.Add(new VipGuest("Kong Harald", "kongen@gmail.com"));
        hotel.GuestRegister.Add(new VipGuest("Dronning Sonja", "dronningen@gmail.com"));
        hotel.GuestRegister.Add(new VipGuest("Prinsesse Ingrid Alexadra", "prinsessen@gmail.com"));
        hotel.GuestRegister.Add(new VipGuest("Prins Sverre Magnus", "prinsmagnus@gmail.com"));

        while (true)
        {
            Console.WriteLine("=== Hotel Gokstad ===");
            Console.WriteLine("Available operations:");
            Console.WriteLine(" 1. Show rooms");
            Console.WriteLine(" 2. Create booking");
            Console.WriteLine(" 3. Check in");
            Console.WriteLine(" 4. Check out");
            Console.WriteLine(" 5. Show my bookings");
            Console.WriteLine(" 6. Register new guest");
            Console.WriteLine(" 0. Exit");


            Console.Write("Choose option (1-6): ");
            int menuOptions = GetValidChoice(0, 6);


            int GetValidChoice(int min, int max)
            {
                while (true)
                {
                    if (int.TryParse(Console.ReadLine(), out int
                            choice) && choice >= min && choice <= max)
                    {
                        return choice;
                    }

                    Console.Write("Invalid choice. Try again: ");
                }
            }

            switch (menuOptions)
            {
                case 1:
                    ShowRooms(hotel);
                    break;
                case 2:
                    CreateBooking(hotel);
                    break;
                /*
                case 3:
                    CheckIn(hotel);
                    break;
                case 4:
                    CheckOut(hotel);
                    break;
                case 5:
                    ShowBookings(hotel);
                    break;
                case 6:
                    RegisterNewGuest(hotel);
                    break;
                    */
                case 0:
                    Console.WriteLine("\nProgram closed. Bye!");
                    return;
            }
        }
    }
    
    static void ShowRooms(Hotel hotel)
    {
        Console.Write("Enter check-in date (dd.mm.yyyy): ");
        string checkinDate = Console.ReadLine();

        Console.Write("Enter check-out date (dd.mm.yyyy): ");
        string checkoutDate = Console.ReadLine();

        DateTime checkIn = DateTime.Parse(checkinDate);
        DateTime checkOut = DateTime.Parse(checkoutDate);

        Console.WriteLine($"=== Available rooms between {checkinDate} and {checkoutDate} ===");
        hotel.GetAvailableRooms(checkIn, checkOut);
    }

    static void CreateBooking(Hotel hotel)
    {
        Console.Write("Enter GuestId: ");
        string guestId = Console.ReadLine();

        Console.Write($"Enter RoomId: ");
        string roomId = Console.ReadLine();
        
        Console.Write($"Enter check-in date (dd.mm.yyyy): ");
        string checkIn = Console.ReadLine();
        DateTime checkInDate = DateTime.Parse(checkIn);

        
        Console.Write($"Enter check-out date (dd.mm.yyyy): ");
        string checkOut = Console.ReadLine();
        DateTime checkOutDate = DateTime.Parse(checkOut);

        
        Console.Write("Payment method (1=Card Payment, 2=Vipps): ");
        string paymentMethod = Console.ReadLine();

        IPayable payment = null;
        
        if (paymentMethod == "1")
        {
            Console.Write($"Enter your card number: ");
            string cardNumber = Console.ReadLine();

            Console.Write($"Enter card type (Visa etc.): ");
            string cardType = Console.ReadLine();

            payment = new CardPayment(cardNumber, cardType);
        }
        else if (paymentMethod == "2")
        {
            Console.Write($"Enter your phone number (## ## ## ##): ");
            string phoneNumber = Console.ReadLine();

            payment = new VippsPayment(phoneNumber);
        }
        else
        {
            Console.WriteLine($"Invalid payment method. Try again.");
            return;
        }
        
        hotel.CreateBooking(guestId, roomId, checkInDate, checkOutDate, payment);
    }
        
        
    }

/*
    static void CheckIn(Hotel hotel)
    {
    }

    static void CheckOut(Hotel hotel)
    {
    }

    static void ShowBookings(Hotel hotel)
    {
    }

    static void RegisterNewGuest(Hotel hotel)
    {
    }
}*/
    



    