let number;
let currentLength = 1;
let score=0;
let numberLength=1;
document.getElementById("numberField").innerHTML="Type your answer";
function startGame()
{
    document.getElementById("answerField").innerHTML="";
    document.getElementById("btnStart").hidden=true;
    document.getElementById("btnStart").disabled=true;
    document.getElementById("answerField").hidden=true;
    document.getElementById("numberField").hidden=false;
    numberLength*=10;
    number=Math.floor(Math.random()*(numberLength));
    document.getElementById("numberField").style.color="black";
    document.getElementById("numberField").innerHTML=`${number}`;
    setTimeout(()=>
    {
        typingAnswer();
    },currentLength*800 + 3000); // Time for the number to show up for
}
function typingAnswer()
{
    document.getElementById("numberField").innerHTML = "Type the number";
    document.getElementById("answerField").hidden = false;
    document.getElementById("answerField").focus();
    setTimeout(()=>{
        checkAnswer();
    },3000+currentLength*400) // Time for the user to type the shown number for
}
// Checks if user's answer is true
function checkAnswer()
{
    let answer = document.getElementById("answerField").value;
    document.getElementById("answerField").value = "";
    document.getElementById("answerField").hidden = true;
    if(number == parseInt(answer))
    {
        document.getElementById("numberField").style.color="green";
        document.getElementById("numberField").innerHTML="Your answer is right. Continuing..."
        currentLength++;
        score++;
        setTimeout(()=>
        {
            startGame();
        },3000);
    }
    else
    {
        document.getElementById("numberField").style.color="red";
        document.getElementById("numberField").innerHTML = "Your answer was wrong!";
        document.getElementById("scoreLabel").hidden=false;
        document.getElementById("scoreLabel").innerHTML=`Your score is ${score}`;
        document.getElementById("answerField").hidden=true;
        document.getElementById("answerField").disabled=true;
        document.getElementById("btnSave").disabled=false;
        document.getElementById("btnSave").hidden=false;
        document.getElementById("restartButton").hidden=false;
        document.getElementById("restartButton").disabled=false;
    }
}

function restartGame()
{
    currentLength = 1;
    score=0;
    numberLength=1;
    document.getElementById("scoreLabel").hidden=true;
    document.getElementById("btnSave").hidden=true;
    document.getElementById("btnSave").disabled=false;
    document.getElementById("restartButton").hidden=true;
    document.getElementById("restartButton").disabled = true;
    document.getElementById("answerField").disabled = false;
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