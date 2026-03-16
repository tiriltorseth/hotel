namespace Arbeidskrav2;

public class Suite : Room
{
    private bool hasJacuzzi;
    private bool hasLounge;

    public bool HasJacuzzi
    {
        get { return hasJacuzzi; }
        private set { hasJacuzzi = value; }
    }
    
    public bool HasLounge{
        get { return hasLounge; }
        private set { hasLounge = value; }
    }
    
    public Suite(string roomID, string roomType)
        : base(roomID, roomType, 3500, 4)
    {
        RoomType = "Suite";
        HasJacuzzi = true;
        HasLounge = true;
    }

    public override void DisplayRoomInfo()
    {
        Console.WriteLine($"[{RoomID}] {RoomType} - " +
                          $"{PricePerNight} kr/night - Max {MaxGuests} guest - " +
                          $"Has Jacuzzi: {(HasJacuzzi ? "Yes" : "No")} - "+
                          $"Has Lounge: {(HasLounge ? "Yes" : "No")}");
    }
}