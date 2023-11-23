variable "IsMocked" {
   type = bool
}

variable "MockedResponseCode" {
  type= string
}

variable "ApplicationName" {
    type = string    
    default = ""
}
variable "RewritePath" {
    type = string    
    default = ""
}

variable "AzureAD_ApplicationID" {
  type = string
  description = "Azure AD Application ID"
}

 locals {
   IsMockedtemplatefile = var.IsMocked == true ? templatefile("${path.module}/api_policy_mock.tpl", { status_code = var.MockedResponseCode }) :  templatefile("${path.module}/api_policy_application.tpl", {  ApplicationName=var.ApplicationName, AzureAD_APPID=var.AzureAD_ApplicationID })
   Outputtemplatefile = var.RewritePath == "" ? local.IsMockedtemplatefile :  templatefile("${path.module}/api_policy_application_rewrite.tpl", { RewritePath = var.RewritePath, ApplicationName=var.ApplicationName,AzureAD_APPID=var.AzureAD_ApplicationID })
 }

output "templateFile" {
    value = local.Outputtemplatefile
}