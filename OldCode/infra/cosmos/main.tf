resource "azurerm_cosmosdb_account" "cosmosdb" {
  name                = "${lower(var.environment)}-cosmos-${lower(var.application_name)}-${lower(var.location)}-001"
  location            = var.location
  resource_group_name = var.ResourceGroupName

  offer_type = "Standard"
  kind       = "MongoDB"
  tags = var.module_tags
  consistency_policy {
    consistency_level = "Session"
  }

  capabilities {
    name = "EnableServerless"
  }
  capabilities {
    name = "mongoEnableDocLevelTTL"
  }

  capabilities {
    name = "MongoDBv3.4"
  }

  capabilities {
    name = "EnableMongo"
  }
  geo_location {
    location = var.location
    failover_priority = 0
  }
}

resource "azurerm_cosmosdb_mongo_database" "database" {
  depends_on = [ azurerm_cosmosdb_account.cosmosdb ]
  name                = "${lower(var.application_name)}"
  resource_group_name = var.ResourceGroupName
  account_name        = azurerm_cosmosdb_account.cosmosdb.name
}



output "cosmos_connection" {
    value = azurerm_cosmosdb_account.cosmosdb.connection_strings[0]
}

output "CosmosDBName" {
    value = azurerm_cosmosdb_mongo_database.database.name
}
