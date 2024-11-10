using System.Collections.Generic;
using UnityEngine;

namespace KC
{
    [CreateAssetMenu(fileName = "New Question", menuName = "Quiz/Question")]
    public class Question : ScriptableObject
    {
        public string questionText;
        public List<AnswerOption> answerOptions = new List<AnswerOption>();
    }
}
