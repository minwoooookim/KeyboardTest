using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace WristKeyboard
{
    public class PositionTransformer : MonoBehaviour
    {
        void Update()
        {
            ChangePosition();
        }
        private string WhichSide()
        {
            Transform sideTransform = transform.parent.parent;
            return sideTransform.gameObject.name;
        }

        public void ChangePosition()
        {
            // Ű���� �Ŵ������� ���� ��������
            float Angle = KeyboardManager.instance.GetZInput(WhichSide());

            // ������ ���� �÷��� ��������
            AngleFlag angleFlag = AngleUtility.GetAngleFlag(Angle);

            // �÷��׿� ���� ������Ʈ ��ġ ����
            float position = 0f;
            switch (angleFlag)
            {
                case AngleFlag.LeftMost:
                    position = -0.2f;
                    break;
                case AngleFlag.NearLeft:
                    position = -0.1f;
                    break;
                case AngleFlag.Center:
                    position = 0f;
                    break;
                case AngleFlag.NearRight:
                    position = 0.1f;
                    break;
                case AngleFlag.RightMost:
                    position = 0.2f;
                    break;
            }

            transform.localPosition = new Vector3(0, position, 0);
        }

    }
}
