variable "APIName" {
  type =string
}
variable "OperationId" {
  type =string
}
variable "IsMocked" {
  type =bool
}
variable "MockingResponseCode" {
  type= string
}

variable "BackendApplicationService" {
  type= string
}
variable "BackendRewrite" {
  type= string
}

variable "ResourceGroupName" {
    type = string    
}
variable "ApiManagementName" {
  type = string
}
variable "AzureAD_ApplicationID" {
  type = string
  description = "Azure AD Application ID"
  default = ""
}