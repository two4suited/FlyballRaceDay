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
  resource_group_name = azurerm_resource_group.resourcegroup
  location            = var.location
}