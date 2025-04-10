let wordsCurrentList = [];
let score=0;
let randomWord="";

async function startGame()
{
    await grabWord();
    document.getElementById("seenBtn").hidden = false;
    document.getElementById("seenBtn").disabled = false;
    document.getElementById("notSeenBtn").hidden = false;
    document.getElementById("notSeenBtn").disabled = false;
}

async function seenWord()
{
    if(wordsCurrentList.includes(randomWord))
    {
        score++;
        await grabWord();
    }
    else
    {
        gameOver();
    }
}

function gameOver()
{
    document.getElementById("seenBtn").hidden = true;
    document.getElementById("seenBtn").disabled = true;
    document.getElementById("notSeenBtn").hidden = true;
    document.getElementById("notSeenBtn").disabled = true;
    document.getElementById("saveBtn").hidden = false;
    document.getElementById("saveBtn").disabled = false;
    document.getElementById("restartBtn").hidden=false;
    document.getElementById("restartBtn").disabled=false;
    document.getElementById("currentWord").innerText=score;
}

function restartGame()
{
    document.getElementById("saveBtn").hidden = true;
    document.getElementById("saveBtn").disabled = true;
    document.getElementById("restartBtn").hidden=true;
    document.getElementById("restartBtn").disabled=true;
    score=0;
    wordsCurrentList=[];
    startGame();
}

async function notSeenWord()
{
    if(wordsCurrentList.includes(randomWord))
    {
        gameOver();
    }
    else
    {
        score++;
        wordsCurrentList.push(randomWord);
        await grabWord();
    }
}

async function grabWord()
{
    let response = await fetch("https://wordsapiv1.p.mashape.com/words/example");
    const text = response.text();
    const words = text.split("\n").filter(w => w.trim().length > 0);
    const randomIndex = Math.floor(Math.random() * words.length);
    randomWord = words[randomIndex];
    document.getElementById("currentWord").innerText = randomWord;
}
