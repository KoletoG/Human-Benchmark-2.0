let avgTime=0;
let index=0;
let date1;
let date2;
let times=[];
let currentLoop = 0;
let leftBtn;
let rightBtn;
let upBtn;
let downBtn;
let btnStart;
let saveBtn;
let avgTimeDoc;
let timeDoc;
document.addEventListener("DOMContentLoaded", () =>
{
    leftBtn = document.getElementById("leftBtn");
    rightBtn = document.getElementById("rightBtn");
    upBtn = document.getElementById("upBtn");
    downBtn = document.getElementById("downBtn");
    btnStart = document.getElementById("btnStart");
    saveBtn = document.getElementById("saveBtn");
    avgTimeDoc = document.getElementById("avgTime");
    timeDoc = document.getElementById("time");

});
function startGame()
{
    answerButtonsDisableHide(false);
    btnStart.hidden=true;
    btnStart.disabled=true;
    saveBtn.hidden=true;
    saveBtn.disabled=true;
    avgTimeDoc.hidden=true;
    avgTimeDoc.disabled=true;
    times=[];
    avgTime=0;
    currentLoop=0;
    playSound();
}
function saveStats()
{
    fetch("/AudioReaction/SaveAudioTime",
        {
            method: "POST",
            headers:
            {
                "Content-Type": "application/json"
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
function nextLevel()
{
    if(currentLoop>4)
    {
        for(let i=0;i<5;i++)
        {
            avgTime+=times[i];
        }
        avgTime/=5;
        avgTimeDoc.hidden=false;
        avgTimeDoc.disabled=false;
        avgTimeDoc.innerText=avgTime;
        saveBtn.hidden=false;
        saveBtn.disabled=false;
        btnStart.hidden=false;
        btnStart.disabled=false;
        answerButtonsDisableHide(true);
    }
    else
    {
        answerButtonsDisableHide(true);
        setTimeout(()=>
        {
            playSound();
        },2000);
    }
}
function playSound()
{
    answerButtonsDisableHide(false);
    let nameId="myAudio";
    index=Math.floor(Math.random()*4)+1;
    nameId+=index ;
    document.getElementById(nameId).play();
    date1=new Date();
}

function failGame()
{
    answerButtonsDisableHide(true);
    btnStart.hidden=false;
    btnStart.disabled=false;
    timeDoc.innerText="";
    avgTimeDoc.innerText="You failed";
}

function checkAnswer(answer)
{
    if(index==answer)
    {
        date2 = new Date();
        times[currentLoop]=date2-date1;
        timeDoc.innerText=times[currentLoop];
        currentLoop++;
        nextLevel();
    }
    else
    {
        failGame();
    }
}

function answerButtonsDisableHide(trueOrFalse)
{
    leftBtn.hidden=trueOrFalse;
    leftBtn.disabled=trueOrFalse;
    rightBtn.hidden=trueOrFalse;
    rightBtn.disabled=trueOrFalse;
    upBtn.hidden=trueOrFalse;
    upBtn.disabled=trueOrFalse;
    downBtn.hidden=trueOrFalse;
    downBtn.disabled=trueOrFalse;
}