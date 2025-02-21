using System;
using System.Collections.Generic;

namespace BloodBowlMVC.Models;

public partial class Skill
{
    public int SkillIdPk { get; set; }

    public string? SkillName { get; set; }

    public virtual ICollection<Player> PlayerSkillsPlayerIdFks { get; set; } = new List<Player>();

    public virtual ICollection<Roster> RosterSkillsRosterIdFks { get; set; } = new List<Roster>();
}
