using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Managers
{
    public class InputManager : Singleton<InputManager>
    {
        [SerializeField] private InputActionAsset inputActionAsset;
        [SerializeField] private Vector2 dragOffset;
        
        private InputAction _clickAction;
        private HexNodeGroup _controllingGroup;

        private void OnEnable()
        {
            inputActionAsset.FindActionMap("Default").Enable();
        }

        private void OnDisable()
        {
            inputActionAsset.FindActionMap("Default").Disable();
        }

        protected override void Awake()
        {
            base.Awake();
            _clickAction = InputSystem.actions.FindAction("Click");
        }
        
        private void Update()
        {
            if (_clickAction.WasPressedThisFrame()) StartDrag();
            if (_clickAction.IsPressed()) UpdateDrag();
            if (_clickAction.WasReleasedThisFrame()) EndDrag();
        }

        private void StartDrag()
        {
            Vector2 worldPos = GetPointerWorldPos();

            Collider2D hit = Physics2D.OverlapPoint(worldPos);
            if (hit != null)
            {
                GameObject nodeObj = hit.gameObject;
                GameObject groupObj = nodeObj.transform.parent.gameObject;
                HexNodeGroup nodeGroup = groupObj.GetComponent<HexNodeGroup>();
                if (nodeGroup == null) return;
                nodeGroup.StartDrag();
                _controllingGroup = nodeGroup;
            }
        }

        private void UpdateDrag()
        {
            if (_controllingGroup == null) return;
            Vector2 worldPos = GetPointerWorldPos();
            _controllingGroup.UpdateDrag(worldPos + dragOffset);
        }

        private void EndDrag()
        {
            if (_controllingGroup == null) return;
            _controllingGroup.EndDrag();
            _controllingGroup = null;
        }

        private Vector2 GetPointerWorldPos()
        {
            Vector2 screenPos = Pointer.current.position.ReadValue();
            return Camera.main.ScreenToWorldPoint(screenPos);
        }
    }
}