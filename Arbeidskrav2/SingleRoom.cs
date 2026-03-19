namespace Arbeidskrav2;

/// <summary>
/// Klasse for rommet singleroom, arver fra abstrakt klasse room
/// </summary>
public class SingleRoom : Room
{
    private bool hasDesk;

    /// <summary>
    /// Bool som informerer om rommet har ekstra pult eller ikke
    /// </summary>
    public bool HasDesk
    {
        get { return hasDesk; }
        private set { hasDesk = value; }
    }
    
    /// <summary>
    /// Oppretter nytt enkelt rom som tar inn rom id, type pris per natt og max gjester, som er hentet fra basen rom
    /// Setter prisen på rommet 800kr og maks plass til en gjest
    /// Setter typen til enkeltrom og at den har pult
    /// </summary>
    public SingleRoom(string roomID,string roomType)
        : base(roomID, roomType, 800, 1)
    {
        RoomType = "SingleRoom";
        HasDesk = true;
    }

    /// <summary>
    /// Printer ut informasjon om rommet
    /// Kjører over den abstrakte metoden Displayroominfo fra rom med override
    /// </summary>
    public override void DisplayRoomInfo()
    {
        Console.WriteLine($"[{RoomID}] {RoomType} - " +
                          $"{PricePerNight} kr/night - Max {MaxGuests} guest - " +
                          $"Has Desk: {(HasDesk ? "Yes" : "No")}");
        
    }
}