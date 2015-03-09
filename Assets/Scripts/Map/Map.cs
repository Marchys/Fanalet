using System;
using Random = UnityEngine.Random;

namespace Gen_mapa
{
    public class Map
    {
        //the multidimensional map array
        private int[,] map;
        //  number of rooms
        int roomCount = 0;        
        //where the point of the map is
        Punt2d pointer;
      

        public Map(int x, int y)
        {           
            map = new int[x, y];            
            Array.Clear(map, 0, map.Length);
            this.Pointer = new Punt2d(randomNumber(Width), randomNumber(Height)); 
        }

        int randomNumber(int limit)
        {
            int num_gen;
            do
            {
                num_gen = Random.Range(0, limit);
            } while (num_gen % 2 == 1);

            return num_gen;
        }

        public int this[int x, int y]
        {
            get
            {
                if (x < Width && y < Height && x >= 0 && y >= 0) return map[x, y];
                else return 666;
            }
            set { map[x, y] = value; }
        }

        public Punt2d Pointer
        {
            get
            {
                return pointer;
            }
            set { pointer = value; }
        }

        public int RoomCount
        {
            get { return roomCount; }
            set { roomCount = value; }
        }

        public int Width
        {
            get { return map.GetUpperBound(0) + 1; }
        }

        public int Height
        {
            get { return map.GetUpperBound(1) + 1; }
        }

       
    }

}



