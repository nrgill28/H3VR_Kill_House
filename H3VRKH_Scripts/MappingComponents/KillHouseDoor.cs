using H3VR_Kill_House.Classes;
using UnityEngine;

namespace H3VR_Kill_House.MappingComponents
{
    [RequireComponent(typeof(MovableObject))]
    public class KillHouseDoor : MonoBehaviour
    {
        public MovableObject Move;

        public void Open() => Move.MoveTo(1f);

        public void Close() => Move.MoveTo(0f);
    }
}