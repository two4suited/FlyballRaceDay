module "api_policy_xml" {
    source = "../api-policy"
    IsMocked = var.IsMocked
    MockedResponseCode = var.MockingResponseCode
    ApplicationName = var.BackendApplicationService
    RewritePath = var.BackendRewrite
    AzureAD_ApplicationID = var.AzureAD_ApplicationID
}

resource "azurerm_api_management_api_operation_policy" "apis_policy" {  
    api_management_name = var.ApiManagementName
    api_name = var.APIName
    resource_group_name = var.ResourceGroupName
    operation_id = var.OperationId
    xml_content = module.api_policy_xml.templateFile  
}