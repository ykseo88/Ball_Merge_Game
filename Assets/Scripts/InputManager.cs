using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private InputActionAsset inputActions;

    private InputAction touchDragAction;
    private InputAction touchPressAction;

    public Vector2 _touchPosition;
    private bool _isTouched;

    public Vector2 TouchPosition => _touchPosition;
    public bool IsTouched => _isTouched;

    private void Awake()
    {
        // "TouchMap" 액션 맵을 찾습니다.
        InputActionMap touchMap = inputActions.FindActionMap("TouchMap");
        if (touchMap == null)
        {
            Debug.LogError("TouchMap not found in the provided InputActionAsset.");
            return;
        }

        // 각 액션을 찾습니다.
        touchDragAction = touchMap.FindAction("Drag");
        touchPressAction = touchMap.FindAction("Press");

        if (touchDragAction == null || touchPressAction == null)
        {
            Debug.LogError("One or more actions (Drag, Press) not found in TouchMap.");
            return;
        }
    }

    private void OnEnable()
    {
        // 터치 시작/종료 이벤트를 구독합니다.
        touchPressAction.started += OnTouchStarted;
        touchPressAction.canceled += OnTouchCanceled;

        // 터치 위치를 실시간으로 읽기 위해 액션을 활성화합니다.
        touchDragAction.Enable();
        touchPressAction.Enable();
    }

    private void OnDisable()
    {
        // 이벤트 구독을 해제합니다.
        touchPressAction.started -= OnTouchStarted;
        touchPressAction.canceled -= OnTouchCanceled;

        // 액션을 비활성화합니다.
        touchDragAction.Disable();
        touchPressAction.Disable();
    }

    public Vector3 convertPosition;
    
    private void Update()
    {
        // 매 프레임 Drag 액션의 현재 값을 읽어와 터치 위치를 업데이트합니다.
        Vector2 temp = touchDragAction.ReadValue<Vector2>();
        
        convertPosition = Camera.main.ScreenToWorldPoint(new Vector3(temp.x, temp.y, Camera.main.transform.position.z));
        

    }

    private void OnTouchStarted(InputAction.CallbackContext context)
    {
        _isTouched = true;
        Debug.Log("Touch Started at: " + context.ReadValue<float>());
    }

    private void OnTouchCanceled(InputAction.CallbackContext context)
    {
        _isTouched = false;
        Debug.Log("Touch Canceled.");
    }
}
