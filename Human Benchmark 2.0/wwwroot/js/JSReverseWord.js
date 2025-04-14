let randomWord="";
let reverseWord="";
let score =0;
let firstTime = true;
let allWords = [];
let startBtn;
let saveBtn;
let scoreOutput;
let answerInput;
let currentWord;
document.addEventListener("DOMContentLoaded", () => {
    startBtn = document.getElementById("startBtn");
    saveBtn = document.getElementById("saveBtn");
    scoreOutput = document.getElementById("scoreOutput");
    answerInput = document.getElementById("answerInput");
    currentWord = document.getElementById("currentWord");
});
function startGame()
{
    score=0;
    reverseWord="";
    startBtn.hidden=true;
    startBtn.disabled=true;
    saveBtn.hidden=true;
    saveBtn.disabled=true;
    scoreOutput.disabled=true;
    scoreOutput.hidden=true;
    currentWord.hidden=false;
    nextWord();
}

async function nextWord()
{
    answerInput.value="";
    answerInput.hidden=true;
    answerInput.disabled=true;
    await grabWord();
    setTimeout(()=>
    {
        checkAnswer();
    },1000*score+3000);
}

function checkAnswer()
{

    currentWord.innerText = "";
    answerInput.hidden=false;
    answerInput.disabled=false;
    setTimeout(() =>
    {
        if (answerInput.value == reverseWord)
            {
                score++;
                currentWord.innerText = "Right!";
                answerInput.hidden=true;
                answerInput.disabled=true;
                setTimeout(()=>{
                    nextWord();
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
    currentWord.hidden=true;
}

async function loadWordsFromApi() 
{
    try {
        let response = await fetch("/ReverseWord/GetWords");
        if (!response.ok) {
            throw new Error("Network response was not ok");
        }
        const words = await response.json();
        console.log("Words from API:", words);
        return words;
    } catch (error) {
        console.error("Fetch error:", error);
        return [];
    }
}

function saveStats()
{
    fetch("/ReverseWord/SaveWordsScore", 
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

async function grabWord()
{
    if (firstTime)
    {
        allWords = await loadWordsFromApi();
        firstTime = false;
    }
    const wordsWithSpecificLength = allWords.filter(x=>x.length==score+3);    
    if (wordsWithSpecificLength.length === 0)
    {
        alert("No more words of that length!");
        failGame();
        return;
    }
    const randomIndex = Math.floor(Math.random() * wordsWithSpecificLength.length);
    randomWord = wordsWithSpecificLength[randomIndex];
    reverseWord = reverseAWord(randomWord);
    document.getElementById("currentWord").innerText = randomWord;
}

function reverseAWord(word) {
    return word.split("").reverse().join("").trim();
}