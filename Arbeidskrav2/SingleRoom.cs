namespace Arbeidskrav2;

public class SingleRoom : Room
{
    private bool hasDesk;

    public bool HasDesk
    {
        get { return hasDesk; }
        private set { hasDesk = value; }
    }
    

    public SingleRoom(string roomID,string roomType)
        : base(roomID, roomType, 800, 1)
    {
        RoomType = "SingleRoom";
        HasDesk = true;
    }

    public override void DisplayRoomInfo()
    {
        Console.WriteLine($"[{RoomID}] {RoomType} - " +
                          $"{PricePerNight} kr/night - Max {MaxGuests} guest - " +
                          $"Has Desk: {(HasDesk ? "Yes" : "No")}");
        
    }
}