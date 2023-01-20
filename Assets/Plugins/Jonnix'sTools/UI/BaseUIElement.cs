using System;
using UnityEngine;

namespace JonnixTools.UI
{
    public abstract class BaseUIElement : MonoBehaviour, IUIElement
    {
        public event Action OnShow;
        public event Action OnHide;
        public event Action OnInitializable;
        [Sirenix.OdinInspector.PropertyOrder(-1000) ,SerializeField, Tooltip("The RectTransform of the object\nIf left empty it will close the gameobject this script is on")]
        private RectTransform _rectTransform;
        [Sirenix.OdinInspector.PropertyOrder(-1000) ,SerializeField, Tooltip("The GameObjects that will be turning on and off\nIf left empty it will close the gameobject this script is on")]
        private GameObject _holderGameObject;
        public GameObject HolderGameObject
        {
            get
            {
                if (_holderGameObject == null)
                    _holderGameObject = gameObject;
                return _holderGameObject;
            }
        }

        public RectTransform RectTransform
        {
            get
            {
                if (_rectTransform == null)
                {
                    _rectTransform = transform as RectTransform;

                    GetComponent<RectTransform>();
                    if (_rectTransform == null)
                        throw new Exception($"This UI Element {gameObject.name} is not an UI element and need to have a recttransfrom or to have a reference to a recttransform!");

                }
                return _rectTransform;
            }

        }


        public bool IsActive()
            => HolderGameObject.activeSelf || HolderGameObject.activeInHierarchy;
        public virtual void Hide()
        {
            OnHide?.Invoke();
            HolderGameObject.SetActive(false);

        }

        public virtual void Init()
            => OnInitializable?.Invoke();


        public virtual void Show()
        {
            OnShow?.Invoke();
            HolderGameObject.SetActive(true);
        }
    }
}