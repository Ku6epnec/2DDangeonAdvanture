using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVC
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private SpriteAnimatorConfig playerConfig;
        [SerializeField] private int _animationSpeed = 10;
        [SerializeField] private LevelObjectView _playerView;

        private AnimatorController _playerAnimator;

        void Awake()
        {
            playerConfig = Resources.Load<SpriteAnimatorConfig>("AnimPlayerCfg");
            _playerAnimator = new AnimatorController(playerConfig);
            _playerAnimator.StartAnimation(_playerView._spriteRenderer, AnimState.Run, true, _animationSpeed);

        }

        void Update()
        {
            _playerAnimator.Update();
        }
        private void FixedUpdate()
        {

        }
    }
}
