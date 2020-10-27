using UnityEngine;
using WurstMod.Runtime;

namespace H3VR_Kill_House.MappingComponents
{
    public class KillHouseStartButton : MonoBehaviour
    {
        public void TargetHit()
        {
            ObjectReferences.CustomScene.GetComponent<KillHouseManager>().StartCountdown();
        }
    }
}