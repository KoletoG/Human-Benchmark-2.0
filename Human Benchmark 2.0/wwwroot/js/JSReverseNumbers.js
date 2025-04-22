let number=0;
let score =0;
let reverseNumber = 0;
let startBtn;
let saveBtn;
let scoreOutput;
let answerInput;
let currentNumber;
document.addEventListener("DOMContentLoaded", () => {
    startBtn = document.getElementById("startBtn");
    saveBtn = document.getElementById("saveBtn");
    scoreOutput = document.getElementById("scoreOutput");
    answerInput = document.getElementById("answerInput");
    currentNumber = document.getElementById("currentNumber");
});
function startGame()
{
    score=0;
    number=0;
    startBtn.hidden=true;
    startBtn.disabled=true;
    saveBtn.hidden=true;
    saveBtn.disabled=true;
    scoreOutput.disabled=true;
    scoreOutput.hidden=true;
    currentNumber.hidden=false;
    nextNumber();
}

async function nextNumber()
{
    answerInput.value="";
    answerInput.hidden=true;
    answerInput.disabled=true;
    grabNumber();
    setTimeout(()=>
    {
        checkAnswer();
    },1000*score+3000);
}

function checkAnswer()
{

    currentNumber.innerText="";
    answerInput.hidden=false;
    answerInput.disabled=false;
    setTimeout(()=>
    {
        if(answerInput.value==reverseNumber)
            {
                score++;
                currentNumber.innerText="Right!";
                answerInput.hidden=true;
                answerInput.disabled=true;
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
    startBtn.hidden=false;
    startBtn.disabled=false;
    answerInput.hidden=true;
    answerInput.disabled=true;
    scoreOutput.disabled=false;
    scoreOutput.hidden=false;
    scoreOutput.innerText=score;
    saveBtn.hidden=false;
    saveBtn.disabled=false;
    currentNumber.hidden=true;
}

function saveStats()
{
    const token = document.querySelector('input[name="__RequestVerificationToken"]').value;
    fetch("/ReverseNumbers/SaveNumbersScore", 
        {
            method: "POST",
            headers: 
            {
                "Content-Type": "application/json",
                "RequestVerificationToken":token
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
    currentNumber.innerText = randomNumber;
    reverseNumber = reverseANumber(randomNumber);
}

function reverseANumber(number) 
{
    let reversedNumber = 0;
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