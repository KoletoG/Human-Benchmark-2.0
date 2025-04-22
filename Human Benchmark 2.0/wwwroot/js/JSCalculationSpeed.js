let totalRounds = 5;
let currentRound = 0;
let startTime;
let timeTakenList = new Set();
let avgTime = 0;
let currentA = 0;
let currentB = 0;
let resultDoc;
let startBtn;
let answerInput;
let submitBtn;
let question;
let saveStatsBtn;
document.addEventListener("DOMContentLoaded", () =>
{
    resultDoc = document.getElementById("result");
    startBtn = document.getElementById("startBtn");
    answerInput = document.getElementById("answerInput");
    submitBtn = document.getElementById("submitBtn");
    question = document.getElementById("question");
    saveStatsBtn = document.getElementById("saveStatsBtn");
});
function startGame() {
    currentRound = 0;
    timeTakenList.clear();
    correctAnswers = 0;
    resultDoc.innerText = "";
    startBtn.disabled = true;
    submitBtn.disabled = false;
    submitBtn.hidden = false;
    answerInput.hidden=false;
    answerInput.disabled = false;
    startBtn.hidden = true;
    nextQuestion();
}

function nextQuestion() 
{
    if (currentRound >= totalRounds)
    {
        finishGame();
        return;
    }
    currentA = Math.floor(Math.random() * 90) + 10;
    currentB = Math.floor(Math.random() * 90) + 10;
    question.innerText = `What is ${currentA} × ${currentB}?`;
    answerInput.value = "";
    answerInput.focus();
    startTime = new Date();
}

function submitAnswer() 
{
    const input = answerInput.value;
    const endTime = new Date();
    const timeTaken = (endTime - startTime) / 1000;
    timeTakenList.add(timeTaken);
    if (parseInt(input) === currentA * currentB) 
    {
        currentRound++;
        nextQuestion();
    }
    else
    {
        failFinish();
    }
}
function failFinish()
{
    question.innerText = "Game Over, calculation was wrong!";
    answerInput.disabled = true;
    submitBtn.disabled = true;
    submitBtn.hidden = true;
    startBtn.disabled = false;
    startBtn.hidden = false;
    saveStatsBtn.disabled = true;
    saveStatsBtn.hidden = true;
    answerInput.hidden=true;
    answerInput.disabled = true;
}
function finishGame() 
{
    question.innerText = "Game Over!";
    answerInput.disabled = true;
    submitBtn.disabled = true;
    submitBtn.hidden = true;
    startBtn.disabled = false;
    saveStatsBtn.disabled = false;
    startBtn.hidden = false;
    saveStatsBtn.hidden = false;
    answerInput.hidden=true;
    avgTime=0;
    for(const time of timeTakenList){
        avgTime+=time;
    }
    avgTime = Number((avgTime / timeTakenList.size).toFixed(2)); 
    resultDoc.innerText = `Average Time: ${avgTime} seconds`;
}
    
function saveResults()
{
    const token = document.querySelector('input[name="__RequestVerificationToken"]').value;
    fetch("/CalculationSpeed/SaveCalc",
        {
            method: "POST",
            headers:
            {
                "Content-Type": "application/json",
                "RequestVerificationToken": token
            },
            body: JSON.stringify(avgTime)
        })
            .then(res => res.json())
            .then(data => {
                if (data.redirectUrl) {
                    window.location.href = data.redirectUrl;
                }
        });
}
