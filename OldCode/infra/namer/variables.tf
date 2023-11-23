variable "application_name" {
  type    = string
}
variable "location" {
    type =string
}

variable "environment" {
    type =string
}
#https://registry.terraform.io/providers/aztfmod/azurecaf/latest/docs/resources/azurecaf_name#resource-types
variable "resource_type" {
    type = string
}