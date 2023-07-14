 locals {  
    api_policies_map = tomap({for policy in var.API_Polices : "${var.APIName}-${policy.operationid}" => policy})
 }



resource "azurerm_api_management_api" "apis" {
  
  name                = var.APIName
  resource_group_name = var.ResourceGroupName
  api_management_name = var.ApiManagementName
  revision            = var.RevisionID
  display_name        = var.DisplayName
  protocols           = var.Protocols
  path                = var.Path

  import {
    content_format =  "openapi"
    content_value  = var.OpenAPISpecFile
  }  
}


module "api_operation_policy" {
  source = "./policies"
  depends_on = [ azurerm_api_management_api.apis ]
  for_each = local.api_policies_map 
  ResourceGroupName = var.ResourceGroupName
  ApiManagementName = var.ApiManagementName 
  APIName = var.APIName
  OperationId = each.value.operationid
  IsMocked = each.value.mocking
  MockingResponseCode = each.value.mockingResponseCode
  BackendApplicationService = each.value.backendService
  BackendRewrite = each.value.backendRewrite
}
