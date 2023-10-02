# https://registry.terraform.io/providers/aztfmod/azurecaf/latest/docs/resources/azurecaf_name

terraform {  
  required_providers {
    azurecaf = {
        source = "aztfmod/azurecaf"       
    }
  }
}

resource "azurecaf_naming_convention" "resource_namer" {
  name = var.application_name
  prefix = var.environment
  resource_type = var.resource_type
  postfix = "${var.location}-001"
  convention = "cafclassic"
}

output "resource_name" {
    value = azurecaf_naming_convention.resource_namer.result
}
