# Bruk av AI i Arbeidskrav 2:


***I dette arbeidskravet har jeg brukt AI mindre til forståelse enn arbeidskrav 1 og mer til debugging. Synes personlig fristen
på kravene er litt for korte og har ikke fysisk tid til å mauelt debugge alt sammen selv, da jeg fortsatt synes dette er vanskelig
og er ny innenfor koding generelt.***

***Noen friheter har jeg måttet ta meg som ikke inngår i funksjonelle krav, da jeg opplevde at selve bookingen av et rom/IsAvailable var litt
klundrete laget. Derfor tipset AI meg og viste meg at bool Isbooked kan være nyttig til å hjelpe å forstå ltt mer, den er nok
den største hjelpen jeg har fått/mest betydlige. Har også kopiert funksjonaliteten til Arbeidskrav 1, der KanLåne ble brukt om ansatt og medlem.
Dette er lagt in som CanBook for å gjøre sjekken til gjestene lettest mulig.***

**Prompt:**
Hva er galt med denne? d står at den skal være abstrakt public abstract decimal GetDiscount(decimal basePrice) { return basePrice; }

**Svar:**
En abstract metode kan ikke ha implementasjon.

Du har skrevet:

        public abstract decimal GetDiscount(decimal basePrice)
        {
        return basePrice;
        }

Her gjør du to ting samtidig som ikke går:

abstract → betyr ingen kode

        { return basePrice; } → er kode

C# tillater ikke begge.

Riktig måte

En abstrakt metode skal bare deklareres, ikke implementeres.

Så i Guest skal det være:

public abstract decimal GetDiscount(decimal basePrice);

Legg merke til:

ingen {}

ingen return

Hvorfor?

Fordi subklassene må implementere metoden selv.

Du får da:

RegularGuest

        public override decimal GetDiscount(decimal basePrice)
        {
        return basePrice;
        }

Ingen rabatt.

**Prompt:** Hva er m i 0.85?

**Svar:** m betyr at tallet er en decimal literal i C#.

I C# har tall uten noe suffix som standard typen double.

Så:

0.85

er egentlig:

    double

Men metoden din bruker:

    decimal basePrice

Da må også tallet du ganger med være decimal, ellers får du type-konflikt.

Derfor skriver man
    
    0.85m

m betyr:

decimal
Eksempel

Dette gir feil:

    decimal price = basePrice * 0.85;

Feil fordi:

    decimal * double

ikke er lov.

Dette fungerer:

    decimal price = basePrice * 0.85m;

fordi det blir:

    decimal * decimal

**Prompt:** Feilsøk:         
    
    var showCardNumber = cardNumber.Substring(Length - 4, 4);

**Svar:**
Problemet er denne delen:

Length - 4

Length alene finnes ikke her. Length må tilhøre en string. Du må derfor bruke cardNumber.Length.

Riktig kode

    var showCardNumber = cardNumber.Substring(cardNumber.Length - 4, 4);

**Prompt:** I en feilsøk av VippsPayment: 

**Svar:**
GetPaymentInfo() bør ikke skrive til konsollen

Metoden i interfacet skal returnere en beskrivelse, ikke skrive den ut.
Oppgaven sier at den skal returnere info om betalingsmetoden.

Arbeidskrav2_HotellSystem

Du har nå:

    Console.WriteLine($"Customer paid with Vipps: {PhoneNumber}");
    return PhoneNumber;

Dette blander to ting:

logging (Console.WriteLine)

informasjon (return)

Bedre:

    public string GetPaymentInfo()
    {
    return $"Vipps {PhoneNumber}";
    }

Hvis du vil skrive til konsollen kan det gjøres et annet sted i programmet.

**Prompt:** I samme feilsøk av VippsPayment:

**Svar:**
Liten forbedring (unngå null-crash)

Hvis value er null kan Regex.IsMatch krasje. En trygg variant er:

    if (value == null || !Regex.IsMatch(value, pattern))
    throw new ArgumentException("Invalid phone number. Please insert in this format ## ## ## ##");


**Prompt:** Sitter litt fast med CalculatePrice() metoden, får den liksom ikke helt riktig, kan du skrive pseudokode uten kode, men med logikk så jeg får litt tips?


**Svar:**

Her er pseudokoden for logikken, uten C#-syntaks:

METHOD CalculateTotalPrice

    IF CheckOutDate <= CheckInDate
        THROW error "Invalid dates"

    nights = difference between CheckOutDate and CheckInDate (in days)

    basePrice = nights * Room.PricePerNight

    finalPrice = Guest.GetDiscount(basePrice)

    RETURN finalPrice

END METHOD

**Prompt:**
kan du feilsøke hele booking klassen min for finner ikke ut av feilen med is paid selvom det er nok der feilen er, tror jeg har mange følgefeil. 


    namespace Arbeidskrav2;
    
    public class Booking
    {
    private static int bookingCounter = 0;
    private readonly string bookingID;

    public Room room { get; }
    public Guest guest { get; }

    private DateTime checkInDate;
    private DateTime checkOutDate;

    private IPayable payable;

    public DateTime CheckInDate
    {
        get { return checkInDate; }
        protected set
        {
            
            checkInDate = value;
        }
    }

    public DateTime CheckOutDate
    {
        get { return checkOutDate; }
        protected set
        {
            if (checkOutDate <= checkInDate)
                throw new ArgumentException("Checkout date cannot be before checkin date!");
            
            checkOutDate = value;
        }
    }

    public Booking(Room room, Guest guest, DateTime checkIn, DateTime checkOut)
    {
        bookingCounter++;
        bookingID = "BK" + bookingCounter.ToString("D3");
        this.room = room;
        this.guest = guest;
        CheckInDate = checkInDate;
        CheckOutDate = checkOutDate;
    }

    public bool IsPaid()
    {
        if (!(payable == null))
        {
            Console.WriteLine($"Hotel has been paid!");
            return true;
        }

        return false;
    }

    public decimal CalculateTotalPrice()
    {
        if (checkOutDate <= checkInDate)
            throw new ArgumentException("Checkout date is before checkin date!");
        
        TimeSpan spentNights = checkOutDate.Subtract(checkInDate);

        decimal basePrice = spentNights.Days * room.PricePerNight;

        decimal finalPrice = guest.GetDiscount(basePrice);

        return finalPrice;
    }

    public void CheckIn()
    {
        if (!IsPaid())
        {
            throw new ArgumentException($"{bookingID} has not been paid. Please pay before checking in.");
        }

        if (DateTime.Today < checkInDate)
        {
            throw new ArgumentException("Cannot check in before the checkin date");
        }
        
        if (room.IsAvailable is false)
        {
            throw new InvalidOperationException($"Room {room.RoomID} is not available. Please choose another room.");
        }
        
        room.IsAvailable = false;
        Console.WriteLine($"[{bookingID}] has been checked in!");
        
    }

    public void CheckOut()
    {
        if (!room.IsAvailable)
        {
            Console.WriteLine($"This room is already available!");
        }
        
        room.IsAvailable = true;
        Console.WriteLine($"Booking [{bookingID}] has been checked out!");
    }

}

**Svar:**

Ja — her er flere konkrete feil i klassen din. Noen av dem gjør at IsPaid aldri kan fungere riktig, og noen lager følgefeil andre steder. Jeg går gjennom dem i rekkefølge så du kan fikse dem systematisk.

1. Største bug: constructor setter aldri datoene

I konstruktøren gjør du dette:

    CheckInDate = checkInDate;
    CheckOutDate = checkOutDate;

Men checkInDate og checkOutDate her er feltene dine, ikke parameterne.

Parameterne heter:

    DateTime checkIn
    DateTime checkOut

Så akkurat nå gjør koden egentlig dette:

    checkInDate = checkInDate

Altså: ingen verdi settes.

Riktig logikk:

    CheckInDate = checkIn
    CheckOutDate = checkOut

Dette er en klassisk bug i C# når felter og parametre har nesten samme navn.

2. Feil validering i CheckOutDate

Her:

    if (checkOutDate <= checkInDate)

Problemet er at du sjekker feltet, ikke den nye verdien (value).

Når en property settes må du validere value.

Riktig logikk:

    if (value <= checkInDate)
3. Betalingen din blir aldri satt

Du har:

private IPayable payable;

Men du setter den aldri.

Dermed vil denne alltid være null.

Derfor vil:

    IsPaid()

alltid returnere false.

I oppgaven står det også at booking skal ha:

    PaymentMethod (IPayable)
    IsPaid (bool)

Så egentlig skal flyten være:

Booking opprettes

PaymentMethod settes

ProcessPayment() kjøres

    IsPaid = true

Akkurat nå mangler hele dette steget.

4. IsPaid()-metoden er feil design

Du gjør:

    if (!(payable == null))

Dette betyr egentlig bare:

hvis betalingsmetode finnes

Men det betyr ikke at betaling er gjennomført.

Oppgaven sier også eksplisitt:

IsPaid (bool) – settes etter vellykket betaling

Arbeidskrav2_HotellSystem

Så IsPaid burde være en bool property, ikke en metode.

Eksempel på riktig logikk:

bool isPaid

og når betaling skjer:

    isPaid = payment.ProcessPayment(amount)
5. Bug i CheckOut()

Her:

    if (!room.IsAvailable)
    {
    Console.WriteLine($"This room is already available!");
    }

Dette er bakvendt.

!room.IsAvailable betyr:

rommet er opptatt

Men du skriver:

rommet er allerede ledig

Så logikken er motsatt.

6. CheckOut burde også sjekke dato

Akkurat nå kan man checke ut når som helst.

Bedre logikk:

    if (DateTime.Today < checkOutDate)
    throw error
7. Unødvendig using

Denne kan fjernes:

    using System.Reflection.Metadata.Ecma335;

Den brukes ikke.

8. CalculateTotalPrice er egentlig ganske bra

Denne delen er faktisk riktig:

netter * pris
→ send til Guest.GetDiscount()
→ returner

Det er helt i tråd med oppgaven.

9. Designproblem: CheckIn burde også hindre dobbel innsjekk

Akkurat nå kan man checke inn flere ganger.

Du burde også sjekke:

    if (!room.IsAvailable)
    throw

før du setter:

    room.IsAvailable = false

10. PaymentMethod lagt inn riktig 

        private void ProcessPayment()
        {
        decimal amount = CalculateTotalPrice();

        if (paymentMethod.ProcessPayment(amount))
        {
            IsPaid = true;
            Console.WriteLine($"Payment successful using {paymentMethod.GetPaymentInfo()}");
        }
        else
        {
            throw new Exception("Payment failed!");
        }
    }

**Prompt:** Kan du hjelpe meg med selve betalingsdelen i CreateBooking? Dette er det jeg har hittil og jeg har ikke peiling
på neste steg: decimal PriceForStay = Booking.CalculateTotalPrice();,


**Svar:**Logikken i kodeform (den lille delen du mangler)

Dette er vanlig måte å gjøre det på:
    
    decimal priceForStay = booking.CalculateTotalPrice();
    
    bool paymentSuccess = payment.ProcessPayment(priceForStay);
    
    if (!paymentSuccess)
    {
    throw new Exception("Payment failed");
    }

**Prompt:**

hvorfor er det feil o guest her public Booking? CancelBooking(string bookingID)

    {
        var guest = GuestRegister.FirstOrDefault(g => g.GuestID == guestID);

        var booking = BookingHistoryRegister.FirstOrDefault(b => b.BookingID == bookingID);

        if (booking == null)
        {
            Console.WriteLine($"Booking [{bookingID}] does not exist.");
            return null;
        }
        
        guest.ActiveBookings.Remove(booking);
        BookingHistoryRegister.Remove(booking);
        
        Console.WriteLine($"Booking [{bookingID}] has been cancelled.");
        return booking;
        
    }

**Svar:**

Feilen med guest her skyldes at du bruker en variabel som ikke finnes i metoden.

Se på denne linjen:

    var guest = GuestRegister.FirstOrDefault(g => g.GuestID == guestID);

Problemet er guestID.

I metodesignaturen din har du bare:

    CancelBooking(string bookingID)

Det betyr at metoden ikke får noe guestID som parameter. Derfor vet ikke kompilatoren hva guestID er.

Hvorfor du egentlig ikke trenger å finne gjesten her

Du har allerede funnet:

    var booking = BookingHistoryRegister.FirstOrDefault(b => b.BookingID == bookingID);

Og i Booking har du en referanse til gjesten:

booking.Guest

Booking-klassen inneholder jo:

rom

gjest

datoer

Så du kan hente gjesten direkte fra bookingen.

**Prompt:** Hvordan kan jeg fp denne inn i en console writeline så jeg skriver ut yes istedenfor true?

    if (HasDesk)
    {
    Console.WriteLine("HasDesk: Yes");
    }
    else
    {
    Console.WriteLine("HasDesk: No");
    }

**Svar:**

    Console.WriteLine($"HasDesk: {(HasDesk ? "Yes" : "No")}");


**Prompt:** I en feilsøk av Guest: Feil logikk i email-validering

if (!(Regex.IsMatch(value, checkEmail) || value.Contains(".")))

Problemer:

value.Contains(".") gjør at nesten alle strenger blir godkjent

regexen blir dermed nesten irrelevant

Feil regex

Regexen din:

@[a-zA-Z0-9.-]+.[a-zA-Z]{2,}

. betyr hvilket som helst tegn i regex.

Du burde hatt:

\.

ellers matcher den feil ting.



**Svar:** 

Riktig regex blir: 

    string checkEmail = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

Fiks logikken i valideringen

Du har nå:

    if (!(Regex.IsMatch(value, checkEmail) || value.Contains(".")))

Problemet:

    || value.Contains(".") gjør at nesten alt blir godkjent.

Du trenger kun regexen.

Riktig løsning:

    if (!Regex.IsMatch(value, checkEmail))
    {
    throw new ArgumentException("Invalid email format.");
    }



**Prompt:** 

Kan du forklare og vise meg hvorfor denne ikke funker? LoyaltyPoints er ikke accesible?

    if (guest is VipGuest)
    {
    guest.LoyaltyPoints = +10;
    }



**Svar:**

LoyaltyPoints blir rød fordi variabelen guest er typen Guest, ikke VipGuest.

Selv om du sjekker:

    if (guest is VipGuest)

vet kompilatoren fortsatt bare at guest er en Guest, og Guest-klassen har ikke propertyen LoyaltyPoints.

Derfor blir dette ulovlig:

    guest.LoyaltyPoints
Hva du må gjøre

Du må gjøre om guest til VipGuest først etter at du har sjekket typen.

Logikken er:

sjekk om guest er VipGuest

lag en VipGuest-variabel av den

øk poengene på den

Den korteste og reneste C#-måten å gjøre dette på er å bruke pattern matching i if.

Da gjør du to ting samtidig:

sjekker om gjesten er VipGuest

lager en VipGuest-variabel du kan bruke

Logikken blir:

hvis guest er en VipGuest

lag en variabel som representerer den VIP-gjesten

øk poengene

Eksempel:

    if (guest is VipGuest vipGuest)
    {
    vipGuest.LoyaltyPoints += 10;
    }
Hva som skjer her

Denne linjen:

    guest is VipGuest vipGuest

betyr:

sjekk om guest er en VipGuest

hvis ja → lag variabelen vipGuest

vipGuest er nå typen VipGuest

Da kan du bruke:

    vipGuest.LoyaltyPoints

uten feil.

**Prompt:** I feilsøk av Hotel: oeki feilsøk using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;

namespace Arbeidskrav2;

public class Hotel
{
private string hotelName;

    public List<Room> RoomRegister = new List<Room>();
    public List<Guest> GuestRegister = new List<Guest>();
    public List<Booking> BookingHistoryRegister = new List<Booking>();

    /// <summary>
    /// Hotell navn, sjekker at det er mer enn 3 karakterer og ikke null
    /// </summary>
    public string HotelName
    {
        get { return hotelName; }
        private set
        {
            if (value.Length < 3 )
                throw new ArgumentException("Hotel name must be at least 3 characters long.");
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Hotelname cannot be empty.");
            hotelName = value;
        }
    }

    /// <summary>
    /// Registrerer ny gjest
    /// </summary>
    public Guest RegisterGuest(Guest guest)
    {
        if (GuestRegister.Any(g => g.Email == guest.Email))
        {
            return null;
        }
        
        GuestRegister.Add(guest);
        return guest;
    }

    
    // I denne må det legges til checkin og checkout så kun tilgjengelige rom vises innenfor en viss dato
    public void GetAvailableRooms(DateTime checkIn, DateTime checkOut)
    {
        foreach (var room in RoomRegister)
        {
            if (room.IsAvailable)
            {
                continue;
            }
            else
            {
                room.DisplayRoomInfo();
            }
        }
    }

    public Booking? CreateBooking(string guestID, string roomID, DateTime checkIn,
        DateTime checkOut, IPayable payable)
    {
        var guest = GuestRegister.FirstOrDefault(g => g.GuestID == guestID);

        if (guest == null)
        {
            Console.WriteLine($"Guest [{guestID}] does not exist.");
            return null;
        }

        if (!guest.CanBook())
        {
            Console.WriteLine($"Guest [{guest.Email}] can not book anymore visits");
            return null;
        }

        var room = RoomRegister.FirstOrDefault(r => r.RoomID == roomID);

        if (room == null)
        {
            Console.WriteLine($"Room {roomID} does not exist.");
            return null;
        }

        if (!room.IsAvailable)
        {
            Console.WriteLine($"Room [{roomID}] is not available");
            return null;
        }
        
        var booking = new Booking(room, guest, checkIn, checkOut, payable);
        
        if (booking.IsPaid)
        {
            guest.ActiveBookings.Add(booking);
            BookingHistoryRegister.Add(booking);
            
            if (guest is VipGuest vipGuest)
            {
                vipGuest.LoyaltyPoints += 10;
            }
        }
        else
        {
            throw new ArgumentException("Payment failed. Booking is not paid.");
            return null;
        }
        
        Console.WriteLine($"Booking created:" + 
                          $"\nRoom [{room.RoomID}]" +
                          $"\nGuest [{guest.Email}]" +
                          $"\nCheck in [{booking.CheckInDate}]" +
                          $"\nCheck out [{booking.CheckOutDate}]" +
                          $"\nPayment Method [{payable}]" +
                          $"\nPrice [{booking.CalculateTotalPrice()}] NOK");
        return booking;
        
    }


    public Booking? CancelBooking(string bookingID)
    {
        var booking = BookingHistoryRegister.FirstOrDefault(b => b.BookingID == bookingID);

        if (booking == null)
        {
            Console.WriteLine($"Booking [{bookingID}] does not exist.");
            return null;
        }
        
        booking.guest.ActiveBookings.Remove(booking);
        BookingHistoryRegister.Remove(booking);
        
        Console.WriteLine($"Booking [{bookingID}] has been cancelled.");
        return booking;
        
    }


    public void GetGuestBookings(string guestID)
    {
        var guest = GuestRegister.FirstOrDefault(g => g.GuestID == guestID);

        if (guest == null)
        {
            Console.WriteLine($"Guest [{guestID}] does not exist.");
            return;
        }

        if (!guest.ActiveBookings.Any())
        {
            Console.WriteLine($"Guest [{guestID}] does not have any active bookings.");
            return;
        }
    
        Console.WriteLine($"Bookings for Guest [{guest.Name}]:");
        foreach (var booking in guest.ActiveBookings)
        {
            Console.WriteLine($"\nBooking ID: {booking.BookingID}" +
                              $"\nTotal Price: {booking.CalculateTotalPrice()}");
        }
    }

}

**Svar:**

5. CreateBooking viser feil betalingsinfo

Her:

    $"\nPayment Method [{payable}]"

Dette vil skrive noe som:

    Arbeidskrav2.CardPayment

ikke betalingsinformasjonen.

Du burde bruke:

    payable.GetPaymentInfo()

OG 

7. CancelBooking setter ikke rom til ledig

Her:

    booking.guest.ActiveBookings.Remove(booking);
    BookingHistoryRegister.Remove(booking);

Men du gjør ikke:

    booking.room.IsAvailable = true

Hvis rommet var opptatt, kan det forbli opptatt.

OG 

Akkurat nå har du skrevet:

    public List<Room> RoomRegister = new List<Room>();
    public List<Guest> GuestRegister = new List<Guest>();
    public List<Booking> BookingHistoryRegister = new List<Booking>();

Dette betyr at alle andre klasser i programmet kan gjøre hva de vil med listene.

For eksempel kan kode utenfor Hotel gjøre:

    hotel.RoomRegister = null

eller

    hotel.GuestRegister.Clear()

eller

    hotel.BookingHistoryRegister = new List<Booking>()

Da mister hotellet plutselig alle rom, gjester eller bookinger.

Systemet ditt mister kontroll over dataene.

Hvorfor dette er et problem

Oppgaven sier at systemet skal bruke innkapsling.

Det betyr at:

viktige data bør være private

andre klasser får bare kontrollert tilgang

Hotel bør være den eneste klassen som styrer:

rom

gjester

bookinger

Hva som er bedre design

I stedet for å gjøre listene helt åpne:

la Hotel eie listene

la andre klasser bare lese dem, ikke erstatte dem

Da kan ingen gjøre:

hotel.RoomRegister = noe annet

**Prompt:** Test 3: Hotel.CreateBooking() gir feilmelding hvis rommet allerede er opptatt. hvordan er denne fysisk mulig npr det er Checkin() som håndterer IsAvailable bool ifølge kravspek? så rommet blir ikke satt til opptatt før i checkin se oppgavetekst.

**Svar:**

Veldig bra observert – dette er faktisk et lite “design-gap” i oppgaven som mange snubler i 👀

Du har helt rett i én ting:

IsAvailable settes først i CheckIn()
→ altså ikke ved booking

Så spørsmålet ditt er helt legitimt:
👉 Hvordan kan rommet være “opptatt” før check-in?

🧠 Svaret: “Opptatt” betyr ikke IsAvailable

I denne oppgaven finnes det egentlig to forskjellige konsepter:

1. 🏨 Fysisk opptatt (under opphold)

styres av IsAvailable

settes i:

    CheckIn() → false
    CheckOut() → true
2. 📅 Reservert (booket i en periode)

styres av booking-datoer

dette er det testen din handler om

🔥 Det oppgaven EGENTLIG mener

Når de sier:

"rommet allerede er opptatt"

så mener de egentlig:

👉 "rommet er allerede booket i den perioden"

Ikke:
👉 "noen har checket inn akkurat nå"

💥 Derfor må du ignorere IsAvailable i CreateBooking

Denne:

    if (!room.IsAvailable)

👉 er nesten irrelevant for Test 3

✅ Det riktige du skal gjøre

Du må sjekke:

👉 Finnes det en booking som overlapper i dato?

    bool isRoomBooked = BookingHistoryRegister.Any(b =>
    b.Room.RoomID == roomID &&
    b.CheckOutDate > checkIn &&
    b.CheckInDate < checkOut
    );
    
    if (isRoomBooked)
    {
    Console.WriteLine($"Room [{roomID}] is already booked in this period.");
    return null;
}
🧠 Intuisjon (superviktig å forstå)

Tenk Airbnb:

Du kan booke rom i juli

Jeg prøver å booke samme rom i juli

👉 Selv om ingen har checket inn enda
→ rommet er fortsatt “opptatt” (reservert)

🔁 Hvordan alt henger sammen
Situasjon	Hva brukes
Booking	📅 dato-sjekk
Check-in	IsAvailable = false
Check-out	IsAvailable = true
⚠️ Litt kritikk av oppgaven (du tenker riktig)

Du har oppdaget at:

IsAvailable brukes i krav

men ikke egentlig nok for booking-logikk

👉 Dette er helt riktig observert
→ og faktisk ganske vanlig i skoleoppgaver

🎯 Konklusjon

Du har ikke misforstått – du har sett et hull i designet ✔️

Test 3 handler om dato-overlapp, ikke IsAvailable

Løsningen er å sjekke eksisterende bookinger, ikke boolen

**Prompt:**

denne printer feilmld 2 ganger når det ikke er rom  

    public void GetAvailableRooms(DateTime checkIn, DateTime checkOut)

    {
    if (BookingHistoryRegister.Count == 0)
    {
    foreach (var room in RoomRegister)
    {
    room.DisplayRoomInfo();
    }
    }
    int numAvailableRooms = 0;
    foreach (var booking in BookingHistoryRegister)
    {
    if (booking.CheckInDate >= checkIn && booking.CheckOutDate <= checkOut)
    {
    if (booking.room.IsAvailable)
    {
    booking.room.DisplayRoomInfo();
    numAvailableRooms++;
    }
    }
    else
    {
    if (numAvailableRooms == 0)
    {
    Console.WriteLine($"There are no available this time period.");
    }
    }
    }
    }

**Svar:**

Jepp — den printer flere ganger fordi logikken din ligger “feil sted” i loopen. La oss bryte det ned 👇

Hvorfor du får feilmeldingen flere ganger

Denne delen er synderen:

    else
    {
    if (numAvailableRooms == 0)
    {
    Console.WriteLine($"There are no available this time period.");
    }
    }

👉 Dette ligger inni foreach-loopen

Så for hver booking som ikke matcher, kjører den:

"There are no available..."

🧠 Hva som faktisk skjer

Si du har 3 bookinger som ikke matcher:

Loopen gjør:

booking 1 → print feilmelding

booking 2 → print feilmelding

booking 3 → print feilmelding

👉 derfor får du spam

Hovedregelen du bryter
Ikke print "ingen funn" inni en loop

👉 Du vet ikke om det finnes noe før loopen er ferdig

✅ Riktig tankegang

Loop gjennom alt

Tell / finn det som er tilgjengelig

ETTER loopen:

hvis 0 → print feilmelding

En annen bug du har (viktig!)

Denne:

if (booking.CheckInDate >= checkIn && booking.CheckOutDate <= checkOut)

👉 er feil logikk for tilgjengelighet

Dette sjekker:

“booking er HELT inni perioden”

Men du vil egentlig sjekke:

“overlapper booking med perioden?”

En booking er ikke tilgjengelig hvis den overlapper

Overlapp skjer når:

checkIn < booking.CheckOut
AND
checkOut > booking.CheckIn

En større designfeil (veldig viktig)

Du looper gjennom:

BookingHistoryRegister

👉 men du burde loope gjennom:

RoomRegister
Hvorfor?

Du vil finne:

“hvilke rom er ledige?”

Ikke:

“hvilke bookinger finnes?”

Legg til return her:

    if (BookingHistoryRegister.Count == 0)

Fjern denne:

    else
    {
    if (numAvailableRooms == 0)
    {
    Console.WriteLine(...)
    }
    }

Legg inn riktig datosjekk:
    checkIn < booking.CheckOutDate && checkOut > booking.CheckInDate




**Prompt:**

**Svar:**

**Prompt:**

**Svar:**

**Prompt:**

**Svar:**

**Prompt:**

**Svar:**

**Prompt:**

**Svar:**

**Prompt:**

**Svar:**







