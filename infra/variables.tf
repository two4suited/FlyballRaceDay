variable "application_name" {
  type    = string 

}
variable "location" {
  type    = string
  default = "westus"
}

variable "environment" {
    type = string
    default = "dev"    
}

variable "branchName" {
  type = string
  default = "local"
}
variable "buildId" {
  type = string
  default = "local"
}
variable "buildNumber" {
  type = string
  default = "local"
}
variable "repository" {
  type = string
  default = "local"
}
variable "deployedBy" {
  type = string
  default = "local"
}
variable "commitId" {
  type = string
  default = "local"
}