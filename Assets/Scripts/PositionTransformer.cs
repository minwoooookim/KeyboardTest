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
            // 키보드 매니저에서 각도 가져오기
            float Angle = KeyboardManager.instance.GetZInput(WhichSide());

            // 각도에 따라 플래그 가져오기
            AngleFlag angleFlag = AngleUtility.GetAngleFlag(Angle);

            // 플래그에 따라 오브젝트 위치 변경
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
