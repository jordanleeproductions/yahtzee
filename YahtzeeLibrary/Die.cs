using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahtzeeLibrary
{
    public class Die
    {
        private int val;
        private bool holdState;
        private static Random rdm = new Random();

        public int Val
        {
            get { return this.val; }
            set { this.val = value; }
        }

        public bool HoldState
        {
            get { return this.holdState; }
            set { this.holdState = value; }
        }

        public Die()
        {
            this.holdState = false;
        }

        public void Roll()
        {
            int v = rdm.Next(1, 7);
            this.val = v; //Range 1 - 6
        }
    }//end class
}//end namespace
