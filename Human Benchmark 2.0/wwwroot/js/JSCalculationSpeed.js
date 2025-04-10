let totalRounds = 5;
let currentRound = 0;
let startTime;
let timeTakenList = [];
let avgTime = 0;
let currentA = 0;
let currentB = 0;
function startGame() {
    currentRound = 0;
    timeTakenList = [];
    correctAnswers = 0;
    document.getElementById("result").innerText = "";
    document.getElementById("startBtn").disabled = true;
    document.getElementById("answerInput").disabled = false;
    document.getElementById("submitBtn").disabled = false;
    document.getElementById("submitBtn").hidden = false;
    document.getElementById("answerInput").hidden=false;
    document.getElementById("answerInput").disabled = false;
    document.getElementById("startBtn").hidden = true;
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
    document.getElementById("question").innerText = `What is ${currentA} × ${currentB}?`;
    document.getElementById("answerInput").value = "";
    document.getElementById("answerInput").focus();
    startTime = new Date();
}

function submitAnswer() 
{
    const input = document.getElementById("answerInput").value;
    const endTime = new Date();
    const timeTaken = (endTime - startTime) / 1000;
    timeTakenList.push(timeTaken);
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
    document.getElementById("question").innerText = "Game Over, calculation was wrong!";
    document.getElementById("answerInput").disabled = true;
    document.getElementById("submitBtn").disabled = true;
    document.getElementById("submitBtn").hidden = true;
    document.getElementById("startBtn").disabled = false;
    document.getElementById("startBtn").hidden = false;
    document.getElementById("saveStatsBtn").disabled = true;
    document.getElementById("saveStatsBtn").hidden = true;
    document.getElementById("answerInput").hidden=true;
    document.getElementById("answerInput").disabled = true;
}
function finishGame() 
{
    document.getElementById("question").innerText = "Game Over!";
    document.getElementById("answerInput").disabled = true;
    document.getElementById("submitBtn").disabled = true;
    document.getElementById("submitBtn").hidden = true;
    document.getElementById("startBtn").disabled = false;
    document.getElementById("saveStatsBtn").disabled = false;
    document.getElementById("startBtn").hidden = false;
    document.getElementById("saveStatsBtn").hidden = false;
    document.getElementById("answerInput").hidden=true;
    avgTime = (timeTakenList.reduce((a, b) => a + b, 0) / totalRounds).toFixed(2);
    document.getElementById("result").innerText = `Average Time: ${avgTime} seconds`;
}
    
function saveResults()
{
    fetch("/CalculationSpeed/SaveCalc", 
        {
            method: "POST",
            headers: 
            {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(avgTime)
        })
        .then(res => res.json())
        .then(data => 
        {
            if (data.redirectUrl)
            {
                window.location.href = data.redirectUrl;
            }
        });
}
