data "azurerm_linux_function_app" "existing" {
  name                = var.FunctionAppName
  resource_group_name = var.ResourceGroupName
}

locals {
  existing_app_settings = coalesce(data.azurerm_linux_function_app.existing.app_settings, {})
}

resource "azurerm_linux_function_app" "FunctionApp" {
  name                          = var.FunctionAppName
  location                      = var.location
  resource_group_name           = var.ResourceGroupName
  service_plan_id               =   var.AppServicePlanId
  storage_account_name          = var.StorageAccountName
  storage_account_access_key    = var.StorageAccountPrimaryKey
  functions_extension_version = "~4"
 
  tags = var.module_tags
  
  identity {
    type="SystemAssigned"
  }

  
  site_config {
    application_insights_connection_string = var.ApplicationInsightsConnectionString
    application_insights_key = var.ApplicationInsightsKey
    application_stack {
     use_dotnet_isolated_runtime = true
      dotnet_version = var.DotNetVersion
    }
    cors {
      allowed_origins = ["https://portal.azure.com"]
    }
  }
  
  
  app_settings= {    
   "RepositoryOptions__CosmosConnectionString" = var.CosmosDatabaseConnectionString
   "RepositoryOptions__DatabaseId" = var.CosmosDatabaseContainer
    "WEBSITE_MOUNT_ENABLED"    = lookup(local.existing_app_settings, "WEBSITE_MOUNT_ENABLED", null)
    "WEBSITE_RUN_FROM_PACKAGE" = lookup(local.existing_app_settings, "WEBSITE_RUN_FROM_PACKAGE", null)
  }   
}

data "azurerm_function_app_host_keys" "FunctionAppHostKey" {
  name                = azurerm_linux_function_app.FunctionApp.name
  resource_group_name = var.ResourceGroupName
}

resource "azurerm_api_management_named_value" "FunctionAppKey" {
  depends_on = [ azurerm_linux_function_app.FunctionApp ]
  name = "${var.FunctionAppName}-key"
  resource_group_name = var.ResourceGroupName
  api_management_name = var.ApiManagementName
  display_name = "${var.FunctionAppName}-key"
  value = data.azurerm_function_app_host_keys.FunctionAppHostKey.default_function_key
  secret = true
}

resource "azurerm_api_management_backend" "APIBackend" {
  depends_on = [ azurerm_api_management_named_value.FunctionAppKey,azurerm_linux_function_app.FunctionApp ]
  name                = var.FunctionAppName
  api_management_name = var.ApiManagementName
  resource_group_name = var.ResourceGroupName
  title               = var.FunctionAppName
  description         = var.FunctionAppName
  url                 = "https://${azurerm_linux_function_app.FunctionApp.default_hostname}/api/"
  protocol            = "http"
  resource_id         = "https://management.azure.com/${azurerm_linux_function_app.FunctionApp.id}"
  credentials {
    header={
      "x-functions-key" = "{{${azurerm_api_management_named_value.FunctionAppKey.name}}}"
    }
  }
  
}