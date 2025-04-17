let score=0;
let randomWord="";
let firstWordPassed = false;
let allWordsFromApi = [];
let seenBtn;
let notSeenBtn;
let btnStart;
let saveBtn;
let restartBtn;
let currentWord;
let wordsCurrentList = new Set();
document.addEventListener("DOMContentLoaded", () => {
    seenBtn = document.getElementById("seenBtn");
    notSeenBtn = document.getElementById("notSeenBtn");
    btnStart = document.getElementById("btnStart");
    saveBtn = document.getElementById("saveBtn");
    restartBtn = document.getElementById("restartBtn");
    currentWord = document.getElementById("currentWord");
});
async function startGame()
{
    await grabWord();
    seenBtn.hidden = false;
    seenBtn.disabled = false;
    notSeenBtn.hidden = false;
    notSeenBtn.disabled = false;
    btnStart.hidden=true;
    btnStart.disabled=true;
}

async function seenWord()
{
    if(wordsCurrentList.has(randomWord))
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
    seenBtn.hidden = true;
    seenBtn.disabled = true;
    notSeenBtn.hidden = true;
    notSeenBtn.disabled = true;
    saveBtn.hidden = false;
    saveBtn.disabled = false;
    restartBtn.hidden=false;
    restartBtn.disabled=false;
    currentWord.innerText=score;
}

function restartGame()
{
    saveBtn.hidden = true;
    saveBtn.disabled = true;
    restartBtn.hidden=true;
    restartBtn.disabled=true;
    score=0;
    wordsCurrentList.clear();
    startGame();
}

async function notSeenWord()
{
    if(wordsCurrentList.has(randomWord))
    {
        gameOver();
    }
    else
    {
        score++;
        wordsCurrentList.add(randomWord);
        await grabWord();
    }
}

async function grabWord()
{
    if(!firstWordPassed)
    {
        allWordsFromApi = await loadWordsFromApi();
        firstWordPassed=true;
        const randomIndex = Math.floor(Math.random() * allWordsFromApi.length);
        randomWord = allWordsFromApi[randomIndex];
    }
    else
    {
        if(Math.random()>0.66)
        {
            let array = [...wordsCurrentList]
            const randomIndex = Math.floor(Math.random() * array.length);
            randomWord = array[randomIndex];
        }
        else
        {
            const randomIndex = Math.floor(Math.random() * allWordsFromApi.length);
            randomWord = allWordsFromApi[randomIndex];
        }
    }
    currentWord.innerText = randomWord;
}

async function loadWordsFromApi() {
    try {
        let response = await fetch("/MemoryWords/GetWords"); // relative to your site root
        if (!response.ok) {
            throw new Error("Network response was not ok");
        }
        const words = await response.json(); // this will be a string[]
        console.log("Words from API:", words);
        return words;
    } catch (error) {
        console.error("Fetch error:", error);
        return [];
    }
}
function saveStats()
{
    fetch("/MemoryWords/SaveWordsScore", 
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