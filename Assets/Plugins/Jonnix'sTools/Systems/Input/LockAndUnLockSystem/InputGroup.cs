using UnityEngine;

namespace JonnixTools.Systems.InputSystem.LockAndUnlock
{
    [CreateAssetMenu(fileName = "Input Group", menuName = "ScriptableObjects/Input/Input Identification/New Input Group")]
    public class InputGroup : ScriptableObject
    {
        [SerializeField] private InputIdentificationSO[] _inputIDs;

        public InputIdentificationSO[] InputIDs
        {
            get => _inputIDs;
        }
    }
}

