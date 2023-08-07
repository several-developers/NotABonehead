using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameCore
{
    public class SoundManager : MonoBehaviour
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [Title(Constants.Settings)]
        [SerializeField, Min(0)]
        private float _musicVolume = 0.5f;
        
        [SerializeField, Min(0)]
        private float _crossFadeDuration = 0.5f;
        
        [Title(Constants.References)]
        [SerializeField, Required]
        private AudioSource _musicOneSource;
        
        [SerializeField, Required]
        private AudioSource _musicTwoSource;
        
        [SerializeField, Required]
        private AudioSource _interfaceSource;
        
        [SerializeField, Required]
        private AudioSource _battleSource;

        [Title("Clips")]
        [SerializeField, Required]
        private AudioClip _click1;
        
        [SerializeField, Required]
        private AudioClip _click2;

        [SerializeField, Required]
        private AudioClip _hit;

        [SerializeField, Required]
        private AudioClip _victory;

        [SerializeField, Required]
        private AudioClip _defeat;

        [SerializeField, Required]
        private AudioClip _collected;

        [SerializeField, Required]
        private AudioClip _lvlUp;

        // GAME ENGINE METHODS: -------------------------------------------------------------------
        
        private void Awake() =>
            Sounds.Setup(this);

        // PUBLIC METHODS: ------------------------------------------------------------------------

        [Button]
        public void PlayMainMenuMusic()
        {
            _musicOneSource.Play();
            _musicOneSource.DOFade(_musicVolume, _crossFadeDuration);
            _musicTwoSource.DOFade(0, _crossFadeDuration);
        }

        [Button]
        public void PlayBattleMusic()
        {
            _musicTwoSource.Play();
            _musicOneSource.DOFade(0, _crossFadeDuration);
            _musicTwoSource.DOFade(_musicVolume, _crossFadeDuration);
        }

        [Button]
        public void StopMusic()
        {
            _musicOneSource.DOFade(0, _crossFadeDuration);
            _musicTwoSource.DOFade(0, _crossFadeDuration);
        }

        [Button]
        public void PlayClick1() => _interfaceSource.PlayOneShot(_click1);
        
        [Button]
        public void PlayClick2() => _interfaceSource.PlayOneShot(_click2);
        
        [Button]
        public void PlayHit() => _battleSource.PlayOneShot(_hit);
        
        [Button]
        public void PlayVictory() => _interfaceSource.PlayOneShot(_victory);
        
        [Button]
        public void PlayDefeat() => _interfaceSource.PlayOneShot(_defeat);
        
        [Button]
        public void PlayCollected() => _interfaceSource.PlayOneShot(_collected);
        
        [Button]
        public void PlayLvlUp() => _interfaceSource.PlayOneShot(_lvlUp);
    }
}
