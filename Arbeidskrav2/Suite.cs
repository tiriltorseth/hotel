namespace Arbeidskrav2;

/// <summary>
/// Klasse for rommet suite, arver fra abstrakt klasse room
/// </summary>
public class Suite : Room
{
    private bool hasJacuzzi;
    private bool hasLounge;

    /// <summary>
    /// Bool som informerer om rommet har jacuzzi eller ikke
    /// </summary>
    public bool HasJacuzzi
    {
        get { return hasJacuzzi; }
        private set { hasJacuzzi = value; }
    }
    
    /// <summary>
    /// Bool som informerer om rommet har lounge område eller ikke
    /// </summary>
    public bool HasLounge{
        get { return hasLounge; }
        private set { hasLounge = value; }
    }
    
    /// <summary>
    /// Oppretter ny suite som tar inn rom id, type pris per natt og max gjester, som er hentet fra basen rom
    /// Setter prisen på rommet 3500r og maks plass til fire gjester
    /// Setter typen til suite og at den har jacuzzi og lounge
    /// </summary>
    public Suite(string roomID, string roomType)
        : base(roomID, roomType, 3500, 4)
    {
        RoomType = "Suite";
        HasJacuzzi = true;
        HasLounge = true;
    }

    /// <summary>
    /// Printer ut informasjon om rommet
    /// Kjører over den abstrakte metoden Displayroominfo fra rom med override
    /// </summary>
    public override void DisplayRoomInfo()
    {
        Console.WriteLine($"[{RoomID}] {RoomType} - " +
                          $"{PricePerNight} kr/night - Max {MaxGuests} guest - " +
                          $"Has Jacuzzi: {(HasJacuzzi ? "Yes" : "No")} - "+
                          $"Has Lounge: {(HasLounge ? "Yes" : "No")}");
    }
}