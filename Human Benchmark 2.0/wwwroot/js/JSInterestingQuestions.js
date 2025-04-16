let questions=[];
let doneQuestions=[-1];
let answers=[];
let startBtn;
let answerA;
let answerB;
let answerC;
let answerD;
let indexOfQuestion;
let currentQuestion;
let correctAnswer =[];

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
    answerA.hidden=false;
    answerA.disabled=false;
    answerB.hidden=false;
    answerB.disabled=false;
    answerC.hidden=false;
    answerC.disabled=false;
    answerD.hidden=false;
    answerD.disabled=false;
    questions=[];
    doneQuestions=[-1];
    answers=[];
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
    indexOfQuestion=Math.floor(Math.random()*questions.length());
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
    answerA.innerText=answers[index];
    answerB.innerText=answers[index+1];
    answerC.innerText=answers[index+2];
    answerD.innerText=answers[index+3];
}

function chooseAnswer(answerStr)
{
    if(correctAnswer[indexOfQuestion]==answerStr)
    {
        currentQuestion.innerText="Right! Loading next question...";
        setTimeout(()=>
        {
            rollQuestion();
        },3000);
    }
    else
    {
        currentQuestion.innerText=`Wrong! The correct answer was ${correctAnswer[indexOfQuestion]}. Loading next question...`;
        setTimeout(()=>
        {
            rollQuestion();
        },3000);
    }
}
