using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace WristKeyboard
{
    public class ShowPracticeResult : MonoBehaviour
    {
        public TextMeshProUGUI resultText;

        void Start()
        {
            string result = "";
            string sceneName = TestPractice.currentSceneName;

            if (sceneName == "Test 1 Practice")
            {
                result += "Test 1 Practice\n";
            }
            else if (sceneName == "Test 2 Practice")
            {
                result += "Test 2 Practice\n";
            }

            float totalTime = 0f;
            for (int i = 0; i < TestPractice.timeTaken.Length; i++)
            {
                result += "문장" + (i + 1) + " : " + TestPractice.timeTaken[i].ToString("0.000") + "초\n";
                totalTime += TestPractice.timeTaken[i];
            }
            resultText.text += result + "총 걸린 시간 " + totalTime.ToString("0.000") + "초";
        }
    }
}
