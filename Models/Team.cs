using System;
using System.Collections.Generic;

namespace BloodBowlMVC.Models;

public partial class Team
{
    public int TeamIdPk { get; set; }

    public string? TeamName { get; set; }

    public string? TeamRace { get; set; }

    public int? TeamFanFactor { get; set; }

    public int? TeamTreasury { get; set; }

    public int? TeamRerolls { get; set; }

    public int? TeamRating { get; set; }

    public int? TeamAssistantCoaches { get; set; }

    public int? TeamCheerleaders { get; set; }

    public int? TeamApothecary { get; set; }

    public int? TeamTrainerIdFk { get; set; }

    public virtual ICollection<Player> Players { get; set; } = new List<Player>();

    public virtual Trainer? TeamTrainerIdFkNavigation { get; set; }
}
