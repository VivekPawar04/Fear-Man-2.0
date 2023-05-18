using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityStandardAssets.CrossPlatformInput;

public class JoystickControll : MonoBehaviour, IDragHandler,IPointerUpHandler, IPointerDownHandler, IPointerEnterHandler {


    private Image joystickBorder;
    private Image joystickCircle;
    private Vector2 inputVector;
    private RectTransform rectTransform;
    private Vector3 actualtPosition;
    CrossPlatformInputManager.VirtualAxis m_HorizontalVirtualAxis; // Reference to the joystick in the cross platform input
    CrossPlatformInputManager.VirtualAxis m_VerticalVirtualAxis; // Reference to the joystick in the cross platform input
    public string horizontalAxisName = "Horizontal"; // The name given to the horizontal axis for the cross platform input
    public string verticalAxisName = "Vertical"; // The name given to the vertical axis for the cross platform input
    public bool isMoving = false;

    void OnEnable()
    {
        CreateVirtualAxes();
    }



    void CreateVirtualAxes()
    {
        m_VerticalVirtualAxis = new CrossPlatformInputManager.VirtualAxis(verticalAxisName);
        CrossPlatformInputManager.RegisterVirtualAxis(m_VerticalVirtualAxis);

        m_HorizontalVirtualAxis = new CrossPlatformInputManager.VirtualAxis(horizontalAxisName);
        CrossPlatformInputManager.RegisterVirtualAxis(m_HorizontalVirtualAxis);
    }

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        actualtPosition = rectTransform.position;

        joystickBorder = GetComponent<Image>();
        joystickCircle = transform.GetChild(0).GetComponent<Image>();


    }

    public virtual void OnPointerEnter(PointerEventData stick)
    {
        if (isMoving)
        {
            rectTransform.position = stick.position;
        }
    }

    public virtual void OnPointerDown(PointerEventData stick)
    {
        OnDrag(stick);
    }


    public virtual void OnPointerUp(PointerEventData stick)
    {
        if (isMoving)
        {
            rectTransform.position += actualtPosition;
        }

        inputVector = Vector2.zero;
        UpdateAxis(inputVector);
        joystickCircle.rectTransform.anchoredPosition = Vector2.zero;
    }

    //private void OnApplicationPause(bool pause)
    //{
    //    inputVector = Vector2.zero;
    //    UpdateAxis(inputVector);
    //    joystickCircle.rectTransform.anchoredPosition = Vector2.zero;
    //}

    public virtual void OnDrag(PointerEventData stick)
    {
        Vector2 pos;
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBorder.rectTransform,stick.position,stick.pressEventCamera, out pos))
        {
            pos.x = (pos.x / joystickBorder.rectTransform.sizeDelta.x);
            pos.y = (pos.y / joystickBorder.rectTransform.sizeDelta.y);

            inputVector = new Vector2(pos.x , pos.y );
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            joystickCircle.rectTransform.anchoredPosition = new Vector2(inputVector.x*(joystickBorder.rectTransform.sizeDelta.x/2), inputVector.y * (joystickBorder.rectTransform.sizeDelta.y / 2));
        }

        UpdateAxis(inputVector);

    }

    private void UpdateAxis(Vector2 axis)
    {
        m_HorizontalVirtualAxis.Update(axis.x);
        m_VerticalVirtualAxis.Update(axis.y);
    }

    void OnDisable()
    {
       
            m_HorizontalVirtualAxis.Remove();     
            m_VerticalVirtualAxis.Remove();
        
    }

}
