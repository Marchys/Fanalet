public class EnterAreaMessage 
{
    public Punt2d Coor { get; set; }
    public EnterAreaMessage(Punt2d coor)
    {
        Coor = coor;
    }
}
