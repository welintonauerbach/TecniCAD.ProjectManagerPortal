const request = new XMLHttpRequest();

request.open("GET", "http://localhost:5000/api/projects/5", true);

const app = document.getElementById("root");
const tcDiv = document.createElement("div");
tcDiv.setAttribute("class", "container");

app.appendChild(tcDiv);

request.onload = function() {
    let data = JSON.parse(this.responseText);

    if (request.status >= 200 && request.status < 400) {
        var map = new Map(Object.entries(data));
        let num = 1;

        setData(data);

        for (const item in data.itens) {
            console.log(item.itemNumber);
            //setData(item);
        }
    } else {
        console.log("erro");
    }
};

function setData(item) {
    const tcDivData = document.createElement("div");
    tcDivData.innerHTML = `Projeto: ${item.projectNumber} - Nome: ${item.projectName}`;

    const tcTable = document.createElement("table");
    tcTable.style.border = "thin solid #000000";
    tcTable.style.flex = "auto";
    tcDivData.appendChild(tcTable);

    item.items.forEach(i => {
        const tcRow = document.createElement("tr");
        const tcData1 = document.createElement("td");
        const tcData2 = document.createElement("td");
        const tcData3 = document.createElement("td");

        tcData1.innerHTML = `${i.itemNumber}`;
        tcData2.innerHTML = `${i.ofNumber}`;
        tcData3.innerHTML = `${i.name}`;

        tcRow.appendChild(tcData1);
        tcRow.appendChild(tcData2);
        tcRow.appendChild(tcData3);
        tcTable.appendChild(tcRow);
    });

    tcDiv.appendChild(tcDivData);
}

request.send();