provider "azurerm" {
  features {}
}

terraform {
  cloud {
    organization = "<ChangeMe>"
    workspaces {
      tags = ["TAG"]

    }
  }
}

data "azurerm_client_config" "current" {}

resource "azurerm_resource_group" "resourcegroup" {
    name     = "${var.application_name}-${var.environment}"
    location = var.location
    tags = local.common_tags
  
}