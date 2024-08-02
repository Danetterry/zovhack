using System.Numerics;

namespace zovhack
{
    public class Entity
    {
        public string name { get; set; }

        public Vector3 position { get; set; }

        public Vector3 viewOffset { get; set; }

        public Vector3 origin { get; set; }

        public Vector3 view { get; set; }

        public Vector2 position2D { get; set; }

        public Vector2 viewPosition2D { get; set; }

        public Vector2 head { get; set; }

        public Vector2 head2d { get; set; }

        public float distance { get; set; }

        public uint lifeState { get; set; }

        public int team { get; set; }

        public int health { get; set; }

        public short currentWeaponIndex { get; set; }

        public string currentWeaponName { get; set; }
    }
}
