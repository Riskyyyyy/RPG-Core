﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Station
{
    public class UiBaseContainer : MonoBehaviour
    {
        [SerializeField] protected UiDragSlotDummy _dragDummy = null;
        protected ContainerReference _containerReference;
        protected BaseItemContainer _container;
        protected ItemsDb _itemDb;
        private bool Initialized;
        
        public UiDragSlotDummy GetDragDummy()
        {
            return _dragDummy;
        }

        public ContainerReference GetContainerReference()
        {
            return _containerReference;
        }

        public void RegisterEvents()
        {
            var container = _containerReference.GetContainer();
            if (container == null)
            {
                Debug.LogWarning($"could not find the container");
            }
            else
            {
                container.OnContentChanged += UpdateUiSlots; 
            }
       
        }

        public void UnregisterEvents()
        {
            if (_containerReference != null)
            {
                _containerReference.GetContainer().OnContentChanged -= UpdateUiSlots;
            }
        }
        

        public void Init(ContainerReference reference)
        {
            UnregisterEvents();
            _containerReference = reference;
            _itemDb = GameInstance.GetDb<ItemsDb>();
            if (Initialized == false)
            {
                CacheComponents();
                Initialized = true;
            }
        }

        protected virtual void CacheComponents()
        {
        }

        public virtual void UpdateUiSlots()
        {
        }
    }

}
