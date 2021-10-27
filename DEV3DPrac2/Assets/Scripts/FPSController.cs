using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    float mYaw;
    float mPitch;
    [Header("Rotation")]
    [SerializeField]
    float mSpeedYaw;
    [SerializeField]
    float mSpeedPitch;
    [SerializeField]
    float mMinPitch;
    [SerializeField]
    float mMaxPitch;
    [SerializeField]
    GameObject mPitchController;
    [SerializeField]
    bool mInvertPitch;
    [SerializeField]
    bool mInvertYaw;
    [Header("Move")]
    [SerializeField]
    CharacterController mCharacterController;
    [SerializeField]
    float mMoveSpeed;
    [SerializeField]
    KeyCode mFordwardKey;
    [SerializeField]
    KeyCode mBackKey;
    [SerializeField]
    KeyCode mRightKey;
    [SerializeField]
    KeyCode mLeftKey;
    [SerializeField]
    KeyCode mJumpKey;
    [SerializeField]
    KeyCode mRunKey;
    [SerializeField]
    float mRunMultiplier;
    bool mOnGround;
    float mVerticalSpeed;
    bool mContactCeiling;
    float mxMouseAxis;
    float myMouseAxis;
    Vector3 mLMovement;
    float mGravity;
    float mMaxSpeed;
    [Header("Jump")]
    [SerializeField]
    float mMaxHeight;
    [SerializeField]
    float mLateralDistance;
    bool mJump;
    bool canJump = true;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //Bloquea raton en sitio
        Cursor.visible = false; //Hace raton invisible
        mJump = false; //Variante de salto en falso
    }
    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }

    private void Awake()
    {
        mYaw = transform.rotation.eulerAngles.y;
        mPitch = mPitchController.transform.rotation.eulerAngles.x;

        mMaxSpeed = mRunMultiplier * mMoveSpeed; //Calcula la velocidad maxima del player

        mGravity = -2 * mMaxHeight * mMaxSpeed * mMaxSpeed / mLateralDistance / mLateralDistance; //Calcula la gravedad en base a la velocidad maxima del jugador, la altura maxima de salto y la distancia de salto
    }
    private void Update()
    {
        mxMouseAxis = Input.GetAxis("Mouse X");
        myMouseAxis = Input.GetAxis("Mouse Y");

        Vector3 forward = new Vector3(Mathf.Sin(mYaw * Mathf.Deg2Rad), 0.0f, Mathf.Cos(mYaw * Mathf.Deg2Rad));
        Vector3 right = new Vector3(Mathf.Sin((mYaw + 90.0f) * Mathf.Deg2Rad), 0.0f, Mathf.Cos((mYaw + 90.0f) * Mathf.Deg2Rad));

        if (Input.GetKey(mFordwardKey)) mLMovement = forward;
        else if (Input.GetKey(mBackKey)) mLMovement -= forward;
        if (Input.GetKey(mRightKey)) mLMovement += right;
        else if (Input.GetKey(mLeftKey)) mLMovement -= right;

        if (canJump && mOnGround && Input.GetKeyDown(mJumpKey)) mJump = true;
    }
    private void FixedUpdate()
    {
        Rotate();
        Move();
    }

    private void Rotate()
    {
        float xMouseAxis = mxMouseAxis;
        float yMouseAxis = myMouseAxis;
        mYaw += xMouseAxis * mSpeedYaw * (mInvertYaw ? -1 : 1);
        mPitch -= yMouseAxis * mSpeedPitch * (mInvertPitch ? -1 : 1);
        mPitch = Mathf.Clamp(mPitch, mMinPitch, mMaxPitch);
        transform.rotation = Quaternion.Euler(0.0f, mYaw, 0.0f);
        mPitchController.transform.localRotation = Quaternion.Euler(mPitch, 0.0f, 0.0f);
        mxMouseAxis = 0;
        myMouseAxis = 0;
    }

    private void Move()
    {

        Vector3 lMovement = new Vector3();

        lMovement = mLMovement;

        if (mJump)
        {
            mJump = false;
            mVerticalSpeed = 2 * mMaxHeight * mMaxSpeed / mLateralDistance;
        }

        lMovement.Normalize();

        lMovement *= mMoveSpeed
                    * Time.deltaTime
                    * (Input.GetKey(mRunKey) ? mRunMultiplier : 1.0f);
        mVerticalSpeed += mGravity * Time.deltaTime;

        lMovement.y = mVerticalSpeed * Time.deltaTime;

        CollisionFlags colls = mCharacterController.Move(lMovement);

        mOnGround = (colls & CollisionFlags.Below) != 0;
        mContactCeiling = (colls & CollisionFlags.Above) != 0;

        if (mOnGround) mVerticalSpeed = 0.0f;
        if (mContactCeiling && mVerticalSpeed > 0.0f) mVerticalSpeed = 0.0f;
        mLMovement = new Vector3(0, 0, 0);
    }

    public void setCanJump(bool b)
    {
        canJump = b;
    }

}
