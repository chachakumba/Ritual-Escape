using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;
public class Player : MonoBehaviour
{
    public float mouseSensitivityX = 1.0f;
    public float mouseSensitivityY = 1.0f;
    public float walkSpeed = 3.5f;
    public float runSpeed = 7f;
    public float stamina = 100;
    public float maxStamina = 100;
    public float staminaAdder = 2;
    public float staminaAdderCharging = 3;
    public float staminaReducer = 1;
    [SerializeField] float maxHeadRot = 60;
    [SerializeField] float minHeadRot = -60;
    [SerializeField] Transform head;
    Transform body;
    [SerializeField] Collider demonCol;

    Rigidbody _rigidbody;

    Vector3 _moveAmount;
    Vector3 _smoothMoveVelocity;
    bool staminaRecharging = false;
    float _verticalLookRotation;
    Manager manager;
    float currentSpeed;

    Controls controls;
    bool moving = false;
    public UnityEngine.Events.UnityEvent OnUse;
    public UnityEngine.Events.UnityEvent OnUnUse;
    Coroutine stunCour;
    public Item carriedItem;
    public float itemPickUpDist = 3;
    Coroutine runCour;
    Coroutine staminaChargeCour;

    public bool hidden = true;
    public HidingSpot hiddenIn;
    //scrolls
    public float stunDuration = 10;
    bool usingStaminaScroll = false;
    public float staminaScrollDuration = 5;
    [Header("UI")]
    public Slider staminaSlider;
    public Image heldItemImage;
    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }
    private void Awake()
    {
        carriedItem = Item.none;
        controls = new Controls();
        controls.Basic.Shout.performed += (ctx) => Shout();
        controls.Basic.Move.performed += (ctx) => moving = true;
        controls.Basic.Move.canceled += (ctx) => moving = false;
        controls.Basic.Look.performed += (ctx) => MouseLook(controls.Basic.Look.ReadValue<Vector2>());
        controls.Basic.Run.performed += (ctx) => Run();
        controls.Basic.Run.canceled += (ctx) => StopRun();
        controls.Basic.Use.performed += (ctx) => Interract();
        controls.Basic.Use.canceled += (ctx) => UnInterract();
        controls.Basic.UseItem.performed += (ctx) => UseItem();
        manager = Manager.instance;
        Manager.instance.player = this;
        _rigidbody = GetComponent<Rigidbody>();
        currentSpeed = walkSpeed;
    }
    void Start()
    {
        body = transform;
        LockMouse();
    }
    void FixedUpdate()
    {
        //body.Translate((body.right * Input.GetAxis("Horizontal") + body.forward * Input.GetAxis("Vertical")).normalized * currentSpeed * Time.deltaTime, Space.World);
        if (moving)
        {
            body.Translate(new Vector3(controls.Basic.Move.ReadValue<Vector2>().x, 0, controls.Basic.Move.ReadValue<Vector2>().y).normalized * Time.deltaTime * currentSpeed, Space.Self);
        }
    }
    void MouseLook(Vector2 delta)
    {
        body.Rotate(Vector3.up * delta.x * mouseSensitivityX);
        _verticalLookRotation += delta.y * mouseSensitivityY;
        _verticalLookRotation = Mathf.Clamp(_verticalLookRotation, minHeadRot, maxHeadRot);
        head.localEulerAngles = Vector3.left * _verticalLookRotation;
    }
    void UnlockMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        manager._cursorVisible = true;
    }

    void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
        manager._cursorVisible = false;
    }
    void Shout()
    {
        manager.InvokeOnSound(new SoundArgs(transform.position - Vector3.up, 100));
    }
    void Interract()
    {
        OnUse.Invoke();
    }
    void UnInterract()
    {
        OnUnUse.Invoke();
    }
    public void Stun(float time)
    {
        controls.Basic.Move.Disable();
        stunCour = StartCoroutine(StunCour(time));
    }
    public void Stun()
    {
        controls.Basic.Move.Disable();
    }
    IEnumerator StunCour(float stunTime)
    {
        yield return new WaitForSeconds(stunTime);
        controls.Basic.Move.Enable();
    }
    public void Unstan()
    {
        if(stunCour != null)
        {
            StopCoroutine(stunCour);
        }
        controls.Basic.Move.Enable();
    }
    public void GetItem(Item newItem)
    {
        carriedItem = newItem;
    }
    public void UseItem()
    {
        switch (carriedItem)
        {
            case Item.stunScroll:
                UseStunScroll();
                carriedItem = Item.none;
                break;
            case Item.staminaScroll:
                UseStaminaScroll();
                carriedItem = Item.none;
                break;
            default:

                break;
        }
    }
    void UseStunScroll()
    {
        Manager.instance.StunDemon(stunDuration);
    }
    void UseStaminaScroll()
    {
        stamina = maxStamina;
        usingStaminaScroll = true;

        if (staminaChargeCour != null)
            StopCoroutine(staminaChargeCour);
        if (runCour != null)
            StopCoroutine(runCour);

        StartCoroutine(StaminaScrollCour());
    }
    IEnumerator StaminaScrollCour()
    {
        yield return new WaitForSeconds(staminaScrollDuration);
        if (currentSpeed == runSpeed) Run();
    }
    void Run()
    {
        if (usingStaminaScroll)
        {
            currentSpeed = runSpeed;
        }
        else if (!staminaRecharging)
        {
            if (staminaChargeCour != null)
            {
                StopCoroutine(staminaChargeCour);
            }
            currentSpeed = runSpeed;
            if (runCour != null) StopCoroutine(runCour);
            runCour = StartCoroutine(RunCour());
        }
    }
    IEnumerator RunCour()
    {
        while (stamina >=0)
        {
            stamina -= Time.deltaTime * staminaReducer;
            yield return null;
        }
        stamina = 0;
        staminaRecharging = true;
        currentSpeed = walkSpeed;
    }
    void StopRun()
    {
        if(runCour != null)
        {
            StopCoroutine(runCour);
        }
        currentSpeed = walkSpeed;
        if (staminaChargeCour != null)
            StopCoroutine(staminaChargeCour);
        staminaChargeCour = StartCoroutine(StaminaRecharger());
    }
    IEnumerator StaminaRecharger()
    {
        while (stamina < maxStamina)
        {
            if (staminaRecharging)
                stamina += Time.deltaTime * staminaAdder;
            else
                stamina += Time.deltaTime * staminaAdderCharging;
            yield return null;
        }
        stamina = maxStamina;
        staminaRecharging = false;
    }
    public void Hide(HidingSpot spot)
    {
        hiddenIn = spot;
        hidden = true;
        controls.Disable();
        demonCol.enabled = false;
    }
    public void UnHide()
    {
        demonCol.enabled = true;
        hidden = false;
        controls.Enable();
    }
}
public enum Item
{
    none,
    stunScroll,
    staminaScroll,
    insightScroll
}
