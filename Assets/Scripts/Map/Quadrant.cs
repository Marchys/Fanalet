namespace Gen_mapa
{
    public class Quadrant
    {
        public Quadrant(int topX, int topY, int bottX, int bottY)
        {
            TopLeftCorner = new Punt2d(topX, topY);
            BottomRightCorner = new Punt2d(bottX, bottY);
            Center = new Punt2d((bottX + topX)/2, (bottY + topY)/2);
        }

        public Punt2d TopLeftCorner { get; set; }

        public Punt2d BottomRightCorner { get; set; }

        public Punt2d Center { get; set; }

    }

}