let wordsCurrentList = [];
let score=0;
let randomWord="";
let firstWordPassed = false;
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
    if(!firstWordPassed)
    {
        const words = await loadWordsFromApi();
        firstWordPassed=true;
        const randomIndex = Math.floor(Math.random() * words.length);
        randomWord = words[randomIndex];
    }
    else
    {
        if(Math.random()>0.66)
        {
            const randomIndex = Math.floor(Math.random() * wordsCurrentList.length);
            randomWord = wordsCurrentList[randomIndex];
        }
        else
        {
            const words = await loadWordsFromApi();
            const randomIndex = Math.floor(Math.random() * words.length);
            randomWord = words[randomIndex];
        }
    }
    document.getElementById("currentWord").innerText = randomWord;
}

async function loadWordsFromApi() {
    try {
        let response = await fetch("/api/GetWords"); // relative to your site root
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