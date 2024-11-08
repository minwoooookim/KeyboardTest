using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System;

namespace WristKeyboard
{
    public class ShowResult : MonoBehaviour
    {
        public TextMeshProUGUI resultText;

        void Start()
        {
            string result = "";
            string sceneName = TestAndTimer.currentSceneName;

            if (sceneName == "Test 1")
            {
                result += "Test 1\n";
            }
            else if (sceneName == "Test 2")
            {
                result += "Test 2\n";
            }

            float totalTime = 0f;
            for (int i = 0; i < TestAndTimer.timeTaken.Length; i++)
            {
                result += "문장" + (i + 1) + " : " + TestAndTimer.timeTaken[i].ToString("0.000") + "초\n";
                totalTime += TestAndTimer.timeTaken[i];
            }
            resultText.text += result + "총 걸린 시간 " + totalTime.ToString("0.000") + "초";

            // 바탕화면 경로 가져오기
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            // 파일 이름: sceneName + 현재 시간 (yyyy-MM-dd_HH-mm-ss)
            string fileName = sceneName + "_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".txt";

            // 파일 경로: 바탕화면 경로 + 파일 이름
            string filePath = Path.Combine(desktopPath, fileName);

            // 파일에 텍스트 쓰기
            File.WriteAllText(filePath, resultText.text);

            // 파일 저장 위치 확인용 로그 출력
            Debug.Log("결과가 바탕화면에 저장되었습니다: " + filePath);

        }
    }
}