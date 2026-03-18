using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;

namespace Arbeidskrav2;

class Program
{
    /// <summary>
    /// Main metode som kjører hotellet
    /// </summary>
    static void Main(string[] args)
    {
        // Lager hotel objekt
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
        hotel.GuestRegister.Add(new VipGuest("Mia Mor", "miagrande@meny.no"));


        //Bookings
        hotel.CreateBooking("G010",
            "302",
            new DateTime(2026, 04, 04),
            new DateTime(2026, 04, 09),
            new VippsPayment("55 66 44 66"));

        hotel.CreateBooking("G002",
            "104",
            new DateTime(2026, 06, 08),
            new DateTime(2026, 06, 012),
            new VippsPayment("33 55 66 77"));


        // Tests
        RunTest1(hotel);
        RunTest2(hotel);
        RunTest3();
        RunTest4();
        BonusTest1();

        while (true)
        {
            Console.WriteLine("\n=== Hotel Gokstad ===");
            Console.WriteLine("Available operations:");
            Console.WriteLine(" 1. Show rooms");
            Console.WriteLine(" 2. Create booking");
            Console.WriteLine(" 3. Check in");
            Console.WriteLine(" 4. Check out");
            Console.WriteLine(" 5. Show my bookings");
            Console.WriteLine(" 6. Register new guest");
            Console.WriteLine(" 7. Cancel booking");
            Console.WriteLine(" 0. Exit");


            Console.Write("Choose option (1-7): ");
            int menuOptions = GetValidChoice(0, 7);


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
                case 7:
                    CancelBooking(hotel);
                    break;
                case 0:
                    Console.WriteLine("\nProgram closed. Bye!");
                    return;
            }
        }
    }

    /// <summary>
    /// Test som sørger for at programmet returnerer korrekt pris med rabatt
    /// </summary>
    static void RunTest1(Hotel hotel)
    {
        Console.WriteLine("=========== TESTER ===========");
        var vip = new VipGuest("Test", "test@test.com");
        decimal result = vip.GetDiscount(1000);

        if (result == 850)
        {
            Console.WriteLine("Test 1 PASSED");
        }
        else
        {
            Console.WriteLine($"Test 1 FAILED, got {result}");
        }
    }

    /// <summary>
    /// Test som beregner riktig pris for romtype
    /// </summary>
    static void RunTest2(Hotel hotel)
    {
        var guest = new RegularGuest("Test", "test@test.com");
        var room = new SingleRoom("000", "SingleRoom");

        var booking = new Booking(
            room,
            guest,
            new DateTime(2026, 01, 01),
            new DateTime(2026, 01, 08),
            new VippsPayment("12 23 34 45"));

        decimal result = booking.CalculateTotalPrice();

        if (result == 5600)
        {
            Console.WriteLine("\nTest 2 PASSED");
        }
        else
        {
            Console.WriteLine($"\nTest 2 FAILED, got {result}");
        }
    }


    /// <summary>
    /// Test som gir feilmld dersom rommet er opptatt
    /// </summary>
    static void RunTest3()
    {
        var testHotel = new Hotel();

        var room = new SingleRoom("101", "SingleRoom");
        var guest = new RegularGuest("Test", "test@test.com");
        var paymemt = new VippsPayment("12 23 34 45");

        testHotel.RoomRegister.Add(room);
        testHotel.GuestRegister.Add(guest);

        var booking1 = testHotel.CreateBooking(
            guest.GuestID,
            room.RoomID,
            new DateTime(2026, 01, 01),
            new DateTime(2026, 01, 08),
            paymemt);

        var booking2 = testHotel.CreateBooking(
            guest.GuestID,
            room.RoomID,
            new DateTime(2026, 01, 01),
            new DateTime(2026, 01, 08),
            paymemt);


        if (booking2 == null)
        {
            Console.WriteLine("\nTest 3 PASSED");
        }
        else
        {
            Console.WriteLine("\nTest 3 FAILED");
        }
    }

    /// <summary>
    /// Test som setter sjekker at rommet er ledig etter checkout
    /// </summary>
    static void RunTest4()
    {
        var testHotel2 = new Hotel();

        var room = new SingleRoom("101", "SingleRoom");
        var guest = new RegularGuest("Test", "test@test.com");
        var paymemt = new VippsPayment("12 23 34 45");

        testHotel2.GuestRegister.Add(guest);
        testHotel2.RoomRegister.Add(room);

        var booking1 = testHotel2.CreateBooking(
            guest.GuestID,
            room.RoomID,
            new DateTime(2026, 01, 01),
            new DateTime(2026, 01, 08),
            paymemt);

        booking1.CheckIn();

        booking1.CheckOut();

        if (room.IsAvailable)
        {
            Console.WriteLine($"\nTest 4 PASSED");
        }
        else
            Console.WriteLine($"\nTest 4 FAILED, got {room.IsAvailable}");

    }


    /// <summary>
    /// Tester at regular guest ikke kan ha mer enn 3 bookinger
    /// </summary>
    static void BonusTest1()
    {
        var testHotel3 = new Hotel();

        var room = new SingleRoom("101", "SingleRoom");
        var guest = new RegularGuest("Test", "test@test.com");
        var paymemt = new VippsPayment("12 23 34 45");

        testHotel3.GuestRegister.Add(guest);
        testHotel3.RoomRegister.Add(room);

        var booking1 = testHotel3.CreateBooking(
            guest.GuestID,
            room.RoomID,
            new DateTime(2026, 01, 01),
            new DateTime(2026, 01, 08),
            paymemt);

        var booking2 = testHotel3.CreateBooking(
            guest.GuestID,
            room.RoomID,
            new DateTime(2026, 02, 01),
            new DateTime(2026, 02, 08),
            paymemt);

        var booking3 = testHotel3.CreateBooking(
            guest.GuestID,
            room.RoomID,
            new DateTime(2026, 03, 01),
            new DateTime(2026, 03, 08),
            paymemt);

        var booking4 = testHotel3.CreateBooking(
            guest.GuestID,
            room.RoomID,
            new DateTime(2026, 04, 01),
            new DateTime(2026, 04, 08),
            paymemt);

        if (!guest.CanBook())
        {
            Console.WriteLine("\nBonus test PASSED");
        }
        else
        {
            Console.WriteLine($"\nBonus test FAILED");
        }

        Console.WriteLine("==============================");
    }


    /// <summary>
    /// Metode som viser alle tilgjengelige rom innenfor et intervall
    /// </summary>

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


    /// <summary>
    /// Booking blir opprettet og verdeiene sjekkes at er skrevet inn korrekt
    /// Skriver ut booking til slutt
    /// </summary>
    static void CreateBooking(Hotel hotel)
    {
        Console.Write("Enter GuestId: ");
        string guestId = Console.ReadLine();

        if (guestId == null)
        {
            Console.WriteLine("GuestId is null");
            return;

        }

        Console.Write($"Enter RoomId: ");
        string roomId = Console.ReadLine();
        
        if (roomId == null)
        {
            Console.WriteLine("RoomId is null");
            return;
        }

        Console.Write($"Enter check-in date (dd.mm.yyyy): ");
        string checkIn = Console.ReadLine();
        DateTime checkInDate = DateTime.Parse(checkIn);
        
        if (checkIn == null)
        {
            Console.WriteLine("Check in date is null");
            return;
        }

        Console.Write($"Enter check-out date (dd.mm.yyyy): ");
        string checkOut = Console.ReadLine();
        DateTime checkOutDate = DateTime.Parse(checkOut);
        
        if (checkOut == null)
        {
            Console.WriteLine("Check in date is null");
            return;
        }

        Console.Write("Payment method (1:Card Payment, 2:Vipps): ");
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

        if (checkOutDate <= checkInDate)
        {
            Console.WriteLine("\nCheckout date can not before checkin date. Please try again.");
            return;
        }

        var booking = hotel.CreateBooking(guestId, roomId, checkInDate, checkOutDate, payment);

        Guest guest = hotel.GuestRegister
            .FirstOrDefault(g => g.GuestID == guestId);

        if (guest == null)
        {
            Console.WriteLine("Guest not found");
        }

        Room room = hotel.RoomRegister.FirstOrDefault(r => r.RoomID == roomId);

        if (room == null)
        {
            Console.WriteLine("Room not found");
        }

        if (guest is RegularGuest regularGuest)
        {
            Console.WriteLine($"\n------------------------------------------------------------" +
                              $"\nBooking {booking.BookingID} created for {guest.Name}" +
                              $"\n{room.RoomType} {room.RoomID} -- {booking.CheckInDate:dd.mm.yyyy} to {booking.CheckOutDate:dd.mm.yyyy}" +
                              $"\nTotal Price: {booking.CalculateTotalPrice()} kr" +
                              $"\nPayment approved with {payment.GetPaymentInfo()}" +
                              $"\n------------------------------------------------------------");

        }
        else if (guest is VipGuest vipGuest)
            Console.WriteLine($"\n------------------------------------------------------------" +
                              $"\nBooking {booking.BookingID} created for {guest.Name}" +
                              $"\n{room.RoomType} {room.RoomID} -- {booking.CheckInDate:dd.mm.yyyy} to {booking.CheckOutDate:dd.mm.yyyy}" +
                              $"\nTotal Price: {booking.CalculateTotalPrice()} kr" +
                              $"\nPayment approved with {payment.GetPaymentInfo()}" +
                              $"\nLoyalty Points [{vipGuest.LoyaltyPoints}]" +
                              $"\n------------------------------------------------------------");
    }


    /// <summary>
    /// Sjekker inn booking
    /// </summary>
    static void CheckIn(Hotel hotel)
    {
        Console.Write("Welcome! Please enter your BookingID (BK###): ");
        string inputBookingId = Console.ReadLine();

        var booking = hotel.BookingHistoryRegister
            .FirstOrDefault(b => b.BookingID == inputBookingId);

        if (booking == null)
        {
            Console.WriteLine("Booking not found");
            return;
        }

        booking.CheckIn();
        
    }

    /// <summary>
    /// Sjekker ut booking
    /// </summary>

    static void CheckOut(Hotel hotel)
    {
            
            Console.Write("Please enter your BookingID (BK###): ");
            string inputBookingId = Console.ReadLine();

            var booking = hotel.BookingHistoryRegister
                .FirstOrDefault(b => b.BookingID == inputBookingId);

            if (booking == null)
            {
                Console.WriteLine("Booking not found");
                return;
            }

            booking.CheckOut();
    }


    /// <summary>
    /// Viser alle bookings for en spesifikk gjest
    /// </summary>
    static void ShowBookings(Hotel hotel)
    {
            Console.Write("Please enter your GuestId: ");
            string inputGuestId = Console.ReadLine();

            hotel.GetGuestBookings(inputGuestId);
    }


    /// <summary>
    /// Registrerer en ny gjest
    /// </summary>
    static void RegisterNewGuest(Hotel hotel)
    {
        Console.Write("Name: ");
        string name = Console.ReadLine();

        if (name == null || (!(Regex.IsMatch(name, @"^[a-zA-Z]+$"))))
        {
            Console.WriteLine("Invalid name");
                return;
        }

        Console.Write("Email: ");
        string email = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(email))
        {
            Console.WriteLine("Email can not be empty.");
            return;
        }
        
        string checkEmail = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

        if (!(Regex.IsMatch(email, checkEmail)))
        {
            Console.WriteLine("Invalid email!");
            return;
        }

        Console.Write("Do your wish to be VIP guest and earn loyalty points? (Yes/No): ");
        string yesOrNo = Console.ReadLine();

        if (yesOrNo == "Yes" || yesOrNo == "yes")
        {
            var guest = new VipGuest(name, email);
            hotel.RegisterGuest(guest);

        }
        else if (yesOrNo == "No" || yesOrNo == "no")
        {
            var guest = new RegularGuest(name, email);
            hotel.RegisterGuest(guest);
        }
        else
        {
            Console.WriteLine($"Invalid input. Try again.");
        }
    }

    /// <summary>
    /// Lar gjesten kansellere bookings
    /// </summary>
    static void CancelBooking(Hotel hotel)
    {
        Console.Write("Please enter your BookingID (BK###): ");
        string inputBookingId = Console.ReadLine();
        
        var booking = hotel.BookingHistoryRegister
            .FirstOrDefault(b => b.BookingID == inputBookingId);

        if (booking == null)
        {
            Console.WriteLine("Booking not found");
            return;
        }

        hotel.CancelBooking(booking.BookingID);

    }
}   








    