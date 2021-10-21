using System.Collections;
using System.Collections.Generic;
using BreakInfinity;

public class GameData
{
    public BigDouble candiesCount;
    public List<BigDouble> clickUpgradeLevel;

    public GameData()
    {
        candiesCount = 0;

        clickUpgradeLevel = Methods.CreateList<BigDouble>(3);
    }
}
