namespace Arbeidskrav2;

public class SingleRoom : Room
{
    private bool hasDesk;

    public bool HasDesk
    {
        get { return hasDesk; }
        protected set { hasDesk = value; }
    }
    

    public SingleRoom(string RoomType, decimal PricePerNight, int MaxGuests)
        : base(RoomType, 800, 1)
    {
        HasDesk = true;
    }

    public override void DisplayRoomInfo()
    {
        /// Her skriver du ut informasjonen fra menyen, kall på denne metoden
    }
}