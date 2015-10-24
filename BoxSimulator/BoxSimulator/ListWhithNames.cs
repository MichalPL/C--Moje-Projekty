using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxSimulator
{
    class ListWhithNames
    {
        string[,] list = new string[27, 27];
        public ListWhithNames()
        {
            list[0, 1] = "Dark Shadow Weapon";
            list[1, 1] = "Infernal Destroyer Weapon";
            list[2, 1] = "Infernal Destroyer Equipment";
            list[3, 1] = "Heavenly Executioner Weapon";
            list[4, 1] = "Heavenly Executioner Equipment";
            list[5, 1] = "Blood Dragon Weapon";
            list[6, 1] = "Blood Dragon Equipment";
            list[7, 1] = "Gold Ingot";
            list[8, 1] = "Gold Coin";
            list[9, 1] = "Silver Coin";
            list[10, 1] = "Stone of Exp";
            list[11, 1] = "Stone of Wealth";
            list[12, 1] = "Lucky Scroll of Power Improvement";
            list[13, 1] = "Lucky Scroll of Evasion Improvement";
            list[14, 1] = "Lucky Scroll of Explosive Blow Improvement";
            list[15, 1] = "Lucky Scroll of Damage Reduction";
            list[16, 1] = "Lucky Scroll of Accuracy Improvement";
            list[17, 1] = "Lucky Potion of Tiger Claws";
            list[18, 1] = "Lucky Potion of Bear Skin";
            list[19, 1] = "Lucky Potion of Eagle wings";
            list[20, 1] = "Absorption Medicine";
            list[21, 1] = "Water of Eight Trigrams";
            list[22, 1] = "Doggebi Solubilizer";
            list[23, 1] = "Silvery Carp";
            list[24, 1] = "Silvery Eel";
            list[25, 1] = "Diamond stone";
            list[26, 1] = "Talisman of expert";

            for (int i = 0; i < 27; i++)
                list[i, 0] = (i + 1).ToString();
        }

        public string get(int x, int y)
        {
            return list[x, y];
        }
    }
}
