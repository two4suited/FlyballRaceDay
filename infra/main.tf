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

resource "azurerm_api_management" "apimanagement" {
  name                = "${var.application_name}${var.environment}"
  location            = var.location
  resource_group_name = azurerm_resource_group.resourcegroup.name
  publisher_name      = "Brian Sheridan"
  publisher_email     = "brian.sheridan@gmail.com"

  sku_name = "Consumption_0"
}