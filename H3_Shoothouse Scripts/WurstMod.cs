using H3_Shoothouse.MappingComponents;
using UnityEngine.SceneManagement;
using WurstMod.MappingComponents.Generic;
using WurstMod.Runtime;
using WurstMod.Runtime.SceneLoaders;
using WurstMod.SceneLoaders;
using WurstMod.UnityEditor;
using WurstMod.UnityEditor.SceneExporters;

namespace H3_Shoothouse
{
    // Loader. Tells WurstMod how to load this game mode.
    // It's got a number of overridable methods but we don't do anything special.
    public class ShoothouseLoader : CustomSceneLoader
    {
        public override string GamemodeId => "nrgill28.shoot_house";
        public override string BaseScene => "ProvingGround";
    }

    // Extend from the sandbox exporter since it's pretty much just that with a could extra validations
    public class ShoothouseExporter : SandboxExporter
    {
        public override string GamemodeId => "nrgill28.shoot_house";

        public override void Validate(Scene scene, CustomScene root, ExportErrors err)
        {
            // Let base validate
            base.Validate(scene, root, err);
            
            RequiredComponents<ShoothouseManager>(1, 1);
            RequiredComponents<ShoothouseStage>(1, int.MaxValue);
            //RequiredComponents<ShoothouseScoreboard>(1, 1);
        }
    }
}