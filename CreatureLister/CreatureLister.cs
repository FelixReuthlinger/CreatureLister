using BepInEx;
using Jotunn;
using Jotunn.Entities;
using Jotunn.Managers;

namespace CreatureLister {
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    [BepInDependency(Main.ModGuid)]
    internal class CreatureListerPlugin : BaseUnityPlugin {
        public const string PluginGuid = "org.bepinex.plugins.creature.lister";
        public const string PluginName = "CreatureLister";
        public const string PluginVersion = "1.0.0";

        private void Awake() {
            CommandManager.Instance.AddConsoleCommand(new CreatureListerController());
        }
    }

    public class CreatureListerController : ConsoleCommand {
        public override void Run(string[] args) {
            HumanoidLister.WriteData();
        }

        public override string Name => "creature_lister_generate_defaults_file";

        public override string Help =>
            "Write all character based default information to a YAML file inside the BepInEx config folder.";
    }
}