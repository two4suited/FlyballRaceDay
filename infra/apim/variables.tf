variable "application_name" {
  type    = string
}
variable "location" {
    type =string
}

variable "environment" {
    type =string
}

variable "resouregroup_name" {
    type=string  
}

variable "publisher_name" {
   type =string
}
variable "publisher_email" {
  type = string
}

variable "SKU_Name" {
  type        = string
  description = "SKU for Api Management"
  validation {
    condition     = var.SKU_Name == "Consumption_0" || var.SKU_Name == "Developer_1"
    error_message = "Invalid value for SKU_Name. Valid values are Consumption_0 or Developer_1."
  }
}

variable "module_tags" {
  description = "A map of tags to be applied to resources in the module"
  type        = map(string)
}