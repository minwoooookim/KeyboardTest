using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace WristKeyboard
{
    public class KeyPrinter : MonoBehaviour
    {
        public static KeyPrinter instance;

        public string character1;
        public string shiftCharacter1;
        public string character2;
        public string shiftCharacter2;
        public string character3;
        public string shiftCharacter3;
        public string character4;
        public string shiftCharacter4;
        public string character5;
        public string shiftCharacter5;

        public void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        public void PrintKeyBasedOnAngle(float Angle)
        {
            // 각도에 따라 플래그 가져오기
            AngleFlag angleFlag = AngleUtility.GetAngleFlag(Angle);

            // 플래그에 따라 다른 키를 출력
            switch (angleFlag)
            {
                case AngleFlag.LeftMost:
                    KeyboardManager.instance.inputField.text += character1;
                    break;
                case AngleFlag.NearLeft:
                    KeyboardManager.instance.inputField.text += character2;
                    break;
                case AngleFlag.Center:
                    KeyboardManager.instance.inputField.text += character3;
                    break;
                case AngleFlag.NearRight:
                    KeyboardManager.instance.inputField.text += character4;
                    break;
                case AngleFlag.RightMost:
                    KeyboardManager.instance.inputField.text += character5;
                    break;
            }

            Debug.Log("PrintKeyBasedOnAngle executed");
        }
    }
}
