 // 7x7, let name += idBtn, **createElement("button")**, random elements 0-49 in array/list, timeout 5 sec, color = red, onclick()="failGame()", int clicked=0; 
let score =0;
let idsFull = [];
let container;
document.addEventListener("DOMContentLoaded",()=>{
     container = document.getElementById("gameContainer");
})
function startGame()
{
    document.getElementById("startBtn").hidden=true;
    document.getElementById("startBtn").disabled=true;
    createBlocks(score);
}

function createBlocks(number)
{
    for(let i =0;i<number+1;i++)
    {
        idsFull.push(Math.floor(Math.random()*49));
    }
    for(let i=0;i<49;i++)
    {
        let idName = "id";
        idName+=i;
        let btn = document.createElement("button");
        btn.setAttribute("id",idName);
        btn.style.height="50px";
        btn.style.width="50px";
        btn.style.margin="10px";
        btn.innerText="HAA";
        if(idsFull.includes(i))
        {
            showRight(btn);
            btn.addEventListener("click",()=>
                {
                    btn.style.backgroundColor="green";
                }
                );
        }
        else
        {
            showWrong(btn);
            btn.addEventListener("click",()=>
            {
                btn.style.backgroundColor="red";
                alert("Game failed");
            }
            );
        }
        container.appendChild(btn);
        if((i+1)%7==0)
        {
            document.createElement("br");
            container.appendChild(document.createElement("br"));
        }
    }
}

function showWrong(btn)
{
    btn.style.backgroundColor="red";
    setTimeout(()=>
    {
        btn.style.backgroundColor="gray";
    },4000)
}

function showRight(btn)
{
    btn.style.backgroundColor="green";
    setTimeout(()=>
        {
            btn.style.backgroundColor="gray";
        },4000)
}