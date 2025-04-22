let score =0;
let idsFull = [];
let container;
let startBtn;
let saveBtn;
let scoreDiv;
document.addEventListener("DOMContentLoaded", () =>
{
    container = document.getElementById("gameContainer");
    startBtn = document.getElementById("startBtn");
    saveBtn = document.getElementById("saveBtn");
    scoreDiv = document.getElementById("scoreDiv");
});

function startGame()
{
    score=0;
    idsFull=[];
    startBtn.hidden=true;
    startBtn.disabled=true;
    saveBtn.hidden=true;
    saveBtn.disabled=true;
    scoreDiv.hidden=true;
    scoreDiv.disabled=true;
    createBlocks(score);
}

function nextLevel()
{
    score++;
    createBlocks(score);
}

function checkBlock(id)
{
    for(let i =0;i<idsFull.length;i++)
    {
        if(idsFull[i]==id)
        {
            idsFull[i]=-1;
            break;
        }
    }
    let last=true;
    for(let i=0;i<idsFull.length;i++)
    {
        if(idsFull[i]!=-1)
        {
            last=false;
            break;
        }
    }
    if(last)
    {
        deleteOldBlocks();
        setTimeout(()=>
        {
            nextLevel();
        },1200);
    }
}

function gameOver()
{
    deleteOldBlocks();
    saveBtn.hidden=false;
    saveBtn.disabled=false;
    startBtn.hidden=false;
    startBtn.disabled = false;
    scoreDiv.hidden = false;
    scoreDiv.disabled=false;
    scoreDiv.innerText=score;
}

function deleteOldBlocks()
{
    for(let i=0;i<49;i++)
    {
        let idName = "id";
        idName+=i;
        document.getElementById(idName).remove();
    }
    for(let i=6;i<=48;i+=7)
        {
        let idName = "idBr";
        idName+=i;
        document.getElementById(idName).remove();
    }
}

function createBlocks(number)
{
    idsFull=[];
    for(let i =0;i<number+1;i++)
    {
        let idNum = Math.floor(Math.random()*49);
        if(idsFull.indexOf(idNum)==-1)
        {
            idsFull.push(idNum);
            continue;
        }
        i--;
    }
    for(let i=0;i<49;i++)
    {
        let idName = "id";
        idName+=i;
        let btn = document.createElement("button");
        btn.setAttribute("id", idName);
        btn.setAttribute("type", "button");
        btn.style.height="50px";
        btn.style.width="50px";
        btn.style.margin="10px";
        btn.disabled=true;
        setTimeout(()=>
        {
            btn.disabled=false;
        },4000)
        if(idsFull.includes(i))
        {
            showRight(btn);
            btn.addEventListener("click",()=>
                {
                    checkBlock(i);
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
                gameOver();
            }
            );
        }
        container.appendChild(btn);
        if((i+1)%7==0)
        {
            idName="idBr";
            idName+=i;
            let line = document.createElement("br");
            line.setAttribute("id",idName)
            container.appendChild(line);
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

function saveStats()
{
    const token = document.querySelector('input[name="__RequestVerificationToken"]').value;
    fetch("/BlocksMemory/SaveBlocksScore", 
    {
        method: "POST",
        headers: 
        {
            "Content-Type": "application/json",
            "RequestVerificationToken": token
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