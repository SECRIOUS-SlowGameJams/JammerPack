using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CompetitionMechanic : MonoBehaviour
{
    public List<CompetitionTeam> competitionTeams;
    public CompetitivePlayer[] allPlayers;

    public TeamCollabMode teamMode;

    public bool matchIsOver;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void CreateTeam()
    {
        CompetitionTeam newTeam = new CompetitionTeam();
        competitionTeams.Add(newTeam);
    }

    public void AssignNameToTeam(int index, string inputTeamName)
    {
        competitionTeams[index].teamName = inputTeamName;
    }

    public void CreateTeam(string newName)
    {
        CompetitionTeam newTeam = new CompetitionTeam(newName);
        competitionTeams.Add(newTeam);
    }

    public void AddMemberToTeam(CompetitionTeam competitionTeam, CompetitivePlayer player)
    {
        competitionTeam.teamMembers.Add(player);
        RegisterPlayers();
    }

    public void AddMemberToTeam(int index, CompetitivePlayer player)
    {
        competitionTeams[index].teamMembers.Add(player);
        RegisterPlayers();
    }

    void RegisterPlayers()
    {
        allPlayers = FindObjectsOfType<CompetitivePlayer>();
        SubscribeToPlayerEvents();
    }
    void SubscribeToPlayerEvents()
    {
        foreach (var item in allPlayers)
        {
            item.playerWonEvent += CheckForTeamWin;
            item.playerLostEvent += CheckForTeamLoss;
        }
    }

    void UnSubscribeToPlayerEvents()
    {
        foreach (var item in allPlayers)
        {
            item.playerWonEvent -= CheckForTeamWin;
            item.playerLostEvent -= CheckForTeamLoss;
        }
    }

    public void CheckForTeamWin(CompetitivePlayerData eventPlayerData)
    {
        CompetitionTeam potentialTeam = FindOutInWhichTeamThePlayerWhoWonBelongsTo(eventPlayerData);
        if (teamMode == TeamCollabMode.OnePlayerResultEndsMatch)
        {
            ResolveMatch(potentialTeam);
        }
    }

    public void CheckForTeamLoss(CompetitivePlayerData eventPlayerData)
    {
        CompetitionTeam potentialTeam = FindOutInWhichTeamThePlayerWhoWonBelongsTo(eventPlayerData);
        if (teamMode == TeamCollabMode.OnePlayerResultEndsMatch)
        {
            AnnounceTeamLost(potentialTeam);
        }
    }

    CompetitionTeam FindOutInWhichTeamThePlayerWhoWonBelongsTo(CompetitivePlayerData eventPlayerData)
    {
        for (int i = 0; i < competitionTeams.Count; i++)
        {
            for (int z = 0; z < competitionTeams[i].teamMembers.Count; z++)
            {
                if (competitionTeams[i].teamMembers[z].CompetitivePlayerData == eventPlayerData)
                    return competitionTeams[i];
            }
        }
        return null;
    }


    void AnnounceTeamWon(CompetitionTeam team)
    {
        foreach (var player in team.teamMembers)
        {
            player.PlayerWon();
        }
    }

    void AnnounceTeamLost(CompetitionTeam team)
    {
        foreach (var player in team.teamMembers)
        {
            player.PlayerLost();
        }
    }

    void ResolveMatch(CompetitionTeam teamThatWon)
    {
        matchIsOver = true;
        UnSubscribeToPlayerEvents();
        AnnounceTeamWon(teamThatWon);
        competitionTeams.Remove(teamThatWon);
        foreach (var team in competitionTeams)
        {
            AnnounceTeamLost(team);
        }
    }

}

public class CompetitionTeam
{
    public CompetitionTeam() { }

    public CompetitionTeam(string TeamName)
    {
        teamName = TeamName;
    }
    public string teamName;
    public List<CompetitivePlayer> teamMembers;
}

public enum TeamCollabMode
{
    OnePlayerResultEndsMatch,
    AllPlayerResultEndsMatch
}

public enum CompetitionModes
{
    MyWinIsYourLoss
}