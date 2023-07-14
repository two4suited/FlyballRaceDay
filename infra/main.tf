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

resource "azurerm_static_site" "staticwebapp" {
  name                = "${var.application_name}-${var.environment}"
  resource_group_name = azurerm_resource_group.resourcegroup.name
  location            = var.location
  sku_tier = "Standard"
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

module "Database" {  
  source = "./cosmos"
  ResourceGroupName = azurerm_resource_group.resourcegroup.name
  module_tags = local.common_tags
  application_name = var.application_name
  location = var.location
  environment = var.environment
}
