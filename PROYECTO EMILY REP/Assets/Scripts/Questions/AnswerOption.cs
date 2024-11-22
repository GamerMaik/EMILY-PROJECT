using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class AnswerOption
{
    public string answerText;
    public bool isCorrect;
}
[System.Serializable]
public class PlayFabQuestionData
{
    public string name; // Nombre de la categor�a
    public List<PlayFabQuestion> questions; // Lista de preguntas
}

[System.Serializable]
public class PlayFabQuestion
{
    public string questionText; // Texto de la pregunta
    public float timeLimit; // L�mite de tiempo
    public List<AnswerOption> answers; // Respuestas (usa tu clase AnswerOption)
}