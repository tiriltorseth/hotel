namespace Arbeidskrav2;

/// <summary>
/// Klasse for rommet doubleroom, arver fra abstrakt klasse room
/// </summary>
public class DoubleRoom : Room
{
    private bool hasExtraBed;

    /// <summary>
    /// Bool som informerer om rommet har ekstra seng eller ikke
    /// </summary>
    public bool HasExtraBed
    {
        get { return hasExtraBed; }
        private set { hasExtraBed = value; }
    }
    
    /// <summary>
    /// Oppretter nytt dobbelt rom som tar inn rom id, type pris per natt og max gjester, som er hentet fra basen rom
    /// Setter prisen på rommet 1200kr og maks plass til to gjester
    /// Setter typen til dobbelt rom og at den har ekstra seng
    /// </summary>
    public DoubleRoom(string roomID, string roomType)
        : base(roomID, roomType, 1200, 2)
    {
        RoomType = "DoubleRoom";
        HasExtraBed = true;
    }


    /// <summary>
    /// Printer ut informasjon om rommet
    /// Kjører over den abstrakte metoden Displayroominfo fra rom med override
    /// </summary>
    public override void DisplayRoomInfo()
    {
        Console.WriteLine($"[{RoomID}] {RoomType} - " +
                          $"{PricePerNight} kr/night - Max {MaxGuests} guest - " +
                          $"Has Extra Bed: {(HasExtraBed ? "Yes" : "No")}");
    }
}