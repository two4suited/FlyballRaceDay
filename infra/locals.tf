locals {
  common_tags = {
     BranchName  = var.branchName
     CommitId    = var.commitId
     User        = var.deployedBy
     BuildId     = var.buildId
     BuildNumber = var.buildNumber
     Repository  = var.repository
     AppName     = var.application_name
     Environment = var.environment
     DeployTime  = timestamp()
  }
}

