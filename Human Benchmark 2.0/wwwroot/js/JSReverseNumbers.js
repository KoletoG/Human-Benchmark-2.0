let number=0;
let score =0;
let reverseNumber=0;
function startGame()
{
    score=0;
    number=0;
    document.getElementById("startBtn").hidden=true;
    document.getElementById("startBtn").disabled=true;
    document.getElementById("saveBtn").hidden=true;
    document.getElementById("saveBtn").disabled=true;
    document.getElementById("scoreOutput").disabled=true;
    document.getElementById("scoreOutput").hidden=true;
    document.getElementById("currentNumber").hidden=false;
    nextNumber();
}

async function nextNumber()
{
    document.getElementById("answerInput").value="";
    document.getElementById("answerInput").hidden=true;
    document.getElementById("answerInput").disabled=true;
    grabNumber();
    setTimeout(()=>
    {
        checkAnswer();
    },1000*score+3000);
}

function checkAnswer()
{

    document.getElementById("currentNumber").innerText="";
    document.getElementById("answerInput").hidden=false;
    document.getElementById("answerInput").disabled=false;
    setTimeout(()=>
    {
        let answer =document.getElementById("answerInput").value;
        if(answer==reverseNumber)
            {
                score++;
                document.getElementById("currentNumber").innerText="Right!";
                document.getElementById("answerInput").hidden=true;
                document.getElementById("answerInput").disabled=true;
                setTimeout(()=>
                {
                    nextNumber();
                },3000);
            }
            else
            {
                failGame();
            }
    },1000*score+3000);
    
} 

function failGame()
{
    document.getElementById("startBtn").hidden=false;
    document.getElementById("startBtn").disabled=false;
    document.getElementById("answerInput").hidden=true;
    document.getElementById("answerInput").disabled=true;
    document.getElementById("scoreOutput").disabled=false;
    document.getElementById("scoreOutput").hidden=false;
    document.getElementById("scoreOutput").innerText=score;
    document.getElementById("saveBtn").hidden=false;
    document.getElementById("saveBtn").disabled=false;
    document.getElementById("currentNumber").hidden=true;
}

function saveStats()
{
    fetch("/ReverseNumbers/SaveNumbersScore", 
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

function grabNumber()
{
    let powNumber = Math.pow(10,score+2);
    let randomNumber = Math.floor(Math.random() * powNumber);
    let powLength = powNumber.toString().length;
    let randomNumLength = randomNumber.toString().length;
    if (randomNumLength < powLength-1)
    {
        while (randomNumLength < powLength)
        {
            randomNumber*=10;
            randomNumber+=Math.floor(Math.random()*10);
        }
    }
    document.getElementById("currentNumber").innerText = randomNumber;
    reverseNumber = reverseANumber(randomNumber);
}

function reverseANumber(number) 
{
    let reversedNumber=0;
    while(number>0)
    {
        if(number>=10)
        {
            let deleteLast = number%10;
            reversedNumber+=deleteLast;
            reversedNumber*=10;
            number-=deleteLast;
            number/=10;
        }
        else
        {
            reversedNumber+=number;
            number=0;
        }
    }
    return reversedNumber;
}