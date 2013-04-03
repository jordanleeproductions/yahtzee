using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahtzeeLibrary
{
    public class Game 
    {
        public List<Die> dice { get; set; }
        private static int MAX_DICE = 5;

        public Game() {
            dice = new List<Die>();
            for (int i = 0; i < MAX_DICE; ++i) {
                Die d = new Die();
                dice.Add(d);
            }
                
        }

        

    }//end class
}//end namespace
