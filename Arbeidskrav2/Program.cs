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
        hotel.RoomRegister.Add(new SingleRoom("101","SingleRoom"));
        hotel.RoomRegister.Add(new SingleRoom("102","SingleRoom"));
        hotel.RoomRegister.Add(new SingleRoom("103","SingleRoom"));
        hotel.RoomRegister.Add(new SingleRoom("104","SingleRoom"));
        hotel.RoomRegister.Add(new SingleRoom("105","SingleRoom"));
        
        // Double Room
        hotel.RoomRegister.Add(new DoubleRoom("201","DoubleRoom"));
        hotel.RoomRegister.Add(new DoubleRoom("202","DoubleRoom"));
        hotel.RoomRegister.Add(new DoubleRoom("203","DoubleRoom"));
        hotel.RoomRegister.Add(new DoubleRoom("204","DoubleRoom"));
        hotel.RoomRegister.Add(new DoubleRoom("205","DoubleRoom"));
        
        //Suite
        hotel.RoomRegister.Add(new Suite("301","Suite"));
        hotel.RoomRegister.Add(new Suite("302","Suite"));
        hotel.RoomRegister.Add(new Suite("303","Suite"));
        
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


        //Program 
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
            { while (true)
                { if (int.TryParse(Console.ReadLine(), out int
                          choice) && choice >= min && choice <= max)
                    { return choice; }
                    Console.Write("Invalid choice. Try again: ");
                }
            }


            switch (menuOptions)
            {
                // Option 1: Show all available rooms
                case 1:
                    Console.Write("Enter check-in date (dd.mm.yyyy): ");
                    string checkinDate = Console.ReadLine();
                    
                    Console.Write("Enter check-out date (dd.mm.yyyy): ");
                    string checkoutDate = Console.ReadLine();
                    
                    DateTime checkIn = DateTime.Parse(checkinDate);
                    DateTime checkOut = DateTime.Parse(checkoutDate);
                    
                    Console.WriteLine($"=== Available rooms between {checkinDate} and {checkoutDate} ===");
                    hotel.GetAvailableRooms(checkIn, checkOut);
                    
                    break;
                
                //Option 2: Create booking
                case 2:
                    
                    Console.Write("Enter GuestId: ");
                    string guestId = Console.ReadLine();
                    
                    Console.WriteLine($"Enter RoomId: ");
                    string roomId = Console.ReadLine();

                    Console.WriteLine("Payment method (1=Card Payment, 2=Vipps: ");
                    string paymentMethod = Console.ReadLine();
                    
                    Console.WriteLine($"Card Number: ");
                    string cardNumber = Console.ReadLine();

                    break;
            }


        }
        
        
        
        
    }
}