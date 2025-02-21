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

                document.getElementById("rosterTable").innerHTML = `
                    <table border="1">
                        <tr><th>Position</th><th>COST</th><th>MOV</th><th>STR</th><th>AGI</th><th>AV</th><th>Skills</th></tr>
                        ${stats.map(player => `
                            <tr>
                                <td>${player.rosterPosition}</td>
                                <td>${player.rosterCost}</td>
                                <td>${player.rosterMovement}</td>
                                <td>${player.rosterStrength}</td>
                                <td>${player.rosterAgility}</td>
                                <td>${player.rosterArmor}</td>
                                <td>${player.skills?.join(", ") || "No Skills"}</td>
                            </tr>
                        `).join('')}
                    </table>`;
            
                    //Försök att få in en bild
                const raceImage = `/img/${rosterRace}.png`;
                document.getElementById("racepic").src = raceImage;

                menu.classList.remove("show");
            }
            catch (error) {
                console.error("Error fetching roster data:", error);
                document.getElementById("rosterTable").innerHTML = `<p>Error loading roster data.</p>`;
            }
        }
    });
});