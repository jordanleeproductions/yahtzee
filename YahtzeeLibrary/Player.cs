using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahtzeeLibrary
{
    public class Player
    {

        public List<Die> dice { get; private set; }
        public int totUpper {get; private set;}
        public int totUpperBonus { get; private set; }
        public int totLower { get; private set; }
        public int totGrand { get; private set; }

        private int ctrUpper { get; set; }
        private int ctrLower { get; set; }


        private static int MAX_DICE = 5;
        
        public Player() {
            
            dice = new List<Die>();
            for (int i = 0; i < MAX_DICE; ++i) {
                Die d = new Die();
                dice.Add(d);
            }

            ctrLower = 7;
            ctrUpper = 6;
            totGrand = 0;

        }//Player

        public void Roll()
        {
            foreach (Die d in dice)
                if (!d.HoldState)
                    d.Roll();
        }

        public void Reset() {
            foreach (Die d in dice)
                d.HoldState = false;
        }
        
        public int Calculate(string lbName)
        {
            int sum = 0;
            switch (lbName)
            {
                case "aces":
                    sum = calcSingles(1);
                    totUpper += sum;
                    --ctrUpper;
                    break;
                case "twos":
                    sum = calcSingles(2);
                    totUpper += sum;
                    --ctrUpper;
                    break;
                case "threes":
                    sum = calcSingles(3);
                    totUpper += sum;
                    --ctrUpper;
                    break;
                case "fours":
                    sum = calcSingles(4);
                    totUpper += sum;
                    --ctrUpper;
                    break;
                case "fives":
                    sum = calcSingles(5);
                    totUpper += sum;
                    --ctrUpper;
                    break;
                case "sixes":
                    sum = calcSingles(6);
                    totUpper += sum;
                    --ctrUpper;
                    break;
                case "kind3":
                    if (calcGroups(3))
                        sum = calcAll();
                    else
                        sum = 0;

                    totLower += sum;
                    --ctrLower;
                    break;
                case "kind4":
                    if (calcGroups(4))
                        sum = calcAll();
                    else
                        sum = 0;

                    totLower += sum;
                    --ctrLower;
                    break;
                case "fullHouse":
                    if (calcGroups(2) && calcGroups(3))
                        sum = 25;
                    else
                        sum = 0;

                    totLower += sum;
                    --ctrLower;
                    break;
                case "smStraight":

                    var dcSmTmp = dice.Select(d => d.Val).Distinct().ToList();
                    dcSmTmp.Sort();
                    bool isSmStrght = true;
                    int valSm = dcSmTmp.ElementAt(0);
                    for (int i = 0; i < 4; ++i) {
                        if (!(valSm == dcSmTmp.ElementAt(i))) {
                            isSmStrght = false;
                            break;
                        }
                        ++valSm;
                    }

                    if (isSmStrght)
                        sum = 30;
                    else
                        sum = 0;

                    totLower += sum;
                    --ctrLower;
                    break;

                case "lgStraight":
                    List<Die> dcLgTmp = dice.OrderBy(d => d.Val).ToList();
                    bool isLgStrght = true;
                    int valLg = dcLgTmp.ElementAt(0).Val;
                    for (int i = 0; i < 5; ++i) {
                        if (!(valLg == dcLgTmp.ElementAt(i).Val)) {
                            isLgStrght = false;
                            break;
                        }
                        ++valLg;
                    }
                    if (isLgStrght)
                        sum = 40;
                    else
                        sum = 0;

                    totLower += sum;
                    --ctrLower;
                    break;
                case "yahtzee":
                    if (calcGroups(5))
                        sum = 50;
                    else
                        sum = 0;

                    totLower += sum;
                    --ctrLower;
                    break;
                case "chance":
                    sum = calcAll();
                    totLower += sum;
                    --ctrLower;
                    break;
                default:
                    sum = 0;
                    break;
            }//end switch

            if (totUpper >= 63) { totUpperBonus = 35; }
            totGrand += sum;
            return sum;
        }//end Calculate()

        public bool isTotUpper() { return ctrUpper == 0; }

        public bool isTotLower() { return ctrLower == 0; }

        public bool isTotGrand() { return isTotUpper() && isTotLower(); }

        private int calcSingles(int num)
        {
            int sum = 0;
            foreach (Die d in dice)
            {
                if (d.Val == num) { sum += d.Val; }
            }

            return sum;
        }//end calcSingles

        private int calcAll()
        {
            int sum = 0;
            foreach (Die d in dice)
                sum += d.Val;
            return sum;
        }

        private bool calcGroups(int num)
        {
            Dictionary<int, int> dc = new Dictionary<int, int>();
            //loads dictionary like map -- Die.Val for index and repeats as value
            foreach (Die d in dice)
            {
                if (dc.ContainsKey(d.Val))
                    ++dc[d.Val];
                else
                    dc.Add(d.Val, 1);
            }

            //Tests that at least num matches found
            foreach (KeyValuePair<int, int> pr in dc)
            {
                if (pr.Value >= num) return true;
            }

            return false;

        }//end calcGroups

    }//end class
}//end namespace
