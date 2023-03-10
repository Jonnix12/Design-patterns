using System;
using JonnixTools.DesignPatterns.Singleton;
using UnityEngine;


namespace JonnixTools.Systems.InputSystem
{
    [DefaultExecutionOrder(-999)]
public class InputReciever : MonoSingleton<InputReciever>
{
    #region Events

    public Touch? PlayerTouch { get; set; }
    public event Action<Vector2> OnTouchDetectedLocation;
    public event Action<Vector2> OnTouchEnded;
    public event Action<Vector2> OnTouchStart;
    public event Action<SwipeData> OnSwipeDetected;
    public event Action OnTouchDetected;

    #endregion
    
    #region Fields
    
    private const float CIRCLE_BORDER_TOP_LEFT = 0.25F;
    private const float CIRCLE_BORDER_TOP_RIGHT = -0.25F;
    private const float CIRCLE_BORDER_BUTTOM_RIGHT = -0.75F;
    private const float CIRCLE_BORDER_BUTTOM_LEFT = 0.75F;

    [SerializeField] private float _swipeDistance;
    [SerializeField] private bool _onlyDetectSwipeAtEnd;
    
    private Vector2 _touchPosOnScreen;
    private Vector2 _startTouchLocation;
    private Vector2 _endTouchLocation;
    
    private bool _isTouching = false;

    private bool _swipeDetected = false;
    
    private Camera _camera;

    public enum SwipeDirection
    {
        Up,
        Down,
        Left,
        Right
    };

    #endregion

    #region Prop

    public bool IsTouching
    {
        get => _isTouching;
    }
    
    public Vector2 TouchPosOnScreen
    {
        get => _touchPosOnScreen;
    }

    #endregion

    #region Monobehaiviour CallBacks

    public override void Awake()
    {
        base.Awake();
        _camera = Camera.main;
    }

    private void Update()
    {
        TouchDetector();
        //MouseDetector();
    }

    #endregion

    #region Private Functions

    private void MouseDetector()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _startTouchLocation = _camera.ScreenToWorldPoint(Input.mousePosition);
            OnTouchStart?.Invoke(_startTouchLocation);
        }

        if (Input.GetMouseButton(0))
        {
            _touchPosOnScreen = _camera.ScreenToWorldPoint(Input.mousePosition);
            OnTouchDetectedLocation?.Invoke(_touchPosOnScreen);
            OnTouchDetected?.Invoke();
        }

        if (Input.GetMouseButtonUp(0))
        {
            _touchPosOnScreen = _camera.ScreenToWorldPoint(Input.mousePosition);
            OnTouchEnded?.Invoke(_touchPosOnScreen);
        }
    }

    private void TouchDetector()
    {
        if (Input.touchCount > 0)
        {
            _isTouching = true;
            
            PlayerTouch = Input.GetTouch(0);

            if (PlayerTouch == null)
                return;

            GetTouchPhase(PlayerTouch.Value);

            return;
        }
        
        _isTouching = false;
    }

    private void GetTouchPhase(Touch touch)
    {
        switch (touch.phase)
        {
            case TouchPhase.Began:
                _startTouchLocation = touch.position;
                _touchPosOnScreen = touch.position;
                _endTouchLocation = touch.position;
                OnTouchStart?.Invoke(_startTouchLocation);
                OnTouchDetected?.Invoke();
                break;
            case TouchPhase.Moved:
                _touchPosOnScreen = touch.position;
                if (!_onlyDetectSwipeAtEnd)
                    SwipeDetector(_startTouchLocation,_touchPosOnScreen);
                OnTouchDetectedLocation?.Invoke(_touchPosOnScreen);
                break;
            case TouchPhase.Ended:
                _endTouchLocation = touch.position;
                _touchPosOnScreen = touch.position;
                if (_onlyDetectSwipeAtEnd)
                    SwipeDetector(_startTouchLocation,_endTouchLocation);
                _swipeDetected = false;
                OnTouchEnded?.Invoke(_endTouchLocation);
                break;
            case TouchPhase.Canceled:
                break;
            case TouchPhase.Stationary:
                _touchPosOnScreen = touch.position;
                OnTouchDetectedLocation?.Invoke(_touchPosOnScreen);
                break;
        }
    }

    private void SwipeDetector(Vector2 start, Vector2 end)
    {
        if (PlayerTouch == null)
            return;

        if (Vector2.Distance(start,end) > _swipeDistance && !_swipeDetected)
        {
            _swipeDetected = true;
            
            SwipeData swipeData;

            swipeData.SwipeStartPosition = start;
            swipeData.SwipeEndPosition = end;
            
            swipeData.SwipeDirection = SwipeDirectionCalculation(start,end);
            
            OnSwipeDetected?.Invoke(swipeData);
        }
    }

    private SwipeDirection SwipeDirectionCalculation(Vector2 start, Vector2 end)
    {
        Vector2 dir = (end - start).normalized;

        float dirAngle = Vector2.SignedAngle(Vector2.up, dir) / 180;

        if (CIRCLE_BORDER_TOP_LEFT > dirAngle && dirAngle > CIRCLE_BORDER_TOP_RIGHT)
            return SwipeDirection.Up;
        if (CIRCLE_BORDER_TOP_RIGHT > dirAngle && dirAngle > CIRCLE_BORDER_BUTTOM_RIGHT)
            return SwipeDirection.Right;
        if (CIRCLE_BORDER_BUTTOM_RIGHT > dirAngle || dirAngle > CIRCLE_BORDER_BUTTOM_LEFT)
            return SwipeDirection.Down;
        if (CIRCLE_BORDER_BUTTOM_LEFT > dirAngle && dirAngle > CIRCLE_BORDER_TOP_LEFT)
            return SwipeDirection.Left;

        Debug.LogWarning("Swipe Error Cant Find Swipe Direction");
        return 0;
    }

    #endregion
}
public struct SwipeData
{
    public Vector2 SwipeStartPosition;
    public Vector2 SwipeEndPosition;
    public InputReciever.SwipeDirection SwipeDirection;
}

}

