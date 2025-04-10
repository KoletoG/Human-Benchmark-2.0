let wordsAllList = ["word1","word2","word3","word4","word5"];
let wordsCurrentList = [""];
let wordChoice = "";
let isTrueOrFalse = false;
let response;
document.addEventListener("DOMContentLoaded", () => 
{
    let currentWord = document.getElementById("currentWord");
});
async function startGame()
{
    await loadWords();
    await grabWord();
}
async function grabWord()
{
    const text = await response.text();
    const words = text.split("\n").filter(w => w.trim().length > 0);
    const randomIndex = Math.floor(Math.random() * words.length);
    const randomWord = words[randomIndex];
    currentWord.innerText = randomWord;
}
async function loadWords()
{
    try 
    {
        response = await fetch("https://www.mit.edu/~ecprice/wordlist.10000");
    }
    catch (error)
    {
        console.error("Error fetching word list:", error);
    }
}