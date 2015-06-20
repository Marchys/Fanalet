using UnityEngine;

public class JocManager 
    {
        private static JocManager instance = new JocManager();
        private JocStats actualStat;
 
        // make sure the constructor is private, so it can only be instantiated here
        private JocManager()
        {

        }

        public static JocManager Generar 
        {
            get { return instance; }
        }       
 
        public void Pause(bool paused)
        {
            Time.timeScale = paused ? 0.0f : 1f;
        }

    public JocStats ActualStat
       {           
             set 
             {
                 if (actualStat != value)
                 {
                     actualStat = value;

                     switch (value)
                     {
                         case JocStats.Intro:
                             // Play Intro here
                             break;
                         case JocStats.Menu:
                             Application.LoadLevel("menu");
                             break;
                         case JocStats.Isla:
                             Application.LoadLevel("illa");
                             break;
                         case JocStats.Dungeon:
                             Application.LoadLevel("dungeon");
                             break;
                         default:
                             // Do this when none of the cases above fit
                             break;
                     }
                 }

             }   
                         
       }
        public enum JocStats
        {
            Intro,
            Menu,
            Isla,
            Dungeon
        }
   }

