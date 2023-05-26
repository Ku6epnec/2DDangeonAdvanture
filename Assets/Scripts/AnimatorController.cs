using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MVC
{
    public class AnimatorController : IDisposable
    {
        private sealed class Animation
        {
            public AnimState Track;
            public List<Sprite> Sprites;
            public bool Loop = true;
            public float Speed = 10;
            public float Counter = 0;
            public bool Sleeps;

            public void UpdateAnimation()
            {
                if (Sleeps) return;
                Counter += Time.deltaTime * Speed;

                if (Loop)
                {
                    while (Counter>Sprites.Count)
                    {
                        Counter -= Sprites.Count;
                    }
                }
                else if(Counter>Sprites.Count)
                {
                    Counter = Sprites.Count;
                }
            }
        }
        private SpriteAnimatorConfig _config;
        private Dictionary<SpriteRenderer, Animation> _activateAnimations = new Dictionary<SpriteRenderer, Animation>();

        public AnimatorController(SpriteAnimatorConfig config)
        {
            _config = config;
        }
        
        public void StartAnimation(SpriteRenderer spriteRenderer, AnimState track, bool loop, float speed)
        {
            if(_activateAnimations.TryGetValue(spriteRenderer, out var animation))
            {
                animation.Loop = loop;
                animation.Speed = speed;
                animation.Sleeps = false;
                if(animation.Track!=track)
                {
                    animation.Track = track;
                    animation.Sprites = _config.Sequences.Find(sequence => sequence.Track == track).Sprites;
                    animation.Counter = 0;  
                }
            }
            else
            {
                _activateAnimations.Add(spriteRenderer, new Animation()
                {
                    Track = track,
                    Sprites = _config.Sequences.Find(sequence => sequence.Track == track).Sprites,
                    Loop = loop,
                    Speed = speed
                });
            }
            
        }

        public void StopAnimation(SpriteRenderer sprite)
        {
            if(_activateAnimations.ContainsKey(sprite))
            {
                _activateAnimations.Remove(sprite);
            }
        }

        public void Update()
        {
            foreach (var animation in _activateAnimations)
            {
                animation.Value.UpdateAnimation();
                if(animation.Value.Counter<animation.Value.Sprites.Count)
                {
                    animation.Key.sprite = animation.Value.Sprites[(int)animation.Value.Counter];
                }
            }
        }
        public void Dispose()
        {
            _activateAnimations.Clear();
        }
    }
}
