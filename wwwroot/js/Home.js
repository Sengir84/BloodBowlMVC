document.addEventListener("DOMContentLoaded", function () {
    const playerSelection = document.getElementById("playerSelection");
    const buyTeamButton = document.getElementById("buyTeamButton");
    const selectedPlayersTable = document.getElementById("selectedPlayersTable").getElementsByTagName("tbody")[0];
    const button = document.getElementById("RosterDrop");
    const menu = document.getElementById("DropdownMenu");


    let rosterData = []; // Array för att lagra spelarinformation

    function generatePlayerDropdowns() {
        playerSelection.innerHTML = ""; // Rensa befintligt innehåll

        for (let i = 0; i < 16; i++) {
            let div = document.createElement("div");
            div.classList.add("player-slot");

            let label = document.createElement("label");
            label.textContent = `Player Slot ${i + 1}: `;

            let select = document.createElement("select");
            select.classList.add("playerDropdown");
            select.innerHTML = '<option value="">Select player</option>';

            // Lägg till spelare i dropdown
            rosterData.forEach(player => {
                let option = document.createElement("option");
                option.value = player.rosterIdPk;
                option.textContent = `${player.rosterPosition} - ${player.rosterCost}`;
                select.appendChild(option);
            });

            div.appendChild(label);
            div.appendChild(select);
            playerSelection.appendChild(div);
        }
    }


    //Används denna?
    button.addEventListener("click", function () {
        menu.classList.toggle("show");
    });


    //Känner av vilken ras som väljs i dropdownmenyn
    document.getElementById("DropdownMenu").addEventListener("click", async function (event) {
        event.preventDefault();

        //Kontrollerar så att valet i listan är en länk <a>
        if (event.target.tagName === "A") {   //hämtar namnet på laget från länken
            let rosterRace = event.target.innerText.trim();

            const raceImage = `/img/${rosterRace}.png`;
            document.getElementById("racepic").src = raceImage;

            try {
                menu.classList.remove("show");
                let response = await fetch(`/api/roster/${rosterRace}`);
                if (!response.ok) throw new Error("Roster not found");

                rosterData = await response.json();

                generatePlayerDropdowns();//Genererar spelardropdowns
                
            }
            catch (error) {
                console.error("Error fetching roster data:", error);

                    let errorMessage = document.createElement("p");
                    errorMessage.textContent = "Error loading roster data.";
                    document.getElementById("rosterTable").innerHTML = ""; // Rensa befintligt innehåll
                    document.getElementById("rosterTable").appendChild(errorMessage);
                
            }
        }
    });
    //Köp lag-knappen

    buyTeamButton.addEventListener("click", async function () {
        let selectedPlayers = [];

        document.querySelectorAll(".playerDropdown").forEach(select => {
            if (select.value) {
                let selectedOption = select.options[select.selectedIndex].textContent;
                selectedPlayers.push({ id: select.value, info: selectedOption });
            }
        });

        if (selectedPlayers.length === 11) {
            alert("Choose a minimum of 11 players");
            return;
        }
        selectedPlayersTable.innerHTML = ""; // Rensa befintligt innehåll

        selectedPlayers.forEach(player => {
            let row = document.createElement("tr");
            let positionCell = document.createElement("td");
            let costCell = document.createElement("td");

            positionCell.textContent = player.info.split(" - ")[0];
            costCell.textContent = player.info.split(" - ")[1];

            row.appendChild(positionCell);
            row.appendChild(costCell);
            selectedPlayersTable.appendChild(row);
        });
        alert("Team succesfully purchased!");




        //Skapa spelardropdown
        let playerDropdown = document.getElementById("playerDropdown");
        playerDropdown.innerHTML = '<option value="">Select player</option>'; //Rensar spelarlistan'
        playerDropDown.disabled = true;

        //Skapa tabell
        let table = document.createElement("table");
        table.setAttribute("border", "1");

        //Tabellhuvud
        let thead = document.createElement("thead");
        let headerRow = document.createElement("tr");
        let headers = ["Position", "COST", "MOV", "STR", "AGI", "AV", "Skills"];

        headers.forEach(headerText => {
            let th = document.createElement("th");
            th.textContent = headerText;
            headerRow.appendChild(th);
        });

        thead.appendChild(headerRow);
        table.appendChild(thead);

        //Tabellkropp
        let tbody = document.createElement("tbody");

        rosterData.forEach(player => {
            let row = document.createElement("tr");

            let values = [
                player.rosterPosition,
                player.rosterCost,
                player.rosterMovement,
                player.rosterStrength,
                player.rosterAgility,
                player.rosterArmor,
                player.skills?.join(", ") || "No Skills"];

            values.forEach(value => {
                let td = document.createElement("td");
                td.textContent = value;
                row.appendChild(td);
            });

            tbody.appendChild(row);

            //Fyll spelardropdown
            let option = document.createElement("option");
            option.value = player.rosterIdPk;
            option.textContent = `${player.rosterPosition} - ${player.rosterCost}`;
            playerDropdown.appendChild(option);
        });

        table.appendChild(tbody);
        rosterTable.appendChild(table);

        //Aktivera spelardropdown om det finns spelare
        if (rosterData.length > 0) {
            playerDropdown.disabled = false;
        }


        ////Bild
        //console.log("FUNKAR");
        //const raceImage = `/img/${rosterRace}.png`;
        //document.getElementById("racepic").src = raceImage;
        

        
    });
});