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

variable "module_tags" {
  description = "A map of tags to be applied to resources in the module"
  type        = map(string)
}
