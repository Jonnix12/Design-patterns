using System.Collections.Generic;
using System.Linq;
using JonnixTools.DesignPatterns.Singleton;
using UnityEngine;

namespace JonnixTools.Systems.InputSystem.LockAndUnlock
{
    [DefaultExecutionOrder(-1000)]
    public class LockAndUnlockSystem : MonoSingleton<LockAndUnlockSystem> 
    {
        #region Fields

        [SerializeField] private InputGroup[] _inputGroups;
    
        private  List<ILockable> _touchableItems = new List<ILockable>();
    
        private List<ILockable> _activeTouchableItems = new List<ILockable>();
    
        private InputGroup _currentInputGroup;

        private int _inputGroupIndex;

        #endregion

        public override void Awake()
        {
            base.Awake();
            _inputGroupIndex = 0;
            _currentInputGroup = _inputGroups[_inputGroupIndex];
        }

        #region TouchableItemManagment

        public void AddTouchableItemToList(TouchableItem touchableItem)
        {
            if (touchableItem == null)
                return;

            if (_touchableItems.Contains(touchableItem))
                return;

            _touchableItems.Add(touchableItem);

            UpdateInputState(true);
        }

        public void RemoveTouchableItemFromAllLists(TouchableItem touchableItem)
        {
            if (touchableItem == null)
                return;
        
            if (_activeTouchableItems.Contains(touchableItem))
            {
                _activeTouchableItems.Remove(touchableItem);
            }
        
            _touchableItems.Remove(touchableItem);
        }

        #endregion
        
        #region InputGroup

    public void MoveToNextInputGroup()
    {
        ResetActiveList();
        
        _inputGroupIndex++;
        _currentInputGroup = _inputGroups[_inputGroupIndex];
        
        UpdateInputState(true);
    }

    public void SetNewInputGroup(InputGroup inputGroup)
    {
        ResetActiveList();
        
        _currentInputGroup = inputGroup;
        
        UpdateInputState(true);
    }
    
    #endregion
    
        private void UpdateInputState(bool isLock)
        {
            AddTouchableItemToActiveListByID(_currentInputGroup);
            ChangeTouchableItemsState(_activeTouchableItems.ToArray(),isLock);
        }
        
        private void AddTouchableItemToActiveListByID(InputGroup inputGroup)
        {
            foreach (var touchableItem in _touchableItems.Where(touchableItem => !_activeTouchableItems.Contains(touchableItem)).Where(touchableItem => touchableItem.InputIdentification != null))
            {
                if (inputGroup.InputIDs.Any(inputId => touchableItem.InputIdentification == inputId))
                {
                    _activeTouchableItems.Add(touchableItem);
                }
            }
        }
        
        private bool FindTouchableItemInCurrentInputIDList(ILockable touchableItem)
        {
            if (_currentInputGroup == null)
                return false;
            
            foreach (var inputID in _currentInputGroup.InputIDs)
            {
                if (inputID == touchableItem.InputIdentification)
                {
                    return true;
                }
            }
        
            return false;
        }
        
        private void ResetActiveList()
        {
            if (_currentInputGroup != null)
                ChangeTouchableItemsState(_activeTouchableItems.ToArray(),false);
            
            _activeTouchableItems.Clear();
        }
        
        #region LockAndUnlockAll
        
        public void ChangeTouchableItemsState(ILockable[] touchableItems,bool isTouchable)
        {
            foreach (var lockable in touchableItems)
            {
                if (lockable == null)
                    continue;
                if (isTouchable && lockable.IsUnlock)
                    continue;
                if (!isTouchable && !lockable.IsUnlock)
                    continue;
                
                if (isTouchable)
                    lockable.UnLock();
                else
                    lockable.Lock();
            }
        }
        
        public void ChangeAllTouchableItemsState(bool isTouchable)
        {
            foreach (var lockable in _touchableItems.Where(lockable => lockable != null))
            {
                if (isTouchable)
                    lockable.UnLock();
                else
                    lockable.Lock();
            }
        }
        
        public void ChangeAllTouchableItemsStateExcept(bool isTouchable,params ILockable[] exceptTouchableItem)
        {
            foreach (var lockable in _touchableItems)
            {
                bool isExcept = false;
                
                if (lockable == null)
                    continue;
        
                for (int j = 0; j < exceptTouchableItem.Length; j++)
                {
                    isExcept = lockable == exceptTouchableItem[j];
                    
                    if (isExcept)
                        break;  
                }
                
                if (!isExcept)
                    continue;  
                
                if (isTouchable)
                    lockable.UnLock();
                else
                    lockable.Lock();
            }
        }
        
        #endregion
        
    }
}       
        
        