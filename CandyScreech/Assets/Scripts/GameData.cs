using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BreakInfinity;

public class GameData
{
    public BigDouble candiesCount;
    public List<int> clickUpgradeLevel;
    public List<int> productionUpgradeLevel;

    public GameData()
    {
        candiesCount = 0;

        clickUpgradeLevel = new int[3].ToList();
        productionUpgradeLevel = new int[3].ToList();
    }
}
