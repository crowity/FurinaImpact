using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FurinaImpact.Common.Data.Excel;
using FurinaImpact.Gameserver.Game.Avatar;
using FurinaImpact.Gameserver.Game.Weapon;

namespace FurinaImpact.Gameserver.Game
{
    internal class Player
    {
        private static readonly uint[] AvatarBlackList = { 10000001, 10000005, 10000007 }; // kate and travelers

        public uint Uid { get; set; }
        public uint GuidSeed { get; set; }
        public string Name { get; set; }
        public List<GameAvatar> Avatars { get; set; }
        public List<GameWeapon> Weapons { get; set; }

        private readonly ExcelTableCollection _excelTables;
        private readonly ExcelTableCollection _weaponTables;

        public Player(ExcelTableCollection excelTables)
        {
            Name = "Traveler";
            Avatars = new List<GameAvatar>();
            Weapons = new List<GameWeapon>(); // Initialize Weapons list

            _excelTables = excelTables;
        }

        public void InitDefaultPlayer()
        {
            // We don't have a database atm, so let's init default player state for every session.
            Uid = 1938;
            Name = "kivanc";

            UnlockAllAvatars();
            UnlockAllWeapons();
        }

        public bool TryGetAvatar(uint avatarId, [MaybeNullWhen(false)] out GameAvatar avatar)
            => (avatar = Avatars.Find(a => a.AvatarId == avatarId)) != null;

        private void UnlockAllAvatars()
        {
            ExcelTable avatarTable = _excelTables.GetTable(ExcelType.Avatar);
            for (int i = 0; i < avatarTable.Count; i++)
            {
                AvatarExcel avatarExcel = avatarTable.GetItemAt<AvatarExcel>(i);
                if (AvatarBlackList.Contains(avatarExcel.Id) || avatarExcel.Id >= 11000000) continue;

                uint currentTimestamp = (uint)DateTimeOffset.Now.ToUnixTimeSeconds();
                GameAvatar avatar = new GameAvatar()
                {
                    AvatarId = avatarExcel.Id,
                    SkillDepotId = avatarExcel.SkillDepotId,
                    WeaponId = avatarExcel.InitialWeapon,
                    BornTime = currentTimestamp,
                    Guid = NextGuid(),
                    WearingFlycloakId = 140001
                };

                avatar.InitDefaultProps(avatarExcel);
                Avatars.Add(avatar);
            }
        }

        private void UnlockAllWeapons()
        {
            ExcelTable weaponTable = _excelTables.GetTable(ExcelType.Weapon);
            for (int i = 0; i < weaponTable.Count; i++)
            {
                WeaponExcel weaponExcel = weaponTable.GetItemAt<WeaponExcel>(i);

                uint currentTimestamp = (uint)DateTimeOffset.Now.ToUnixTimeSeconds();

                GameWeapon weapon = new GameWeapon()
                {
                    id = weaponExcel.Id,
                    weight = weaponExcel.Weight,
                    ItemType = weaponExcel.ItemType,
                    WeaponType = weaponExcel.WeaponType,
                    GadgetId = weaponExcel.GadgetId,
                    Guid = NextGuid()
                };
                Console.WriteLine("sexo");
                Weapons.Add(weapon);
            }
        }

        public ulong NextGuid()
        {
            return ((ulong)Uid << 32) + (++GuidSeed);
        }
    }
}
