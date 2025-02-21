using System;
using System.Collections.Generic;

namespace BloodBowlMVC.Models;

public partial class Roster
{
    public int RosterIdPk { get; set; }

    public string? RosterRace { get; set; }

    public string? RosterPosition { get; set; }

    public int? RosterCost { get; set; }

    public int? RosterMovement { get; set; }

    public int? RosterStrength { get; set; }

    public int? RosterAgility { get; set; }

    public int? RosterArmor { get; set; }

    public virtual ICollection<Player> Players { get; set; } = new List<Player>();

    public virtual ICollection<Skill> RosterSkillsSkillsIdFks { get; set; } = new List<Skill>();
}
