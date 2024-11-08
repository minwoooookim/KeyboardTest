using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;

namespace WristKeyboard
{
    public class TestPractice : MonoBehaviour
    {
        public TextMeshProUGUI sentenceBox;
        [SerializeField] private TMP_InputField inputBox;

        public static float[] timeTaken = new float[2];
        public static string currentSceneName;

        private string[] sentences = new string[2];
        private int currentSentenceIndex = 0;
        private float startTime;

        void Start()
        {
            string filePath = Path.Combine(Application.dataPath, "phrases.txt");

            List<string> allSentences = new List<string>(File.ReadAllLines(filePath));

            if (allSentences.Count >= 5)
            {
                for (int i = 0; i < 2; i++)
                {
                    int randomIndex = Random.Range(0, allSentences.Count);
                    sentences[i] = allSentences[randomIndex];
                    allSentences.RemoveAt(randomIndex);  // 중복 선택 방지
                }
            }

            //sentences[0] = "test";
            //sentences[1] = "test";
            //sentences[2] = "test";
            //sentences[3] = "test";
            //sentences[4] = "test";

            currentSceneName = SceneManager.GetActiveScene().name;
            sentenceBox.text = sentences[currentSentenceIndex];
            startTime = Time.time;
        }

        void Update()
        {
            if (currentSceneName == "Test 1 Practice")
            {
                Test_1();
            }
            else if (currentSceneName == "Test 2 Practice")
            {
                Test_2();
            }
        }

        private void Test_1()
        {
            if (inputBox.text == sentenceBox.text)
            {
                float endTime = Time.time;
                timeTaken[currentSentenceIndex] = endTime - startTime;

                inputBox.text = "";

                currentSentenceIndex++;

                if (currentSentenceIndex < sentences.Length)
                {
                    sentenceBox.text = sentences[currentSentenceIndex];
                    startTime = Time.time;
                }
                else
                {
                    SceneManager.LoadScene("Practice Result Scene");
                }
            }
            else if (inputBox.text == "out")
            {
                SceneManager.LoadScene("Practice Result Scene");
            }
        }

        private void Test_2()
        {
            if (inputBox.text == sentenceBox.text)
            {
                float endTime = Time.time;
                timeTaken[currentSentenceIndex] = endTime - startTime;

                inputBox.text = "";

                currentSentenceIndex++;

                if (currentSentenceIndex < sentences.Length)
                {
                    sentenceBox.text = sentences[currentSentenceIndex];
                    startTime = Time.time;
                }
                else
                {
                    SceneManager.LoadScene("Practice Result Scene");
                }
            }
            else if (inputBox.text == "out")
            {
                SceneManager.LoadScene("Practice Result Scene");
            }
        }
    }
}
