namespace Arbeidskrav2;

public class DoubleRoom : Room
{
    private bool hasExtraBed;

    public bool HasExtraBed
    {
        get { return hasExtraBed; }
        private set { hasExtraBed = value; }
    }
    
    public DoubleRoom(string roomID, string roomType)
        : base(roomID, roomType, 1200, 2)
    {
        RoomType = "DoubleRoom";
        HasExtraBed = true;
    }

    public override void DisplayRoomInfo()
    {
        Console.WriteLine($"[{RoomID}] {RoomType} - " +
                          $"{PricePerNight} kr/night - Max {MaxGuests} guest - " +
                          $"Has Extra Bed: {(HasExtraBed ? "Yes" : "No")}");
    }
}