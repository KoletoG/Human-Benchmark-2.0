let questions=["2, 6, 12, 20, 30, ___","How many squares are there in a 3×3 grid (including smaller and larger squares)?","A father is 36 years old and his son is 6. In how many years will the father’s age be exactly twice his son's?","I am a two-digit number. My tens digit is twice my ones digit. The sum of my digits is 9. What number am I?","I am a number between 30 and 47. I am divisible by both 3 and 4. What number am I?"];
let answers = [42,14,24,63,36];
let startBtn;
let answerField;
let questionField;
let submitBtn;
let index=0;
document.addEventListener("DOMContentLoaded",()=>
{
    startBtn=document.getElementById("startBtn");
    answerField=document.getElementById("answerField");
    questionField=document.getElementById("questionField");
    submitBtn=document.getElementById("submitBtn");
});
function startGame()
{
    startBtn.hidden=true;
    startBtn.disabled=true;
}

function pickQuestion()
{
    submitBtn.hidden=false;
    submitBtn.disabled=false;
    answerField.hidden=false;
    answerField.disabled=false;
    index = Math.floor(Math.random()*questions.length);
    questionField.innerText=questions[index];
}

function checkAnswer()
{
    if(answerField.value==answers[index])
    {
        questionField.innerText="Right!, loading next question...";
    }
    else
    {
        questionField.innerText=`Wrong! The answer was ${answers[index]}, loading next question...`;
    }
}

function loadNextQuestion()
{
    submitBtn.hidden=true;
    submitBtn.disabled=true;
    answerField.value="";
    answerField.hidden=true;
    answerField.disabled=true;
    setTimeout(()=>
    {
        pickQuestion();
    },3000);
}