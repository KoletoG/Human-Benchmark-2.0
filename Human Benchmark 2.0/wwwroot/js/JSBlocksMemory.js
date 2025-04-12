 // 7x7, let name += idBtn, **createElement("button")**, random elements 0-49 in array/list, timeout 5 sec, color = red, onclick()="failGame()", int clicked=0; 
let score =0;
let idsFull = [];
let container;

document.addEventListener("DOMContentLoaded",()=>
{
    container = document.getElementById("gameContainer");
})

function startGame()
{
    score=0;
    idsFull=[];
    document.getElementById("startBtn").hidden=true;
    document.getElementById("startBtn").disabled=true;
    document.getElementById("saveBtn").hidden=true;
    document.getElementById("saveBtn").disabled=true;
    document.getElementById("scoreDiv").hidden=true;
    document.getElementById("scoreDiv").disabled=true;
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
    document.getElementById("saveBtn").hidden=false;
    document.getElementById("saveBtn").disabled=false;
    document.getElementById("startBtn").hidden=false;
    document.getElementById("startBtn").disabled=false;
    document.getElementById("scoreDiv").hidden=false;
    document.getElementById("scoreDiv").disabled=false;
    document.getElementById("scoreDiv").innerText=score;
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
        btn.disabled=true;
        setTimeout(()=>{
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