let avgTime=0;
let index=0;
let date1;
let date2;
let times=[];
let currentLoop=0;
function startGame()
{
    document.getElementById("leftBtn").hidden=false;
    document.getElementById("leftBtn").disabled=false;
    document.getElementById("rightBtn").hidden=false;
    document.getElementById("rightBtn").disabled=false;
    document.getElementById("upBtn").hidden=false;
    document.getElementById("upBtn").disabled=false;
    document.getElementById("downBtn").hidden=false;
    document.getElementById("downBtn").disabled=false;
    document.getElementById("btnStart").hidden=true;
    document.getElementById("btnStart").disabled=true;
    document.getElementById("saveBtn").hidden=true;
    document.getElementById("saveBtn").disabled=true;
    document.getElementById("avgTime").hidden=true;
    document.getElementById("avgTime").disabled=true;
    times=[];
    avgTime=0;
    currentLoop=0;
    playSound();
}
function nextLevel()
{
    if(currentLoop>4)
    {
        for(let i=0;i<5;i++)
        {
            avgTime+=time[i];
        }
        avgTime/=5;
        document.getElementById("avgTime").hidden=false;
        document.getElementById("avgTime").disabled=false;
        document.getElementById("avgTime").innerText=avgTime;
        document.getElementById("saveBtn").hidden=false;
        document.getElementById("saveBtn").disabled=false;
    }
    else
    {
    setTimeout(playSound(),2000);
    }
}
function playSound()
{
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
        document.getElementById("time").innerText=times[currentLoop];
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
    document.getElementById("leftBtn").hidden=true;
    document.getElementById("leftBtn").disabled=true;
    document.getElementById("rightBtn").hidden=true;
    document.getElementById("rightBtn").disabled=true;
    document.getElementById("upBtn").hidden=true;
    document.getElementById("upBtn").disabled=true;
    document.getElementById("downBtn").hidden=true;
    document.getElementById("downBtn").disabled=true;
    document.getElementById("btnStart").hidden=false;
    document.getElementById("btnStart").disabled=false;
    document.getElementById("time").innerText="";
    document.getElementById("avgTime").innerText="You failed";
}
function checkAnswerRight()
{
    if(index==2)
    {
        date2 = new Date();
        times[currentLoop]=date2-date1;
        document.getElementById("time").innerText=times[currentLoop];
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
        document.getElementById("time").innerText=times[currentLoop];
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
        document.getElementById("time").innerText=times[currentLoop];
        currentLoop++;
        nextLevel();
    }
    else
    {
        failGame();
    }
}