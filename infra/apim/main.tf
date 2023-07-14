
resource "azurerm_api_management" "api_management" {
  name                = "${var.application_name}-${var.environment}-api"
  resource_group_name = var.resouregroup_name
  location            = var.location
  publisher_name      = var.publisher_name
  publisher_email     = var.publisher_email
  sku_name            = var.SKU_Name
  tags                = var.module_tags
  identity {
    type="SystemAssigned"
  }
}