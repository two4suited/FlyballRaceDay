provider "azurerm" {
  features {}
}

terraform {
  cloud {
    organization = "BS_INC"
    workspaces {
      tags = ["flyballraceday"]

    }
  }
}

data "azurerm_client_config" "current" {}

resource "azurerm_resource_group" "resourcegroup" {
    name     = "${var.application_name}-${var.environment}"
    location = var.location
    tags = local.common_tags  
}
module "StorageAccount" {
  depends_on = [ azurerm_resource_group.resourcegroup ]
  source = "./storageaccount"
  ResourceGroupName = azurerm_resource_group.resourcegroup.name
  module_tags = local.common_tags
  application_name = var.application_name
  location = var.location
  environment = var.environment  
}
module "ApplicationInsights" {
  depends_on = [ azurerm_resource_group.resourcegroup ]
  source = "./appinsights"
  application_name = var.application_name
  location = var.location
  ResourceGroupName = azurerm_resource_group.resourcegroup.name
  environment = var.environment
  module_tags = local.common_tags
}

module "api_management" {  
  source            = "./apim"
  application_name  = var.application_name
  location          = var.location
  resouregroup_name = azurerm_resource_group.resourcegroup.name
  environment       = var.environment
  module_tags       = local.common_tags
  publisher_name    = "Brian Sheridan"
  publisher_email   = "brian.sheridan@gmail.com"
  SKU_Name          = "Consumption_0"
}

module "apis" {
  source            = "./apis"  
  ApiManagementName = module.api_management.API_Management_Name
  ResourceGroupName = azurerm_resource_group.resourcegroup.name
  ApplicationName = var.application_name 
}

module "Functions" {
  depends_on = [ azurerm_resource_group.resourcegroup ]
  source = "./functions"  
  ResourceGroupName = azurerm_resource_group.resourcegroup.name
  module_tags = local.common_tags
  application_name = var.application_name
  location = var.location
  environment = var.environment
  StorageAccountName = module.StorageAccount.storage_account_name
  StorageAccountPrimaryKey = module.StorageAccount.storage_account_primary_access_key
  CosmosDatabaseConnectionString = module.Database.cosmos_connection
  CosmosDatabaseContainer = module.Database.CosmosDBName
  ApplicationInsightsConnectionString = module.ApplicationInsights.connectionstring
  ApplicationInsightsKey = module.ApplicationInsights.instrumentation_key
  ApiManagementName = module.api_management.API_Management_Name
}


module "Database" {  
  source = "./cosmos"
  ResourceGroupName = azurerm_resource_group.resourcegroup.name
  module_tags = local.common_tags
  application_name = var.application_name
  location = var.location
  environment = var.environment
}
