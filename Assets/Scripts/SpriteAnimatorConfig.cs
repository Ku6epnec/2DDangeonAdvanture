using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVC
{
    [CreateAssetMenu(fileName = "SpriteAnimatorCfg", menuName = "AnimatorConfigs", order = 1)]
    public class SpriteAnimatorConfig : ScriptableObject
    {
        [Serializable]
        public sealed class SpriteSequence
        {
            public AnimState Track;
            public List<Sprite> Sprites = new List<Sprite>();
        }

        public List<SpriteSequence> Sequences = new List<SpriteSequence>();
    }
}
