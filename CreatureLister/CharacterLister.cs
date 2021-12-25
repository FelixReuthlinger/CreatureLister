using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BepInEx;
using JetBrains.Annotations;
using Jotunn.Managers;
using UnityEngine;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using Logger = Jotunn.Logger;

namespace CreatureLister {
    public class HumanoidModel {
        public HumanoidModel(string internalName, Character.Faction faction, string group, float health,
            HitData.DamageModifiers damageModifiers, string defeatSetGlobalKey, List<string> defaultItems,
            List<string> randomWeapons, List<string> randomArmors, List<string> randomShields) {
            InternalName = internalName;
            Faction = faction;
            Group = group;
            Health = health;
            DamageModifiers = damageModifiers;
            DefeatSetGlobalKey = defeatSetGlobalKey;
            DefaultItems = defaultItems;
            RandomWeapons = randomWeapons;
            RandomArmors = randomArmors;
            RandomShields = randomShields;
        }

        [UsedImplicitly] public readonly string InternalName;
        [UsedImplicitly] public readonly Character.Faction Faction;
        [UsedImplicitly] public readonly string Group;
        [UsedImplicitly] public readonly float Health;
        [UsedImplicitly] public readonly HitData.DamageModifiers DamageModifiers;
        [UsedImplicitly] public readonly string DefeatSetGlobalKey;
        [UsedImplicitly] public readonly List<string> DefaultItems;
        [UsedImplicitly] public readonly List<string> RandomWeapons;
        [UsedImplicitly] public readonly List<string> RandomArmors;
        [UsedImplicitly] public readonly List<string> RandomShields;
    }

    public static class HumanoidLister {
        private const string CloneString = "(Clone)";
        private const string PlayerString = "Player";
        private static readonly string DefaultConfigRootPath = Paths.ConfigPath;
        private static readonly string DefaultOutputFileName = $"{CreatureListerPlugin.PluginGuid}.defaults.yaml";
        private static readonly string DefaultFile = Path.Combine(DefaultConfigRootPath, DefaultOutputFileName);

        public static void WriteData() {
            var yamlContent = new SerializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance).Build()
                .Serialize(ListHumanoids());
            File.WriteAllText(DefaultFile, yamlContent);
            Logger.LogInfo($"wrote yaml content to file '{DefaultFile}'");
        }

        private static Dictionary<string, HumanoidModel> ListHumanoids() {
            Dictionary<string, Humanoid> characters = PrefabManager.Cache.GetPrefabs(typeof(Humanoid))
                .Where(kv => !kv.Key.Contains(CloneString))
                .Where(kv => !kv.Key.Contains(PlayerString))
                .ToDictionary(pair => pair.Key, pair => (Humanoid) pair.Value);

            Dictionary<string, HumanoidModel> output = characters.ToDictionary(pair => pair.Key, pair => {
                string internalName = pair.Value.m_name;
                Character.Faction faction = pair.Value.m_faction;
                string group = pair.Value.m_group;
                float health = pair.Value.m_health;
                HitData.DamageModifiers damageModifiers = pair.Value.m_damageModifiers;
                string defeatSetGlobalKey = pair.Value.m_defeatSetGlobalKey;
                List<string> defaultItems = ExtractItemNameList(pair.Value.m_defaultItems);
                List<string> randomWeapons = ExtractItemNameList(pair.Value.m_randomWeapon);
                List<string> randomArmors = ExtractItemNameList(pair.Value.m_randomArmor);
                List<string> randomShields = ExtractItemNameList(pair.Value.m_randomShield);
                return new HumanoidModel(internalName, faction, group, health, damageModifiers,
                    defeatSetGlobalKey, defaultItems, randomWeapons, randomArmors, randomShields);
            });
            return output;
        }

        private static List<string> ExtractItemNameList(GameObject[] items) {
            return items.Where(item => item != null).Select(item => item.name).ToList();
        }
    }
}