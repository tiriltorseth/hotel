namespace Arbeidskrav2;

public class Suite : Room
{
    private bool hasJacuzzi;
    private bool hasLounge;

    public bool HasJacuzzi
    {
        get { return hasJacuzzi; }
        protected set { hasJacuzzi = value; }
    }
    
    public bool HasLounge{
        get { return hasLounge; }
        protected set { hasLounge = value; }
    }
    
    public Suite(string roomID, string RoomType, decimal PricePerNight, int MaxGuests)
        : base(roomID, RoomType, 3500, 4)
    {
        HasJacuzzi = true;
        HasLounge = true;
    }

    public override void DisplayRoomInfo()
    {
        /// Her skriver du ut informasjonen fra menyen, kall på denne metoden
    }
}