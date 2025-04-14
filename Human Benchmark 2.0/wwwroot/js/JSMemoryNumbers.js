let number;
let currentLength = 1;
let score=0;
let numberLength = 1;
let answerField;
let btnStart;
let numberField;
let scoreLabel;
let btnSave;
let restartButton;
document.addEventListener("DOMContentLoaded", () =>
{
    answerField = document.getElementById("answerField");
    btnStart = document.getElementById("btnStart");
    numberField = document.getElementById("numberField");
    scoreLabel = document.getElementById("scoreLabel");
    btnSave = document.getElementById("btnSave");
    restartButton = document.getElementById("restartButton");
});
function startGame()
{
    answerField.innerHTML="";
    btnStart.hidden=true;
    btnStart.disabled=true;
    answerField.hidden=true;
    numberField.hidden=false;
    numberLength*=10;
    number = Math.floor(Math.random() * (numberLength));
    numberField.style.color = "black";
    numberField.innerHTML=`${number}`;
    setTimeout(()=>
    {
        typingAnswer();
    },currentLength*800 + 3000); // Time for the number to show up for
}
function typingAnswer()
{
    numberField.innerHTML = "Type the number";
    answerField.hidden = false;
    answerField.focus();
    setTimeout(()=>{
        checkAnswer();
    },3000+currentLength*400) // Time for the user to type the shown number for
}
// Checks if user's answer is true
function checkAnswer()
{
    let answer = answerField.value;
    answerField.value = "";
    answerField.hidden = true;
    if (number == parseInt(answer))
    {
        numberField.style.color="green";
        numberField.innerHTML="Your answer is right. Continuing..."
        currentLength++;
        score++;
        setTimeout(()=>
        {
            startGame();
        },3000);
    }
    else
    {
        numberField.style.color="red";
        numberField.innerHTML = "Your answer was wrong!";
        scoreLabel.hidden=false;
        scoreLabel.innerHTML=`Your score is ${score}`;
        answerField.hidden=true;
        answerField.disabled=true;
        btnSave.disabled=false;
        btnSave.hidden=false;
        restartButton.hidden=false;
        restartButton.disabled=false;
    }
}

function restartGame()
{
    currentLength = 1;
    score=0;
    numberLength=1;
    scoreLabel.hidden=true;
    btnSave.hidden=true;
    btnSave.disabled=false;
    restartButton.hidden=true;
    restartButton.disabled = true;
    answerField.disabled = false;
    startGame();
}

function saveStats()
{
    fetch("/MemoryNumbers/MemoryNumbersSave", 
    {
        method: "POST",
        headers: 
        {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(score)
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