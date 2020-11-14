using H3_Shoothouse.Classes;
using UnityEngine;

namespace H3_Shoothouse.MappingComponents
{
    [RequireComponent(typeof(MovableObject))]
    public class ShoothouseDoor : MonoBehaviour
    {
        public MovableObject Move;

        public void Open() => Move.MoveTo(1f);

        public void Close() => Move.MoveTo(0f);
    }
}