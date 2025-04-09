let number = "";
let currentLength = 1;

for(let i =0;i<currentLength;i++)
{
    number[i]=Math.random()*10;
}
document.getElementById("numberField").style.color="black";
document.getElementById("numberField").innerText=number;
let answer = document.getElementById("answerField").innerText;
function checkAnswer()
{
if(parseInt(number) == parseInt(answer))
{
    document.getElementById("numberField").style.color="green";
    document.getElementById("numberField").innerText="Your answer is right. Continuing..."
}
else
{
    
    document.getElementById("numberField").style.color="red";
    document.getElementById("numberField").innerText="Your answer was wrong!";
}
}