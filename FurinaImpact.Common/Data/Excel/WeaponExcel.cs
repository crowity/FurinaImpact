using System.Collections.Generic;
using System.Text.Json.Serialization;
using FurinaImpact.Common.Data.Excel.Attributes;

namespace FurinaImpact.Common.Data.Excel
{
    [Excel(ExcelType.Weapon, "WeaponExcelConfigData.json")]
    public class WeaponExcel : ExcelItem
    {
        // TODO: implement enums for some fields!

        public override uint ExcelId => Id;

        [JsonPropertyName("awakenCosts")]
        public List<object> AwakenCosts { get; set; } // Adjust the type based on the actual content

        [JsonPropertyName("awakenIcon")]
        public string AwakenIcon { get; set; }

        [JsonPropertyName("awakenLightMapTexture")]
        public string AwakenLightMapTexture { get; set; }

        [JsonPropertyName("awakenTexture")]
        public string AwakenTexture { get; set; }

        [JsonPropertyName("descTextMapHash")]
        public ulong DescTextMapHash { get; set; }

        [JsonPropertyName("destroyReturnMaterial")]
        public List<int> DestroyReturnMaterial { get; set; }

        [JsonPropertyName("destroyReturnMaterialCount")]
        public List<int> DestroyReturnMaterialCount { get; set; }

        [JsonPropertyName("destroyRule")]
        public string DestroyRule { get; set; }

        [JsonPropertyName("gachaCardNameHashSuffix")]
        public ulong GachaCardNameHashSuffix { get; set; }

        [JsonPropertyName("gadgetId")]
        public uint GadgetId { get; set; }

        [JsonPropertyName("icon")]
        public string Icon { get; set; }

        [JsonPropertyName("id")]
        public uint Id { get; set; }

        [JsonPropertyName("itemType")]
        public string ItemType { get; set; }

        [JsonPropertyName("nameTextMapHash")]
        public ulong NameTextMapHash { get; set; }

        [JsonPropertyName("rank")]
        public int Rank { get; set; }

        [JsonPropertyName("rankLevel")]
        public int RankLevel { get; set; }

        [JsonPropertyName("skillAffix")]
        public List<int> SkillAffix { get; set; }

        [JsonPropertyName("storyId")]
        public uint StoryId { get; set; }

        [JsonPropertyName("weaponBaseExp")]
        public int WeaponBaseExp { get; set; }

        [JsonPropertyName("weaponPromoteId")]
        public uint WeaponPromoteId { get; set; }

        [JsonPropertyName("weaponProp")]
        public List<WeaponProp> WeaponProp { get; set; }

        [JsonPropertyName("weaponType")]
        public string WeaponType { get; set; }

        [JsonPropertyName("weight")]
        public int Weight { get; set; }

        public WeaponExcel()
        {
            AwakenCosts = new List<object>();
            DestroyReturnMaterial = new List<int>();
            DestroyReturnMaterialCount = new List<int>();
            SkillAffix = new List<int>();
            WeaponProp = new List<WeaponProp>();
        }
    }

    public class WeaponProp
    {
        [JsonPropertyName("initValue")]
        public double InitValue { get; set; }

        [JsonPropertyName("propType")]
        public string PropType { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}
