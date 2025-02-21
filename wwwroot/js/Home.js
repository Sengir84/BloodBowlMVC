document.addEventListener("DOMContentLoaded", function () {

    const button = document.getElementById("RosterDrop");
    const menu = document.getElementById("DropdownMenu");

    button.addEventListener("click", function () {
        menu.classList.toggle("show");
    });

    const dropdownMenu = document.getElementById("DropdownMenu");

    //Känner av vilket val som gjorts i dropdown menyn
    dropdownMenu.addEventListener("click", async function (event) {
        event.preventDefault();

        //Kontrollerar så att valet i listan är en länk <a>
        if (event.target.tagName === "A") {   //hämtar namnet på laget från länken
            let rosterRace = event.target.innerText.trim();

            try {
                let response = await fetch(`/api/roster/${rosterRace}`);
                if (!response.ok) throw new Error("Roster not found");

                let stats = await response.json();

                let rosterTable = document.getElementById("rosterTable");
                rosterTable.innerHTML = ""; //Rensar tabellen

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

                stats.forEach(player => {
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
                });

                table.appendChild(tbody);
                rosterTable.appendChild(table);


                    //ild
                const raceImage = `/img/${rosterRace}.png`;
                document.getElementById("racepic").src = raceImage;

                menu.classList.remove("show");
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
});