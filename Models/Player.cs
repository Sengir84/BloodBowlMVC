using System;
using System.Collections.Generic;

namespace BloodBowlMVC.Models;

public partial class Player
{
    public int PlayerIdPk { get; set; }

    public string? PlayerName { get; set; }

    public string? PlayerPosition { get; set; }

    public int? Playercost { get; set; }

    public int? PlayerMovement { get; set; }

    public int? PlayerStrength { get; set; }

    public int? PlayerAgility { get; set; }

    public int? PlayerArmorValue { get; set; }

    public string? PlayerStatus { get; set; }

    public int? PlayerTeamIdFk { get; set; }

    public int? PlayerRosterIdFk { get; set; }

    public virtual Roster? PlayerRosterIdFkNavigation { get; set; }

    public virtual Team? PlayerTeamIdFkNavigation { get; set; }

    public virtual ICollection<Skill> PlayerSkillsSkillIdFks { get; set; } = new List<Skill>();
}
