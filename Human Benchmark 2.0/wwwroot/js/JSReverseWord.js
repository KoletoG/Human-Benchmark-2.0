let randomWord="";
let reverseWord="";
let score =0;

function startGame()
{
    score=0;
    reverseWord="";
    document.getElementById("startBtn").hidden=true;
    document.getElementById("startBtn").disabled=true;
    document.getElementById("saveBtn").hidden=true;
    document.getElementById("saveBtn").disabled=true;
    document.getElementById("scoreOutput").disabled=true;
    document.getElementById("scoreOutput").hidden=true;
    document.getElementById("currentWord").hidden=false;
    nextWord();
}

async function nextWord()
{
    document.getElementById("answerInput").value="";
    document.getElementById("answerInput").hidden=true;
    document.getElementById("answerInput").disabled=true;
    await grabWord();
    setTimeout(()=>
    {
        checkAnswer();
    },1000*score+3000);
}

function checkAnswer()
{

    document.getElementById("currentWord").innerText="";
    document.getElementById("answerInput").hidden=false;
    document.getElementById("answerInput").disabled=false;
    setTimeout(()=>{
        let answer =document.getElementById("answerInput").value;
        if(answer==reverseWord)
            {
                score++;
                document.getElementById("currentWord").innerText="Right!";
                document.getElementById("answerInput").hidden=true;
                document.getElementById("answerInput").disabled=true;
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

async function grabWord()
{
    const allWords = await loadWordsFromApi();
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