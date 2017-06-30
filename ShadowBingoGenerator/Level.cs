using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowBingoGenerator
{
    class Level
    {
        string name;

        bool hasHeroMission;
        bool hasNeutralMission;
        bool hasDarkMission;

        bool hasHeroNoCCGMission;
        bool hasNeutralNoCCGMission;
        bool hasDarkNoCCGMission;

        bool hasHeroMissionPicked;
        bool hasNeutralMissionPicked;
        bool hasDarkMissionPicked;

        bool hasKeysPicked;

        int numOfUsedItems = 0;
        int numOfUsedItemsMax = 3;

        Random rng = new Random();

        public Level(string name, bool hero, bool neutral, bool dark, bool heroNoCCG, bool neutralNoCCG, bool darkNoCCG)
        {
            this.name = name;
            hasHeroMission = hero;
            hasNeutralMission = neutral;
            hasDarkMission = dark;
            hasHeroNoCCGMission = heroNoCCG;
            hasNeutralNoCCGMission = neutralNoCCG;
            hasDarkNoCCGMission = darkNoCCG;
        }

        int NumOfMissions(bool NoCCG)
        {
            var numOfMissions = 0;
            if (NoCCG)
            {
                if (hasHeroNoCCGMission && !hasHeroMissionPicked)
                    numOfMissions++;
                if (hasNeutralNoCCGMission && !hasNeutralMissionPicked)
                    numOfMissions++;
                if (hasDarkNoCCGMission && !hasDarkMissionPicked)
                    numOfMissions++;
            }
            else
            {
                if (hasHeroMission && !hasHeroMissionPicked)
                    numOfMissions++;
                if (hasNeutralMission && !hasNeutralMissionPicked)
                    numOfMissions++;
                if (hasDarkMission && !hasDarkMissionPicked)
                    numOfMissions++;
            }
            return numOfMissions;
        }

        public string GrabLevelItem(bool NoCCG)
        {
            //Check to be sure that the maximum number of items from this level hasnt been met.
            if (numOfUsedItems == numOfUsedItemsMax)
            {
                return null;
            }
            else
            {
                var numOfMissions = NumOfMissions(NoCCG);

                //No more valid mission are avalible.
                if (numOfMissions == 0)
                {
                    return null;
                }
                
                var i = rng.Next() % numOfMissions;

                if (((NoCCG) ? hasHeroNoCCGMission : hasHeroMission) && !hasHeroMissionPicked)
                {
                    if (i > 0)
                    {
                        i--;
                    }
                    else
                    {
                        numOfUsedItems++;
                        hasHeroMissionPicked = true;
                        return name + " - H" + ((NoCCG) ? " (No CCG)" : "");
                    }
                }

                if (((NoCCG) ? hasNeutralNoCCGMission : hasNeutralMission) && !hasNeutralMissionPicked)
                {
                    if (i > 0)
                    {
                        i--;
                    }
                    else
                    {
                        numOfUsedItems++;
                        hasNeutralMissionPicked = true;
                        return name + " - N" + ((NoCCG) ? " (No CCG)" : "");
                    }
                }

                if (((NoCCG) ? hasDarkNoCCGMission : hasDarkMission) && !hasDarkMissionPicked)
                {
                    if (i > 0)
                    {
                        i--;
                    }
                    else
                    {
                        numOfUsedItems++;
                        hasDarkMissionPicked = true;
                        return name + " - D" + ((NoCCG) ? " (No CCG)" : "");
                    }
                }


                //Shouldnt reach here unless something was not calculated right.
                return null;
            }
        }

        public string GrabKeyItem(int key1Weight, int key2Weight, int key3Weight, int key4Weight, int key5Weight)
        {
            if (hasKeysPicked || numOfUsedItems == numOfUsedItemsMax)
            {
                //Only one keys item allowed or max number of items for level met.
                return null;
            }

            numOfUsedItems++;
            hasKeysPicked = true;

            int numOfKeys = 0;
            int i = rng.Next() % 100;

            if (i < key1Weight)
            {
                numOfKeys = 1;
            }
            else if (i < key2Weight)
            {
                numOfKeys = 2;
            }
            else if (i < key3Weight)
            {
                numOfKeys = 3;
            }
            else if (i < key4Weight)
            {
                numOfKeys = 4;
            }
            else if (i < key5Weight)
            {
                numOfKeys = 5;
            }

            return name + " - " + numOfKeys + ((numOfKeys > 1) ? " Keys" : " Key");
        }

        public void ResetFlags()
        {
            numOfUsedItems = 0;
            hasKeysPicked = false;
            hasHeroMissionPicked = false;
            hasNeutralMissionPicked = false;
            hasDarkMissionPicked = false;
        }
    }
}
