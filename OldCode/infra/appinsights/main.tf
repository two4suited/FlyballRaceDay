resource "azurerm_application_insights" "appinsights" {
  name                = "${var.application_name}-${var.environment}-appinsights"
  location            = var.location
  resource_group_name = var.ResourceGroupName
  application_type    = "web"
  tags = var.module_tags
}

