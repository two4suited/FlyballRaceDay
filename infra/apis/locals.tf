locals {
  backEndService = "${var.ApplicationName}-ClubApi"

  api_policies = [
     {
      operationid         = "getalltournaments"
      mocking             = true
      mockingResponseCode = "200"
      backendService      = ""
      backendRewrite      = ""    
    },
    {
      operationid         = "createTournament"
      mocking             = true
      mockingResponseCode = "200"
      backendService      = ""
      backendRewrite      = ""    
    },
    {
      operationid         = "updateTournament"
      mocking             = true
      mockingResponseCode = "200"
      backendService      = ""
      backendRewrite      = ""    
    },
    {
      operationid         = "deleteTournament"
      mocking             = true
      mockingResponseCode = "200"
      backendService      = ""
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