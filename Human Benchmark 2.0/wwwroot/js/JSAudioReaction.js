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
let avgTime;
let timeDoc;
document.addEventListener("DOMContentLoaded", () =>
{
    leftBtn = document.getElementById("leftBtn");
    rightBtn = document.getElementById("rightBtn");
    upBtn = document.getElementById("upBtn");
    downBtn = document.getElementById("downBtn");
    btnStart = document.getElementById("btnStart");
    saveBtn = document.getElementById("saveBtn");
    avgTime = document.getElementById("avgTime");
    timeDoc = document.getElementById("time");

});
function startGame()
{
    leftBtn.hidden = false;
    leftBtn.disabled=false;
    rightBtn.hidden=false;
    rightBtn.disabled=false;
    upBtn.hidden=false;
    upBtn.disabled=false;
    downBtn.hidden=false;
    downBtn.disabled=false;
    btnStart.hidden=true;
    btnStart.disabled=true;
    saveBtn.hidden=true;
    saveBtn.disabled=true;
    avgTime.hidden=true;
    avgTime.disabled=true;
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
        avgTime.hidden=false;
        avgTime.disabled=false;
        avgTime.innerText=avgTime;
        saveBtn.hidden=false;
        saveBtn.disabled=false;
        btnStart.hidden=false;
        btnStart.disabled=false;
        leftBtn.hidden=true;
        leftBtn.disabled=true;
        rightBtn.hidden=true;
        rightBtn.disabled=true;
        upBtn.hidden=true;
        upBtn.disabled=true;
        downBtn.hidden=true;
        downBtn.disabled=true;
    }
    else
    {
        leftBtn.hidden=true;
        leftBtn.disabled=true;
        rightBtn.hidden=true;
        rightBtn.disabled=true;
        upBtn.hidden=true;
        upBtn.disabled=true;
        downBtn.hidden=true;
        downBtn.disabled=true;
        setTimeout(()=>
        {
            playSound();
        },2000);
    }
}
function playSound()
{
    leftBtn.hidden=false;
    leftBtn.disabled=false;
    rightBtn.hidden=false;
    rightBtn.disabled=false;
    upBtn.hidden=false;
    upBtn.disabled=false;
    downBtn.hidden=false;
    downBtn.disabled=false;
    let nameId="myAudio";
    index=Math.floor(Math.random()*4)+1;
    document.getElementById("testS").innerText=index;
    nameId+=index ;
    document.getElementById(nameId).play();
    date1=new Date();
}
function checkAnswerLeft()
{
    if(index==1)
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
function failGame()
{
    leftBtn.hidden=true;
    leftBtn.disabled=true;
    rightBtn.hidden=true;
    rightBtn.disabled=true;
    upBtn.hidden=true;
    upBtn.disabled=true;
    downBtn.hidden=true;
    downBtn.disabled=true;
    btnStart.hidden=false;
    btnStart.disabled=false;
    timeDoc.innerText="";
    avgTime.innerText="You failed";
}
function checkAnswerRight()
{
    if(index==2)
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
function checkAnswerUp()
{
    if(index==3)
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
function checkAnswerDown()
{
    if(index==4)
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