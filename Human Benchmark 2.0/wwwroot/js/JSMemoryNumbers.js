let number = "";
let currentLength = 1;
let score=0;
function startGame()
{
    for(let i =0;i<currentLength;i++)
    {
        number[i]=Math.random()*10;
    }
    document.getElementById("numberField").style.color="black";
    document.getElementById("numberField").innerText=number;
    document.getElementById("answerField").innerText="";
    setTimeout(()=>
    {
        checkAnswer();
    },currentLength*1000 + 3000);
}

function checkAnswer()
{
    document.getElementById("numberField").innerText="Type your answer";
    let answer = document.getElementById("answerField").innerText;
    if(parseInt(number) == parseInt(answer))
    {
        document.getElementById("numberField").style.color="green";
        document.getElementById("numberField").innerText="Your answer is right. Continuing..."
        currentLength++;
        score++;
        startGame();
    }
    else
    {
        document.getElementById("numberField").style.color="red";
        document.getElementById("numberField").innerText="Your answer was wrong!";
        document.getElementById("scoreLabel").hidden=false;
        document.getElementById("scoreLabel").innerText=`Your score is ${score}`;
        document.getElementById("answerField").hidden=true;
        document.getElementById("answerField").disabled=true;
    }
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