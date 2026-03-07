namespace Arbeidskrav2;

public class DoubleRoom : Room
{
    private bool hasExtraBed;

    public bool HasExtraBed
    {
        get { return hasExtraBed; }
        protected set { hasExtraBed = value; }
    }
    
    public DoubleRoom(string RoomType, decimal PricePerNight, int MaxGuests)
        : base(RoomType, 1200, 2)
    {
        HasExtraBed = true;
    }

    public override void DisplayRoomInfo()
    {
        /// Her skriver du ut informasjonen fra menyen, kall på denne metoden
    }
}