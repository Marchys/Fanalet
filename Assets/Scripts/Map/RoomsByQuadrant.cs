using System.Collections.Generic;

namespace Gen_mapa
{
    public class RoomsByQuadrant
    {
        private List<Punt2d>[] _roomsByQuadrant = new List<Punt2d>[4];
        private int _selectedQuadrant;

        public RoomsByQuadrant()
        {
            SelectedQuadrant = 0;
            _roomsByQuadrant[0] = new List<Punt2d>();
            _roomsByQuadrant[1] = new List<Punt2d>();
            _roomsByQuadrant[2] = new List<Punt2d>();
            _roomsByQuadrant[3] = new List<Punt2d>();
        }

        public int SelectedQuadrant
        {
            get { return _selectedQuadrant; }
            set
            {
                if (value >= 0 && value <= 3)
                {
                    _selectedQuadrant = value;
                }
                else
                {
                    _selectedQuadrant = 0;
                }
            }
        }

        public Punt2d this[int listIndex]
        {
            get { return _roomsByQuadrant[SelectedQuadrant][listIndex]; }

            set { _roomsByQuadrant[SelectedQuadrant][listIndex] = value; }
        }
      
    }

}