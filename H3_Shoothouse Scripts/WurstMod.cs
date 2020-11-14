using H3_Shoothouse.MappingComponents;
using UnityEngine.SceneManagement;
using WurstMod.MappingComponents.Generic;
using WurstMod.Runtime;
using WurstMod.SceneLoaders;
using WurstMod.UnityEditor;
using WurstMod.UnityEditor.SceneExporters;

namespace H3_Shoothouse
{
    // Yeah we don't do anything special here so...
    [CustomSceneLoader("nrgill28.shoot_house")]
    public class ShoothouseLoader : SandboxSceneLoader
    {
        
    }

    // Extend from the sandbox exporter since it's pretty much just that with a could extra validations
    [SceneExporter("nrgill28.shoot_house")]
    public class ShoothouseExporter : SandboxExporter
    {
        public override void Validate(Scene scene, CustomScene root, ExportErrors err)
        {
            // Let base validate
            base.Validate(scene, root, err);
            
            RequiredComponents<ShoothouseManager>(1, 1);
            RequiredComponents<ShoothouseStage>(1, int.MaxValue);
            RequiredComponents<ShoothouseScoreboard>(1, 1);
        }
    }
}