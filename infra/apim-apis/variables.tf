variable "ApiManagementName" {
    type=string  
}
variable "ResourceGroupName" {
  type=string
}

variable "AzureAD_ApplicationID" {
  type = string
  description = "Azure AD Application ID"
  default = ""
}

variable "APIName" {
  type = string
  description = "API Name"  
}
  
variable "RevisionID" {
  type = string
  description = "Revision ID"  
}

variable "DisplayName" {
  type = string
  description = "Display Name"  
}
variable "Path" {    
  type = string
  description = "Path"    
}

variable "Protocols" {
  type = list(string)
  description = "Protocols"  
  default = [ "https" ]
}

variable "OpenAPISpecFile" {
  type = string
  description = "Open API Spec File"  
}


variable "API_Polices" {
    type = list(object({    
      operationid         = string
      mocking             = bool
      mockingResponseCode = string
      backendService      = string
      backendRewrite      = string
    }))  
}