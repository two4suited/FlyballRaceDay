variable "application_name" {
  type    = string
}
variable "location" {
    type =string
}

variable "environment" {
    type =string
}

variable "ResourceGroupName" {
    type = string
}
variable "StorageAccountName" {
  type=string 
}
variable "StorageAccountPrimaryKey" {
  type = string
}

variable "module_tags" {
  description = "A map of tags to be applied to resources in the module"
  type        = map(string)
}

variable "CosmosDatabaseConnectionString" {
  type = string
}
variable "CosmosDatabaseContainer" {
  type = string
}
variable "ApplicationInsightsKey" {
  type = string  
}
variable "ApplicationInsightsConnectionString" {
  type = string
  
}

variable "ApiManagementName" {
  type = string
}