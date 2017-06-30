using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ShadowBingoGenerator
{
    public partial class Form1 : Form
    {
        List<Level> Levels = new List<Level>()
        {
            new Level("Westopolis", true, true, true, true, true, false),
            new Level("Digital Circuit", true, false, true, false, false, false),
            new Level("Glyphic Canyon", true, true, true, true, true, true),
            new Level("Lethal Highway", true, false, true, false, false, true),
            new Level("Cryptic Castle", true, true, true, true, true, true),
            new Level("Prison Island", true, true, true, true, true, false),
            new Level("Circus Park", true, true, true, false, true, false),
            new Level("Central City", true, false, true, false, false, false),
            new Level("The Doom", true, true, true, false, true, false),
            new Level("Sky Troops", true, true, true, true, true, true),
            new Level("Mad Matrix", true, true, true, true, false, false),
            new Level("Death Ruins", true, false, true, false, false, true),
            new Level("The ARK", false, true, true, false, true, false),
            new Level("Air Fleet", true, true, true, true, true, false),
            new Level("Iron Jungle", true, true, true, false, true, false),
            new Level("Space Gadget", true, true, true, false, false, false),
            new Level("Lost Impact", true, true, false, true, true, false),
            new Level("GUN Fortress", true, false, true, true, false, true),
            new Level("Black Comet", true, false, true, true, false, false),
            new Level("Lava Shelter", true, false, true, true, false, true),
            new Level("Cosmic Fall", true, false, true, true, false, true),
            new Level("Final Haunt", true, false, true, true, false, false)
        };

        List<string> ListOfExtras = new List<string>()
        {
            "The Doom - N (No Sequence Breaks/Shortcuts)",
            "Space Gadget - H (Dark Path)",
        };

        List<string> ListToGenerateBoardARanks = new List<string>()
        {
            //"Get 5 A Ranks Total",
            //"Get 10 A Ranks Total",
            //"Get 15 A Ranks Total",
            "Get 20 A Ranks Total",
            "Get 25 A Ranks Total",
            "Get 30 A Ranks Total",
            "Get 35 A Ranks Total",
            "Get 40 A Ranks Total",
            //"Get 45 A Ranks Total",
            //"Get 50 A Ranks Total",
            //"Get 55 A Ranks Total",
            //"Get 60 A Ranks Total",
            //"Get 65 A Ranks Total",
            //"Get 70 A Ranks Total",
            //"Get 71 A Ranks Total",
        };

        List<string> ListToGenerateBoardBosses = new List<string>()
        {
            "Black Bull - Hero",
            "Black Bull - Pure Hero",
            "Egg Breaker - Dark",
            "Egg Breaker - Normal",
            "Egg Breaker - Hero",
            "Heavy Dog",
            "Blue Falcon",
            "Sonic and Diablon - Pure Dark",
            "Sonic and Diablon - Dark",
            "Sonic and Diablon - Pure Hero",
            "Egg Dealer - Dark",
            "Egg Dealer - Normal",
            "Egg Dealer - Hero",
            "Black Doom - Pure Dark",
            "Black Doom - Hero",
            "Black Doom - Pure Hero"
        };

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var random = new Random();

            foreach (var level in Levels)
            {
                level.ResetFlags();
            }

            var ListOfItemsToUse = new List<string>(ListOfExtras);

            //if (SeperateNoCCGLevelsCheckbox.Checked)
            //{
            //    ListOfItemsToUse.AddRange(ListToGenerateBoardNoCCG);
            //}

            //if (KeysCheckbox.Checked)
            //{
            //    ListOfItemsToUse.AddRange(ListToGenerateBoardKeys);
            //}

            if (GetARanksCheckbox.Checked)
            {
                ListOfItemsToUse.AddRange(ListToGenerateBoardARanks);
            }

            if (IncludeBossesCheckbox.Checked)
            {
                ListOfItemsToUse.AddRange(ListToGenerateBoardBosses);
            }

            List<string> generatedBoard = new List<string>();

            for (int i = 0; i < 25; i++)
            {
                var itemType = random.Next() % 100;
                string item = null;

                var levelPercent = KeysCheckbox.Checked ? 70: 80;
                var keyPercent = 90;

                if (itemType < levelPercent)
                {
                    while (item == null)
                    {
                        var index = random.Next() % Levels.Count;
                        var NoCCG = SeperateNoCCGLevelsCheckbox.Checked && (random.Next() % 100 < 30);
                        item = Levels[index].GrabLevelItem(NoCCG);
                    }
                }
                else if (itemType < keyPercent && KeysCheckbox.Checked)
                {
                    while (item == null)
                    {
                        var index = random.Next() % Levels.Count;
                        item = Levels[index].GrabKeyItem(5, 30, 80, 95, 100);
                    }
                }
                else if(ListOfItemsToUse.Count > 0)
                {
                    var index = random.Next() % ListOfItemsToUse.Count;
                    item = ListOfItemsToUse[index];
                    ListOfItemsToUse.RemoveAt(index);
                }
                else
                {
                    i--;
                }

                if (item != null)
                {
                    generatedBoard.Add(item);
                }
            }

            textBox1.Text = "[";
            foreach (var item in generatedBoard)
            {
                textBox1.Text += "{\"name\": " + item + "}," + "\r\n";
            }
            textBox1.Text += "]";
        }
    }
}
