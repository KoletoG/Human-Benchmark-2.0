
let trialCount = 0;
let reactionTimes = [];
let startTime;
let waiting = false;
let timeoutId = null;
let cycleRepeat = 3;
let button;
let box;
let avgDisplay;
let startBtn;
let submitBtn;
document.addEventListener("DOMContentLoaded", () => {
    submitBtn = document.getElementById("submitt");
    submitBtn.disabled = true;
    startBtn = document.getElementById("startBtn");
    button = document.getElementsByName("submitt");
    box = document.getElementById("box");
    avgDisplay = document.getElementById("averageTime");
});

function getRandomDelay() {
    return Math.floor(Math.random() * 4000) + 2000;
}

function showBox() {
    waiting = true;
    startTime = Date.now();
    box.style.backgroundColor = "green";
    box.textContent = "CLICK!";
}

function startTrial() {
    box.textContent = "Wait for green...";
    box.style.backgroundColor = "red";
    timeoutId = setTimeout(showBox, getRandomDelay());
}

function endTest() {
    const average = Math.round(reactionTimes.reduce((a, b) => a + b) / reactionTimes.length);
    avgDisplay.textContent = `Average Reaction Time: ${average} ms`;
    box.textContent = "Done!";
    box.style.backgroundColor = "gray";
    finalAverage = average;
    submitBtn.disabled = false;
    submitBtn.hidden = false;
    startBtn.hidden = false;
    startBtn.disabled = false;
}

function startNewGame()
{
    submitBtn.disabled = true;
    submitBtn.hidden = true;
    trialCount=0;
    waiting=false;
    reactionTimes=[];
    timeoutId = null;
    startBtn.hidden = true;
    startBtn.disabled = true;
    avgDisplay.innerText="";
    startReaction();
}
function saveStats()
{
   
    if (finalAverage == null) return;
        fetch("/ReactionTime/ReactionTimeSave", 
        {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(finalAverage)
        })
            .then(res => res.json())
            .then(data => {
                if (data.redirectUrl) {
                    window.location.href = data.redirectUrl;
                }
        });
    
}

function startReaction()
{
        if (trialCount >= cycleRepeat) return;
    
        if (!waiting && timeoutId === null) {
            startTrial();
        }
        else if (waiting) {
            const reactionTime = Date.now() - startTime;
            reactionTimes.push(reactionTime);
            trialCount++;
            waiting = false;
            timeoutId = null;
            if (trialCount < cycleRepeat) {
                setTimeout(startTrial);
            }
            else {
                endTest();
            }
        }
        else 
        {
            clearTimeout(timeoutId);
            timeoutId = null;
            box.textContent = "Too Soon! Restarting...";
            box.style.backgroundColor = "gray";
            avgDisplay.textContent = "You clicked too early. Trial restarting...";
            setTimeout(startTrial);
        }
}