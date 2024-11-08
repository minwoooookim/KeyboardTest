using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Readers;
using TMPro;
using UnityEngine.TextCore.Text;

namespace WristKeyboard
{
    public class KeyboardManager : MonoBehaviour
    {
        public static KeyboardManager instance;
        public TMP_InputField inputField;

        [SerializeField]
        XRInputValueReader<Quaternion> m_RightControllerRotation = new XRInputValueReader<Quaternion>("Rotation");

        [SerializeField]
        XRInputValueReader<Quaternion> m_LeftControllerRotation = new XRInputValueReader<Quaternion>("Rotation");

        [SerializeField]
        XRInputButtonReader m_RightTriggerActivation = new XRInputButtonReader("TriggerActivation");

        [SerializeField]
        XRInputButtonReader m_LeftTriggerActivation = new XRInputButtonReader("TriggerActivation");

        [SerializeField]
        XRInputButtonReader m_RightGripInput = new XRInputButtonReader("Grip");

        [SerializeField]
        XRInputButtonReader m_LeftGripInput = new XRInputButtonReader("Grip");

        private GameObject currentKeyIndicator_Right = null;
        private GameObject currentKeyIndicator_Left = null;

        private bool isRightPrintKeyCalled = false; // 반복입력 방지용 플래그
        private bool isLeftPrintKeyCalled = false; // 반복입력 방지용 플래그

        private bool isDeleteCalled = false;
        private bool isSpaceCalled = false;

        public void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            FindKeyIndicator("KeyBars_Y", "Right Side").SetActive(false);
            FindKeyIndicator("KeyBars_H", "Right Side").SetActive(false);
            FindKeyIndicator("KeyBars_N", "Right Side").SetActive(false);
            FindKeyIndicator("KeyBars_T", "Left Side").SetActive(false);
            FindKeyIndicator("KeyBars_G", "Left Side").SetActive(false);
            FindKeyIndicator("KeyBars_B", "Left Side").SetActive(false);
        }

        void OnEnable() // 왜 있는지 모르겠지만 일단 오류는 없으니 수정하지 않음.
        {
            m_RightTriggerActivation?.EnableDirectActionIfModeUsed();
            m_LeftTriggerActivation?.EnableDirectActionIfModeUsed();
            m_RightGripInput?.EnableDirectActionIfModeUsed();
            m_LeftGripInput?.EnableDirectActionIfModeUsed();
        }

        void OnDisable()
        {
            m_RightTriggerActivation?.DisableDirectActionIfModeUsed();
            m_LeftTriggerActivation?.DisableDirectActionIfModeUsed();
            m_RightGripInput?.DisableDirectActionIfModeUsed();
            m_LeftGripInput?.DisableDirectActionIfModeUsed();
        }

        private void Update()
        {
            RightHand();
            LeftHand();

            Debug.Log(m_RightTriggerActivation.ReadValue());
            Debug.Log("오프셋 적용 오른쪽: " + AdjustRotation("Right Side").eulerAngles);
            Debug.Log("오프셋 미적용 우측: " + m_RightControllerRotation.ReadValue().eulerAngles);
            //Debug.Log("오프셋 적용 왼쪽: " + AdjustRotation("Left Side").eulerAngles);
        }


        /* 양 손 기능 부분 */


        private void RightHand()
        {
            if (m_RightControllerRotation != null)
            {
                var rightControllerRotation_Z = GetZInput("Right Side");
                var rightControllerRotation_X = GetXInput("Right Side");

                //if (m_RightTriggerActivation.ReadIsPerformed() == false)
                //{
                //    SelectBar_Right(rightControllerRotation_X);
                //    isRightPrintKeyCalled = false;
                //}
                //else if (m_RightTriggerActivation.ReadIsPerformed() == true && isRightPrintKeyCalled == false)
                //{
                //    ExecutePrintKey_Right(rightControllerRotation_Z);
                //    isRightPrintKeyCalled = true;
                //}

                if (m_RightTriggerActivation.ReadValue() == 0)
                {
                    SelectBar_Right(rightControllerRotation_X);
                    isRightPrintKeyCalled = false;
                }
                else if (m_RightTriggerActivation.ReadValue() != 0 && isRightPrintKeyCalled == false)
                {
                    ExecutePrintKey_Right(rightControllerRotation_Z);
                    isRightPrintKeyCalled = true;
                }


                // 지우기 기능
                if (m_RightGripInput.ReadIsPerformed() == true && isDeleteCalled == false)
                {
                    DeleteOnce();
                    isDeleteCalled = true;
                }
                if (m_RightGripInput.ReadIsPerformed() == false)
                {
                    isDeleteCalled = false;
                }

                // 한칸 띄우기 기능
                //if (m_RightGripInput.ReadIsPerformed() == true && isSpaceCalled == false)
                //{
                //    inputField.text += " ";
                //    isSpaceCalled = true;

                //}
                //if (m_RightGripInput.ReadIsPerformed() == false)
                //{
                //    isSpaceCalled = false;
                //}
            }
        }

        private void LeftHand()
        {
            if (m_LeftControllerRotation != null)
            {
                var LeftControllerRotation_Z = GetZInput("Left Side");
                var LeftControllerRotation_X = GetXInput("Left Side");

                //if (m_LeftTriggerActivation.ReadIsPerformed() == false)
                //{
                //    SelectBar_Left(LeftControllerRotation_X);
                //    isLeftPrintKeyCalled = false;
                //}
                //else if (m_LeftTriggerActivation.ReadIsPerformed() == true && isLeftPrintKeyCalled == false)
                //{
                //    ExecutePrintKey_Left(LeftControllerRotation_Z);
                //    isLeftPrintKeyCalled = true;
                //}

                if (m_LeftTriggerActivation.ReadValue() == 0)
                {
                    SelectBar_Left(LeftControllerRotation_X);
                    isLeftPrintKeyCalled = false;
                }
                else if (m_LeftTriggerActivation.ReadValue() != 0 && isLeftPrintKeyCalled == false)
                {
                    ExecutePrintKey_Left(LeftControllerRotation_Z);
                    isLeftPrintKeyCalled = true;
                }

                // 한칸 띄우기 기능
                if (m_LeftGripInput.ReadIsPerformed() == true && isSpaceCalled == false)
                {
                    inputField.text += " ";
                    isSpaceCalled = true;
                }
                else if (m_LeftGripInput.ReadIsPerformed() == false)
                {
                    isSpaceCalled = false;
                }

                // 지우기 기능
                //if (m_LeftGripInput.ReadIsPerformed() == true && isDeleteCalled == false)
                //{

                //    DeleteOnce();
                //    isDeleteCalled = true;
                //}
                //else if (m_LeftGripInput.ReadIsPerformed() == false)
                //{

                //    isDeleteCalled = false;
                //}
            }
        }

        private void SelectBar_Right(float ControllerRotation_X)
        {
            GameObject targetKeyIndicator = null;
            WhichBar selectedBar = SelectBar(ControllerRotation_X);

            switch (selectedBar)
            {
                case WhichBar.Top:
                    targetKeyIndicator = FindKeyIndicator("KeyBars_Y", "Right Side");
                    break;
                case WhichBar.Center:
                    targetKeyIndicator = FindKeyIndicator("KeyBars_H", "Right Side");
                    break;
                case WhichBar.Bottom:
                    targetKeyIndicator = FindKeyIndicator("KeyBars_N", "Right Side");
                    break;
            }

            if (targetKeyIndicator != null && targetKeyIndicator != currentKeyIndicator_Right)
            {
                HideCurrentKeyIndicator_Right();
                currentKeyIndicator_Right = targetKeyIndicator;
                currentKeyIndicator_Right.SetActive(true);
            }
        }

        private void SelectBar_Left(float ControllerRotation_X)
        {
            GameObject targetKeyIndicator = null;
            WhichBar selectedBar = SelectBar(ControllerRotation_X);

            switch (selectedBar)
            {
                case WhichBar.Top:
                    targetKeyIndicator = FindKeyIndicator("KeyBars_T", "Left Side");
                    break;
                case WhichBar.Center:
                    targetKeyIndicator = FindKeyIndicator("KeyBars_G", "Left Side");
                    break;
                case WhichBar.Bottom:
                    targetKeyIndicator = FindKeyIndicator("KeyBars_B", "Left Side");
                    break;
            }

            if (targetKeyIndicator != null && targetKeyIndicator != currentKeyIndicator_Left)
            {
                HideCurrentKeyIndicator_Left();
                currentKeyIndicator_Left = targetKeyIndicator;
                currentKeyIndicator_Left.SetActive(true);
            }
        }

        private GameObject FindKeyIndicator(string parentName, string side)
        {
            return transform.Find(side + "/" + parentName + "/KeyIndicator").gameObject;
        }

        private void HideCurrentKeyIndicator_Right()
        {
            if (currentKeyIndicator_Right != null)
            {
                currentKeyIndicator_Right.SetActive(false);
                currentKeyIndicator_Right = null;
            }
        }

        private void HideCurrentKeyIndicator_Left()
        {
            if (currentKeyIndicator_Left != null)
            {
                currentKeyIndicator_Left.SetActive(false);
                currentKeyIndicator_Left = null;
            }
        }

        private void ExecutePrintKey_Right(float zAngle)
        {
            Transform parentTransform_Right = currentKeyIndicator_Right.transform.parent;
            var component_Right = parentTransform_Right.GetComponent<KeyPrinter>();
            component_Right.PrintKeyBasedOnAngle(zAngle);
        }

        private void ExecutePrintKey_Left(float zAngle)
        {
            Transform parentTransform_Left = currentKeyIndicator_Left.transform.parent;
            var component_Left = parentTransform_Left.GetComponent<KeyPrinter>();
            component_Left.PrintKeyBasedOnAngle(zAngle);
        }

        private void DeleteOnce()
        {
            int length = inputField.text.Length - 1;
            inputField.text = inputField.text.Substring(0, length);
        }


        /* 각도 조절 부분 */


        private float AdjustZAngle(float inputZAngle)
        {
            if ((inputZAngle >= 0 && inputZAngle <= 90) || (inputZAngle >= 270 && inputZAngle <= 360))
            {
                float adjustedZAngle;
                float sensitivity = 1.3f; // Z각도 민감도 조절

                if (inputZAngle <= 90)
                {
                    adjustedZAngle = inputZAngle * sensitivity;
                    adjustedZAngle = Mathf.Clamp(adjustedZAngle, 0, 90);
                }
                else
                {
                    adjustedZAngle = 360 - (360 - inputZAngle) * sensitivity;
                    adjustedZAngle = Mathf.Clamp(adjustedZAngle, 270, 360);
                }

                return adjustedZAngle;
            }
            else
            {
                return inputZAngle;
            }
        }
        //private float AdjustXAngle(float inputXAngle)
        //{
        //    float adjustedXAngle;
        //    float sensitivity = 1.5f; // X각도 민감도 조절

        //    adjustedXAngle = 348 + (inputXAngle - 348) * sensitivity;

        //    return adjustedXAngle;
        //}


        private Quaternion AdjustRotation(string side) // 컨트롤러 오프셋 Y축 30f만큼 변경
        {
            Quaternion controllerRotation;

            //if (side == "Right Side")
            //{
            //    controllerRotation = m_RightControllerRotation.ReadValue() * Quaternion.Euler(0f, -30f, 0f); // 새로운 기준 축으로 변환
            //}
            //else if (side == "Left Side")
            //{
            //    controllerRotation = m_LeftControllerRotation.ReadValue() * Quaternion.Euler(0f, +30f, 0f);
            //}
            //else
            //{
            //    return Quaternion.identity; // 유효하지 않은 경우 기본 Quaternion 반환
            //}
            //return controllerRotation;


            if (side == "Right Side")
            {
                controllerRotation = m_RightControllerRotation.ReadValue() * Quaternion.Euler(0f, -20f, 0f); // 새로운 기준 축으로 변환
            }
            else if (side == "Left Side")
            {
                controllerRotation = m_LeftControllerRotation.ReadValue() * Quaternion.Euler(0f, +20f, 0f);
            }
            else
            {
                return Quaternion.identity; // 유효하지 않은 경우 기본 Quaternion 반환
            }
            return controllerRotation;

        }

        public float GetZInput(string side)
        {
            Quaternion adjustedRotation = AdjustRotation(side);
            return AdjustZAngle(adjustedRotation.eulerAngles.z);
        }

        public float GetXInput(string side)
        {
            Quaternion adjustedRotation = AdjustRotation(side);
            //return AdjustXAngle(adjustedRotation.eulerAngles.x);
            return adjustedRotation.eulerAngles.x;
        }

        public enum WhichBar
        {
            Top,
            Center,
            Bottom
        }

        //private WhichBar SelectBar(float ControllerRotation_X) // KeyBar의 위치 선택
        //{
        //    if (ControllerRotation_X >= 180 && ControllerRotation_X < 345)
        //    {
        //        return WhichBar.Top;
        //    }
        //    else if (ControllerRotation_X >= 345 || ControllerRotation_X < 5)
        //    {
        //        return WhichBar.Center;
        //    }
        //    else if (ControllerRotation_X >= 5 && ControllerRotation_X < 180)
        //    {
        //        return WhichBar.Bottom;
        //    }
        //    else return WhichBar.Center;
        //}

        private WhichBar SelectBar(float ControllerRotation_X) // KeyBar의 위치 선택
        {
            if (ControllerRotation_X >= 180 && ControllerRotation_X < 345)
            {
                return WhichBar.Top;
            }
            else if (ControllerRotation_X >= 345 || ControllerRotation_X < 15)
            {
                return WhichBar.Center;
            }
            else if (ControllerRotation_X >= 15 && ControllerRotation_X < 180)
            {
                return WhichBar.Bottom;
            }
            else return WhichBar.Center;
        }

    }

    /* 아래 부분은 같은 namespace에 있으나 KeyboardManager 클래스에 포함되어 있지 않음. */

    // 각도에 따른 플래그 정의
    public enum AngleFlag
    {
        LeftMost,
        NearLeft,
        Center,
        NearRight,
        RightMost
    }

    public static class AngleUtility
    {
        // 각도를 받아서 해당하는 플래그를 반환
        public static AngleFlag GetAngleFlag(float Angle) // 좌우 위치 선택
        {
            if (Angle >= 54 && Angle < 180)
            {
                return AngleFlag.LeftMost;
            }
            else if (Angle >= 18 && Angle < 54)
            {
                return AngleFlag.NearLeft;
            }
            else if (Angle >= 342 || Angle < 18)
            {
                return AngleFlag.Center;
            }
            else if (Angle >= 306 && Angle < 342)
            {
                return AngleFlag.NearRight;
            }
            else if (Angle >= 180 && Angle < 306)
            {
                return AngleFlag.RightMost;
            }

            return AngleFlag.Center; // 기본값으로 Center 반환
        }
    }

}
