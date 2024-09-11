using System;
using UnityEngine;
using UnityEngine.Events;

namespace Games.TrashSort
{
    public class TrashObject : MonoBehaviour
    {
        #region Private Field

        private TrashType _type;

        #endregion

        #region Actions

        public UnityAction<TrashType> OnSetType;
        public Func<TrashType> OnGetTrashType;

        #endregion

        #region OnEnable

        private void OnEnable()
        {
            OnSetType += SetType;
            OnGetTrashType += GetTrashType;
        }

        #endregion

        #region Functions

        private void SetType(TrashType type)
        {
            _type = type;
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>($"Games/TrashSort/TrashImages/{_type.ToString()}");
        }

        private TrashType GetTrashType()
        {
            return _type;
        }

        #endregion
    }
}