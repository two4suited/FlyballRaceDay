locals {
  backEndService = "${var.ApplicationName}-ClubApi"

  api_policies = [
     {
      operationid         = "getClubs"
      mocking             = false
      mockingResponseCode = "200"
      backendService      = local.backEndService
      backendRewrite      = "GetAllClubs"    
    }
    ]
  }