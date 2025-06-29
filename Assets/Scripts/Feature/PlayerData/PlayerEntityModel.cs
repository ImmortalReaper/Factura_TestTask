using System;
using UnityEngine;

namespace ShootingCar.Feature.PlayerData
{
    public class PlayerEntityModel
    {
        private GameObject _playerEntity;

        public GameObject PlayerEntity {
            get =>
                _playerEntity;
            set {
                if(_playerEntity == value)
                    return;
                
                _playerEntity = value;
                OnPlayerEntityChanged?.Invoke();
            }
        }

        public event Action OnPlayerEntityChanged;
    }
}
