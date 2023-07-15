locals {
  backEndService = "${var.ApplicationName}-ClubApi"

  api_policies = [
     {
      operationid         = "getalltournaments"
      mocking             = true
      mockingResponseCode = "200"
      backendService      = ""
      backendRewrite      = ""    
    }
    ]
  }