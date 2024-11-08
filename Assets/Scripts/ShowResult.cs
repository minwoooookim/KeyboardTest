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
                result += "����" + (i + 1) + " : " + TestAndTimer.timeTaken[i].ToString("0.000") + "��\n";
                totalTime += TestAndTimer.timeTaken[i];
            }
            resultText.text += result + "�� �ɸ� �ð� " + totalTime.ToString("0.000") + "��";

            // ����ȭ�� ��� ��������
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            // ���� �̸�: sceneName + ���� �ð� (yyyy-MM-dd_HH-mm-ss)
            string fileName = sceneName + "_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".txt";

            // ���� ���: ����ȭ�� ��� + ���� �̸�
            string filePath = Path.Combine(desktopPath, fileName);

            // ���Ͽ� �ؽ�Ʈ ����
            File.WriteAllText(filePath, resultText.text);

            // ���� ���� ��ġ Ȯ�ο� �α� ���
            Debug.Log("����� ����ȭ�鿡 ����Ǿ����ϴ�: " + filePath);

        }
    }
}