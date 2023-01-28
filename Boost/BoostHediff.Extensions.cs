namespace MechTowers;

public static class BoostHediff_Extensions
{
    public static List<BoostHediff> GetBoosts(this Pawn_HealthTracker health, Building tower = null)
    {
        List<BoostHediff> hediffs = new();
        health?.hediffSet.GetHediffs<BoostHediff>(ref hediffs);
        var hediffsToRead = hediffs.ToList();
        hediffs.Clear();
        foreach (var hediff in hediffsToRead)
        {
            if (hediff is null) continue;
            if (tower is not null && hediff.Tower != tower) continue;
            hediffs.Add(hediff);
        }
        return hediffs;
    }
    public static void RemoveBoosts(this Pawn_HealthTracker health, Building tower = null)
    {
        foreach (var hediff in health.GetBoosts(tower))
            health.RemoveHediff(hediff);
    }
    public static BoostHediff AddBoost(this Pawn_HealthTracker health, Building tower)
    {
        var boostMods = tower.Def.Boost;
        if (!boostMods.HasValue) return null;
        var boost = health.AddBoost(boostMods.value);
        boost.Tower = tower;
        return boost;
    }
    public static BoostHediff AddBoost(this Pawn_HealthTracker health, BoostModifiers boostModifiers)
    {
        var part = health?.pawn.RaceProps.body.corePart;
        var hediff = health?.AddHediff(Defs.MechTowerBoost, part, new(), new());
        if (hediff is not BoostHediff boost) return null;
        boost.Modifiers = boostModifiers;
        return boost;
    }
}