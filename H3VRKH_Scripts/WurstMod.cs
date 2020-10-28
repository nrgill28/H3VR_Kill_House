using H3VR_Kill_House.MappingComponents;
using UnityEngine.SceneManagement;
using WurstMod.MappingComponents.Generic;
using WurstMod.Runtime;
using WurstMod.SceneLoaders;
using WurstMod.UnityEditor;
using WurstMod.UnityEditor.SceneExporters;

namespace H3VR_Kill_House
{
    // Yeah we don't do anything special here so...
    [CustomSceneLoader("nrgill28.kill_house")]
    public class KillHouseLoader : SandboxSceneLoader
    {
        
    }

    // Extend from the sandbox exporter since it's pretty much just that with a could extra validations
    [SceneExporter("nrgill28.kill_house")]
    public class KillHouseExporter : SandboxExporter
    {
        public override void Validate(Scene scene, CustomScene root, ExportErrors err)
        {
            // Let base validate
            base.Validate(scene, root, err);
            
            RequiredComponents<KillHouseManager>(1, 1);
            RequiredComponents<KillHouseStage>(1, int.MaxValue);
        }
    }
}