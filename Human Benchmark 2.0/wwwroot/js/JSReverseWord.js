let randomWord="";
let reverseWord="";
let score =0;

function startGame()
{
    score=0;
    reverseWord="";
    document.getElementById("startBtn").hidden=true;
    document.getElementById("startBtn").disabled=true;
    document.getElementById("answerInput").hidden=false;
    document.getElementById("answerInput").disabled=false;
    document.getElementById("saveBtn").hidden=true;
    document.getElementById("saveBtn").disabled=true;
    document.getElementById("scoreOutput").disabled=true;
    document.getElementById("scoreOutput").hidden=true;
    document.getElementById("currentWord").hidden=false;
    nextWord();
}

function nextWord()
{
    grabWord();
    setTimeout(()=>
    {
        checkAnswer();
    },1000*score+2000);
}

function checkAnswer()
{
    document.getElementById("currentWord").innerText="";
    if(document.getElementById("answerInput").value.trim()==reverseWord)
    {
        score++;
        nextWord();
    }
    else
    {
        failGame();
    }
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
    document.getElementById("currentWord").hidden=true;
}

async function loadWordsFromApi() 
{
    try {
        let response = await fetch("/ReverseWord/GetWords"); // relative to your site root
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

async function grabWord()
{
    const allWords = await loadWordsFromApi();
    const wordsWithSpecificLength = allWords.filter(x=>x.length==score+1);
    const randomIndex = Math.floor(Math.random() * words.length);
    randomWord = wordsWithSpecificLength[randomIndex];
    document.getElementById("currentWord").innerText = randomWord;
}

function reverseAWord()
{
    // possible error
    for(let i=0;i<randomWord.length;i++)
    {
        reverseAWord[i] = randomWord[randomWord.length-1-i];
    }
}