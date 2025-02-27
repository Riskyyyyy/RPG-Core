
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Station
{
    public class UiInteractionPopup : UiPopup
    {
        #region FIELDS

        public const string POPUP_ID = "interaction_popup";
        
        [SerializeField] private TextMeshProUGUI _nameText = null;
        [SerializeField] private TextMeshProUGUI _roleText = null;

        [SerializeField] private LayoutGroup _entriesRoot = null;
        [SerializeField] private UiButton _prefabEntry = null;
        
        private GenericUiList<InteractionLine, UiButton> _entriesList;

        
        #endregion

        protected override void Awake()
        {
            base.Awake();
            _entriesList = new GenericUiList<InteractionLine, UiButton>(_prefabEntry.gameObject, _entriesRoot);
        }

        public void SetData(BaseCharacter owner, BaseCharacter demander, List<InteractionLine> interactions)
        {
            _entriesList.Generate(interactions, (data, button) =>
            {
                if (data.CanTrigger(demander))
                {
                    button.SetName(data.GetUnLockedLocalization());
                }
                else
                {
                    button.SetName(data.GetLockedLocalization());
                }
            });
        }
        
        public void OnDeselect()
        {
            Debug.Log("deselect");
            //Mouse was clicked outside
            Hide();
        }
    }

}
