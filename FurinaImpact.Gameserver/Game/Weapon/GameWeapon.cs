using FurinaImpact.Common.Constants;
using FurinaImpact.Common.Data.Excel;
using FurinaImpact.Protocol;

namespace FurinaImpact.Gameserver.Game.Weapon;
internal class GameWeapon
{
    public ulong Guid { get; set; }

    public uint id { get; set; }
    public int weight { get; set; }

    public string WeaponType { get; set; }
    public string ItemType { get; set; }

    public uint GadgetId { get; set; }

    // Properties
    public List<PropValue> Properties;
    public List<FightPropPair> FightProperties;

    public GameWeapon()
    {
        Properties = new List<PropValue>();
        FightProperties = new List<FightPropPair>();
    }

}