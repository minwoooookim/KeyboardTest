using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace WristKeyboard
{
    public class TestAndTimer : MonoBehaviour
    {
        
        public TextMeshProUGUI sentenceBox;
        [SerializeField] private TMP_InputField inputBox;

        public static float[] timeTaken = new float[5];
        public static string currentSceneName;

        private string[] sentences = new string[5];
        private int currentSentenceIndex = 0;
        private float startTime;

        void Start()
        {
            sentences[0] = "time";
            sentences[1] = "always";
            sentences[2] = "together";
            sentences[3] = "assignment";
            sentences[4] = "information";

            //sentences[0] = "time to go shopping";
            //sentences[1] = "he is still on our team";
            //sentences[2] = "get rid of that immediately";
            //sentences[3] = "the minimum amount of time";
            //sentences[4] = "employee recruitment takes a lot of effort";

            //sentences[0] = "e";
            //sentences[1] = "e";
            //sentences[2] = "e";
            //sentences[3] = "e";
            //sentences[4] = "e";

            currentSceneName = SceneManager.GetActiveScene().name;
            sentenceBox.text = sentences[currentSentenceIndex];
            startTime = Time.time;
        }

        void Update()
        {
            if (currentSceneName == "Test 1")
            {
                Test_1();
            }
            else if (currentSceneName == "Test 2")
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
                    SceneManager.LoadScene("Result Scene");
                }
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
                    SceneManager.LoadScene("Result Scene");
                }
            }
        }
    }
}
