let questions=["What is the capital city of Australia?","Which planet is known as the Red Planet?","Which language is the most spoken in the world by number of native speakers?","In computing, what does HTTP stand for?","Who was the first person to win two Nobel Prizes?"];
let doneQuestions=[-1];
let answers=[["Sydney","Melbourne","Brisbane","Canberra"],["Earth","Venus","Mars","Jupiter"],["English","Hindi","Spanish","Mandarin Chinese"],["HyperText Transmission Protocol","HyperTransfer Text Protocol","HyperText Transfer Protocol","HighText Transfer Program"],["Albert Einstein","Marie Curie","Linus Pauling","Niels Bohr"]];
let startBtn;
let answerA;
let answerB;
let answerC;
let answerD;
let indexOfQuestion;
let currentQuestion;
let correctAnswer =["D","C","D","C","B"];

document.addEventListener("DOMContentLoaded",()=>
{
    startBtn = document.getElementById("startBtn");
    answerA = document.getElementById("answerA");
    answerB = document.getElementById("answerB");
    answerC = document.getElementById("answerC");
    answerD = document.getElementById("answerD");
    currentQuestion = document.getElementById("currentQuestion");
});

function startGame()
{
    startBtn.hidden=true;
    startBtn.disabled=true;
    setHiddenAndDisableAnswers(false);
    doneQuestions=[-1];
    rollQuestion();
}

function rollQuestion()
{
    let currentIndex=checkRepeatedQuestion();
    currentQuestion.innerText=questions[currentIndex];
    fillAnswers(currentIndex);
}

function checkRepeatedQuestion()
{
    indexOfQuestion=Math.floor(Math.random()*questions.length);
    if(doneQuestions.includes(indexOfQuestion))
    {
        return checkRepeatedQuestion();
    }
    else
    {
        doneQuestions.push(indexOfQuestion);
        return indexOfQuestion;
    }
}

function fillAnswers(index)
{
    setHiddenAndDisableAnswers(false);
    answerA.innerText=answers[index][0];
    answerB.innerText=answers[index][1];
    answerC.innerText=answers[index][2];
    answerD.innerText=answers[index][3];
}

function checkAnswer(answerStr)
{
    if(correctAnswer[indexOfQuestion]==answerStr)
    {
        currentQuestion.innerText="Right! Loading next question...";
        setHiddenAndDisableAnswers(true);
        setTimeout(()=>
        {
            rollQuestion();
        },3000);
    }
    else
    {
        currentQuestion.innerText=`Wrong! The correct answer was ${correctAnswer[indexOfQuestion]}. Loading next question...`;
        setHiddenAndDisableAnswers(true);
        setTimeout(()=>
        {
            rollQuestion();
        },3000);
    }
}

function setHiddenAndDisableAnswers(trueOrFalse)
{
    answerA.hidden=trueOrFalse;
    answerA.disabled=trueOrFalse;
    answerB.hidden=trueOrFalse;
    answerB.disabled=trueOrFalse;
    answerC.hidden=trueOrFalse;
    answerC.disabled=trueOrFalse;
    answerD.hidden=trueOrFalse;
    answerD.disabled=trueOrFalse;
}
