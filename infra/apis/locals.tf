locals {
  tournamentBackendService = "${var.ApplicationName}-TournamentApi"

  api_policies = [
     {
      operationid         = "getalltournaments"
      mocking             = false
      mockingResponseCode = "200"
      backendService      = local.tournamentBackendService
      backendRewrite      = "GetAll"    
    },
    {
      operationid         = "createTournament"
      mocking             = false
      mockingResponseCode = "200"
      backendService      = local.tournamentBackendService
      backendRewrite      = "Create"    
    },
    {
      operationid         = "updateTournament"
      mocking             = false
      mockingResponseCode = "200"
      backendService      = local.tournamentBackendService
      backendRewrite      = "{id}"    
    },
    {
      operationid         = "deleteTournament"
      mocking             = true
      mockingResponseCode = "200"
      backendService      = local.tournamentBackendService
      backendRewrite      = ""    
    },
    {
      operationid         = "createSchedule"
      mocking             = true
      mockingResponseCode = "200"
      backendService      = ""
      backendRewrite      = ""    
    },
    {
      operationid         = "getschedulebyid"
      mocking             = true
      mockingResponseCode = "200"
      backendService      = ""
      backendRewrite      = ""    
    },
    {
      operationid         = "getraces"
      mocking             = true
      mockingResponseCode = "200"
      backendService      = ""
      backendRewrite      = ""    
    },
    {
      operationid         = "racemarkasdone"
      mocking             = true
      mockingResponseCode = "200"
      backendService      = ""
      backendRewrite      = ""    
    },
    {
      operationid         = "raceupnext"
      mocking             = true
      mockingResponseCode = "200"
      backendService      = ""
      backendRewrite      = ""    
    },
    {
      operationid         = "updateRing"
      mocking             = true
      mockingResponseCode = "200"
      backendService      = ""
      backendRewrite      = ""    
    },
    {
      operationid         = "getRings"
      mocking             = true
      mockingResponseCode = "200"
      backendService      = ""
      backendRewrite      = ""    
    },
    {
      operationid         = "addRing"
      mocking             = true
      mockingResponseCode = "200"
      backendService      = ""
      backendRewrite      = ""    
    }
    ]
  }