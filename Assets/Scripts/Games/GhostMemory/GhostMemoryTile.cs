using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Games.GhostMemory
{
    public class GhostMemoryTile : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer pictureSpriteRenderer;
        [SerializeField] private List<Sprite> pictures;
        public bool isSelectable;

        public void Mark()
        {
            pictureSpriteRenderer.sprite = pictures[Random.Range(0, pictures.Count)];
            pictureSpriteRenderer.color = Color.white;
            pictureSpriteRenderer.size = Vector2.one * 3;
            StartCoroutine(ShowPictureAndHide());
        }

        private void Start()
        {
            StartCoroutine(SetSelectableAfterDelay(GhostMemorySignals.Instance.OnGetHidePicturesDelay()));
        }

        IEnumerator SetSelectableAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            isSelectable = true;
        }
        IEnumerator ShowPictureAndHide()
        {
            pictureSpriteRenderer.enabled = true;
            yield return new WaitForSeconds(GhostMemorySignals.Instance.OnGetHidePicturesDelay());
            pictureSpriteRenderer.enabled = false;
        }

        private void OnMouseDown()
        {
            if (!isSelectable) return;

            Select();
        }

        private void Select()
        {
            isSelectable = false;
            pictureSpriteRenderer.enabled = true;
            GhostMemorySignals.Instance.OnTileSelected?.Invoke(this);
        }
    }
}
