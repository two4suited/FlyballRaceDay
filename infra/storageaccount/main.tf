module "storageAccount_name" {
  source           = "../namer"
  application_name = var.application_name
  resource_type    = "st"
  environment      = var.environment
  location         = var.location
}

resource "azurerm_storage_account" "storageAccount" {
  name                     = module.storageAccount_name.resource_name
  resource_group_name      = var.ResourceGroupName
  location                 = var.location
  account_tier             = "Standard"
  account_replication_type = "LRS"
  tags = var.module_tags
}

output "storage_account_name" {
    value = azurerm_storage_account.storageAccount.name
}

output "storage_account_primary_access_key" {
    value = azurerm_storage_account.storageAccount.primary_access_key
}