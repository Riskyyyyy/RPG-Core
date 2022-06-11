﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Station
{
    public class UiCharacterHotBarSwitcher : MonoBehaviour
    {
        #region [[ FIELDS ]]

        [SerializeField] private UiCharacterHotBar _memberPrefab = null;
        [SerializeField] private Transform _root = null;

        private List<UiCharacterHotBar> _member = new List<UiCharacterHotBar>();
        private Dictionary<BaseCharacter, UiCharacterHotBar> _map = new Dictionary<BaseCharacter, UiCharacterHotBar>();
        private TeamSystem _teamsystem;
        #endregion

        public void Awake()
        {
            _teamsystem = GameInstance.GetSystem<TeamSystem>();
            foreach (var member in _teamsystem.GetTeamMembers())
            {
                OnMemberAdded(member);
            }

            OnLeaderChanged(_teamsystem.GetTeamMembers().FirstOrDefault());
            
            RpgGameGlobalEvents.OnCharacterAdded.AddListener(OnMemberAdded);
            RpgGameGlobalEvents.OnCharacterRemoved.AddListener(OnMemberRemoved);
            RpgGameGlobalEvents.OnLeaderChanged.AddListener(OnLeaderChanged);
        }

        private void OnDestroy()
        {
            RpgGameGlobalEvents.OnCharacterAdded.RemoveListener(OnMemberAdded);
            RpgGameGlobalEvents.OnCharacterRemoved.RemoveListener(OnMemberRemoved);
            RpgGameGlobalEvents.OnLeaderChanged.RemoveListener(OnLeaderChanged);
        }

        #region DELEGATES

        private void OnMemberAdded(BaseCharacter member)
        {
            var instance = Instantiate(_memberPrefab, _root);
            _member.Add(instance);
            instance.OnCreate(member);
            _map.Add(member, instance);
        }

        private void OnMemberRemoved(BaseCharacter member)
        {
            var uiObject = _map[member];
            uiObject.OnCancel();
            _member.Remove(uiObject);
            _map.Remove(member);
            Destroy(uiObject.gameObject);
        }

        private void OnLeaderChanged(BaseCharacter leader)
        {
            foreach (var entry in _map)
            {
                if (entry.Key == leader)
                {
                    entry.Value.Select();
                }
                else
                {
                    entry.Value.Unselect();
                }
            }
        }
        #endregion
    }
}

